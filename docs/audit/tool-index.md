# Reverse-engineering tool index

- Scan time: 2026-09-26 16:54:35 +01:00
- Routing entry: `SKILL.md` → `routing.md` → matching child skill
- Generation: `skills/scripts/refresh-tool-index.ps1`
- Purpose: confirm tool availability and resolved paths for agent-side routing.
- Note: MCP-only capabilities are evaluated separately from local runtime
  availability. `npx` alone does not make jshookmcp or reqable-mcp ready.

| Tool | Owning skill | Purpose | Available | Resolved path | Version | Source | Script references |
|---|---|---|---|---|---|---|---|
| jadx | apk-reverse | Java decompilation | no | — | — | Missing | apk-reverse/scripts/decode.ps1 |
| apktool | apk-reverse | APK decoding and rebuilding | no | — | — | Missing | apk-reverse/scripts/decode.ps1; rebuild-sign-install.ps1 |
| adb | apk-reverse | Device connection and logcat | no | — | — | Missing | apk-reverse/scripts/rebuild-sign-install.ps1 |
| java | apk-reverse | Run JAR files and Java tooling | yes | `C:\Program Files (x86)\Common Files\Oracle\Java\java8path\java.exe` | 1.8.0_501 | Get-Command | apk-reverse/scripts/decode.ps1 |
| apksigner | apk-reverse | APK signing | no | — | — | Missing | apk-reverse/scripts/rebuild-sign-install.ps1 |
| zipalign | apk-reverse | APK alignment | no | — | — | Missing | apk-reverse/scripts/rebuild-sign-install.ps1 |
| idalib-mcp | ida-reverse | IDA Pro idalib MCP server | no | — | — | Missing | — |
| ida-pro-mcp | ida-reverse | IDA Pro MCP CLI/plugin installer | no | — | — | Missing | — |
| ida | ida-reverse | IDA Pro application | no | — | — | Missing | — |
| binaryninja | binary-ninja-reverse | Binary Ninja GUI/Python platform | no | — | — | Missing | — |
| frida | apk-reverse | Dynamic instrumentation | no | — | — | Missing | apk-reverse/scripts/frida-run.ps1 |
| frida-ps | apk-reverse | Frida process enumeration | no | — | — | Missing | apk-reverse/scripts/frida-run.ps1 |
| r2 | radare2 | Main radare2 analyzer | no | — | — | Missing | radare2/scripts/recon.ps1 |
| rabin2 | radare2 | Binary reconnaissance | no | — | — | Missing | radare2/scripts/recon.ps1 |
| rasm2 | radare2 | Assembly and disassembly | no | — | — | Missing | radare2/SKILL.md |
| radiff2 | radare2 | Binary diffing | no | — | — | Missing | radare2/SKILL.md |
| rahash2 | radare2 | Hashing and verification | no | — | — | Missing | radare2/SKILL.md |
| rax2 | radare2 | Numeric and bitwise conversion | no | — | — | Missing | radare2/SKILL.md |
| r2pm | radare2 | radare2 plugin management | no | — | — | Missing | radare2/SKILL.md |
| r2xsql | radare2 | radare2 SQL query tool | no | — | — | Missing | radare2/SKILL.md |
| r2xsql-full | radare2 | Full radare2 SQL query tool | no | — | — | Missing | radare2/SKILL.md |
| r2mcp | radare2 | radare2 MCP analysis | no | — | — | Missing | radare2/SKILL.md |
| radius2 | radare2 | Symbolic and dynamic analysis | no | — | — | Missing | radare2/SKILL.md |
| python | reverse-engineering | Helper-script execution | no | — | — | Missing | frida-run.ps1; review_case.py |
| pip | reverse-engineering | Python package management | no | — | — | Missing | — |
| node | js-reverse | Node.js reproduction and MCP clients | yes | `D:\Program Files\nodejs\node.exe` | — | Get-Command | js-reverse/SKILL.md |
| npx | js-reverse | Temporary npm packages and MCP entry points | yes | `D:\Program Files\nodejs\npx.cmd` | — | Get-Command | js-reverse/SKILL.md |
| jshookmcp | js-reverse | Launch jshook MCP through npx | no | — | — | Missing | js-reverse/SKILL.md |
| reqable-mcp | pentest-tools | Launch Reqable desktop MCP through npx | no | — | — | Missing | pentest-tools/SKILL.md |
| xquik-mcp | threat-intelligence | Remote public X/Twitter intelligence MCP | no | — | — | Missing | threat-intelligence/SKILL.md |
| agent-browser | browser-automation | Playwright browser automation | no | — | — | Missing | browser-automation/SKILL.md |
| analyzeHeadless | reverse-engineering | Headless Ghidra analysis | no | — | — | Missing | reverse-engineering/SKILL.md |
| jeb-pro | apk-reverse | Commercial Android/ARM decompiler | no | — | — | Missing | apk-reverse/SKILL.md |
| playwright | browser-automation | Playwright browser engine | no | — | — | Missing | browser-automation/SKILL.md |
| proxycat | pentest-tools | Proxy-pool management and rotation | no | — | — | Missing | pentest-tools/SKILL.md |
| seclists | pentest-tools | Security wordlists | no | — | — | Missing | pentest-tools/SKILL.md |
| pentestswarm | pentest-tools | Autonomous swarm penetration testing | no | — | — | Missing | pentest-tools/SKILL.md |
| nmap | pentest-tools | Port scanning and service detection | no | — | — | Missing | pentest-tools/SKILL.md |
| binwalk | firmware-pentest | Firmware extraction and analysis | no | — | — | Missing | firmware-pentest/SKILL.md |
| yara | malware-analysis | Malware rule-matching engine | no | — | — | Missing | malware-analysis/SKILL.md |
| pwntools | reverse-engineering | CTF exploit-development framework | no | — | — | Missing | reverse-engineering/SKILL.md |
| bkcrack | reverse-engineering | Known-plaintext ZIP/ZipCrypto attack tool | no | — | — | Missing | crypto-decode-tools.md |

