# API 版本多账户管理

## 概述

本文档描述 MementoMori API 版本的多账户管理系统架构。与 Blazor 版本不同，API 版本采用前后端分离架构，通过 RESTful API 提供账户管理功能。

## 架构设计

### 技术栈

**后端 (ASP.NET Core Web API)**
- .NET 10.0
- RESTful API
- MessagePack 序列化
- TypeGen (自动生成 TypeScript 类型)
- SignalR (实时通信)

**前端 (React)**
- React 19
- TypeScript
- Zustand (状态管理)
- React Router (路由)
- Axios (HTTP 客户端)

### 核心组件

#### 1. 基础设施层 (Infrastructure)

**MeMoriHttpClientHandler**
- 管理 Ortega 协议的特殊 HTTP Headers
- 自动处理 Access Token 更新
- 处理 Master/Asset 版本信息

```csharp
// 位置: api/MementoMori.Api/Infrastructure/MeMoriHttpClientHandler.cs
public class MeMoriHttpClientHandler : HttpClientHandler
{
    public string OrtegaAccessToken { get; private set; }
    public string OrtegaMasterVersion { get; private set; }
    public string OrtegaAssetVersion { get; private set; }
    public string AppVersion { get; set; }
}
```

**NetworkManager**
- 统一的网络请求管理
- MessagePack 序列化/反序列化
- API 错误处理

```csharp
// 位置: api/MementoMori.Api/Infrastructure/NetworkManager.cs
public class NetworkManager
{
    public async Task<TResp> SendRequest<TReq, TResp>(TReq request, bool useAuthApi = true)
    public void SetGameApiUrl(string url)
}
```

#### 2. 服务层 (Services)

**AccountService**
- 账号的 CRUD 操作
- 账号信息本地存储 (accounts.json)
- 登录流程协调

```csharp
// 位置: api/MementoMori.Api/Services/AccountService.cs
public class AccountService
{
    public List<AccountDto> GetAllAccounts()
    public AccountDto AddAccount(long userId, string clientKey, string name)
    public void DeleteAccount(long userId)
    public async Task<Models.LoginResponse> LoginAsync(long userId, string clientKey)
}
```

**AuthService**
- 与游戏服务器的认证通信
- 使用 Ortega API 进行登录
- 获取玩家世界列表

```csharp
// 位置: api/MementoMori.Api/Services/AuthService.cs
public class AuthService
{
    public async Task<List<PlayerDataInfo>> GetPlayerDataAsync(long userId)
}
```

#### 3. 控制器层 (Controllers)

**AuthController**
- RESTful API 端点
- DTO 转换
- 请求验证

```csharp
// 位置: api/MementoMori.Api/Controllers/AuthController.cs
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet("accounts")]
    [HttpPost("accounts")]
    [HttpDelete("accounts/{userId}")]
    [HttpPost("login")]
}
```

#### 4. 数据模型 (Models)

**AccountDto** - 账号信息
```csharp
[ExportTsClass]
public class AccountDto
{
    public long UserId { get; set; }
    public string Name { get; set; }
    public string ClientKey { get; set; }
}
```

**LoginResponse** - 登录响应
```csharp
[ExportTsClass]
public class LoginResponse
{
    public bool Success { get; set; }
    public long UserId { get; set; }
    public string PlayerName { get; set; }
    public string Message { get; set; }
}
```

### 前端架构

#### 状态管理 (Zustand)

```typescript
// 位置: src/store/authStore.ts
interface AuthState {
  isAuthenticated: boolean;
  currentAccount: number | null;
  setCurrentAccount: (userId: number) => void;
  logout: () => void;
}
```

#### API 客户端

```typescript
// 位置: src/api/auth-service.ts
export const authApi = {
  getAccounts: () => apiClient.get<AccountDto[]>('/api/auth/accounts'),
  addAccount: (request: AddAccountRequest) => apiClient.post<AccountDto>('/api/auth/accounts', request),
  deleteAccount: (userId: number) => apiClient.delete(`/api/auth/accounts/${userId}`),
  login: (request: LoginRequest) => apiClient.post<LoginResponse>('/api/auth/login', request),
};
```

## 数据流程

### 账号管理流程

```
┌─────────────┐     HTTP Request      ┌──────────────┐
│   React    │ ──────────────────────> │AuthController│
│  Frontend  │                         └──────┬───────┘
└─────────────┘                                │
      ▲                                        ▼
      │                              ┌─────────────────┐
      │                              │ AccountService  │
      │                              └────────┬────────┘
      │                                       │
      │                                       ▼
      │       JSON Response        ┌──────────────────┐
      └────────────────────────────│  accounts.json   │
                                   └──────────────────┘
```

