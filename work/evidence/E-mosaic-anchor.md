# E-mosaic-anchor — runtime censorship component

- Source: `Mods\types\global.d.ts`
- Quality: generated SDK type declaration

The generated declaration contains:

```ts
declare class CheckMosaic extends UnityEngine.MonoBehaviour {
    gameObjects: UnityEngine.GameObject[];
    materials: UnityEngine.Material[];
}
```

The same SDK exposes `Scene.GetRootGameObjects()` and `GameObject.GetComponent("CheckMosaic")`. This is sufficient to support a non-binary approach that recursively surveys loaded scene objects and locates censorship components.

No implementation was created or tested because the age-safety gate in `E-age-gate.md` did not pass.

