using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Shop;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 月卡奖励领取处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class MonthlyBoostHandler : IGameActionHandler
{
    private readonly ILogger<MonthlyBoostHandler> _logger;
    private readonly JobLogger _jobLogger;

    public string ActionName => "领取月卡奖励";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, "正在检查月卡奖励...");

        try
        {
            var listResponse = await nm.SendRequest<GetListRequest, GetListResponse>(new GetListRequest(), false);
            var shopProductInfo = listResponse.ShopTabInfoList
                .SelectMany(d => d.ShopProductInfoList)
                .FirstOrDefault(d => d.ShopProductType == ShopProductType.MonthlyBoost);

            if (shopProductInfo != null &&
                shopProductInfo.ShopProductMonthlyBoost.ExpirationTimeStamp >= DateTimeOffset.Now.ToUnixTimeMilliseconds())
            {
                if (shopProductInfo.ShopProductMonthlyBoost.IsAlreadyReceive)
                {
                    await _jobLogger.LogAsync(userId, "月卡奖励今日已领取。");
                }
                else
                {
                    var receiveRewardResponse = await nm.SendRequest<ReceiveRewardRequest, ReceiveRewardResponse>(
                        new ReceiveRewardRequest
                        {
                            MBId = shopProductInfo.MbId,
                            ProductId = shopProductInfo.ShopProductMonthlyBoost.ProductId,
                            ShopProductType = ShopProductType.MonthlyBoost
                        }, false);
                    await _jobLogger.LogAsync(userId, "月卡奖励已领取。");
                }
            }
            else
            {
                await _jobLogger.LogAsync(userId, "月卡未激活或已过期。");
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "领取月卡奖励失败 for user {UserId}", userId);
        }
    }
}
