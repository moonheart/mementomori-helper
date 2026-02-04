using MementoMori.Api.Extensions;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.Item;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 自动使用道具处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class AutoUseItemsHandler : IGameActionHandler
{
    private readonly ILogger<AutoUseItemsHandler> _logger;
    private readonly JobLogger _jobLogger;
    private readonly PlayerSettingService _playerSettingService;

    public string ActionName => "自动使用道具";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, "正在检查自动使用道具...");

        try
        {
            var autoJobSettings = await _playerSettingService.GetAutoJobSettingsAsync(userId);
            if (!autoJobSettings.AutoUseItems)
            {
                await _jobLogger.LogAsync(userId, "自动使用道具未开启，跳过。");
                return;
            }

            var itemSettings = await _playerSettingService.GetItemSettingsAsync(userId);
            var autoUseItemTypes = itemSettings.AutoUseItemTypes;

            if (autoUseItemTypes.Count == 0)
            {
                await _jobLogger.LogAsync(userId, "未配置自动使用道具类型。");
                return;
            }

            var openedCount = 0;
            var maxIterations = 20; // 防止无限循环
            var iterations = 0;

            while (iterations < maxIterations)
            {
                iterations++;
                var successOpen = false;

                foreach (var userItemDtoInfo in nm.UserSyncData.UserItemDtoInfo
                    .Where(d => d.ItemCount > 0 && d.ItemType == ItemType.TreasureChest)
                    .ToList())
                {
                    var treasureChestMb = Masters.TreasureChestTable.GetById(userItemDtoInfo.ItemId);
                    if (treasureChestMb == null) continue;

                    if (treasureChestMb.TreasureChestLotteryType != TreasureChestLotteryType.Fix &&
                        treasureChestMb.TreasureChestLotteryType != TreasureChestLotteryType.Random)
                        continue;

                    if (userItemDtoInfo.ItemCount < treasureChestMb.MinOpenCount)
                        continue;

                    long openCount;
                    if (treasureChestMb.ChestKeyItemId > 0)
                    {
                        var keyCount = nm.UserSyncData.GetUserItemCount(ItemType.TreasureChestKey, treasureChestMb.ChestKeyItemId);
                        var available = Math.Min(userItemDtoInfo.ItemCount, keyCount);
                        if (available <= 0) continue;
                        openCount = available / treasureChestMb.MinOpenCount * treasureChestMb.MinOpenCount;
                    }
                    else
                    {
                        openCount = userItemDtoInfo.ItemCount / treasureChestMb.MinOpenCount * treasureChestMb.MinOpenCount;
                    }

                    if (openCount <= 0) continue;

                    try
                    {
                        await nm.SendRequest<OpenTreasureChestRequest, OpenTreasureChestResponse>(
                            new OpenTreasureChestRequest
                            {
                                OpenCount = (int) openCount,
                                TreasureChestId = treasureChestMb.Id
                            }, false);
                        openedCount++;
                        successOpen = true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "打开宝箱失败 {ChestId} for user {UserId}", treasureChestMb.Id, userId);
                    }
                }

                if (!successOpen) break;
            }

            if (openedCount > 0)
            {
                await _jobLogger.LogAsync(userId, $"已打开 {openedCount} 种宝箱。");
            }
            else
            {
                await _jobLogger.LogAsync(userId, "暂无可使用的道具。");
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "自动使用道具失败 for user {UserId}", userId);
        }
    }
}
