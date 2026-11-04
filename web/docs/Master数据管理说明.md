# 游戏 Master 数据管理说明

本文档说明了 MementoMori 游戏中 Master 数据（静态配置数据）的加载、同步与管理机制。

## 核心组件

### 1. Masters 静态类
[`api/MementoMori.Ortega/Share/Masters.cs`](api/MementoMori.Ortega/Share/Masters.cs) 是全局访问点。它包含了游戏中所有配置表的单例引用。

- **职责**: 提供对各个 `Table` 实例的统一访问。
- **示例**: `Masters.CharacterTable`, `Masters.TextResourceTable`。

### 2. Table 系统
每个配置表都遵循相同的架构：
- **接口**: [`ITable`](api/MementoMori.Ortega/Share/Master/Interfaces/ITable.cs) 定义了 `Load()` 和 `GetMasterBookName()` 等基本操作。
- **基类**: [`TableBase<TM>`](api/MementoMori.Ortega/Share/Master/Table/TableBase.cs) 实现了通用的加载逻辑。
- **实现类**: 如 [`CharacterTable`](api/MementoMori.Ortega/Share/Master/Table/CharacterTable.cs)，负责提供特定表的业务查询方法。

### 3. MasterBook 数据模型 (MB)
数据模型是与二进制文件一一对应的 C# 类。
- **命名约定**: 以 `MB` 结尾，如 `CharacterMB`。
- **序列化**: 使用 `MessagePack` 协议进行二进制序列化/反序列化。
- **特性**: 类上通常带有 `[MessagePackObject(true)]` 特性。

## 数据加载流程

1.  **文件定位**: `TableBase` 会根据模型名称在本地 `Master/` 目录下查找文件（例如 `Master/CharacterMB`）。
2.  **反序列化**: 使用 `MessagePackSerializer.Deserialize<TM[]>(fileStream)` 将二进制内容直接转为内存中的对象数组。
3.  **内存驻留**: 数据一旦加载，就常驻在各个 Table 实例的 `_datas` 字段中，提供高性能查询。

## 数据同步机制 (API 层)

由于游戏数据会随版本更新，后端 API 实现了自动同步机制：

### MasterDataService
位于 [`api/MementoMori.Api/Services/MasterDataService.cs`](api/MementoMori.Api/Services/MasterDataService.cs)，这是一个后台服务。

1.  **版本检查**: 通过 `VersionService` 获取最新的游戏 `MasterVersion`。
2.  **目录下载**: 下载 `master-catalog` 文件，其中包含所有 MB 文件的 MD5 哈希值。
3.  **增量更新**:
    - 遍历本地 `Master/` 目录下的文件。
    - 比对 MD5 哈希值。
    - 如果不一致或不存在，则从游戏官方 CDN 下载最新的二进制文件。
4.  **初始化**: 在 API 启动时（`Program.cs`），会首先执行同步任务，确保本地数据是最新的。

## 业务使用示例

在代码中获取配置数据通常如下：

```csharp
// 获取指定 ID 的角色配置
var character = Masters.CharacterTable.GetById(characterId);

// 获取本地化文本
var name = Masters.TextResourceTable.Get(character.NameKey);
```

## 注意事项
- **语言包**: `TextResource` 系列文件包含多国语言。同步服务目前配置为仅同步 `JaJp`, `ZhTw`, `ZhCn`, `EnUs`, `KoKr`。
- **文件路径**: 在开发环境下，Master 数据通常存储在 `api/MementoMori.Api/Master/` 目录下。
