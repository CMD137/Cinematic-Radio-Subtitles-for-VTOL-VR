# Cinematic Radio Subtitles

![Cinematic Radio Subtitles Workshop cover](Assets/workshop-cover-v1.0.jpg)

Cinematic Radio Subtitles is a client-side VTOL VR Mod Loader mod that makes
English NPC radio calls easier to follow in flight. It presents the current
transmission as a clean, cinema-style subtitle in a stereo-safe area of the VR
view, rather than using the game's tutorial-label presentation.

## v1.0

- Tower and LSO calls
- AWACS calls, with compact BRAA and Bullseye-style formatting where possible
- A two-line subtitle: coloured speaker label plus the current radio message
- A comfortable stereo depth that lets both eyes fuse the subtitle naturally
- No translation layer: subtitle wording remains the original English game text

## How it works

The mod uses focused Harmony hooks at the game's Tower / LSO and AWACS radio
dispatch points. When a supported NPC transmission is played, its game-provided
text is sent to this mod's own Unity Canvas overlay.

The overlay is rendered at a comfortable virtual distance in front of the VR
camera, so it reads as one subtitle instead of a separate image per eye. It is
drawn above cockpit geometry and retains just the current transmission, keeping
the view clear during flight. AWACS reports additionally receive lightweight
formatting so headings and positional information are faster to scan.

## Scope and roadmap

This project is intentionally client-side and does not change mission logic,
voice audio, or multiplayer state. Future updates will expand coverage to more
radio roles and refine presentation based on in-game testing.

## Source and reference

Source code: <https://github.com/CMD137/Cinematic-Radio-Subtitles-for-VTOL-VR>

The project is published as a reference for how the overlay and radio hooks are
implemented. It is an independent implementation and does not reuse another
mod's source code or UI.

## Workshop artwork

`Assets/workshop-cover-v1.0.jpg` is the v1.0 Workshop thumbnail. It is 0.24 MB,
within Steam's 1 MB thumbnail limit.

