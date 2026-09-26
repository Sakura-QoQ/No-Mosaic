# Work Items

| ID | title | role | targets | surface | status | evidence | notes |
|----|-------|------|---------|---------|--------|----------|-------|
| WI-001 | Establish scope and auth | lead | case | process | complete | scope.md | Local owner-operated workspace |
| WI-002 | Triage Unity/IL2CPP package | lead | game files | binary/metadata | complete | E-triage | No execution performed |
| WI-003 | Locate censorship implementation | lead | Mod SDK | static | complete | E-mosaic-anchor | CheckMosaic object/material component |
| WI-004 | Validate adult-only precondition | lead | narrative metadata | safety | complete | E-age-gate | User confirmed character is a 20-year-old university student |
| WI-005 | Implement Mod | lead | Mods | TypeScript | complete | E-checkmosaic-disassembly | Corrected v1.1 disables only objects named Mosaic |
| WI-006 | Runtime validation | lead | game | dynamic | complete | E-direct-sharedassets-material-patch | Direct launch stable; user confirmed success in affected scene |
| WI-007 | Static configuration/API validation | lead | Mod files | static | complete | E-mosaic-anchor | JSON parses; all referenced SDK hooks and methods exist |
| WI-008 | Diagnose v1.0 regression | lead | CheckMosaic native methods | static | complete | E-checkmosaic-disassembly | v1.0 reversed field semantics and was disabled/replaced |
| WI-009 | Replace live serialized material | lead | sharedassets0.assets | static patch | complete | E-direct-sharedassets-material-patch | Preserved PathID 83; full Hided material payload installed |
| WI-010 | Consolidate technical documentation | lead | case docs | synthesis | complete | NoMosaic-Reverse-Engineering-Guide.md | Failure history, implementation, hashes, verification, and recovery documented |

## Coverage
- [x] Recon/analysis complete for relevant in_scope assets
- [ ] Critical/High candidates triaged (or N/A for pure RE)
- [x] Validated findings have Evidence (E-*)
- [x] Path documented (attack/call/solve)
- [x] Timeline continuous across major phases
- [x] Report written in report/analysis.md
- [x] field-journal anonymized locally

## Refs
- skills/ops/timeline-workitem.md
- skills/ops/evidence-finding-path.md
