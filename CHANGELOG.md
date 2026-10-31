# Changelog

All notable changes to this project are documented here.

## [Unreleased]

### 中文

- 开发中。

### English

- In development.

## [2.2.1] - 2026-10-31

### 中文

- Web: 支持动态反向代理前缀。前端使用 `<base href>` + `document.baseURI` 推导 base path，资源、路由、API、SignalR 都会跟随 nginx 的 `/数字/` 前缀，不再需要重新构建。
- API: 运行时加载 `appsettings.user.json`。优先级为 `appsettings.json` < `appsettings.user.json` < 环境变量 < 命令行，升级覆盖 `appsettings.json` 不会丢失用户启动配置。
- CI: GitHub Release 自动附带 `CHANGELOG.md` 中对应版本的更新日志。

### English

- Web: support dynamic reverse-proxy prefixes. The frontend derives its base path from `<base href>` and `document.baseURI`, so assets, routes, API calls, and SignalR follow nginx `/port/` prefixes without rebuilding.
- API: load `appsettings.user.json` at runtime. Precedence is `appsettings.json` < `appsettings.user.json` < environment variables < command line, so upgrading no longer loses user-managed startup settings.
- CI: GitHub Releases now include the matching changelog section from `CHANGELOG.md`.

## [2.2.0] - 2026-10-31

Compared with v1.14.2.

### 中文

#### 重要变更

- **架构重构**：从 Blazor Server 重构为「独立 ASP.NET Core API + React Web UI」；Android 客户端从旧 MAUI 版本迁移到新的 API + Web UI 架构。
- **旧配置一次性导入**：新版本不再在运行时读取旧 `appsettings.user.json` 里的账号和任务配置。首次启动且数据库没有账号时，会自动检测并导入旧配置；也可以手动执行 `--import-legacy`。
- **设置改为按账号存储**：旧的全局 `GameConfig` 会复制到每个账号，`PlayersOption` 会通过登录解析的 PlayerId 映射到账号。
- **部分旧字段不再支持**：`DungeonBattleRelicSort`、`RecordBattleLog` / `BattleLogDir`、`ServerUrl`、`ShopDiscountPercent`、`CharacterFilter.BattleParameterType`、`FilterStrategy.PropertyMoreThanSelf` 等；导入时会列在“未导入”清单里。
- **Android 客户端重构**：从旧 MAUI 版本迁移为直接内置 API + Web UI 的新 Android 版本；功能与网页版一致，保留前台服务保活和通知栏停止。发布沿用同一包名/签名，通常可直接覆盖安装旧版 APK。
- **默认端口变化**：`dotnet run` 使用 `http://localhost:5127`；发布包启动脚本使用 `http://localhost:5290`；Docker 容器内监听 8080，仓库自带的 compose 映射到宿主机 5290。旧 `appsettings.user.json` 里的 Kestrel 配置不再生效，改端口请使用 `ASPNETCORE_URLS` 或 `appsettings.json` 的 `Kestrel`。
- **AutoLogin 保留旧语义**：旧配置里 `AutoLogin=false` 的账号，导入后仍需手动登录一次。

#### 新功能

- **全新 Web UI**：账号、仪表盘、角色、装备、道具、商店、召唤、战斗、PVP、无穷之塔、任务、战斗记录、公会、好友、幻影神殿、时空洞窟、祈愿之泉、队伍强度、Autopilot、自动化设置。
- **多账号**：支持 ClientKey / 引继密码添加账号、切换世界、按账号独立配置和任务。
- **自动化任务**：每日/每小时任务、Cron 配置、按动作开关；竞技场/传奇竞技场目标策略；时空洞窟、本地团队战、悬赏、商店、抽卡、公会、好友等自动化。
- **Autopilot**：抽卡 → 升星 → 组队 → 等级链接 → 升级/装备/强化 → 推图/爬塔；带成长记录、进度曲线、Tamamo 队伍强度和卡池建议。
- **后端能力**：REST API、SignalR 任务日志/本地团队战 Hub、Swagger（开发环境）、Master 二进制数据同步、图片资源缓存、战斗日志入库、文件日志。
- **Web 体验**：简中/繁中/日/韩/英多语言，深色模式。

#### 改进与修复

以下为 v1.14.2 之后的变化：

- 悬赏任务：Claim All 未解锁时改为逐个领取；祈愿之泉列表未加载时重抽不再无效。
- 本地团队战：没有可加入的房间时自动开房；单刷连输 3 次后停止。
- 每日/每小时任务读取角色回忆。
- Master 表默认返回空集合而不是 null，减少 KeyNotFound。
- Web：等待后端自动登录完成后再渲染；移动端头部更紧凑。
- Android：内容避开系统栏，状态栏跟随页面主题。
- 设置：前后端配置 key 统一，修复未类型化设置被写成 `{"ValueKind":...}` 的问题；新增旧 key 只读回退。

#### 升级说明

1. 备份旧 `appsettings.user.json`。
2. 用新版本启动；如果数据库没有账号，会自动检测同目录或旁边 `MementoMori.WebUI` 下的旧配置并导入。
3. 自动导入没触发时，手动执行：

   ```bash
   MementoMori.Api --import-legacy <path-to-appsettings.user.json> --dry-run
   ```

   确认无误后去掉 `--dry-run` 再执行一次。

