using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 竞技场 (Arena) 自动战斗处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class ArenaPvpHandler : IGameActionHandler
{
    private readonly ILogger<ArenaPvpHandler> _logger;
    private readonly JobLogger _jobLogger;
    private readonly IServiceProvider _serviceProvider;

    public string ActionName => "竞技场自动战斗";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, "正在检查竞技场挑战次数...");

        // TODO: 实现具体的竞技场逻辑
        // 1. 获取竞技场信息 (BattleLeague/GetInfo)
        // 2. 检查剩余次数
        // 3. 选择对手
        // 4. 执行战斗

        await _jobLogger.LogAsync(userId, "竞技场挑战检查完毕。");
    }
}
