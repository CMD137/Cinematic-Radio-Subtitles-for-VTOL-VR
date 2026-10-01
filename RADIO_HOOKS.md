# Radio hook reference

This document records the high-value radio routes covered in v1.1.

## Provenance and limits

The method inventory and clip-list names below came from static decompilation of the installed VTOL VR compiled assembly:

`VTOLVR_Data/Managed/Assembly-CSharp.dll`

This is compiled metadata and IL inspection, not access to or reuse of the game's source code. It identifies the method that dispatches a radio message and the vanilla clip list it uses. Clip lists do not contain an editable sentence transcript, so **Rendered subtitle** records the English text emitted by this mod, including runtime placeholders such as `{callsign}` and `{runway}`.

## Normal-airfield landing coverage added in v1.1

| Game dispatch method | Vanilla static evidence | Rendered subtitle |
| --- | --- | --- |
| `ATCVoiceProfile.PlayLandingPatternFullMsg` | `landingPatternFullClips` -> `PlayMessageString` | `{callsign}, landing pattern full.` |
| `ATCVoiceProfile.PlayCancelledRequestMsg` | `towerClips` + `cancelRequestClips` -> `PlayMessageString` | `{callsign}, request cancelled.` |
| `ATCVoiceProfile.PlayUnableMsg` | `towerClips` + `unableClips` -> `PlayMessageString` | `{callsign}, unable.` |
| `ATCVoiceProfile.PlayLandedBeforeClearanceMsg` | `towerClips` + `landedBeforeClearanceClips` -> `PlayMessageString` | `{callsign}, you landed before receiving clearance.` |
| `ATCVoiceProfile.PlayLandedElseWhereMsg` | `towerClips` + `landedElsewhereClips` -> `PlayMessageString` | `{callsign}, you landed at the wrong airfield.` |
| `ATCVoiceProfile.PlayRequestedWrongATCMsg` | `towerClips` + `contactedWrongTowerClips` -> `PlayMessageString` | `{callsign}, you contacted the wrong tower.` |

The game's player-landing routines call these branches alongside the already covered `PlayLandingFlyHeadingMsg` and `PlayLandingClearedForRunwayMsg` routes. This is why a takeoff subtitle could work while an arrival subtitle was absent in v1.0.

## Existing tower, carrier, and LSO coverage

| Game dispatch method | Rendered subtitle |
| --- | --- |
| `PlayTaxiToRunwayMsg` | `Taxi to runway {runway}.` |
| `PlayHoldShortAtRunwayMsg` | `{callsign}, hold short runway {runway}.` |
| `PlayClearForTakeoffRunwayMsg` | `{callsign}, cleared for takeoff, runway {runway}.` |
| `PlayLandingClearedForRunwayMsg` | `{callsign}, cleared to land, runway {runway}.` |
| `PlayLandingFlyHeadingMsg` | `{callsign}, fly heading {heading}. Expect runway {runway}.` |
| `PlayClearedToLandCarrierMsg` | `{callsign}, cleared to land on carrier.` |
| `PlayWaitForCatapultClearanceMsg` | `{callsign}, wait for catapult clearance.` |
| `PlayVerticalLandingFlyHeadingMsg` | `{callsign}, fly heading {heading}.` |
| `PlayClearedVerticalTakeoffMsg` | `{callsign}, cleared for vertical takeoff.` |
| `PlayClearedVerticalLandingMsg` | `{callsign}, cleared to land on pad {pad}.` |
| `PlayTaxiToParkingMsg` | `Welcome back. Follow the taxi paths to your parking area.` |
| `PlayTaxiToCatapultMsg` | `{callsign}, cleared to taxi to catapult {catapult}.` |
| `PlayPreCatapultMsg` | `{callsign}, locked in. Throttle down and run your launch checklist.` |
| `PlayRunUpEnginesCatapultMsg` | `{callsign}, shields up. Ready to go.` |
| `PlayCallTheBallMsg` | `{callsign}, call the ball.` |
| `PlayRogerBallMsg` | `Roger ball.` |
| `PlayLSOWaveOff` | `Wave off, wave off!` |
| `PlayLSOFoulDeck` | `Foul deck, wave off!` |
| `PlayLSOBolter` | `Bolter, bolter!` |
| `PlayLSOXwire` | `{wire} wire!` |
| `PlayLSOComeLeft` | `Come left.` |
| `PlayLSOHighLeft` | `You're high left.` |
| `PlayLSOHighRight` | `You're high right.` |
| `PlayLSOLinedUp` | `Lined up.` |
| `PlayLSOLowLeft` | `You're low left.` |
| `PlayLSOLowRight` | `You're low right.` |
| `PlayLSOPowerLow` | `Power, power.` |
| `PlayLSOReturnToHolding` | `Return to holding.` |
| `PlayLSORightForLineup` | `Right for lineup.` |
| `PlayLSOYoureHigh` | `You're high.` |

