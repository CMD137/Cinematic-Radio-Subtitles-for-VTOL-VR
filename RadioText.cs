using UnityEngine;

namespace Cmd137.RightCommsSubtitles
{
    internal static class RadioText
    {
        internal static void Tower(string message, float duration = 5f)
        {
            if (RightCommsSubtitles.Subtitles != null)
            {
                RightCommsSubtitles.Subtitles.Push(RadioSource.Tower, message, duration);
            }
        }

        internal static void Lso(string message, float duration = 2.5f)
        {
            if (RightCommsSubtitles.Subtitles != null)
            {
                RightCommsSubtitles.Subtitles.Push(RadioSource.Lso, message, duration);
            }
        }

        internal static void Awacs(string message, float duration = 8f)
        {
            if (RightCommsSubtitles.Subtitles != null)
            {
                RightCommsSubtitles.Subtitles.Push(RadioSource.Awacs, message, duration);
            }
        }

        internal static string Callsign()
        {
            return FlightSceneManager.instance != null && FlightSceneManager.instance.playerActor != null
                ? FlightSceneManager.instance.playerActor.designation.ToString()
                : "Pilot";
        }

        internal static string RunwayDesignation(float heading, Runway.ParallelDesignations designation)
        {
            var number = Mathf.RoundToInt(heading / 10f);
            if (number == 0)
            {
                number = 36;
            }

            switch (designation)
            {
                case Runway.ParallelDesignations.Left: return number + " Left";
                case Runway.ParallelDesignations.Center: return number + " Center";
                case Runway.ParallelDesignations.Right: return number + " Right";
                default: return number.ToString("00");
            }
        }

        internal static string Braa(Vector3 reference, Vector3 target, Vector3 velocity, string subject, string geometryLabel = "BRAA")
        {
            var bearing = Mathf.RoundToInt(VectorUtils.Bearing(reference, target)).ToString("000");
            var range = Vector3.ProjectOnPlane(reference - target, Vector3.up).magnitude;
            var distance = MeasurementManager.instance.ConvertedDistance(range);
            if (MeasurementManager.instance.distanceMode == MeasurementManager.DistanceModes.Meters)
            {
                distance /= 1000f;
            }

            var altitude = WaterPhysics.GetAltitude(target);
            var convertedAltitude = MeasurementManager.instance.ConvertedAltitude(altitude);
            var aspect = Aspect(target, velocity);
            return string.Format("{0} {1} {2} / {3:0.#} / {4:0} / {5}", subject, geometryLabel, bearing, distance, convertedAltitude, aspect);
        }

        private static string Aspect(Vector3 position, Vector3 velocity)
        {
            if (FlightSceneManager.instance == null || FlightSceneManager.instance.playerActor == null)
            {
                return "UNKNOWN";
            }

            var direction = (position - FlightSceneManager.instance.playerActor.position).normalized;
            var dot = Vector3.Dot(velocity.normalized, direction);
            if (dot < -0.8f) return "HOT";
            if (dot > 0.5f) return "COLD";
            return "FLANK";
        }
    }
}
