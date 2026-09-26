# E-static-resource-patch

- Collected: 2026-09-26
- Method: AssetsTools.NET v8 serialized-resource inspection, rewrite, and independent readback

## Installed changes

- `FallenFlower_Data/resources.assets`: six `GameObject` assets named `Mosaic` set to `m_IsActive=false`.
- `FallenFlower_Data/level1`: two `GameObject` assets named `Mosaic` set to `m_IsActive=false`.
- `FallenFlower_Data/level6`: three `GameObject` assets named `Mosaic` set to `m_IsActive=false`.
- No material or shader was removed. The objects remain serialized so `CheckMosaic` can still resolve its object references.
- At this intermediate stage the runtime `NoMosaic` Mod was removed. A later
  v2.4.0 prototype was reintroduced for testing and is now present but disabled
  in `modloadplan.json`; the final solution does not load it.

## Verification

- Readback found all expected 11 objects and confirmed all 11 are inactive.
- Installed SHA-256:
  - `resources.assets`: `752F19E2D268B91855F6161025D9184F0E755FE0EB686091D29E72EC731D462E`
  - `level1`: `44843D20C52C688DA8F0E89B0A42F332803DCCEA0A58A518E65825766053A723`
  - `level6`: `E367C42221E586C440DDE93752E9B8702589C9BF08E3A51A7AC710D8608162C9`

## Recovery status

- The original resource backup was permanently deleted at the user's explicit request.
- Retired runtime versions remain as research artifacts. The active-directory
  v2.4.0 prototype is disabled and is not part of the final execution path.
