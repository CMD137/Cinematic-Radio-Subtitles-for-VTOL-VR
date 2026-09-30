# Cinematic Radio Subtitles

An independent VTOL VR Mod Loader mod that presents original English NPC radio
transcripts as cinematic, stereo-safe subtitles in the pilot's view.

## v1.0 scope

- Tower / LSO radio calls
- AWACS calls, compacted into readable BRAA or Bullseye lines
- A camera-relative, stereo-safe overlay rather than the built-in tutorial label
- One current transmission at a time, styled as a two-line cinema subtitle

## Build

The project defaults to this PC's installation paths. If VTOL VR or the Mod
Loader is installed elsewhere, override them when building:

```powershell
dotnet build -p:VtolVrPath='E:\\SteamLibrary\\steamapps\\common\\VTOL VR' -p:ModLoaderPath='E:\\SteamLibrary\\steamapps\\common\\VTOL VR Mod Loader'
```

For local testing, create this folder in the VTOL VR game directory:

```text
@Mod Loader\\Mods\\CinematicRadioSubtitles
```

Copy both `Builds\\CinematicRadioSubtitles\\item.json` and
`Builds\\CinematicRadioSubtitles\\CinematicRadioSubtitles.dll` into it. Start **VTOL VR
Mod Loader** (not the base game), enable the local item in its list, then use
the loader's **Play** button.

## Notes

This project has its own overlay, queue, presentation rules, and event
handlers. Radio wording is an English transcript of the game's audio; it does
not reuse another mod's source code or UI.

## Workshop artwork

Use `Assets\\workshop-cover-v1.0.jpg` as the thumbnail in **VTOL VR Mod
Uploader**. It is 0.24 MB, within Steam's 1 MB thumbnail limit.