### 登录流程

```
┌─────────┐  1. Login Request     ┌──────────────┐
│ Frontend│ ───────────────────> │AuthController│
└─────────┘                       └──────┬───────┘
                                          │ 2. LoginAsync
                                          ▼
                                 ┌────────────────┐
                                 │ AccountService │
                                 └───────┬────────┘
                                          │ 3. GetPlayerData
                                          ▼
                                  ┌──────────────┐
                                  │ AuthService  │
                                  └──────┬───────┘
                                          │ 4. SendRequest
                                          ▼
                                 ┌─────────────────┐
                                 │ NetworkManager  │
                                 └────────┬────────┘
                                          │ 5. HTTP + MessagePack
                                          ▼
                                 ┌─────────────────┐
                                 │  Ortega 游戏服务器 │
                                 └─────────────────┘
```

## API 端点设计

### 账号管理

| 方法 | 路径 | 描述 |
|------|------|------|
| GET | `/api/auth/accounts` | 获取所有账号列表 |
| POST | `/api/auth/accounts` | 添加新账号 |
| DELETE | `/api/auth/accounts/{userId}` | 删除指定账号 |
| POST | `/api/auth/login` | 登录到游戏 |

### 请求/响应示例

**添加账号**
```http
POST /api/auth/accounts
Content-Type: application/json

{
  "userId": 123456,
  "clientKey": "your-client-key",
  "name": "我的账号"
}
```

**登录**
```http
POST /api/auth/login
Content-Type: application/json

{
  "userId": 123456,
  "clientKey": "your-client-key"
}
```

**响应**
```json
{
  "success": true,
  "userId": 123456,
  "playerName": "玩家昵称",
  "message": "Logged in to World 1001"
}
```

## 配置管理

### 后端配置

账号信息存储在 `accounts.json`:
```json
[
  {
    "userId": 123456,
    "clientKey": "your-client-key",
    "name": "我的账号"
  }
]
```

### 前端配置

环境变量 `.env`:
```env
VITE_API_URL=http://localhost:5000
```

## 类型安全

使用 TypeGen 自动生成 TypeScript 类型：

```bash
# 生成类型
pnpm run generate-types

# 生成的文件位于
src/api/generated/
├── accountDto.ts
├── loginRequest.ts
├── loginResponse.ts
└── index.ts
```

## 与 Blazor 版本的主要区别

| 特性 | Blazor 版本 | API 版本 |
|------|------------|----------|
| 架构 | 单体应用 | 前后端分离 |
| 状态管理 | Blazor 组件状态 | Zustand |
| 数据存储 | IWritableOptions<AuthOption> | 文件 (accounts.json) |
| 实时通信 | 无 | SignalR (可选) |
| 类型共享 | C# 共享 | TypeGen 生成 |
| 依赖注入 | 复杂 (多层依赖) | 简化 (最小依赖) |

## 优势

1. **前后端分离**：前端和后端可独立开发和部署
2. **类型安全**：自动生成 TypeScript 类型
3. **简洁架构**：避免了 Blazor 的复杂依赖注入
4. **现代化 UI**：React 生态系统丰富
5. **易于扩展**：RESTful API 便于添加新功能
6. **跨平台**：API 可被多种客户端使用

## 待实现功能

- [ ] 自动登录支持
- [ ] 世界选择功能
- [ ] 账号密码加密存储
- [ ] 批量账号管理
- [ ] SignalR 实时通知
- [ ] 其他游戏功能 API (Shop, Mission, Character 等)

## 开发指南

### 启动开发环境

**后端：**
```bash
cd api/MementoMori.Api
dotnet run --urls http://localhost:5000
```

**前端：**
```bash
pnpm dev
# 访问 http://localhost:5173
```

### 添加新的 API 端点

1. 在 `Models/` 创建 DTO 并添加 `[ExportTsClass]`
2. 在 `Services/` 创建业务逻辑
3. 在 `Controllers/` 创建 API 端点
4. 运行 `pnpm run generate-types` 生成前端类型
5. 在前端添加 API 调用

## 总结

API 版本的多账户管理系统采用现代化的前后端分离架构，比 Blazor 版本更加灵活和易于维护。通过 TypeGen 实现类型安全，通过简化的架构降低复杂度，为后续功能扩展奠定了良好基础。
