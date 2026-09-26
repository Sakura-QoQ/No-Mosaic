# NoMosaic v2.3.0 runtime material evidence

Date: 2026-09-26

## Dynamic result

Fresh process log:

```text
[NoMosaic] v2.3.0 loaded; runtime material patch + finite scans active (no per-frame hook).
[NoMosaic] material-patch reason=Player.Start:immediate material=Mosaic shader=Shader Graphs/NotMosaic _Size=120->0 _UIBlur=2->0
```

This identifies the live censorship implementation that corresponds to the
pixel-block screenshot. It is a material parameter effect, not a renderer found
by walking `RoomScene` names.

## Implementation

- Obtain the existing `CheckMosaic` component through `GameManager.Singleton`.
- Preserve the component, its material array, and all referenced GameObjects.
- Locate only materials whose material or shader name contains `Mosaic`.
- Set `_Size` and `_UIBlur` to zero on the live material instance.
- Set the matching global shader properties to zero as a fallback.
- Reapply only at finite lifecycle events; no `Update` or `LateUpdate` hook.

The process remained responsive and no Mod execution error followed the v2.3.0
marker. Visual confirmation in the same in-game view remains the final check.