4. 导入不会修改旧 JSON，结果写入 `mementomori.db`；`AutoLogin=false` 的账号需要手动登录一次。
5. 旧 `BattleLogs/` 里的 JSON 不会导入；`Master/` 缓存可以继续复用。
6. 数据库已有账号时默认不会重复导入，需要覆盖请加 `--force`。
7. Docker 用户把旧配置放到 `/data/appsettings.user.json` 即可被自动检测。

#### 已知限制

- 一个账号有多个角色时，只导入其中一个角色的玩家配置，其余会列在 warning 里。
- `PlayersOption` 的 PlayerId 与账号 UserId 没有直接映射，需要联网解析；离线时只导入账号和全局配置。
- `AutoLogin` 目前只能通过导入值或数据库修改，前端还没有开关。
- 旧 `BattleLogs`、`DungeonBattleRelicSort`、`ServerUrl` 等不在本次导入范围内。

### English

#### Breaking Changes

- **Architecture rewrite**: rebuilt from Blazor Server into a standalone ASP.NET Core API + React web UI. The Android client is migrated from the legacy MAUI app to the new API + web UI architecture.
- **One-time legacy config import**: the new version no longer reads accounts and job settings from the old `appsettings.user.json` at runtime. On first start, when the database has no accounts, it auto-detects and imports the legacy config. You can also run `--import-legacy` manually.
- **Per-account settings**: the legacy global `GameConfig` is copied to every account, and `PlayersOption` entries are mapped to accounts via PlayerIds resolved at login.
- **Some legacy fields are no longer supported**: `DungeonBattleRelicSort`, `RecordBattleLog` / `BattleLogDir`, `ServerUrl`, `ShopDiscountPercent`, `CharacterFilter.BattleParameterType`, `FilterStrategy.PropertyMoreThanSelf`, etc. These are listed in the "not imported" report.
- **Android client rebuilt**: migrated from the legacy MAUI app to a new Android app that embeds the API + web UI. It keeps the on-device server, foreground service keep-alive, and notification stop action. The release uses the same package name and signing key, so it can usually be installed over the old APK.
- **Default port changes**: `dotnet run` uses `http://localhost:5127`; the published start scripts use `http://localhost:5290`; the Docker container listens on 8080 and the bundled compose file maps it to 5290 on the host. Kestrel settings in the old `appsettings.user.json` no longer apply; use `ASPNETCORE_URLS` or `Kestrel` in `appsettings.json` to change the port.
- **AutoLogin keeps its legacy per-account meaning**: accounts with `AutoLogin=false` still require a manual login after import.

#### New Features

- **New web UI**: Accounts, Dashboard, Characters, Equipment, Items, Shop, Gacha, Battle, PvP, Infinite Tower, Missions, Battle Logs, Guild, Friends, Phantom Temple, Time Space Cave, Wishing Fountain, Team Tiers, Autopilot, and Automation settings.
- **Multi-account**: add accounts by ClientKey or transfer password, switch worlds, and keep settings and jobs per account.
- **Automation**: daily/hourly jobs, cron configuration, per-action toggles; Arena/Legend League target strategies; automation for Time Space Cave, Local Raid, Bounty Quest, Shop, Gacha, Guild, Friends, and more.
- **Autopilot**: gacha → rank up → team build → level link → level up/equipment/reinforcement → quest/tower climbing; includes a growth feed, progress chart, Tamamo team tiers, and banner advice.
- **Backend capabilities**: REST API, SignalR hubs for job logs and Local Raid, Swagger (development), Master data binary sync, asset cache, battle logs stored in SQLite, and file logging.
- **Web experience**: five languages (Simplified Chinese, Traditional Chinese, Japanese, Korean, English) and dark mode.

#### Improvements and Fixes

Changes since v1.14.2:

- Bounty Quest: claim rewards one by one when Claim All is locked; reroll no longer does nothing before the Wishing Fountain list is loaded.
- Local Raid: host a room when no room is available; stop solo runs after 3 losses.
- Daily/hourly jobs read character memories.
- Master tables return empty collections instead of null, reducing KeyNotFound errors.
- Web: wait for backend auto-login to finish before rendering; more compact mobile header.
- Android: content clears the system bars; status bar follows the page theme.
- Settings: unified frontend/backend keys; fixed untyped settings being stored as `{"ValueKind":...}`; added read-only fallbacks for legacy keys.

#### Upgrade Notes

1. Back up the old `appsettings.user.json`.
2. Start the new version. If the database has no accounts, it auto-detects the legacy config in the working directory or a sibling `MementoMori.WebUI` folder and imports it.
3. If auto-import does not trigger, run:

   ```bash
   MementoMori.Api --import-legacy <path-to-appsettings.user.json> --dry-run
   ```

   Review the report, then run it again without `--dry-run`.

4. Import does not modify the old JSON. Data is written to `mementomori.db`. Accounts with `AutoLogin=false` still need a manual login.
5. Old JSON files in `BattleLogs/` are not imported. The `Master/` cache can be reused.
6. If the database already has accounts, import is skipped unless `--force` is used.
7. Docker users can place the old config at `/data/appsettings.user.json` for auto-detection.

#### Known Limitations

- If an account has multiple characters, only one character's player config is imported; the others are listed in warnings.
- `PlayersOption` PlayerIds have no direct mapping to account UserIds and require online resolution. Offline imports only accounts and global settings.
- `AutoLogin` can currently only be set by import or directly in the database; there is no UI toggle yet.
- Old `BattleLogs`, `DungeonBattleRelicSort`, `ServerUrl`, etc. are not imported.
