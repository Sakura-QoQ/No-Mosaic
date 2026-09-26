# E-mod-manager-integration

- `UcModLauncher.exe` contains `InstallArchive`, `ExportZip`, `RefreshMods`, enable/disable, load-order, and overwrite-confirmation commands.
- After the Mod manager was opened, `Mods/modloadplan.json` was rewritten and normalized by the launcher.
- The normalized entry is `Id=NoMosaic`, `DirectoryName=NoMosaic`, `Enabled=true`, `LoadOrder=1`.
- `NoMosaic-v1.1.0.zip` contains a single top-level `NoMosaic` directory with `info.json`, `main.ts`, `README.md`, and `tsconfig.json`.

Conclusion: the Mod is already recognized by the manager, and the ZIP can be installed through its archive-install action for reinstall/overwrite workflows.

