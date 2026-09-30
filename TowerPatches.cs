using HarmonyLib;

namespace Cmd137.CinematicRadioSubtitles
{
    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayTaxiToRunwayMsg")]
    internal static class TaxiToRunwayPatch
    {
        private static void Postfix(float heading, Runway.ParallelDesignations pDes)
        {
            RadioText.Tower("Taxi to runway " + RadioText.RunwayDesignation(heading, pDes) + ".");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayHoldShortAtRunwayMsg")]
    internal static class HoldShortPatch
    {
        private static void Postfix(float heading)
        {
            RadioText.Tower(RadioText.Callsign() + ", hold short runway " + RadioText.RunwayDesignation(heading, Runway.ParallelDesignations.None) + ".");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayClearForTakeoffRunwayMsg")]
    internal static class ClearedTakeoffPatch
    {
        private static void Postfix(float heading)
        {
            RadioText.Tower(RadioText.Callsign() + ", cleared for takeoff, runway " + RadioText.RunwayDesignation(heading, Runway.ParallelDesignations.None) + ".");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayLandingClearedForRunwayMsg")]
    internal static class ClearedLandingPatch
    {
        private static void Postfix(float heading, Runway.ParallelDesignations parallelDesignation)
        {
            RadioText.Tower(RadioText.Callsign() + ", cleared to land, runway " + RadioText.RunwayDesignation(heading, parallelDesignation) + ".");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayLandingFlyHeadingMsg")]
    internal static class LandingHeadingPatch
    {
        private static void Postfix(float heading, Runway runway)
        {
            var runwayHeading = VectorUtils.Bearing(runway.transform.position, runway.transform.position + runway.transform.forward);
            RadioText.Tower(RadioText.Callsign() + ", fly heading " + RadioText.RunwayDesignation(heading, Runway.ParallelDesignations.None) + ". Expect runway " + RadioText.RunwayDesignation(runwayHeading, runway.parallelDesignation) + ".");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayVerticalLandingFlyHeadingMsg")]
    internal static class VerticalHeadingPatch
    {
        private static void Postfix(float heading)
        {
            RadioText.Tower(RadioText.Callsign() + ", fly heading " + RadioText.RunwayDesignation(heading, Runway.ParallelDesignations.None) + ".");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayClearedVerticalTakeoffMsg")]
    internal static class ClearedVerticalTakeoffPatch
    {
        private static void Postfix()
        {
            RadioText.Tower(RadioText.Callsign() + ", cleared for vertical takeoff.");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayClearedVerticalLandingMsg")]
    internal static class ClearedVerticalLandingPatch
    {
        private static void Postfix(int padNumber)
        {
            RadioText.Tower(RadioText.Callsign() + ", cleared to land on pad " + padNumber + ".");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayTaxiToParkingMsg")]
    internal static class TaxiToParkingPatch
    {
        private static void Postfix()
        {
            RadioText.Tower("Welcome back. Follow the taxi paths to your parking area.");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayTaxiToCatapultMsg")]
    internal static class TaxiToCatapultPatch
    {
        private static void Postfix(CarrierCatapult c)
        {
            RadioText.Tower(RadioText.Callsign() + ", cleared to taxi to catapult " + c.catapultDesignation + ".");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayPreCatapultMsg")]
    internal static class PreCatapultPatch
    {
        private static void Postfix()
        {
            RadioText.Tower(RadioText.Callsign() + ", locked in. Throttle down and run your launch checklist.");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayRunUpEnginesCatapultMsg")]
    internal static class RunUpEnginesPatch
    {
        private static void Postfix()
        {
            RadioText.Tower(RadioText.Callsign() + ", shields up. Ready to go.");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayCallTheBallMsg")]
    internal static class CallTheBallPatch
    {
        private static void Postfix()
        {
            RadioText.Lso(RadioText.Callsign() + ", call the ball.");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayRogerBallMsg")]
    internal static class RogerBallPatch
    {
        private static void Postfix()
        {
            RadioText.Lso("Roger ball.");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayLSOWaveOff")]
    internal static class WaveOffPatch
    {
        private static void Postfix()
        {
            RadioText.Lso("Wave off, wave off!");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayLSOFoulDeck")]
    internal static class FoulDeckPatch
    {
        private static void Postfix()
        {
            RadioText.Lso("Foul deck, wave off!");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayLSOBolter")]
    internal static class BolterPatch
    {
        private static void Postfix()
        {
            RadioText.Lso("Bolter, bolter!");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayLSOXwire")]
    internal static class WirePatch
    {
        private static void Postfix(int idx)
        {
            RadioText.Lso((idx + 1) + " wire!");
        }
    }
}
