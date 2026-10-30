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

    public string ActionName => Text.T("ActionVipDailyGift");

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, Text.T("VipChecking"));

        if (nm.UserSyncData.ExistVipDailyGift == true)
        {
            try
            {
                var bonus = await nm.SendRequest<GetDailyGiftRequest, GetDailyGiftResponse>(new GetDailyGiftRequest());
                await _jobLogger.LogAsync(userId, Text.T("VipGiftClaimed", nm.UserSyncData.UserStatusDtoInfo.Vip));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "领取VIP礼包失败 for user {UserId}", userId);
            }
        }
        else
        {
            await _jobLogger.LogAsync(userId, Text.T("VipGiftUnavailable"));
        }
    }
}
