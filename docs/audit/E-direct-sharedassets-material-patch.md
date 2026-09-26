# Direct sharedassets material replacement

Date: 2026-09-26

## Why the last screenshot was not a v2.4 test

The active Player.log contains:

```text
[ModLoader] Optional mods are disabled. Pass --enable-mods to enable plan-based mod loading.
```

There is no NoMosaic version marker. The game was started directly and did not
load the optional Mod.

## Permanent resource patch installed

Asset scan found the live serialized material in:

```text
FallenFlower_Data/sharedassets0.assets
Material PathID: 83
Name: Mosaic
Original shader PPtr: 0:691
Original _Size: 120
```

The patch preserves PathID 83 and the name `Mosaic`, but serializes the complete
transparent `Hided` material data at that PathID. The resulting shader pointer
is `1:165` (the Hided HDRP/Unlit shader).

Independent readback:

```text
VERIFY_MATERIAL_HIDDEN_OK pathId=83 shader=1:165
```

Installed target:

```text
D:\FallenFlower\FallenFlower_Data\sharedassets0.assets
SHA256: 08A35F46DF9088AE97E280977F889F092674FD6791F115B304A156EC95DFD5E3
```

The file was installed directly without creating a backup, following the
user's explicit request. `NoMosaic` was disabled in `Mods/modloadplan.json`
because the runtime Mod is superseded by this permanent resource patch.

## Direct-launch validation

The game was started through `FallenFlower.exe` without `--enable-mods`.
The process remained responsive and the log confirmed optional Mods were not
loaded. No `CheckMosaic`, `MosaicError`, or resource-load failure was emitted
during startup. The user subsequently entered the affected scene and confirmed
that the censorship was removed successfully.
