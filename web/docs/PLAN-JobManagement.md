# 定时任务管理系统设计方案 (PLAN-JobManagement)

本方案旨在实现前后端分离架构下的游戏账户自动化任务管理，支持账户级独立配置、时区感知调度以及实时执行监控。

## 1. 核心目标
- **账户级独立配置**：每个游戏账户可拥有独立的 Cron 表达式和功能开关。
- **时区感知调度**：根据游戏服务器时区偏移自动调整执行时间。
- **实时监控**：前端可查看任务下次运行时间，并通过 SignalR 接收实时执行日志。
- **手动控制**：支持前端立即触发特定任务。

## 2. 后端设计 (ASP.NET Core)

### 2.1 数据存储
- 使用现有的 `PlayerSettingService`，在 `PlayerSettingEntity` 中存储 key 为 `AutoJob` 的配置项。
- 配置模型 `AutoJobModel` 将包含各任务的 Cron 字符串及子项布尔开关。

### 2.2 任务调度引擎 (`JobManagerService`)
- 基于 **Quartz.NET** 实现。
- **动态注册**：
    - 当账户通过 API 登录或系统启动时，加载该账户配置并注册任务。
    - 任务 ID 命名规则：`job-{userId}-{jobType}`。
- **时区处理**：
    - 调用 `networkManager.TimeManager.DiffFromUtc` 获取偏移。
    - 使用 `TimeZoneInfo.CreateCustomTimeZone` 为 Quartz Trigger 设置专属时区。

### 2.3 API 接口 (`JobsController`)
| 接口 | 方法 | 描述 |
| :--- | :--- | :--- |
| `/api/jobs/{userId}/status` | GET | 返回该账户所有任务的详情（名称、Cron、上次/下次运行时间、状态）。 |
| `/api/jobs/{userId}/config` | GET | 获取账户的自动化配置。 |
| `/api/jobs/{userId}/config` | POST | 保存账户的自动化配置并重新注册任务。 |
| `/api/jobs/{userId}/trigger/{type}` | POST | 手动立即触发一个任务执行。 |

### 2.4 实时反馈 (SignalR)
- 创建 `JobHub`。
- 在 `IJob` 执行过程中，通过 `IHubContext` 向特定 `userId` 的客户端发送日志消息：
  `{ "time": "2024-01-25 10:00:00", "level": "Info", "message": "开始执行每日任务..." }`

## 3. 前端设计 (React + TypeScript)

### 3.1 任务仪表盘 (`MissionsPage` / `JobDashboard`)
- **状态卡片**：展示所有活跃任务的倒计时。
- **配置面板**：
    - 使用 `Switch` 控制子项功能（如：自动购买、自动 PVP）。
    - 提供 Cron 表达式输入框，并附带简易说明或预设选项。
- **控制台**：一个实时滚动的日志区域，展示 SignalR 推送的后端执行详情。

### 3.2 交互逻辑
- 用户修改配置并点击保存 -> 调用 POST API -> 后端更新数据库并调用 Quartz `RescheduleJob`。
- 点击“立即执行” -> 调用触发 API -> 后端立即运行 Job。

## 4. 实施阶段

### 第一阶段：后端基础构建
- [ ] 引入 `Quartz.NET` NuGet 包。
- [ ] 移植旧版 Jobs 逻辑 (`DailyJob`, `HourlyJob` 等) 到新项目。
- [ ] 实现 `JobManagerService` 核心逻辑。

### 第二阶段：数据与 API
- [ ] 完善 `AutoJobModel` 数据结构。
- [ ] 实现 `JobsController` 接口。
- [ ] 确保配置持久化到 SQLite。

### 第三阶段：前端集成
- [ ] 生成 TypeScript 类型定义。
- [ ] 开发任务状态列表组件。
- [ ] 开发配置编辑表单。

### 第四阶段：实时化增强
- [ ] 后端集成 SignalR 日志推送。
- [ ] 前端实现日志控制台组件。

---
*注：本计划基于原 Blazor 版本逻辑演进，优先保证功能对齐并提升账户独立性。*
