# FallenFlower Mod permission audit

## Root cause observed in the current launch

`Player.log` says:

```text
[ModLoader] Optional mods are disabled. Pass --enable-mods to enable plan-based mod loading.
[ModLoader] Loading Mod: Core
```

There is no `[NoMosaic]` startup marker. `modloadplan.json` being enabled only makes the Mod eligible; the process must also be started with `--enable-mods`.

## Allowed Mod capabilities

The generated SDK and runtime types expose:

- Scene enumeration through `SceneManager`, including root GameObjects.
- Hierarchy traversal through `Transform`.
- GameObject lookup, creation, component lookup/addition, activation, instantiation, and destruction.
- Public exposed fields/properties on registered Unity and game types, including writable `Renderer.enabled`, `Renderer.forceRenderingOff`, material properties, and `GameObject.SetActive`.
- Hooks only for generated `@hook` methods on `@hookable` types. Hook context can intercept calls or set results.
- Coroutines and delayed callbacks through `JintCoroutine`.
- Unity resource loading through the exposed `LoadResource`/`Resources` APIs.
- Module loading and file reads restricted to the Mod directory through `require` and `ReadModFile`.
- Registered Mod UI windows and conversation injection.

The operations used by NoMosaic—scene traversal, `GetComponent("UnityEngine.SkinnedMeshRenderer")`, and writing `renderer.enabled=false`—are all exposed and are not blocked by the sandbox.

## Explicit runtime sandbox blocks

Blocked namespace prefixes (13):

```text
System.IO
System.Net
System.Diagnostics
System.Reflection
System.Runtime.InteropServices
JetBrains
Unity.IO
Unity.Jobs.LowLevel.Unsafe
Unity.Collections.LowLevel.Unsafe
Unity.Profiling.LowLevel.Unsafe
UnityEngine.Windows
UnityEngine.Rendering
UnityEngineInternal
```

Blocked CLR types (17):

```text
System.Type
System.Activator
System.AppDomain
System.Environment
System.Console
System.IntPtr
System.UIntPtr
System.RuntimeType
System.Reflection.Assembly
System.Reflection.MemberInfo
System.Reflection.MethodInfo
System.Reflection.PropertyInfo
System.Reflection.FieldInfo
System.Reflection.EventInfo
System.Reflection.ConstructorInfo
System.Diagnostics.Process
System.Diagnostics.ProcessStartInfo
```

Blocked member names:

```text
GetType
MemberwiseClone
```

Consequently a Mod cannot use arbitrary filesystem APIs, networking, process launch/control, reflection, pointer/native interop, or unsafe Unity internals. `ReadModFile` rejects absolute/path-escape requests and only reads inside the current Mod folder.

## Static patch status

Independent serialized readback confirms all eleven known `Mosaic` GameObjects are inactive in `resources.assets`, `level1`, and `level6`. Their continued visible effect implies they are reactivated or replaced during runtime. A correctly loaded event-driven Mod has sufficient permission to disable their renderers again after activation.

## Evidence anchors

- `Mods/types/global.d.ts`: exported API and hook allowlist.
- `GameAssembly.dll` IL2CPP metadata: loader/sandbox native methods.
- `JintHotInterop::.cctor` at RVA `0x1CC9DD0`: exact blocked namespace/type arrays.
- `JintHotInterop::IsBlockedMemberName` at RVA `0x1CC8710`: `GetType` and `MemberwiseClone`.
- `ModLoadPlanResolver::ShouldLoadMods` at RVA `0x862B00`: process-level optional-Mod gate.
- `Player.log`: current runtime load decision.
