# NoMosaic v2.1.1 runtime validation

Date: 2026-09-26

- ModLoader loaded `NoMosaic` and printed the exact `v2.1.1` marker.
- The previous startup failure was caused by starting a coroutine on the
  not-yet-created `Player.LocalPlayer` native object. That aborted the script
  before hook registration.
- Startup now performs only a synchronous scan. Deferred scans are launched
  from live Unity objects supplied to lifecycle hooks.
- Runtime log confirms all three `AfterChangeScene:RoomScene` passes ran:
  immediate, next frame, and 0.5 seconds.
- The current RoomScene contained 931 traversed objects and zero mosaic-name,
  mosaic-material, or mosaic-shader targets. This is expected before entering
  content that dynamically creates a censorship renderer.
- No `Update` or `LateUpdate` hook is registered by the Mod.
- No execution error was logged after the v2.1.1 marker.

Pending visual validation: enter an interaction that normally displays the
censorship effect, then inspect the subsequent `[NoMosaic] target` and
`PlayerSex.StartSex` / `NPCSex.OnStartSex` records.
