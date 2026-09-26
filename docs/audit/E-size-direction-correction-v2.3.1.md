# Mosaic `_Size` direction correction

Date: 2026-09-26

## Visual A/B evidence

- Original runtime value: `_Size=120`, producing multiple medium-sized blocks.
- v2.3.0 value: `_Size=0`, producing fewer and visibly larger blocks in the
  user's second screenshot.

The second screenshot proves that `_Size` represents sampling-grid density,
not block width. Setting it to zero coarsens the effect instead of disabling it.

## v2.3.1 correction

- Set `_Size=8192`, above practical render dimensions, so grid cells are
  sub-pixel sized.
- Keep `_UIBlur=0`.
- Preserve `CheckMosaic`, its material object, shader, and referenced objects.
- Apply only during existing finite lifecycle scans; no per-frame hook.

TypeScript validation passed. The running v2.3.0 process must be restarted to
load v2.3.1 before visual verification.
