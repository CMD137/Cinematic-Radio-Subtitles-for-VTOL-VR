using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace Cmd137.CinematicRadioSubtitles
{
    [HarmonyPatch(typeof(AWACSVoiceProfile), "ReportHostile")]
    internal static class ReportHostilePatch
    {
        private static void Postfix(Vector3 pos, Vector3 velocity, bool braaOnly)
        {
            RadioText.Awacs(RadioText.Braa(AwacsFormatting.ReferencePosition(braaOnly), pos, velocity, "HOSTILE", AwacsFormatting.GeometryLabel(braaOnly)));
        }
    }

    [HarmonyPatch(typeof(AWACSVoiceProfile), "ReportGroup")]
    internal static class ReportGroupPatch
    {
        private static void Postfix(Vector3 pos, Vector3 velocity, bool braaOnly)
        {
            RadioText.Awacs(RadioText.Braa(AwacsFormatting.ReferencePosition(braaOnly), pos, velocity, "GROUP", AwacsFormatting.GeometryLabel(braaOnly)));
        }
    }

    [HarmonyPatch(typeof(AWACSVoiceProfile), "ReportPictureClean")]
    internal static class PictureCleanPatch
    {
        private static void Postfix()
        {
            RadioText.Awacs("PICTURE CLEAN");
        }
    }

    [HarmonyPatch(typeof(AWACSVoiceProfile), "ReportGrandSlam")]
    internal static class GrandSlamPatch
    {
        private static void Postfix()
        {
            RadioText.Awacs(RadioText.Callsign() + ", GRAND SLAM.");
        }
    }

    [HarmonyPatch(typeof(AWACSVoiceProfile), "ReportRTB")]
    internal static class RtbPatch
    {
        private static void Postfix()
        {
            RadioText.Awacs("OVERLORD OFF STATION. RTB.");
        }
    }

    [HarmonyPatch(typeof(AWACSVoiceProfile), "ReportGoingDown")]
    internal static class GoingDownPatch
    {
        private static void Postfix()
        {
            RadioText.Awacs("MAYDAY, MAYDAY, MAYDAY. OVERLORD IS GOING DOWN!", 12f);
        }
    }

    [HarmonyPatch(typeof(AWACSVoiceProfile), "ReportHomeplateBra")]
    internal static class HomeplatePatch
    {
        private static void Postfix(Vector3 homePos)
        {
            var player = AwacsFormatting.PlayerPosition();
            RadioText.Awacs(RadioText.Braa(player, homePos, Vector3.zero, "HOMEPLATE"));
        }
    }

    [HarmonyPatch(typeof(AWACSVoiceProfile), "ReportGroups")]
    internal static class ReportGroupsPatch
    {
        private static void Postfix(List<AIAWACSSpawn.ContactGroup> groups, int offset, int count)
        {
            RadioText.Awacs(AwacsFormatting.BuildGroupList(groups, offset, count, "PICTURE"), 11f);
        }
    }

    [HarmonyPatch(typeof(AWACSVoiceProfile), "ReportPopups", new[] { typeof(List<AIAWACSSpawn.ContactGroup>), typeof(int), typeof(int) })]
    internal static class ReportPopupsPatch
    {
        private static void Postfix(List<AIAWACSSpawn.ContactGroup> groups, int offset, int count)
        {
            RadioText.Awacs(AwacsFormatting.BuildGroupList(groups, offset, count, "POPUP"), 11f);
        }
    }

    [HarmonyPatch(typeof(AWACSVoiceProfile), "ReportPopups", new[]
    {
        typeof(bool), typeof(Vector3), typeof(Vector3),
        typeof(bool), typeof(Vector3), typeof(Vector3),
        typeof(bool), typeof(Vector3), typeof(Vector3), typeof(int)
    })]
    internal static class ReportLegacyPopupsPatch
    {
        private static void Postfix(bool grpA, Vector3 gPosA, Vector3 velA, bool grpB, Vector3 gPosB, Vector3 velB, bool grpC, Vector3 gPosC, Vector3 velC, int count)
        {
            RadioText.Awacs(AwacsFormatting.BuildPopupList(grpA, gPosA, velA, grpB, gPosB, velB, grpC, gPosC, velC, count), 11f);
        }
    }

    [HarmonyPatch(typeof(AWACSVoiceProfile), "ReportThreatToAwacs")]
    internal static class ThreatToAwacsPatch
    {
        private static void Postfix(int count, Vector3 pos, Vector3 velocity)
        {
            var subject = count > 1 ? "GROUP" : "HOSTILE";
            RadioText.Awacs(RadioText.Braa(AwacsFormatting.PlayerPosition(), pos, velocity, subject) + "\nLEANS ON.");
        }
    }

    [HarmonyPatch(typeof(AWACSVoiceProfile), "ReportUnable")]
    internal static class AwacsUnablePatch
    {
        private static void Postfix()
        {
            RadioText.Awacs(RadioText.Callsign() + ", UNABLE.");
        }
    }

    internal static class AwacsFormatting
    {
        internal static Vector3 ReferencePosition(bool braaOnly)
        {
            if (!braaOnly && WaypointManager.instance != null && WaypointManager.instance.bullseye != null)
            {
                return WaypointManager.instance.bullseye.position;
            }

            return PlayerPosition();
        }

        internal static string GeometryLabel(bool braaOnly)
        {
            return !braaOnly && WaypointManager.instance != null && WaypointManager.instance.bullseye != null
                ? "BULLSEYE"
                : "BRAA";
        }

        internal static Vector3 PlayerPosition()
        {
            return FlightSceneManager.instance != null && FlightSceneManager.instance.playerActor != null
                ? FlightSceneManager.instance.playerActor.position
                : Vector3.zero;
        }

        internal static string BuildGroupList(List<AIAWACSSpawn.ContactGroup> groups, int offset, int count, string title)
        {
            if (groups == null || groups.Count == 0)
            {
                return title;
            }

            var text = new StringBuilder(title);
            var reference = ReferencePosition(false);
            var geometryLabel = GeometryLabel(false);
            var last = Mathf.Min(groups.Count, offset + count);
            for (var index = offset; index < last; index++)
            {
                var group = groups[index];
                var subject = group.count > 1 ? "GROUP" : "HOSTILE";
                text.Append("\n");
                text.Append(RadioText.Braa(reference, group.globalPos.point, group.velocity, subject, geometryLabel));
            }

            return text.ToString();
        }

        internal static string BuildPopupList(bool grpA, Vector3 gPosA, Vector3 velA, bool grpB, Vector3 gPosB, Vector3 velB, bool grpC, Vector3 gPosC, Vector3 velC, int count)
        {
            var text = new StringBuilder("POPUP");
            var reference = ReferencePosition(false);
            var limit = Mathf.Clamp(count, 0, 3);
            if (limit > 0)
            {
                text.Append("\n").Append(RadioText.Braa(reference, gPosA, velA, grpA ? "GROUP" : "HOSTILE", GeometryLabel(false)));
            }

            if (limit > 1)
            {
                text.Append("\n").Append(RadioText.Braa(reference, gPosB, velB, grpB ? "GROUP" : "HOSTILE", GeometryLabel(false)));
            }

            if (limit > 2)
            {
                text.Append("\n").Append(RadioText.Braa(reference, gPosC, velC, grpC ? "GROUP" : "HOSTILE", GeometryLabel(false)));
            }

            return text.ToString();
        }
    }
}
