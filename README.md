# NoMosaic

Reverse-engineering research, release sources, tools, and documentation for the
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
├─ docs/                  Formal report, technical guide, and audit records
│  └─ audit/              Stage-by-stage validation and correction records
├─ versions/              Release versions
│  ├─ NoMosaic-Permanent-v1.0.0/
│  └─ NoMosaic-Permanent-v1.0.0.zip
└─ work/                  RE workspace, evidence, tools, exports, and outputs
```

## Documentation

- [Formal reverse-engineering report](docs/2026-09-26_reverse-FallenFlower-NoMosaic-report.md)
- [Detailed reverse-engineering guide](docs/NoMosaic-Reverse-Engineering-Guide.md)
- [Investigation timeline](work/timeline.md)
- [Patcher source](work/tools/MosaicAssetPatch/Program.cs)
- [License, ownership, and third-party notices](LICENSE)

## Important notes

- The only current release is `versions/NoMosaic-Permanent-v1.0.0.zip`.
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
