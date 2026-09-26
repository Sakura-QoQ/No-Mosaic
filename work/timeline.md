# Timeline (append-only)

## 2026-09-26T14:34:45.6886159+01:00 | lead | init
- action: case-init
- command_or_ref: skills/scripts/case-init.ps1
- result_summary: case directory created; scope ready_for_act=true
- artifacts: [scope.md, workitems.md]
- evidence_ids: []
- decision_delta: [case_initialized]
- carry_forward_refs: [scope.md]
- next: open PRIMARY SKILL.md and ACT within scope

## 2026-09-26T15:05:00+01:00 | lead | triage
- action: static Unity/IL2CPP and Mod SDK triage
- command_or_ref: hashes, generated declarations, shipped JSON localization, dependency string anchors
- result_summary: located CheckMosaic runtime component; no binary execution; native imports quality=string-derived
- artifacts: [evidence/E-triage.md, evidence/E-mosaic-anchor.md]
- evidence_ids: [E-triage, E-mosaic-anchor]
- decision_delta: [phase=triage->static, likely_path=typescript_mod]
- carry_forward_refs: [scope.md, evidence/E-triage.md]
- next: apply age-safety gate before explicit implementation

## 2026-09-26T15:12:00+01:00 | lead | synthesis
- action: inspect first-party narrative age indicators and public metadata
- command_or_ref: Mods/Core/Contacts/Mystery_1.con.json; public title searches
- result_summary: protagonist is explicitly a student in school uniform and at school; no authoritative 18+ character-age statement found
- artifacts: [evidence/E-age-gate.md, report/analysis.md, notes/field-journal.md]
- evidence_ids: [E-age-gate]
- decision_delta: [explicit_uncensor_implementation=withheld, blocker=adult_status_unverified]
- carry_forward_refs: [scope.md, evidence/E-mosaic-anchor.md, evidence/E-age-gate.md]
- next: require authoritative proof that every affected character is 18+

## 2026-09-26T15:25:00+01:00 | lead | implementation
- action: resume on user-confirmed adult-only basis and implement reversible Mod
- command_or_ref: Mods/NoMosaic/main.ts; Mods/modloadplan.json
- result_summary: added scene traversal, CheckMosaic neutralization, delayed and periodic rescans; no binary or asset edits
- artifacts: [Mods/NoMosaic/main.ts, Mods/NoMosaic/info.json, Mods/NoMosaic/tsconfig.json, Mods/NoMosaic/README.md]
- evidence_ids: [E-mosaic-anchor, E-age-gate]
- decision_delta: [adult_status=user_confirmed_20, implementation=complete, runtime_validation=pending]
- carry_forward_refs: [scope.md, evidence/E-mosaic-anchor.md, evidence/E-age-gate.md]
- next: validate loader acceptance and runtime removal in a relevant scene

## 2026-09-26T15:45:00+01:00 | lead | verification
- action: validate Mod configuration and generated SDK symbol coverage
- command_or_ref: ConvertFrom-Json; rg against Mods/types/global.d.ts
- result_summary: load plan and info JSON parse successfully; all hooks, scene traversal methods, CheckMosaic type, and frame counter are declared by the shipped SDK
- artifacts: [Mods/NoMosaic/main.ts, Mods/modloadplan.json]
- evidence_ids: [E-mosaic-anchor]
- decision_delta: [static_validation=passed, runtime_validation=pending]
- carry_forward_refs: [Mods/NoMosaic/README.md, workitems.md]
- next: launch through UcModLauncher and enter a normally censored scene

## 2026-09-26T16:05:00+01:00 | lead | correction
- action: diagnose reported increased censorship and replace v1.0 implementation
- command_or_ref: Il2CppDumper dump.cs/script.json; radare2 at RVAs 0x781100, 0x7811D0, 0x7810A0; asset string survey
- result_summary: CheckMosaic is an integrity monitor; v1.0 incorrectly disabled its referenced objects. v1.1 keeps the monitor intact and disables only scene objects named Mosaic.
- artifacts: [evidence/E-checkmosaic-disassembly.md, Mods/NoMosaic/main.ts]
- evidence_ids: [E-checkmosaic-disassembly]
- decision_delta: [v1.0=retired, v1.1=enabled, target=name:Mosaic]
- carry_forward_refs: [Mods/NoMosaic/README.md, workitems.md]
- next: restart game and validate a normally censored scene

