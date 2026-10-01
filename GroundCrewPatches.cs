using HarmonyLib;

namespace Cmd137.CinematicRadioSubtitles
{
    [HarmonyPatch(typeof(GroundCrewVoiceProfile), "PlayMessage")]
    internal static class GroundCrewPatch
    {
        // GroundCrewMessages is internal to the game assembly. Harmony's __0
        // receives its first argument without this mod needing that type.
        private static void Postfix(object __0)
        {
            switch (__0 == null ? string.Empty : __0.ToString())
            {
                case "NotAvailable":
                    RadioText.GroundCrew("Rearming is not available.");
                    break;
                case "IsAirborne":
                    RadioText.GroundCrew("Unable. Aircraft is airborne.");
                    break;
                case "TaxiToStation":
                    RadioText.GroundCrew("Taxi to the rearming station.");
                    break;
                case "EnteredStation":
                    RadioText.GroundCrew("You have entered the rearming station.");
                    break;
                case "TurnOffEngines":
                    RadioText.GroundCrew("Please turn off your engines.");
                    break;
                case "DisarmWeapons":
                    RadioText.GroundCrew("Please disarm your weapons.");
                    break;
                case "Success":
                    RadioText.GroundCrew("Rearming complete.");
                    break;
                case "ReturnedToVehicle":
                    RadioText.GroundCrew("Ground crew returning to vehicle.");
                    break;
            }
        }
    }
}
