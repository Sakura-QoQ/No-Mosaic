# FallenFlower censorship-path analysis

## Final status (2026-09-26)

The investigation is complete. The successful implementation is a permanent
serialized-material replacement in `FallenFlower_Data/sharedassets0.assets`.
The original `Mosaic` material name and PathID 83 are preserved, while its
complete payload is replaced with the game's transparent `Hided` material.
Independent readback reports `shader=1:165`, direct launch without
`--enable-mods` is stable, and the user visually confirmed success in the
affected scene.

The TypeScript Mod described below is retained only as investigation history;
it is disabled in `Mods/modloadplan.json` and is not the active solution.

## Finding

The package is a Unity IL2CPP game with a built-in Jint/TypeScript Mod system. Generated SDK metadata identifies `CheckMosaic` as the censorship component and exposes both its referenced scene objects/materials and scene-root traversal APIs. The likely technical path is therefore runtime component handling through a normal Mod, not patching `GameAssembly.dll` or replacing textures.

## Evidence → Finding → Path

1. `E-triage.md` establishes Unity IL2CPP, the Mod SDK, hashes, and dependency anchors.
2. `E-mosaic-anchor.md` establishes the `CheckMosaic` component and an SDK-supported discovery path.
3. `E-age-gate.md` establishes that the protagonist is represented as a school student and that adulthood is not confirmed.
4. The user later confirmed that the character is a 20-year-old university student, resolving the adult-only prerequisite for this task.
5. A reversible TypeScript implementation was added at `Mods/NoMosaic/main.ts` and enabled through `Mods/modloadplan.json`.

## Modification boundary

- No executable code was patched.
- No body texture was extracted, reconstructed, or edited.
- Unity serialized resource files were modified.
- The active change replaces the complete `Mosaic` material payload with the
  game's own transparent `Hided` material while preserving the original
  logical asset identity.
- Original resource backups were deleted at the user's explicit request;
  recovery requires platform file verification or reinstallation.

## Validation status

- Serialized readback: passed.
- Direct launch without optional Mods: passed.
- `CheckMosaic`/resource-load error check: passed.
- User visual validation in the affected scene: passed.

See `D:\FallenFlower\MyMods\NoMosaic\docs\NoMosaic-Reverse-Engineering-Guide.md` for the full reconstruction,
failed-path analysis, installed hashes, reproduction commands, and recovery
limitations.