## Capability status

| Capability | Tool available | Ready | MCP registered | Service online | MCP HTTP verified | Auto-install | Bootstrap method |
|---|---:|---:|---:|---:|---:|---:|---|
| jadx | no | no | n/a | n/a | n/a | yes | github-release-zip |
| apktool | no | no | n/a | n/a | n/a | yes | github-release-jar-wrapper |
| jeb-pro | no | no | n/a | n/a | n/a | no | manual |
| frida / frida-ps | no | no | n/a | n/a | n/a | yes | pip-package |
| idalib-mcp | no | no | no | no | no | yes | pip-package |
| jshookmcp | no | no | no | no | no | yes | npm-mcp |
| reqable-mcp | no | no | no | no | no | yes | npm-mcp |
| xquik-mcp | no | no | no | no | no | yes | remote-http-mcp |
| anything-analyzer | no | no | no | no | no | yes | local-http-mcp |
| idapro | no | no | no | no | no | yes | local-http-mcp |
| r2 / rabin2 | no | no | n/a | n/a | n/a | yes | github-release-zip |
| adb | no | no | n/a | n/a | n/a | yes | winget-package |
| agent-browser | no | no | n/a | n/a | n/a | yes | npm-global |
| ghidra-mcp | no | no | no | no | no | yes | github-release-zip |
| seclists | no | no | n/a | n/a | n/a | yes | git-clone |
| proxycat | no | no | n/a | n/a | n/a | yes | git-clone |
| burpsuite-mcp | no | no | no | no | no | no | local-http-mcp |
| pentestswarm | no | no | n/a | n/a | n/a | yes | go-install |
| nmap | no | no | n/a | n/a | n/a | yes | winget-package |
| binwalk | no | no | n/a | n/a | n/a | no | manual |
| yara | no | no | n/a | n/a | n/a | yes | winget-package |
| pwntools | no | no | n/a | n/a | n/a | yes | pip-package |
| bkcrack | no | no | n/a | n/a | n/a | yes | github-release-zip |
