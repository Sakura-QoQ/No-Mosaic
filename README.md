# NoMosaic

FallenFlower 的去马赛克逆向、版本源码、工具和文档归档。

> 本目录仅用于开发和归档，不参与游戏加载。`versions` 中的任何版本都不会自动生效。

## 当前状态

最终生效方案是已写入游戏资源的永久材质补丁，不依赖这里的运行时 Mod 文件。正常启动 `D:\FallenFlower\FallenFlower.exe` 即可，不需要 `--enable-mods`。

## 目录结构

```text
NoMosaic/
├─ docs/                  正式报告、详细技术文档和审计记录
│  └─ audit/              各阶段验证与纠错记录
├─ versions/              发布版本
│  ├─ NoMosaic-Permanent-v1.0.0/
│  └─ NoMosaic-Permanent-v1.0.0.zip
└─ work/                  逆向工作区、Evidence、工具、导出和补丁产物
```

## 文档入口

- [正式逆向报告](docs/2026-09-26_逆向-FallenFlower-NoMosaic-report.md)
- [详细逆向技术文档](docs/NoMosaic-逆向技术文档.md)
- [调查时间线](work/timeline.md)
- [补丁源码](work/tools/MosaicAssetPatch/Program.cs)

## 注意事项

- 当前唯一发布包是 `versions/NoMosaic-Permanent-v1.0.0.zip`；旧的 v2.4.0 运行时 ZIP 已删除。
- 新发布包在用户本机修补资源，不安装到游戏的 `Mods` 目录，也不需要 `--enable-mods`。
- 游戏目录 `Mods` 中只保留游戏自身需要的内容；`modloadplan.json` 已移除 NoMosaic 条目。
- 原始游戏资源备份已按用户要求删除。恢复原版需通过游戏平台验证文件或重新安装。
- 游戏更新后不可直接套用旧资源文件，应重新扫描和生成补丁。