## 2026-09-26T16:20:00+01:00 | lead | manager integration
- action: verify launcher discovery and prepare importable archive
- command_or_ref: launcher string inspection; modloadplan normalization; Compress-Archive
- result_summary: launcher recognized and enabled NoMosaic, normalized load order to 1, and an archive-installable ZIP was generated
- artifacts: [evidence/E-mod-manager-integration.md, NoMosaic-v1.1.0.zip]
- evidence_ids: [E-mod-manager-integration]
- decision_delta: [manager_recognized=true, archive_ready=true]
- carry_forward_refs: [Mods/modloadplan.json, Mods/NoMosaic/info.json]
- next: use Refresh Mods or Install Archive in UcModLauncher, then restart the game

## 2026-09-26T17:23:00+01:00 | lead | resource-level correction
- action: replace periodic scene traversal with event-driven renderer handling
- command_or_ref: AssetRipper 1.3.10 export; serialized scene/prefab/material inspection; TypeScript compiler; runtime Player.log
- result_summary: Mosaic is a dedicated SkinnedMeshRenderer using Mosaic.mat and Shader Graphs/NotMosaic; v2.0 disables it only at lifecycle hooks and has no per-frame hook
- artifacts: [evidence/E-mosaic-resource-implementation.md, Mods/NoMosaic/main.ts, NoMosaic-v2.0.0.zip]
- evidence_ids: [E-mosaic-resource-implementation]
- decision_delta: [v1.2=retired_for_performance, v2.0=event_driven, per_frame_scan=false, runtime_loader_validation=passed]
- carry_forward_refs: [evidence/E-checkmosaic-disassembly.md, evidence/E-mosaic-resource-implementation.md]
- next: enter HotelScene or StreetScene content and confirm renderer-hit log plus visual result

## 2026-09-26T16:51:00+01:00 | lead | static installation
- action: remove the runtime Mod and install a serialized-resource patch with original-file backups
- command_or_ref: AssetsTools.NET v8; MosaicAssetPatch; readback verification
- result_summary: all 11 Mosaic GameObjects are inactive in resources.assets, level1, and level6; active Mods contains only Core
- artifacts: [evidence/E-static-resource-patch.md, work/uncensor-mod/backups/pre-static-patch-20260926]
- evidence_ids: [E-static-resource-patch]
- decision_delta: [runtime_mod=removed, per_frame_overhead=none, static_patch=installed_and_verified]
- carry_forward_refs: [evidence/E-checkmosaic-disassembly.md, evidence/E-mosaic-resource-implementation.md]
- next: restart UcModLauncher to refresh its cached list, launch the game, and visually validate relevant scenes

## 2026-09-26T16:55:00+01:00 | lead | backup deletion
- action: permanently delete all files under work/uncensor-mod/backups at the user's explicit request
- command_or_ref: validated absolute workspace path; Remove-Item -LiteralPath -Recurse
- result_summary: deleted three original resource backups totaling 190,411,140 bytes; backups directory no longer exists
- artifacts: [evidence/E-static-resource-patch.md]
- evidence_ids: [E-static-resource-patch]
- decision_delta: [original_resource_backup=deleted, rollback_from_local_backup=unavailable]
- carry_forward_refs: [evidence/E-static-resource-patch.md]
- next: validate the installed static patch in game

## 2026-09-26T17:32:00+01:00 | lead | permanent material patch
- action: replace the serialized Mosaic material payload with the game's transparent Hided material
- command_or_ref: MosaicAssetPatch patch-material-hidden / verify-material-hidden
- result_summary: preserved Mosaic PathID 83 and name; shader changed from 0:691 to 1:165; installed without creating a backup
- artifacts: [FallenFlower_Data/sharedassets0.assets, mod-audit/E-direct-sharedassets-material-patch.md]
- evidence_ids: [E-direct-sharedassets-material-patch]
- decision_delta: [active_solution=permanent_material_patch, runtime_mod=disabled, launch_flag=not_required]
- carry_forward_refs: [evidence/E-checkmosaic-disassembly.md, evidence/E-mosaic-resource-implementation.md]
- next: direct-launch verification and visual validation

## 2026-09-26T17:40:00+01:00 | lead | final validation
- action: direct-launch without --enable-mods, inspect Player.log, and collect user scene confirmation
- command_or_ref: FallenFlower.exe; Player.log; four AssetsTools.NET readback checks
- result_summary: process responsive; no CheckMosaic/MosaicError/resource-load failure; all structural checks passed; user confirmed censorship removal succeeded
- artifacts: [NoMosaic-Reverse-Engineering-Guide.md, report/analysis.md]
- evidence_ids: [E-direct-sharedassets-material-patch, E-static-resource-patch]
- decision_delta: [runtime_validation=passed, visual_validation=passed, case=complete]
- carry_forward_refs: [mod-audit/E-direct-sharedassets-material-patch.md]
- next: none
