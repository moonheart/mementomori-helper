# 自动注入重构计划 - MementoMori.Api

本计划旨在通过 `Injectio` 和 `AutoConstructor` 两个库，简化 `MementoMori.Api` 项目中的依赖注入（DI）和构造函数样板代码。

## 目标

1.  **自动构造函数生成**: 使用 `AutoConstructor` 自动生成包含所有 `private readonly` 字段的构造函数。
2.  **自动服务注册**: 使用 `Injectio` 通过特性（Attribute）标记实现服务的自动发现与注册，减少 `Program.cs` 的维护工作。

## 涉及范围

- **Controllers**: 所有 Web API 控制器。
- **Services**: 业务逻辑服务。
- **Handlers**: 游戏动作处理器。
- **Infrastructure**: 网络管理、日志等基础设施。

## 实施步骤

### 1. 环境准备
- 检查 `MementoMori.Api.csproj` 是否包含：
  - `Injectio`
  - `AutoConstructor`

### 2. 类改造模板

对于每一个需要注入的类，将按照以下模板进行重构：

```csharp
[RegisterScoped] // 根据需要使用 [RegisterSingleton] 或 [RegisterTransient]
[AutoConstructor]
public partial class ExampleService
{
    private readonly ILogger<ExampleService> _logger;
    private readonly OtherService _otherService;

    // 手动编写的构造函数将被删除，AutoConstructor 会自动生成它
}
```

### 3. 具体改造清单

#### 服务层 (Services)
- `AccountService`: Scoped
- `AccountManager`: Singleton
- `AccountCredentialService`: Scoped
- `GameActionService`: Singleton
- `GameNetworkService`: Scoped
- `JobManagerService`: Singleton
- `LocalizationService`: Scoped
- `MasterDataService`: Singleton (需保留 HostedService 注册)
- `MissionService`: Scoped
- `OrtegaApiDiscoveryService`: Singleton
- `VersionService`: Singleton

#### 处理器 (Handlers)
- `ActionExecutor`: Singleton
- `ArenaPvpHandler`: Transient
- `DailyLoginBonusHandler`: Transient
- `ShopAutoBuyHandler`: Transient

#### 基础设施 (Infrastructure)
- `NetworkManager`: Singleton
- `OrtegaInvoker`: Scoped
- `JobLogger`: Singleton

#### 控制器 (Controllers)
- 所有的 `Controller` 将应用 `[AutoConstructor]`。由于 ASP.NET Core 默认发现控制器，通常不需要 `Injectio` 注册，但需确保它们是 `partial`。

### 4. Program.cs 更新

- 移除 `// Register infrastructure services` 之后的大部分手动 `AddScoped/Singleton/Transient` 调用。
- 在 `builder.Services.AddControllers();` 附近添加：
  ```csharp
  builder.Services.AddMementoMoriApi(); // 名称由程序集决定
  ```

## 风险评估
- **循环依赖**: 自动注入可能更容易隐藏循环依赖问题。
- **HostedService**: `MasterDataService` 需要特殊处理，既要作为单例注入，又要作为 `HostedService` 运行。

## 验证方法
- 编译项目，确保无编译错误。
- 启动 API，访问 Swagger 页面，测试基本接口（如 `/api/Auth/accounts`）确保注入正常。
