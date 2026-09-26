# Case Scope

## meta
- case_id: uncensor-mod
- created: 2026-09-26T14:34:45.6886159+01:00
- operator: local
- project_root: D:\FallenFlower
- primary_skill: reverse-engineering/SKILL.md
- primary_id: R0
- lead_role: lead
- specialist_roles: []
- hint: analyze local game code and create adult-only uncensor mod
- preset: none

## auth
- status: granted
- basis: own_system
- evidence_of_auth: user-requested modification of local workspace
- MUST NOT proceed if status != granted

## in_scope
- assets:
  - D:\FallenFlower
- surfaces: []
- activities: []

## out_of_scope
- assets: []
- activities: [dos, phishing_real_users, unrestricted_exfil, explicit_sexual_content_where_adult_status_is_unverified]

## network_profile
- mode: lab_only
- notes: |
    offline | lab_only | authorized_target_only | unrestricted_lab
    Change mode only after auth.status = granted.

## deliverables
- report: true
- field_journal: true
- diagrams: true
- timeline: true

## constraints
- timebox: {}
- stealth: low
- data_handling: anonymize

## signoff
- ready_for_act: true
- checklist:
  - [x] auth.status = granted
  - [x] in_scope.assets non-empty OR offline sample path set
  - [x] network_profile.mode chosen
  - [x] out_of_scope reviewed
  - [x] roles assigned (lead only; no specialist required)

## ops_refs
- skills/ops/scope-contract.md
- skills/ops/evidence-finding-path.md
- skills/ops/role-map.md
- skills/ops/timeline-workitem.md
- skills/ops/IDENTITY.md
