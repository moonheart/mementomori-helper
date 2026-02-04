using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Present;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 礼物箱领取处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class PresentReceiveHandler : IGameActionHandler
{
    private readonly ILogger<PresentReceiveHandler> _logger;
    private readonly JobLogger _jobLogger;

    public string ActionName => "领取礼物箱";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, "正在检查礼物箱...");

        try
        {
            var getListResponse = await nm.SendRequest<GetListRequest, GetListResponse>(
                new GetListRequest { LanguageType = nm.LanguageType }, false);

            if (getListResponse.userPresentDtoInfos.Any(d => !d.IsReceived))
            {
                try
                {
                    var resp = await nm.SendRequest<ReceiveItemRequest, ReceiveItemResponse>(
                        new ReceiveItemRequest { LanguageType = nm.LanguageType }, false);
                    var count = resp.ResultItems?.Count ?? 0;
                    await _jobLogger.LogAsync(userId, $"礼物箱已领取 {count} 件物品。");
                }
                catch (Exception ex) when (ex.Message.Contains("OverLimitCount"))
                {
                    // 超出限制时逐个领取
                    var receivedCount = 0;
                    foreach (var present in getListResponse.userPresentDtoInfos.Where(d => !d.IsReceived))
                    {
                        try
                        {
                            await nm.SendRequest<ReceiveItemRequest, ReceiveItemResponse>(
                                new ReceiveItemRequest { LanguageType = nm.LanguageType, PresentGuid = present.Guid }, false);
                            receivedCount++;
                        }
                        catch
                        {
                            // 忽略单个失败
                        }
                    }
                    await _jobLogger.LogAsync(userId, $"礼物箱逐个领取，成功 {receivedCount} 件。");
                }
            }
            else
            {
                await _jobLogger.LogAsync(userId, "礼物箱已清空。");
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "领取礼物箱失败 for user {UserId}", userId);
        }
    }
}
