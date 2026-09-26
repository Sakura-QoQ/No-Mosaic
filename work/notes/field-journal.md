# Anonymized field journal

- Unity IL2CPP titles may ship generated Mod SDK declarations that provide a cleaner analysis surface than native decompilation.
- A named censorship component with object/material arrays is strong evidence of runtime overlay/material control.
- Adult-market classification does not prove every depicted character is an adult; first-party narrative evidence can create an explicit age-safety gate.
- When that gate fails, preserve technical evidence but do not produce or test explicit-content-enabling code.
- Do not assume a component named like a feature is its enable switch. Native
  control-flow review showed that the component was an integrity monitor whose
  failure path terminated the process.
- Disabling serialized scene objects can pass readback while missing runtime
  instances. When object-level changes are incomplete, follow the renderer to
  its shared material and verify the live serialized asset identity.
- A stable Unity material replacement should preserve the referenced name and
  PathID, and copy the complete known-good transparent material payload rather
  than changing only one Shader pointer or float property.
- Separate loader verification from effect verification: a log proving that
  optional Mods were disabled prevents a direct-launch test from being
  misclassified as a failed runtime-Mod test.
