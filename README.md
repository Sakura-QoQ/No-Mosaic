# NoMosaic

Source, releases, and verified reverse-engineering documentation for the
FallenFlower NoMosaic patch.

> This directory is for development and archival use only. The game does not
> load anything from here, and no item under `versions` becomes active
> automatically.

## Current status

The active solution is a permanent serialized-material patch installed in the
game resources. It does not depend on a runtime Mod. Launch
`D:\FallenFlower\FallenFlower.exe` normally; `--enable-mods` is not required.

## Directory structure

```text
NoMosaic/
├─ src/                   Maintained patcher source
├─ third_party/           Pinned patcher build/runtime dependencies
├─ docs/                  Formal report and implementation guide
├─ versions/              Release versions
│  ├─ NoMosaic-Permanent-v1.0.0/
│  └─ NoMosaic-Permanent-v1.0.0.zip
└─ work/                  RE workspace, evidence, tools, exports, and outputs
```

## Documentation

- [Formal reverse-engineering report](docs/2026-09-26_reverse-FallenFlower-NoMosaic-report.md)
- [Detailed reverse-engineering guide](docs/NoMosaic-Reverse-Engineering-Guide.md)
- [Investigation timeline](work/timeline.md)
- [Patcher source](src/MosaicAssetPatch/Program.cs)
- [Non-commercial derivative-development license](LICENSE)

## Build

```powershell
./build.ps1
```

The maintained patcher has only two operations: create a complete `Hided`
material replacement and independently verify the resulting Shader pointer.
The discarded `_Size`, GameObject-deactivation, tree-dump, and scan experiments
remain described in the reverse-engineering guide, not in production code.

## Important notes

- NoMosaic is not MIT-licensed. Commercial use is prohibited; modification and
  derivative development are allowed only under the conditions in `LICENSE`.
- The current release is `versions/NoMosaic-Permanent-v1.0.1.zip`; v1.0.0 is retained as history.
  The obsolete v2.4.0 runtime ZIP was deleted.
- The release patches the user's local resource file. It does not install into
  the game's `Mods` directory and does not require `--enable-mods`.
- The game `Mods` directory contains only game-required content; the NoMosaic
  entry was removed from `modloadplan.json`.
- Original game-resource backups were deleted at the user's request. Restore
  the original files through the game platform's verification feature or by
  reinstalling the game.
- Do not apply old resource files after a game update. Rescan the updated build
  and regenerate the patch.
- Original and patched game resource bundles are not distributed by this
  repository. See `LICENSE` for the complete rights boundary.

## Maintained source verification

Command parsing lives in `PatchCommand.cs`; asset loading and material patching remain separate. The class database is declared once in the project and copied by MSBuild for both build and publish. Patch output must be a new file: existing output files and in-place writes are rejected. Duplicate target materials and a null Hided Shader are incompatible assets.

After building, run `./tools/Test-Patcher.ps1 -AssetPath <local-sharedassets0.assets>`. The test uses isolated copies under artifacts, checks CLI exit codes and patch/verify, and checks that both inputs retain their SHA-256 hashes. It never installs a patch. Verification checks Shader pointers; it is not a full material or in-game visual comparison. Historical releases under versions are unchanged by this build.
