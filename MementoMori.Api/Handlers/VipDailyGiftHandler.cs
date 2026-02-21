using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Vip;

namespace MementoMori.Api.Handlers;

/// <summary>
/// VIP每日礼包领取处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class VipDailyGiftHandler : IGameActionHandler
{
    private readonly ILogger<VipDailyGiftHandler> _logger;
    private readonly JobLogger _jobLogger;

    public string ActionName => "领取VIP每日礼包";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, "正在检查VIP每日礼包...");

        if (nm.UserSyncData.ExistVipDailyGift == true)
        {
            try
            {
                var bonus = await nm.SendRequest<GetDailyGiftRequest, GetDailyGiftResponse>(new GetDailyGiftRequest());
                await _jobLogger.LogAsync(userId, $"VIP{nm.UserSyncData.UserStatusDtoInfo.Vip}每日礼包已领取。");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "领取VIP礼包失败 for user {UserId}", userId);
            }
        }
        else
        {
            await _jobLogger.LogAsync(userId, "VIP每日礼包已领取或不可用。");
        }
    }
}
