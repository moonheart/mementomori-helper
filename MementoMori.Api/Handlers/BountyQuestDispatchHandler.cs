using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;
using MementoMori.Api.Services;
using MementoMori.Api.Utils;
using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest;
using MementoMori.Ortega.Share.Data.BountyQuest;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Extensions;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 悬赏任务自动派遣处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class BountyQuestDispatchHandler : IGameActionHandler
{
    private readonly ILogger<BountyQuestDispatchHandler> _logger;
    private readonly JobLogger _jobLogger;
    private readonly PlayerSettingService _settingService;

    public string ActionName => "悬赏任务自动派遣";

    /// <summary>
    /// 每刷新一次花费20钻石，期望获得的钻石数量（根据任务板等级）
    /// 数据来源: https://x.com/bumiudi/status/1786553689932718292
    /// </summary>
    private static readonly Dictionary<long, double> DiamondExpectedCount = new()
    {
        [8] = 13,
        [7] = 11.6,
        [6] = 9.8,
        [5] = 7.9,
        [4] = 5.7
    };

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        // 检查悬赏任务功能是否已解锁
        var userSyncData = nm.UserSyncData;
        var openContentMb = OpenContentTable.GetByOpenCommandType(OpenCommandType.BountyQuest);
        var isAvailable = userSyncData?.UserBattleBossDtoInfo?.BossClearMaxQuestId >= openContentMb.OpenContentValue;
        
        if (!isAvailable)
        {
            await _jobLogger.LogAsync(userId, "悬赏任务功能尚未解锁");
            return;
        }

        await _jobLogger.LogAsync(userId, "开始执行悬赏任务自动派遣...");

        // 获取配置
        var bountyQuestAuto = await _settingService.GetSettingAsync<GameConfig.BountyQuestAutoConfig>(userId, "bountyquestauto");
        var playerOption = await _settingService.GetSettingAsync<PlayerOption>(userId, "playeroption");
        var bountyQuestOption = playerOption?.BountyQuest ?? new BountyQuestOption();

        // 获取悬赏任务列表
        var response1 = await GetBountyRequestInfo(nm);
        if (response1 == null) return;

        // 判断是否有目标物品配置
        if (bountyQuestAuto?.TargetItems.Count > 0)
        {
            var itemNames = string.Join(",", bountyQuestAuto.TargetItems.Select(ItemUtil.GetItemName));
            await _jobLogger.LogAsync(userId, $"指定目标道具: {itemNames}");
            await _jobLogger.LogAsync(userId, "正在派遣符合目标道具的任务...");
            
            var bountyQuestStartInfos = BountyQuestAutoFormationUtil.CalcAutoFormation(response1, userSyncData!, bountyQuestAuto);
            await DispatchAsync(nm, bountyQuestStartInfos, userId);
        }
        else
        {
            await _jobLogger.LogAsync(userId, "未指定目标道具，派遣所有可用任务...");
            var bountyQuestStartInfos = BountyQuestAutoFormationUtil.CalcAutoFormation(response1, userSyncData!, bountyQuestAuto ?? new GameConfig.BountyQuestAutoConfig(), force: true);
            await DispatchAsync(nm, bountyQuestStartInfos, userId);
        }

        // 重新获取任务列表
        response1 = await GetBountyRequestInfo(nm);
        if (response1 == null) return;

        // 检查是否需要刷新（只有目标物品包含货币时才刷新）
        if (!bountyQuestAuto!.TargetItems.Exists(d => d.IsCurrency())) return;

        // 如果刷新被禁用，不刷新
        if (bountyQuestOption.MaxRefreshCount == 0) return;

        // 如果等级不在列表中，不刷新（等级太低）
        if (!DiamondExpectedCount.ContainsKey(response1.UserBoardRank)) return;

        // 处理每日刷新计数
        var date = context.TimeManager.ServerNow.ToString("yyyy-MM-dd");
        if (!bountyQuestOption.TodayRefreshCount.ContainsKey(date))
        {
            bountyQuestOption.TodayRefreshCount.Clear();
            bountyQuestOption.TodayRefreshCount.Add(date, 0);
            
            // 保存更新后的配置
            if (playerOption != null)
            {
                playerOption.BountyQuest = bountyQuestOption;
                await _settingService.SaveSettingAsync(userId, "playeroption", playerOption);
            }
        }

        // 自动刷新逻辑（最多100次循环，防止无限循环）
        for (var i = 0; i < 100; i++)
        {
            response1 = await GetBountyRequestInfo(nm);
            if (response1 == null) return;

            // 获取未派遣的任务
            var notDispatchedQuests = response1.BountyQuestInfos
                .Where(d => response1.UserBountyQuestDtoInfos.Find(x => d.BountyQuestId == x.BountyQuestId)?.BountyQuestEndTime == 0)
                .ToList();

            // 计算期望钻石收益
            var expectedDiamond = DiamondExpectedCount[response1.UserBoardRank] * notDispatchedQuests.Count;
            var eventMb = BountyQuestEventTable.GetByInTime(d => context.TimeManager.IsInTime(d));
            if (eventMb != null)
            {
                expectedDiamond *= 1 + eventMb.MultipleNumber / 100.0;
            }

            // 如果期望收益低于20钻石，直接派遣不刷新
            if (expectedDiamond < 20)
            {
                await _jobLogger.LogAsync(userId, "当前任务的期望钻石收益低于20，不再刷新，直接派遣...");
                var bountyQuestStartInfos = BountyQuestAutoFormationUtil.CalcAutoFormation(response1, userSyncData!, bountyQuestAuto, force: true);
                await DispatchAsync(nm, bountyQuestStartInfos, userId);
                break;
            }

            // 如果刷新次数已达上限，直接派遣
            if (bountyQuestOption.MaxRefreshCount <= bountyQuestOption.TodayRefreshCount[date])
            {
                await _jobLogger.LogAsync(userId, $"刷新次数已达上限 ({bountyQuestOption.TodayRefreshCount[date]}/{bountyQuestOption.MaxRefreshCount})，直接派遣...");
                var bountyQuestStartInfos = BountyQuestAutoFormationUtil.CalcAutoFormation(response1, userSyncData!, bountyQuestAuto, force: true);
                await DispatchAsync(nm, bountyQuestStartInfos, userId);
                break;
            }

            // 执行刷新
            await _jobLogger.LogAsync(userId, 
                $"当前期望收益: {expectedDiamond:F1} 钻石，今日已刷新: {bountyQuestOption.TodayRefreshCount[date]}/{bountyQuestOption.MaxRefreshCount}，正在刷新...");

            await nm.SendRequest<RemakeRequest, RemakeResponse>(new RemakeRequest(), false);
            
            // 更新刷新计数
            bountyQuestOption.TodayRefreshCount[date]++;
            if (playerOption != null)
            {
                playerOption.BountyQuest = bountyQuestOption;
                await _settingService.SaveSettingAsync(userId, "playeroption", playerOption);
            }

            // 重新获取任务列表
            response1 = await GetBountyRequestInfo(nm);
            if (response1 == null) return;

            // 获取刷新后的未派遣任务
            notDispatchedQuests = response1.BountyQuestInfos
                .Where(d => response1.UserBountyQuestDtoInfos.Find(x => d.BountyQuestId == x.BountyQuestId)?.BountyQuestEndTime == 0)
                .ToList();

            // 检查是否有钻石任务
            var diamondQuests = notDispatchedQuests.Where(d => d.RewardItems.Exists(x => x.IsCurrency())).ToList();
            if (diamondQuests.Count > 0)
            {
                // 有钻石任务，派遣
                var bountyQuestStartInfos = BountyQuestAutoFormationUtil.CalcAutoFormation(response1, userSyncData!, bountyQuestAuto, diamondQuestOnly: true);
                await DispatchAsync(nm, bountyQuestStartInfos, userId);
            }
        }

        // 最后获取一次任务列表更新状态
        await GetBountyRequestInfo(nm);
        await _jobLogger.LogAsync(userId, "悬赏任务自动派遣完成");
    }

    private async Task<GetListResponse?> GetBountyRequestInfo(NetworkManager nm)
    {
        try
        {
            var response = await nm.SendRequest<GetListRequest, GetListResponse>(new GetListRequest(), false);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get bounty quest list");
            return null;
        }
    }

    private async Task DispatchAsync(NetworkManager nm, List<BountyQuestStartInfo> bountyQuestStartInfos, long userId)
    {
        foreach (var bountyQuestStartInfo in bountyQuestStartInfos)
        {
            try
            {
                var startResponse = await nm.SendRequest<StartRequest, StartResponse>(
                    new StartRequest { BountyQuestStartInfos = new List<BountyQuestStartInfo> { bountyQuestStartInfo } }, 
                    false);
                
                await _jobLogger.LogAsync(userId, $"已派遣悬赏任务: {bountyQuestStartInfo.BountyQuestId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to dispatch bounty quest {BountyQuestId}", bountyQuestStartInfo.BountyQuestId);
            }
        }
    }
}
