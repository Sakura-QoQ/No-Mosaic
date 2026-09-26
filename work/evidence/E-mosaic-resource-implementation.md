# E-mosaic-resource-implementation

- Collected: 2026-09-26
- Source: AssetRipper 1.3.10 export of the local Unity 2022.3.62f3 player
- Quality: serialized Unity YAML plus generated Mod SDK declarations

## Bottom-layer implementation

- `Mosaic` is a dedicated GameObject with a `SkinnedMeshRenderer`, not a post-process.
- The renderer uses `Assets/Material/Mosaic.mat`.
- `Mosaic.mat` uses `Shader Graphs/NotMosaic` and serializes `_Size: 120` and `_UIBlur: 2`.
- Eleven serialized `Mosaic` objects were found across six monster prefabs and the Hotel/Street scenes.
- `CheckMosaic` remains an integrity monitor and is not modified.

## Replacement design

`NoMosaic` v2.0 disables only the dedicated renderer at scene/character lifecycle hooks:

- `Player.Start`
- `GameDirector.AfterChangeScene`
- `NPC.Awake`, `NPC.OnEnable`, and `NPC.Init`
- `PlayerSex.StartSex`
- `NPCSex.OnStartSex`

No `Update` or `LateUpdate` hook is registered. TypeScript compilation passed and runtime startup logged the v2.0 marker without an exception.
