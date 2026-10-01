# Cinematic Radio Subtitles

![Cinematic Radio Subtitles Workshop cover](Assets/workshop-cover-v1.0.jpg)

Cinematic Radio Subtitles is a client-side VTOL VR Mod Loader mod that makes
English NPC radio calls easier to follow in flight. It presents the current
transmission as a clean, cinema-style subtitle in a stereo-safe area of the VR
view, rather than using the game's tutorial-label presentation.

Steam Workshop: <https://steamcommunity.com/sharedfiles/filedetails/?id=3810788987>

Chinese README: [README-zh.md](README-zh.md)

## v1.1

- Tower and LSO calls
- AWACS calls, with compact BRAA and Bullseye-style formatting where possible
- Expanded normal-airfield landing branches: denied, full pattern, cancelled,
  wrong tower, landed-before-clearance, and wrong-airfield calls
- The remaining AWACS report paths, including threat, unable, and legacy popup
  reports
- Reading-time subtitle duration: length now follows the amount of English
  text, with source-specific minimum and maximum limits
- A two-line subtitle: coloured speaker label plus the current radio message
- A comfortable stereo depth that lets both eyes fuse the subtitle naturally
- English-only subtitle layer: it does not translate radio calls

## How it works

The mod uses focused Harmony hooks at the game's Tower / LSO and AWACS radio
dispatch points. When a supported NPC transmission is played, the hook receives
its parameters and sends a matching English subtitle to this mod's own Unity
Canvas overlay.

The overlay is rendered at a comfortable virtual distance in front of the VR
camera, so it reads as one subtitle instead of a separate image per eye. It is
drawn above cockpit geometry and retains just the current transmission, keeping
the view clear during flight. AWACS reports additionally receive lightweight
formatting so headings and positional information are faster to scan.

## Hook reference

[RADIO_HOOKS.md](RADIO_HOOKS.md) records the important radio methods, the
vanilla clip-list evidence associated with each method, and the English text
this mod renders. The method inventory came from static decompilation of the
installed game's compiled `Assembly-CSharp.dll`; it is not a copy of VTOL VR
source code.

## Scope and roadmap

This project is intentionally client-side and does not change mission logic,
voice audio, or multiplayer state. The next major area is original wingman
radio, after the relevant messages have been mapped and verified in game.

Scenario scripts and third-party vehicles can play arbitrary audio paths with
no accompanying text. Those messages require an author-provided transcript or
a separately maintained text mapping; audio alone is not enough to produce a
reliable original-English subtitle.

## Source and reference

Source code: <https://github.com/CMD137/Cinematic-Radio-Subtitles-for-VTOL-VR>

The project is published as a reference for how the overlay and radio hooks are
implemented. It is an independent implementation and does not reuse another
mod's source code or UI.

## Workshop artwork

`Assets/workshop-cover-v1.0.jpg` is the v1.0 Workshop thumbnail. It is 0.24 MB,
within Steam's 1 MB thumbnail limit.

