# E-checkmosaic-disassembly — corrected component semantics

IL2CPP output identifies:

- `CheckMosaic.Start`: RVA `0x781100`
- `CheckMosaic.Update`: RVA `0x7811D0`
- `CheckMosaic.MosaicError`: RVA `0x7810A0`

Radare2 inspection shows:

- `Start` calls `UnityEngine.Shader.Find`, compares the result with null, and invokes the error path if the expected shader is missing.
- `Update` iterates `materials`, checks each material and `Material.get_shader`, then iterates `gameObjects` and checks object existence.
- `MosaicError` calls `UnityEngine.Application.Quit`, obtains the current process, and calls `Process.Kill`.

Therefore `CheckMosaic` is an integrity monitor, not the censorship toggle. Its complete arrays must not be disabled or destroyed.

Static asset strings independently show scene/prefab objects named exactly `Mosaic`. The corrected Mod leaves `CheckMosaic` intact and disables only those named scene objects.

