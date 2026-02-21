using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;
using MementoMori.Api.Services;
using MementoMori.Api.Utils;
using MementoMori.Ortega.Share.Data.ApiInterface.BookSort;
using MementoMori.Ortega.Share.Data.ApiInterface.Mission;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;
using System.Linq;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 书库大扫除自动清扫处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class BookSortAutoHandler : IGameActionHandler
{
    private readonly ILogger<BookSortAutoHandler> _logger;
    private readonly JobLogger _jobLogger;

    public string ActionName => "书库大扫除自动清扫";

    // 网格常量
    private const int GridWidth = 7;
    private const int GridHeight = 5;
    private const int TotalCells = GridWidth * GridHeight;

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, "开始书库大扫除自动清扫...");

        // 获取用户进度
        var userSyncData = nm.UserSyncData;
        var maxClearQuestId = userSyncData?.UserBattleBossDtoInfo?.BossClearMaxQuestId ?? 0;

        int maxIterations = 50; // 防止死循环
        int iterations = 0;

        while (iterations < maxIterations)
        {
            iterations++;

            try
            {
                // 获取当前状态
                var infoRequest = new BookSortGetInfoRequest();
                var infoResponse = await nm.SendRequest<BookSortGetInfoRequest, BookSortGetInfoResponse>(infoRequest, false);

                var syncData = infoResponse.BookSortSyncData;
                if (syncData == null)
                {
                    await _jobLogger.LogAsync(userId, "获取书库大扫除数据失败。");
                    break;
                }

                // 1. 处理奖励层
                bool isBonusFloor = syncData.CurrentFloor % 5 == 0;
                if (isBonusFloor && syncData.SelectedBonusFloorRewardIndex == -1)
                {
                    // 获取当前层对应的奖励配置
                    var selectItemsList = Masters.BookSortBonusFloorRewardTable.GetSelectItemsListByFloor(syncData.CurrentFloor);

                    if (selectItemsList != null && selectItemsList.Count > 0)
                    {
                        // 寻找符合条件的最高奖励索引（即列表最靠后的满足 maxClearQuestId 的奖励）
                        int bestIndex = -1;
                        for (int i = 0; i < selectItemsList.Count; i++)
                        {
                            var selectItem = selectItemsList[i];
                            if (selectItem.StartMaxClearQuestId <= maxClearQuestId && selectItem.EndMaxClearQuestId >= maxClearQuestId)
                            {
                                bestIndex = i;
                            }
                        }

                        if (bestIndex != -1)
                        {
                            await _jobLogger.LogAsync(userId, $"到达奖励层 (第 {syncData.CurrentFloor} 层)，自动选择奖励索引: {bestIndex}");
                            var selectReq = new BookSortSelectBonusFloorRewardRequest { BonusFloorRewardIndex = bestIndex };
                            await nm.SendRequest<BookSortSelectBonusFloorRewardRequest, BookSortSelectBonusFloorRewardResponse>(selectReq, false);

                            // 选完奖励进入下一循环重新获取数据
                            continue;
                        }
                    }
                }

                // 2. 检查是否满足进入下一层的条件
                if (syncData.IsUnlockedWinAndKeyGridCell())
                {
                    await _jobLogger.LogAsync(userId, $"第 {syncData.CurrentFloor} 层已找到钥匙和被锁奖励，自动进入下一层...");
                    await nm.SendRequest<BookSortUpFloorRequest, BookSortUpFloorResponse>(new BookSortUpFloorRequest(), false);
                    continue;
                }

                // 获取已解锁的格子
                var unlockedIndexes = syncData.UnlockedGridCellInfoList?.Select(x => x.GridCellIndex).ToHashSet() ?? new HashSet<int>();
                if (unlockedIndexes.Count >= TotalCells)
                {
                    await _jobLogger.LogAsync(userId, $"第 {syncData.CurrentFloor} 层已全部清扫完毕，自动进入下一层...");
                    await nm.SendRequest<BookSortUpFloorRequest, BookSortUpFloorResponse>(new BookSortUpFloorRequest(), false);
                    continue;
                }

                // 3. 执行清扫
                // 筛选背包中属于 BookSortGridCellUnlockItem 的道具
                var unlockItemsList = nm.UserSyncData.UserItemDtoInfo
                    .Where(x => x.ItemType == ItemType.BookSortGridCellUnlockItem && x.ItemCount > 0)
                    .ToList();

                if (!unlockItemsList.Any())
                {
                    // 没有扫除道具，仍不退出，让后面的逻辑继续执行去尝试领取任务奖励
                }

                bool acted = false;

                // 大道具精确打击尝试
                var largeItems = unlockItemsList.Where(x => x.ItemId > 1).OrderByDescending(x => GetItemAreaShape(x.ItemId).Count).ToList();

                if (largeItems.Any())
                {
                    // 寻找最优解
                    int bestItemId = -1;
                    int bestGridIndex = -1;
                    int maxYield = 0;
                    int minWasteArea = int.MaxValue;

                    foreach (var uItem in largeItems)
                    {
                        var shapeOffsets = GetItemAreaShape(uItem.ItemId);
                        int itemArea = shapeOffsets.Count;

                        // 遍历所有可能的中心点 (0 到 34)
                        for (int c = 0; c < TotalCells; c++)
                        {
                            int centerCol = c % GridWidth;
                            int centerRow = c / GridWidth;
                            int yield = 0;

                            foreach (var (cOff, rOff) in shapeOffsets)
                            {
                                int col = centerCol + cOff;
                                int row = centerRow + rOff;

                                // 越界检查
                                if (col >= 0 && col < GridWidth && row >= 0 && row < GridHeight)
                                {
                                    int index = row * GridWidth + col;
                                    if (!unlockedIndexes.Contains(index))
                                    {
                                        yield++;
                                    }
                                }
                            }

                            // 以能够覆盖最多未解锁格子为首要目标。
                            // 当预期收益相同时，优先使用占用面积更小的大道具，避免浪费
                            if (yield > maxYield || (yield == maxYield && yield > 0 && itemArea < minWasteArea))
                            {
                                maxYield = yield;
                                bestItemId = (int) uItem.ItemId;
                                bestGridIndex = c;
                                minWasteArea = itemArea;
                            }
                        }
                    }

                    // 只有能扫到 >= 2 个格子，我们才认为使用大道具是划算的
                    if (maxYield >= 2)
                    {
                        await _jobLogger.LogAsync(userId, $"使用大道具 (ID:{bestItemId}) 瞄准格子 {bestGridIndex}，预期扫除 {maxYield} 格");
                        var unlockReq = new BookSortUnlockGridCellRequest
                        {
                            GridCellUnlockItemId = bestItemId,
                            GridCellIndex = bestGridIndex
                        };

                        await nm.SendRequest<BookSortUnlockGridCellRequest, BookSortUnlockGridCellResponse>(unlockReq, false);
                        acted = true;
                    }
                }

                // 大道具不够用/或者剩下零散格子时，使用普通掸子批量清扫
                if (!acted)
                {
                    var normalDuster = unlockItemsList.FirstOrDefault(x => x.ItemId == 1);
                    if (normalDuster != null && normalDuster.ItemCount > 0)
                    {
                        await _jobLogger.LogAsync(userId, $"使用普通掸子批量清扫...");
                        var bulkUnlockReq = new BookSortBulkUnlockGridCellRequest();
                        await nm.SendRequest<BookSortBulkUnlockGridCellRequest, BookSortBulkUnlockGridCellResponse>(bulkUnlockReq, false);
                        acted = true;
                    }
                }

                // 如果没做任何动作（比如没扫除道具了、或者没有 1 号道具且大道具只能开 1 格），则尝试领取任务奖励看能不能获得新道具
                if (!acted)
                {
                    bool missionClaimed = await TryClaimBookSortMissionsAsync(nm, userId);
                    if (missionClaimed)
                    {
                        // 领取到了奖励，可能会获得新扫把，继续下一轮循环
                        continue;
                    }

                    await _jobLogger.LogAsync(userId, "没有可用的清扫道具，且没有待领取的任务奖励，自动清扫结束。");
                    break;
                }

            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "执行书库大扫除自动化出错 for user {UserId}", userId);
                break;
            }
        }

        await _jobLogger.LogAsync(userId, "书库大扫除自动清扫已结束。");
    }

    /// <summary>
    /// 自动领取书库大扫除的任务奖励
    /// </summary>
    /// <returns>是否有领取动作</returns>
    private async Task<bool> TryClaimBookSortMissionsAsync(NetworkManager nm, long userId)
    {
        try
        {
            var missionInfoResp = await nm.SendRequest<GetMissionInfoRequest, GetMissionInfoResponse>(
                new GetMissionInfoRequest { TargetMissionGroupList = new List<MissionGroupType> { MissionGroupType.BookSort } }, false);

            if (missionInfoResp.MissionInfoDict != null)
            {
                var allMissionIds = new List<long>();
                foreach (var group in missionInfoResp.MissionInfoDict.Values)
                {
                    if (group.UserMissionDtoInfoDict == null) continue;
                    foreach (var missionList in group.UserMissionDtoInfoDict.Values)
                    {
                        foreach (var mission in missionList)
                        {
                            if (mission.MissionStatusHistory != null &&
                                mission.MissionStatusHistory.TryGetValue(MissionStatusType.NotReceived, out var ids))
                            {
                                allMissionIds.AddRange(ids);
                            }
                        }
                    }
                }

                if (allMissionIds.Count > 0)
                {
                    var rewardResp = await nm.SendRequest<RewardMissionRequest, RewardMissionResponse>(
                        new RewardMissionRequest { TargetMissionIdList = allMissionIds }, false);

                    await _jobLogger.LogAsync(userId, $"成功领取了 {allMissionIds.Count} 项书库大扫除任务奖励。");
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "自动领取书库任务奖励失败 for user {UserId}", userId);
        }

        return false;
    }

    /// <summary>
    /// 获取道具形状对应的坐标偏移列表 (相对中心点 0,0)
    /// 返回值：列偏移(x), 行偏移(y)
    /// </summary>
    private List<(int colOffset, int rowOffset)> GetItemAreaShape(long itemId)
    {
        return itemId switch
        {
            // ID:2 旧扫帚 (2x1) 假设向右
            2 => new List<(int, int)> { (0, 0), (1, 0) },

            // ID:3 绿意扫帚(横向) (4x1)
            3 => new List<(int, int)> { (0, 0), (1, 0), (2, 0), (3, 0) },

            // ID:4 绿意扫帚(纵向) (1x4)
            4 => new List<(int, int)> { (0, 0), (0, 1), (0, 2), (0, 3) },

            // ID:5 微风掸子 (2x2)
            5 => new List<(int, int)> { (0, 0), (1, 0), (0, 1), (1, 1) },

            // ID:6 书库大水桶(十字型) (十形 9格，中心周边上下各2)
            //  #
            //  #
            // ##X##
            //  #
            //  #
            6 => new List<(int, int)> {
                (0, 0),
                (-1, 0), (-2, 0), (1, 0), (2, 0),
                (0, -1), (0, -2), (0, 1), (0, 2)
            },

            // ID:7 书库大水桶(交叉型) (X形 9格)
            // #   #
            //  # # 
            //   X
            //  # #
            // #   #
            7 => new List<(int, int)> {
                (0, 0),
                (-1, -1), (-2, -2),
                (-1, 1), (-2, 2),
                (1, -1), (2, -2),
                (1, 1), (2, 2)
            },

            // ID:8 除尘旋风 (3x3形 9格)
            8 => new List<(int, int)> {
                (-1, -1), (0, -1), (1, -1),
                (-1, 0),  (0, 0),  (1, 0),
                (-1, 1),  (0, 1),  (1, 1)
            },

            // 未知/普通 的只认自己当前格
            _ => new List<(int, int)> { (0, 0) }
        };
    }
}