`{runway}`, `{heading}`, `{pad}`, `{catapult}`, and `{wire}` are populated from the original dispatch method parameters. Tower and carrier services share `ATCVoiceProfile`; the LSO calls above are dispatched by that same profile.

## AWACS coverage added in v1.1

| Game dispatch method | Vanilla static evidence | Rendered subtitle |
| --- | --- | --- |
| `AWACSVoiceProfile.ReportPopups(bool, Vector3, ...)` | `popupClips` and up to three popup group parameters -> `CommRadioManager.PlayMessageString` | `POPUP` followed by one to three `GROUP/HOSTILE BRAA/BULLSEYE` lines |
| `AWACSVoiceProfile.ReportThreatToAwacs` | `groupBraaClips`, `hostileBraaClips`, `leansOnClips` -> `CommRadioManager.PlayMessageString` | `GROUP/HOSTILE BRAA ...` followed by `LEANS ON.` |
| `AWACSVoiceProfile.ReportUnable` | `AppendCallsigns` + `unableClips` -> `CommRadioManager.PlayMessageString` | `{callsign}, UNABLE.` |

The existing list-based `ReportPopups(List<ContactGroup>, int, int)` route remains covered. The boolean/vector overload above is a distinct dispatch method and requires its own Harmony signature.

## Existing AWACS coverage

| Game dispatch method | Rendered subtitle |
| --- | --- |
| `ReportHostile` | `HOSTILE BRAA/BULLSEYE {bearing} / {range} / {altitude} / {aspect}` |
| `ReportGroup` | `GROUP BRAA/BULLSEYE {bearing} / {range} / {altitude} / {aspect}` |
| `ReportPictureClean` | `PICTURE CLEAN` |
| `ReportGrandSlam` | `{callsign}, GRAND SLAM.` |
| `ReportRTB` | `OVERLORD OFF STATION. RTB.` |
| `ReportGoingDown` | `MAYDAY, MAYDAY, MAYDAY. OVERLORD IS GOING DOWN!` |
| `ReportHomeplateBra` | `HOMEPLATE BRAA {bearing} / {range} / {altitude} / {aspect}` |
| `ReportGroups` | `PICTURE` followed by one or more `GROUP/HOSTILE BRAA/BULLSEYE` lines |
| `ReportPopups(List<ContactGroup>, int, int)` | `POPUP` followed by one or more `GROUP/HOSTILE BRAA/BULLSEYE` lines |

`BRAA` is used when no Bullseye is available; otherwise the same geometry is labelled `BULLSEYE`. The values are calculated from the game-supplied contact position and velocity at dispatch time.

## Ground crew coverage added in v1.2

| Game message identifier | Vanilla static evidence | Rendered subtitle |
| --- | --- | --- |
| `GroundCrewMessages.NotAvailable` | `rearmingNotAvailableClips` | `Rearming is not available.` |
| `GroundCrewMessages.IsAirborne` | `isAirborneClips` | `Unable. Aircraft is airborne.` |
| `GroundCrewMessages.TaxiToStation` | `taxiToStationClips` | `Taxi to the rearming station.` |
| `GroundCrewMessages.EnteredStation` | `enteredRearmingStationClips` | `You have entered the rearming station.` |
| `GroundCrewMessages.TurnOffEngines` | `turnOffEnginesClips` | `Please turn off your engines.` |
| `GroundCrewMessages.DisarmWeapons` | `disarmWeaponsClips` | `Please disarm your weapons.` |
| `GroundCrewMessages.Success` | `successClips` | `Rearming complete.` |
| `GroundCrewMessages.ReturnedToVehicle` | `returnedToVehicleClips` | `Ground crew returning to vehicle.` |

All eight entries are dispatched through `GroundCrewVoiceProfile.PlayMessage(...)`. The message enum is internal to the game assembly, so the Harmony postfix receives its first parameter generically and matches its stable enum name. The compiled profile exposes clip-list and enum names, but no editable sentence transcript. Accordingly, these are clear English templates matched to the original message identifiers, and should be verified against each voice variant in game.

## Deliberately not treated as text sources

`CommRadioManager` can play arbitrary clips, while scenario actions such as `VTSRadioMessagePlayer.PlayMessage(string audioPath, bool copilot)` receive only an audio path. They do not expose reliable English text, so this mod does not claim to generate original-English subtitles from them automatically.

Wingman radio is a separate next step. The game exposes 25 stable message identifiers, but its `WingmanVoiceProfile` stores audio clips rather than an editable transcript. It needs a verified, maintained English mapping before it can be added responsibly.
