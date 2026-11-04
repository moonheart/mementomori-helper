# Changelog

All notable changes to this project are documented here.

## [Unreleased]

### 中文

- 开发中。

### English

- In development.

## [2.2.2] - 2026-11-01

Compared with v2.2.1.

### 中文

#### 新功能

**幻影神殿（设置 → 社交）**

- 「找不到房间就自己开房单刷」改为开关，**默认关闭**：关闭时等不到房间就放弃（恢复 2.2 之前的行为，小号继续等主号的房间）；打开才会自己开房单刷。
- 「等待房间时间」改为可配置，默认 10 分钟。
- 「进房后多久没开战就退出换个房间」改为可配置，默认 3 分钟，账号不会再有卡在死房间里的情况。
- 「自己开房连输几次就停」改为可配置，默认 3 次，`0` = 不停。
- 「整个任务最长跑多久」可配置，默认 20 分钟，防止账号卡在任务里。
- 「等不到房间 / 连败太多时降低一级关卡继续」开关，以及「在同一关卡等多少分钟后降」时间，默认 3 分钟；降低时活动关和普通关都会考虑。
- 「关卡等级」可手动指定，默认 `0` = 按奖励自动选择；低等级小号可以固定打某个等级，该等级不存在时用最接近的低级。

**Tamamo**

- 支持 tamamo.dev 的账号专用 key（账号菜单 → Issue Dedicated Key → Issue / Reissue → Copy），随请求发送 `X-Api-Auth-Token`，和原有的 `ApiKey` / `X-Auth-Token` 并存。
- 队伍页的提示从「在 F12 里从 localStorage 复制 token」改为账号菜单的操作步骤，并配上三张示意图（key 已打码），五种语言均已翻译。
- 专用 key 只显示一次、不过期；重新发行会让旧的失效。

#### 改进

- **默认语言改为简体中文**：之前首次打开是英文，现在界面直接是简体中文；词条缺失时仍回退到英语。
- **多语言补全**：`jaJP` 591 条、`koKR` 591 条、`zhTW` 593 条此前缺失，只能显示英文；现在五种语言都是完整的 1323 条。
- 修掉 7 个在游戏文本里根本不存在的 key（`[Loading]`、`[NoData]`、`[GuildDetailDialogTitle]`、`[GuildFameLabel]`、`[GuildTowerMaxFloorLabel]`、`[GuildDescriptionLabel]`、`[CommonReloadButtonLabel]`），之前会把这些原始 key 直接显示在界面上。
- 语言菜单改为按支持列表生成，顺序为 简体中文 / 繁體中文 / English / 日本語 / 한국어。

#### 修复

- **切换语言后界面立即更新**，不再需要刷新页面：`t` 在语言或词典变化时换新引用，所有订阅它的组件和依赖 `t` 的 memo/effect 自动重算；同时修掉快速连续切换时，先发出的旧语言请求晚到后覆盖新语言的问题。
- **部分语言显示空白**：游戏文本里有些语言（`zhCN` 430 条、`zhTW` 674 条、`koKR` 696 条、`jaJP` 82 条，其余语言上千条）是空字符串，界面会出现空白；接口现在用英语补齐缺失或为空的条目，前端 `t()` 也不再命中空串。
- **本地化接口性能**：不再每次请求都重新读取并反序列化 3 万条文本；缓存以 Master 文件的修改时间为准，Master 数据更新后自动重读，不需要重启。

#### 其他

- 新增 `scripts/locale-tools.mjs`（`pnpm locale:check` / `pnpm locale:missing`）：检查各语言覆盖率、列出缺失 key、导入导出待翻译 JSON，并可与游戏 Master 数据交叉核对。

### English

#### New

**Temple of Illusions (Settings → Social)**

- "Host a room and run it solo when no room is found" is now a switch, **off by default**: alts keep waiting to join your main's room like before 2.2, and only host when you turn it on.
- "Keep looking for a room for (minutes)" is now configurable, default 10.
- "Leave a room that doesn't start after (minutes)" is now configurable, default 3, so an account no longer sits in a dead room.
- "Stop after this many losses in a row" is now configurable, default 3, `0` = never.
- "Stop the whole task after (minutes)" is configurable, default 20, so no account gets stuck.
- "Try a lower quest instead of stopping" switch, plus "minutes on one quest before trying a lower one" (default 3); both event and normal quests are considered when dropping.
- "Quest level" can be pinned, default `0` = pick automatically by rewards; useful for low-level alts. If that level does not exist, the closest lower one is used.

**Tamamo**

- Support the per-account dedicated key (account menu → Issue Dedicated Key → Issue / Reissue → Copy), sent as `X-Api-Auth-Token` alongside the existing `ApiKey` / `X-Auth-Token` headers.
- The team page no longer tells you to dig the token out of localStorage in F12; it now shows the account-menu steps with three screenshots (key blacked out), translated into all five languages.
- The dedicated key is shown only once and never expires; reissuing cancels the old one.

#### Improvements

- **Simplified Chinese is now the default**: the UI used to start in English; it now starts in Simplified Chinese, with English still used when an entry is missing.
- **Completed the translations**: 591 `jaJP`, 591 `koKR` and 593 `zhTW` strings were missing and fell back to English; all five languages now define the full 1323 keys.
- Fixed 7 keys that do not exist in the game data and were rendered as raw text (`[Loading]`, `[NoData]`, `[GuildDetailDialogTitle]`, `[GuildFameLabel]`, `[GuildTowerMaxFloorLabel]`, `[GuildDescriptionLabel]`, `[CommonReloadButtonLabel]`).
- The language menu is generated from the supported list: Simplified Chinese / Traditional Chinese / English / Japanese / Korean.

#### Fixes

- **Switching the language updates the UI immediately**, with no reload: `t` gets a new identity whenever the language or the dictionary changes, so subscribers and `t`-based memo/effect dependencies recompute. Also fixed a fast switch being overwritten by a late response for the previous language.
- **Blank labels in some languages**: a number of master entries are empty strings (`zhCN` 430, `zhTW` 674, `koKR` 696, `jaJP` 82, thousands in the other languages), which rendered as blank text. The API now fills missing or empty entries with English, and `t()` no longer returns an empty string.
- **Localization endpoint performance**: no longer re-reads and deserializes 30k rows on every request. The cache is keyed on the master file's timestamp, so updated master data is picked up without a restart.

#### Other

- Added `scripts/locale-tools.mjs` (`pnpm locale:check` / `pnpm locale:missing`): coverage report, missing-key listing, pending-translation import/export, and cross-checks against the game master data.


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
