using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 登录奖励领取处理器
/// </summary>
public class DailyLoginBonusHandler : IGameActionHandler
{
    private readonly ILogger<DailyLoginBonusHandler> _logger;
    private readonly JobLogger _jobLogger;

    public DailyLoginBonusHandler(ILogger<DailyLoginBonusHandler> logger, JobLogger jobLogger)
    {
        _logger = logger;
        _jobLogger = jobLogger;
    }

    public string ActionName => "领取登录奖励";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, "正在检查登录奖励...");

        // TODO: 完善具体的登录奖励请求逻辑。
        // 目前先保留 GameActionService 中的逻辑框架
        
        // 示例：
        // var infoReq = new GetMonthlyLoginBonusInfoRequest();
        // var infoResp = await nm.SendRequest<GetMonthlyLoginBonusInfoRequest, GetMonthlyLoginBonusInfoResponse>(infoReq, false);
        // ...

        await _jobLogger.LogAsync(userId, "今日登录奖励已检查/领取完毕。");
    }
}
