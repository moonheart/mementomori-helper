using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.Gacha;
using MementoMori.Ortega.Share.Data.Gacha;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 免费扭蛋处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class FreeGachaHandler : IGameActionHandler
{
    private readonly ILogger<FreeGachaHandler> _logger;
    private readonly JobLogger _jobLogger;
    private readonly PlayerSettingService _playerSettingService;

    public string ActionName => "免费扭蛋";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, "正在检查免费扭蛋...");

        try
        {
            var autoJobSettings = await _playerSettingService.GetAutoJobSettingsAsync(userId);
            if (!autoJobSettings.AutoFreeGacha)
            {
                await _jobLogger.LogAsync(userId, "免费扭蛋未开启，跳过。");
                return;
            }

            var gachaSettings = await _playerSettingService.GetGachaSettingsAsync(userId);
            var autoGachaConsumeItems = gachaSettings.AutoGachaConsumeUserItems ?? new List<MementoMori.Ortega.Share.Data.Item.UserItem>();

            var ignoredButtonIds = new HashSet<long>();
            var drawnCount = 0;
            var maxIterations = 20;

            for (var i = 0; i < maxIterations; i++)
            {
                var drawn = await DoFreeGacha();
                if (!drawn) break;
                drawnCount++;
            }

            if (drawnCount > 0)
            {
                await _jobLogger.LogAsync(userId, $"免费扭蛋完成，抽取 {drawnCount} 次。");
            }
            else
            {
                await _jobLogger.LogAsync(userId, "暂无可用的免费扭蛋。");
            }

            async Task<bool> DoFreeGacha()
            {
                var gachaListResponse = await nm.SendRequest<GetListRequest, GetListResponse>(new GetListRequest(), false);

                foreach (var gachaCaseInfo in gachaListResponse.GachaCaseInfoList)
                {
                    var userItems = nm.UserSyncData.UserItemDtoInfo.ToList();

                    // 查找可用的免费抽取按钮
                    var buttonInfo = gachaCaseInfo.GachaButtonInfoList
                        .OrderByDescending(d => d.LotteryCount)
                        .FirstOrDefault(d =>
                            d.ConsumeUserItem == null ||
                            d.ConsumeUserItem.ItemCount == 0 ||
                            CheckConsumeItem(d, userItems, autoGachaConsumeItems));

                    if (buttonInfo == null) continue;
                    if (ignoredButtonIds.Contains(buttonInfo.GachaButtonId)) continue;

                    try
                    {
                        await nm.SendRequest<DrawRequest, DrawResponse>(
                            new DrawRequest { GachaButtonId = buttonInfo.GachaButtonId }, false);
                        return true;
                    }
                    catch (NetworkManager.ApiErrorException ex) when (ex.Message.Contains("HaveMaxCharacter"))
                    {
                        ignoredButtonIds.Add(buttonInfo.GachaButtonId);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "扭蛋失败 for user {UserId}", userId);
                    }
                }

                return false;
            }

            bool CheckConsumeItem(GachaButtonInfo buttonInfo,
                List<MementoMori.Ortega.Share.Data.DtoInfo.UserItemDtoInfo> userItems,
                List<MementoMori.Ortega.Share.Data.Item.UserItem> consumeItems)
            {
                if (buttonInfo.ConsumeUserItem == null || buttonInfo.ConsumeUserItem.ItemCount == 0)
                    return true;

                foreach (var consumeItem in consumeItems)
                {
                    if (buttonInfo.ConsumeUserItem.ItemType == consumeItem.ItemType &&
                        buttonInfo.ConsumeUserItem.ItemId == consumeItem.ItemId)
                    {
                        var count = userItems.FirstOrDefault(d =>
                            d.ItemType == consumeItem.ItemType && d.ItemId == consumeItem.ItemId)?.ItemCount ?? 0;
                        return buttonInfo.ConsumeUserItem.ItemCount <= count;
                    }
                }

                return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "免费扭蛋失败 for user {UserId}", userId);
        }
    }
}
