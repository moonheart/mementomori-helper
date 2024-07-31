using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest;
using MementoMori.Ortega.Share.Data.BountyQuest;
using MementoMori.Utils;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    private bool IsBountyQuestAvailable => UserSyncData?.UserBattleBossDtoInfo?.BossClearMaxQuestId >= OpenContentTable.GetByOpenCommandType(OpenCommandType.BountyQuest).OpenContentValue;

    public async Task BountyQuestStartAuto()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            if (!IsBountyQuestAvailable) return;

            var response1 = await GetResponse<GetListRequest, GetListResponse>(new GetListRequest());
            if (GameConfig.BountyQuestAuto.TargetItems.Count > 0 && !BountyRequestForceAll)
            {
                var itemNames = string.Join(",", GameConfig.BountyQuestAuto.TargetItems.Select(ItemUtil.GetItemName));
                log($"{TextResourceTable.Get("[CommonHeaderBountyQuestLabel]")}: {ResourceStrings.DesignatedTargetProp} {itemNames}");
                log($"{TextResourceTable.Get("[CommonHeaderBountyQuestLabel]")}: {ResourceStrings.DispatchingTargetPropMission}");
                var bountyQuestStartInfos = BountyQuestAutoFormationUtil.CalcAutoFormation(response1, UserSyncData, GameConfig.BountyQuestAuto);
                foreach (var bountyQuestStartInfo in bountyQuestStartInfos)
                {
                    var startResponse = await GetResponse<StartRequest, StartResponse>(
                        new StartRequest {BountyQuestStartInfos = new List<BountyQuestStartInfo> {bountyQuestStartInfo}});
                    log($"{ResourceStrings.Dispatched} {bountyQuestStartInfo.BountyQuestId}");
                }
            }
            else
            {
                log($"{TextResourceTable.Get("[CommonHeaderBountyQuestLabel]")}: {ResourceStrings.DispatchingAll}");
                var bountyQuestStartInfos = BountyQuestAutoFormationUtil.CalcAutoFormation(response1, UserSyncData, GameConfig.BountyQuestAuto, true);
                foreach (var bountyQuestStartInfo in bountyQuestStartInfos)
                {
                    var startResponse = await GetResponse<StartRequest, StartResponse>(
                        new StartRequest {BountyQuestStartInfos = new List<BountyQuestStartInfo> {bountyQuestStartInfo}});
                    log($"{ResourceStrings.Dispatched} {bountyQuestStartInfo.BountyQuestId}");
                }
            }

            await GetBountyRequestInfo();
        });
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

    public async Task GetBountyRequestInfo()
    {
        if (!IsBountyQuestAvailable) return;

        var response = await GetResponse<GetListRequest, GetListResponse>(new GetListRequest());
        BountyQuestResponseInfo = response;
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