# NoMosaic Permanent v1.0.0

Permanent material patch for FallenFlower. It replaces the serialized `Mosaic`
material payload with the game's own transparent `Hided` material while
preserving the original material name and PathID.

NoMosaic is not MIT-licensed. Commercial use is prohibited. Non-commercial
secondary development is permitted only under the conditions in `LICENSE.txt`.

This release is the successfully validated solution. It is not the retired
TypeScript/Jint runtime Mod and does not perform per-frame scans.

## Installation

1. Extract the ZIP to a normal writable folder.
2. Close FallenFlower completely.
3. Double-click `Install-NoMosaic.cmd`.
4. If prompted, enter the full FallenFlower installation directory.
5. Wait for `Installation completed successfully`.
6. Launch `FallenFlower.exe` normally.

No Mod manager or `--enable-mods` launch option is required.

The installer patches the existing local
`FallenFlower_Data\sharedassets0.assets` file and verifies the result before
finishing. It does not install anything into the game's `Mods` directory.

## Requirements

- Windows PowerShell 5.1 or later.
- Microsoft .NET 8 Runtime.
- A compatible FallenFlower version containing the expected `Mosaic` and
  `Hided` materials.

## Updating the game

A game update or file verification may restore `sharedassets0.assets`. Run the
installer again only if the patcher accepts the updated resource. If it reports
an incompatible resource, wait for an updated release instead of forcing it.

## Uninstallation

Use the game platform's Verify/Repair Files feature, or reinstall the game.
This package intentionally does not create a backup copy.

## Technical verification

Successful installation ends with output similar to:

```text
VERIFY_MATERIAL_HIDDEN_OK pathId=83 shader=1:165
Installation completed successfully.
```

## Ownership and third-party licenses

Read `LICENSE.txt` for the ownership boundary and licenses covering bundled
third-party components. FallenFlower and Unity content are not owned by this
project.
