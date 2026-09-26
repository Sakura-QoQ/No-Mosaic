# E-triage — FallenFlower Unity IL2CPP package

- Collected: 2026-09-26
- Scope: local files under `D:\FallenFlower`
- Execution: no game or launcher binary was executed
- Quality: static metadata and text inspection; native import names are string-derived because the indexed PE tools are unavailable

## Artifact identity

| Artifact | SHA-256 |
|---|---|
| `FallenFlower.exe` | `D644FF01B9AEDA4BC48F47E5FFB10F5F53C1A9F3AC9768245E8980FC837A8541` |
| `GameAssembly.dll` | `639279AA2243F584BE294CA5AA6AC1BD12687B9220584F4FB2AC5C9A97BD1EE3` |
| `UcModLauncher.exe` | `C3FBACF66876BBFBEB6AA47FC37598804B489BB2FA2B40982CE52E50F4CA3CA3` |
| `global-metadata.dat` | `BE8EA931577B3709CAFF058AC154FA1DBA20F35DE99D1FFB92BA23BAAC531D32` |

## Type and architecture anchors

- Unity Windows x64 player layout: `FallenFlower.exe`, `UnityPlayer.dll`, `GameAssembly.dll`.
- IL2CPP metadata exists at `FallenFlower_Data\il2cpp_data\Metadata\global-metadata.dat`.
- `ScriptingAssemblies.json` lists `Assembly-CSharp.dll`, `HookRuntime.dll`, `Jint.dll`, Unity modules, and supporting managed assemblies.
- The shipped Mod SDK exposes TypeScript declarations in `Mods\types\global.d.ts` and loads mods according to `Mods\modloadplan.json`.

## Imports / equivalent anchors

- `FallenFlower.exe`: string-derived native anchors include `KERNEL32.dll` and `UnityPlayer.dll`.
- `GameAssembly.dll`: string-derived native anchors include `KERNEL32.dll`, `USER32.dll`, `ADVAPI32.dll`, `WS2_32.dll`, `IPHLPAPI.DLL`, `SHELL32.dll`, and `dbghelp.dll`.
- IL2CPP managed-equivalent anchors are preserved in `ScriptingAssemblies.json`; the relevant application assemblies are `Assembly-CSharp.dll`, `HookRuntime.dll`, and `Jint.dll`.
- Indexed `rabin2`, `dumpbin`, and `llvm-objdump` tools were unavailable, so the native import list is marked `quality=string-derived`, not a parsed IAT.

## Signature state

PowerShell `Get-AuthenticodeSignature` reports `NotSigned` for `FallenFlower.exe`, `GameAssembly.dll`, and `UcModLauncher.exe`.

## Initial hypotheses

1. The visual censorship is implemented at runtime by scene objects/materials rather than irreversibly baked textures.
2. The built-in TypeScript Mod API is the lowest-risk modification surface; no binary patch should be needed.

