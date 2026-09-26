# Screenshot target correction

Date: 2026-09-26

## Observation

The supplied screenshot visibly contains a block-pixel censorship effect on the
character model even though v2.1.1 reported zero targets while traversing 931
objects in `RoomScene`.

## Static evidence

- Serialized objects named `Mosaic` are `SkinnedMeshRenderer` objects.
- Their exported default material is `Hided` (GUID
  `d4dd14f19ce967c49be795d5ca06abe5`), not `Mosaic.mat`.
- `CheckMosaic.Start` resolves `Shader Graphs/NotMosaic`.
- `CheckMosaic.Update` checks its material and GameObject arrays for valid Unity
  objects; its failure path exits and kills the process. It must remain intact.

## Root-cause hypothesis and correction

The scene-only traversal omitted actor roots moved into Unity's hidden
`DontDestroyOnLoad` scene. This explains why the visual effect remained while
the ordinary `RoomScene` scan found no targets.

NoMosaic v2.2.0 now traverses:

1. ordinary loaded scene roots;
2. `Player.LocalPlayer.gameObject`;
3. every actor returned by `NPC.GetAllNPC()`.

The startup pass deliberately excludes persistent actors because the native
`Player.LocalPlayer` object is not valid yet at ModLoader startup. All scans are
still lifecycle-driven; no per-frame hook is installed.

Status: TypeScript validation passed. Runtime/visual validation requires a game
restart because the currently running process loaded v2.1.1 before the file was
changed.
