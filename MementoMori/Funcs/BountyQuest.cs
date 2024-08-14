using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest;
using MementoMori.Ortega.Share.Data.BountyQuest;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Utils;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    private bool IsBountyQuestAvailable => UserSyncData?.UserBattleBossDtoInfo?.BossClearMaxQuestId >= OpenContentTable.GetByOpenCommandType(OpenCommandType.BountyQuest).OpenContentValue;

    /// <summary>
    ///     The number of expected diamonds to be obtained for each refresh(cost 20 diamonds).
    /// </summary>
    /// <remarks>
    ///     data from https://x.com/bumiudi/status/1786553689932718292
    /// </remarks>
    private Dictionary<long, double> DiamondExpectedCount { get; } = new()
    {
        [8] = 13,
        [7] = 11.6,
        [6] = 9.8,
        [5] = 7.9,
        [4] = 5.7
    };

    public async Task BountyQuestStartAuto()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var response1 = await GetBountyRequestInfo();
            if (response1 == null) return;
            if (GameConfig.BountyQuestAuto.TargetItems.Count > 0 && !BountyRequestForceAll)
            {
                var itemNames = string.Join(",", GameConfig.BountyQuestAuto.TargetItems.Select(ItemUtil.GetItemName));
                log($"{TextResourceTable.Get("[CommonHeaderBountyQuestLabel]")}: {ResourceStrings.DesignatedTargetProp} {itemNames}");
                log($"{TextResourceTable.Get("[CommonHeaderBountyQuestLabel]")}: {ResourceStrings.DispatchingTargetPropMission}");
                var bountyQuestStartInfos = BountyQuestAutoFormationUtil.CalcAutoFormation(response1, UserSyncData, GameConfig.BountyQuestAuto);
                await Dispatch(bountyQuestStartInfos, log);
            }
            else
            {
                log($"{TextResourceTable.Get("[CommonHeaderBountyQuestLabel]")}: {ResourceStrings.DispatchingAll}");
                var bountyQuestStartInfos = BountyQuestAutoFormationUtil.CalcAutoFormation(response1, UserSyncData, GameConfig.BountyQuestAuto, true);
                await Dispatch(bountyQuestStartInfos, log);
            }

            response1 = await GetBountyRequestInfo();

            // if no diamond, do not refresh
            if (!GameConfig.BountyQuestAuto.TargetItems.Exists(d => d.IsCurrency())) return;

            // if refresh is disabled, do not refresh
            if (PlayerOption.BountyQuest.MaxRefreshCount == 0) return;

            // if rank is not in the list, do not refresh (rank is too low)
            if (!DiamondExpectedCount.ContainsKey(response1!.UserBoardRank)) return;

            var date = TimeManager.ServerNow.ToString("yyyy-MM-dd");
            // if new date, clear old values and initialize new date
            if (!PlayerOption.BountyQuest.TodayRefreshCount.ContainsKey(date))
            {
                _playersOption.Update(opt =>
                {
                    var playerOpt = opt.GetOrAdd(NetworkManager.PlayerId, id => new PlayerOption {PlayerId = id});
                    playerOpt.BountyQuest.TodayRefreshCount.Clear();
                    playerOpt.BountyQuest.TodayRefreshCount.Add(date, 0);
                });
            }

            for (var i = 0; i < 100; i++)
            {
                response1 = await GetBountyRequestInfo();
                if (response1 == null) return;

                var notDispatchedQuests = response1.BountyQuestInfos
                    .Where(d => response1.UserBountyQuestDtoInfos.Find(x => d.BountyQuestId == x.BountyQuestId).BountyQuestEndTime == 0)
                    .ToList();

                var expectedDiamond = DiamondExpectedCount[response1.UserBoardRank] * notDispatchedQuests.Count;
                var eventMb = BountyQuestEventTable.GetByInTime(d => TimeManager.IsInTime(d));
                if (eventMb != null)
                {
                    expectedDiamond *= 1 + eventMb.MultipleNumber / 100.0;
                }

                if (expectedDiamond < 20)
                {
                    // if the expected diamond value of the current task is now below 20, do not refresh, dispatch directly
                    log(ResourceStrings.The_expected_diamond_value_of_the_current_task_is_now_below_20__do_not_refresh__dispatch_directly_);
                    var bountyQuestStartInfos = BountyQuestAutoFormationUtil.CalcAutoFormation(response1, UserSyncData, GameConfig.BountyQuestAuto, true);
                    await Dispatch(bountyQuestStartInfos, log);
                    break;
                }

                if (PlayerOption.BountyQuest.MaxRefreshCount <= PlayerOption.BountyQuest.TodayRefreshCount[date])
                {
                    // if the number of refreshes has reached the maximum, dispatch directly
                    log(ResourceStrings.Refresh_limit_reached__dispatch_directly);
                    var bountyQuestStartInfos = BountyQuestAutoFormationUtil.CalcAutoFormation(response1, UserSyncData, GameConfig.BountyQuestAuto, true);
                    await Dispatch(bountyQuestStartInfos, log);
                    break;
                }

                // refresh
                log(string.Format(ResourceStrings.Current_expected_value___0___Today___1___2__auto_refreshes_completed__Refreshing_now_, expectedDiamond,
                    PlayerOption.BountyQuest.TodayRefreshCount[date], PlayerOption.BountyQuest.MaxRefreshCount));

                await GetResponse<RemakeRequest, RemakeResponse>(new RemakeRequest());
                _playersOption.Update(opt =>
                {
                    var playerOpt = opt.GetOrAdd(NetworkManager.PlayerId, id => new PlayerOption {PlayerId = id});
                    playerOpt.BountyQuest.TodayRefreshCount[date]++;
                });
                response1 = await GetBountyRequestInfo();
                if (response1 == null) return;
                notDispatchedQuests = response1.BountyQuestInfos
                    .Where(d => response1.UserBountyQuestDtoInfos.Find(x => d.BountyQuestId == x.BountyQuestId)!.BountyQuestEndTime == 0)
                    .ToList();

                var diamondQuests = notDispatchedQuests.Where(d => d.RewardItems.Exists(x => x.IsCurrency())).ToList();
                if (diamondQuests.Count > 0)
                {
                    // if there are diamond quests, dispatch
                    var bountyQuestStartInfos = BountyQuestAutoFormationUtil.CalcAutoFormation(response1, UserSyncData, GameConfig.BountyQuestAuto, diamondQuestOnly: true);
                    await Dispatch(bountyQuestStartInfos, log);
                }
            }

            await GetBountyRequestInfo();
        });

        async Task Dispatch(List<BountyQuestStartInfo> bountyQuestStartInfos, Action<string> log)
        {
            foreach (var bountyQuestStartInfo in bountyQuestStartInfos)
            {
                var startResponse = await GetResponse<StartRequest, StartResponse>(
                    new StartRequest {BountyQuestStartInfos = new List<BountyQuestStartInfo> {bountyQuestStartInfo}});
                log($"{ResourceStrings.Dispatched} {bountyQuestStartInfo.BountyQuestId}");
            }
        }
    }

    public async Task BountyQuestRewardAuto()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            if (!IsBountyQuestAvailable) return;

            log($"{TextResourceTable.Get("[CommonHeaderBountyQuestLabel]")}:\n");
            var getListResponse = await GetResponse<GetListRequest, GetListResponse>(
                new GetListRequest());

            var questIds = getListResponse.UserBountyQuestDtoInfos
                .Where(d => d.BountyQuestEndTime > 0 && !d.IsReward && DateTimeOffset.Now.Add(TimeManager.DiffFromUtc).ToUnixTimeMilliseconds() > d.BountyQuestEndTime)
                .Select(d => d.BountyQuestId).ToList();

            if (questIds.Count > 0)
            {
                var rewardResponse = await GetResponse<RewardRequest, RewardResponse>(new RewardRequest {BountyQuestIds = questIds, ConsumeCurrency = 0, IsQuick = false});
                rewardResponse.RewardItems.PrintUserItems(log);
                await GetResponse<GetListRequest, GetListResponse>(new GetListRequest());
            }
            else
                log(ResourceStrings.NothingToReceive);

            await GetBountyRequestInfo();
        });
    }

    public async Task<GetListResponse?> GetBountyRequestInfo()
    {
        if (!IsBountyQuestAvailable) return null;

        var response = await GetResponse<GetListRequest, GetListResponse>(new GetListRequest());
        BountyQuestResponseInfo = response;
        return response;
    }

    public async Task RemakeBountyRequest()
    {
        if (BountyQuestResponseInfo.UserBountyQuestDtoInfos.Any(d => d.BountyQuestEndTime == 0))
        {
            var response = await GetResponse<RemakeRequest, RemakeResponse>(new RemakeRequest());
            await GetBountyRequestInfo();
        }
        else
            AddLog(string.Format(ResourceStrings.NoAvailable, TextResourceTable.Get("[BountyQuestTypeSolo]")));
    }
}