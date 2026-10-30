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

    public string ActionName => Text.T("ActionMonthlyBoost");

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, Text.T("MonthlyBoostChecking"));

        try
        {
            var listResponse = await nm.SendRequest<GetListRequest, GetListResponse>(new GetListRequest());
            var shopProductInfo = listResponse.ShopTabInfoList
                .SelectMany(d => d.ShopProductInfoList)
                .FirstOrDefault(d => d.ShopProductType == ShopProductType.MonthlyBoost);

            if (shopProductInfo != null &&
                shopProductInfo.ShopProductMonthlyBoost.ExpirationTimeStamp >= DateTimeOffset.Now.ToUnixTimeMilliseconds())
            {
                if (shopProductInfo.ShopProductMonthlyBoost.IsAlreadyReceive)
                {
                    await _jobLogger.LogAsync(userId, Text.T("MonthlyBoostAlreadyClaimed"));
                }
                else
                {
                    var receiveRewardResponse = await nm.SendRequest<ReceiveRewardRequest, ReceiveRewardResponse>(
                        new ReceiveRewardRequest
                        {
                            MBId = shopProductInfo.MbId,
                            ProductId = shopProductInfo.ShopProductMonthlyBoost.ProductId,
                            ShopProductType = ShopProductType.MonthlyBoost
                        });
                    await _jobLogger.LogAsync(userId, Text.T("MonthlyBoostClaimed"));
                }
            }
            else
            {
                await _jobLogger.LogAsync(userId, Text.T("MonthlyBoostInactive"));
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "领取月卡奖励失败 for user {UserId}", userId);
        }
    }
}
