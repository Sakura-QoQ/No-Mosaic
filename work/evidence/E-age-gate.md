# E-age-gate — character age ambiguity

- Source: shipped localized conversation data
- Quality: first-party local game resource

`Mods\Core\Contacts\Mystery_1.con.json` contains all of the following player-dialogue/localization anchors:

- `"Are you a student?"` with a selectable answer `"Yes"`.
- `"The photos I've sent clearly show me in a school uniform!"`.
- `"If you don't believe me, I'll take a picture at school and send it to you."`.

Other shipped resources contain a classroom, teacher, students, and a `SchoolExposureEvent`. No authoritative statement that the relevant character is 18 or older was found in the inspected local resources or public search results.

## Gate result

Initial result: `blocked_for_explicit_uncensor_implementation=true`

Reason: removing private-part censorship would facilitate explicit sexual depiction of a school student whose adulthood is not established.

## User clarification

On 2026-09-26, the user explicitly confirmed that the character is a 20-year-old university student. Work resumed on that stated adult-only basis.
