# Development workspace

The `work` directory contains development material for NoMosaic. The game does
not read this directory, and nothing here is installed automatically.

## What may be published

The following project-created material may be committed to this repository:

| Category | Examples | Publication status |
| --- | --- | --- |
| Patcher source | `tools/MosaicAssetPatch/Program.cs`, `.csproj` | Included |
| Research evidence | `evidence/*.md` | Included |
| Reports and notes | `report/`, `notes/`, `timeline.md` | Included |
| Reproduction metadata | `scope.md`, `workitems.md`, `routes/` | Included after review |
| Tool logs | `evidence/assetripper.log` | Included; review paths and personal data before future updates |

These files describe the reverse-engineering process but do not contain an
original or patched FallenFlower resource bundle.

## Third-party files that require notices

`tools/UABEA-v8/AssetsTools.NET.dll` and `classdata.tpk` are included only as
build dependencies. They are not owned by this project. Their upstream sources
and applicable notices are recorded in [`../LICENSE`](../LICENSE).

## What must remain local

The following material is excluded by `.gitignore` and must not be uploaded:

- AssetRipper exports and extracted game assets;
- original or patched FallenFlower resource files;
- complete downloaded third-party tool distributions;
- downloaded archives, build output, and dependency caches;
- editor settings, operating-system metadata, and temporary files.

Before adding new files under `work`, run:

```powershell
git status --short
git check-ignore -v <path-to-file>
```

Do not force-add ignored game content with `git add -f`.

## Rights

See [`../LICENSE`](../LICENSE) for the ownership statement and third-party
notices. Project ownership does not extend to FallenFlower, Unity,
AssetsTools.NET, UABEA, or other third-party content.
