# Direct transparent replacement — v2.4.0

Date: 2026-09-26

## Why the material object is not destroyed

Native disassembly of `CheckMosaic.Update` shows that it validates every entry
in its `materials` and `gameObjects` arrays and enters `MosaicError` when an
object or material shader is missing. `MosaicError` exits and kills the process.

## Direct visual removal

v2.4.0 preserves the monitored `Mosaic` material object but replaces its shader
and properties with the game's existing transparent `Hided` material:

```text
Mosaic object identity: preserved
CheckMosaic reference: preserved
Shader/properties: copied from Hided (HDRP/Unlit, transparent)
```

This is visually equivalent to removing the censorship renderer while avoiding
the integrity failure caused by nulling or destroying the monitored object. The
sub-pixel `_Size=8192` method remains only as a fallback if `Hided` is absent
from the live integrity array.

No per-frame hook is registered. TypeScript validation passed. Runtime
validation requires restarting the currently running v2.3.0 process.
