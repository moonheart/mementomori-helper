using MementoMori.Api.Services;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 游戏动作处理器接口
/// </summary>
public interface IGameActionHandler
{
    /// <summary>
    /// 动作显示名称
    /// </summary>
    string ActionName { get; }

    /// <summary>
    /// 执行动作逻辑
    /// </summary>
    /// <param name="context">账户上下文，包含 NetworkManager</param>
    Task ExecuteAsync(AccountContext context);
}
