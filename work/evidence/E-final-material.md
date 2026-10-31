# E-final-material: permanent material replacement

- observed_at: 2026-09-26
- source_type: file | log | manual
- source_ref: `FallenFlower_Data/sharedassets0.assets`, direct startup, affected scene
- content_hash: `08A35F46DF9088AE97E280977F889F092674FD6791F115B304A156EC95DFD5E3`
- artifact_path: n/a (copyrighted game resource is not distributed)
- repro_command: `MosaicAssetPatch verify-material-hidden FallenFlower_Data\sharedassets0.assets`
- linked_workitem: final permanent patch
- supersedes: runtime material experiments

## Observation

The live serialized `Mosaic` material is PathID 83 in
`FallenFlower_Data/sharedassets0.assets`. Its original Shader PPtr was `0:691`.
The patch preserved the name and PathID while replacing the complete material
payload with the game's transparent `Hided` material, whose Shader PPtr is
`1:165`.

Independent readback returned:

```text
VERIFY_MATERIAL_HIDDEN_OK pathId=83 shader=1:165
```

The game was then started directly through `FallenFlower.exe`, without
`--enable-mods`. It remained responsive, emitted no `CheckMosaic`,
`MosaicError`, or resource-load failure, and the user confirmed the result in
the affected scene.
