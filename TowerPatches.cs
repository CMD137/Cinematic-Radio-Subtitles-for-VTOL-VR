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

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayClearedToLandCarrierMsg")]
    internal static class ClearedCarrierLandingPatch
    {
        private static void Postfix()
        {
            RadioText.Tower(RadioText.Callsign() + ", cleared to land on carrier.");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayLandingPatternFullMsg")]
    internal static class LandingPatternFullPatch
    {
        private static void Postfix()
        {
            RadioText.Tower(RadioText.Callsign() + ", landing pattern full.");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayCancelledRequestMsg")]
    internal static class CancelledRequestPatch
    {
        private static void Postfix()
        {
            RadioText.Tower(RadioText.Callsign() + ", request cancelled.");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayUnableMsg")]
    internal static class UnablePatch
    {
        private static void Postfix()
        {
            RadioText.Tower(RadioText.Callsign() + ", unable.");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayLandedBeforeClearanceMsg")]
    internal static class LandedBeforeClearancePatch
    {
        private static void Postfix()
        {
            RadioText.Tower(RadioText.Callsign() + ", you landed before receiving clearance.");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayLandedElseWhereMsg")]
    internal static class LandedElsewherePatch
    {
        private static void Postfix()
        {
            RadioText.Tower(RadioText.Callsign() + ", you landed at the wrong airfield.");
        }
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayRequestedWrongATCMsg")]
    internal static class WrongAtcPatch
    {
        private static void Postfix()
        {
            RadioText.Tower(RadioText.Callsign() + ", you contacted the wrong tower.");
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

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayLSOComeLeft")]
    internal static class LsoComeLeftPatch
    {
        private static void Postfix() => RadioText.Lso("Come left.");
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayLSOHighLeft")]
    internal static class LsoHighLeftPatch
    {
        private static void Postfix() => RadioText.Lso("You're high left.");
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayLSOHighRight")]
    internal static class LsoHighRightPatch
    {
        private static void Postfix() => RadioText.Lso("You're high right.");
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayLSOLinedUp")]
    internal static class LsoLinedUpPatch
    {
        private static void Postfix() => RadioText.Lso("Lined up.");
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayLSOLowLeft")]
    internal static class LsoLowLeftPatch
    {
        private static void Postfix() => RadioText.Lso("You're low left.");
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayLSOLowRight")]
    internal static class LsoLowRightPatch
    {
        private static void Postfix() => RadioText.Lso("You're low right.");
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayLSOPowerLow")]
    internal static class LsoPowerLowPatch
    {
        private static void Postfix() => RadioText.Lso("Power, power.");
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayLSOReturnToHolding")]
    internal static class LsoReturnToHoldingPatch
    {
        private static void Postfix() => RadioText.Lso("Return to holding.");
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayLSORightForLineup")]
    internal static class LsoRightForLineupPatch
    {
        private static void Postfix() => RadioText.Lso("Right for lineup.");
    }

    [HarmonyPatch(typeof(ATCVoiceProfile), "PlayLSOYoureHigh")]
    internal static class LsoYoureHighPatch
    {
        private static void Postfix() => RadioText.Lso("You're high.");
    }
}
