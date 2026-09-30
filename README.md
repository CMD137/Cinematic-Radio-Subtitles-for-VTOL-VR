# Right Comms Subtitles

An independent VTOL VR Mod Loader mod that presents original English NPC radio
transcripts in a compact, DCS-inspired panel at the upper-right of the pilot's
view.

## v1.0 scope

- Tower / LSO radio calls
- AWACS calls, compacted into readable BRAA or Bullseye lines
- A camera-relative, stereo-safe panel rather than the built-in tutorial label
- Up to three recent transmissions, with priority-aware replacement

## Build

The project defaults to this PC's installation paths. If VTOL VR or the Mod
Loader is installed elsewhere, override them when building:

```powershell
dotnet build -p:VtolVrPath='E:\\SteamLibrary\\steamapps\\common\\VTOL VR' -p:ModLoaderPath='E:\\SteamLibrary\\steamapps\\common\\VTOL VR Mod Loader'
```

For local testing, create this folder in the VTOL VR game directory:

```text
@Mod Loader\\Mods\\RightCommsSubtitles
```

Copy both `Builds\\RightCommsSubtitles\\item.json` and
`Builds\\RightCommsSubtitles\\RightCommsSubtitles.dll` into it. Start **VTOL VR
Mod Loader** (not the base game), enable the local item in its list, then use
the loader's **Play** button.

## Notes

This project has its own overlay, queue, presentation rules, and event
handlers. Radio wording is an English transcript of the game's audio; it does
not reuse another mod's source code or UI.

