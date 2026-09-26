# E-triage

- `UcModLauncher.exe` SHA-256: `C3FBACF66876BBFBEB6AA47FC37598804B489BB2FA2B40982CE52E50F4CA3CA3`.
- Format: x64 PE32+ .NET single-file host with overlay; native import surface includes file/process launch APIs as expected for the launcher host.
- `GameAssembly.dll` SHA-256: `639279AA2243F584BE294CA5AA6AC1BD12687B9220584F4FB2AC5C9A97BD1EE3`.
- Format: x64 PE32+ Unity IL2CPP GameAssembly; imports and IL2CPP exports parsed successfully with rabin2 6.2.2.
- Equivalent managed anchor: IL2CPP metadata exposes `JintModLoader`, `JintModEngineFactory`, `JintHotInterop`, `JintModEnvironment`, and `ModLoadPlanResolver`.
- Quality: high for loader gate and sandbox lists; generated SDK declarations and native static initializer agree.
