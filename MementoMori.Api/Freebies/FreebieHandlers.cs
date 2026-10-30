using MementoMori.Api.Extensions;
using MementoMori.Api.Handlers;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Character;
using MementoMori.Ortega.Share.Data.ApiInterface.EquipmentSynchro;
using MementoMori.Ortega.Share.Data.ApiInterface.Friend;
using MementoMori.Ortega.Share.Data.ApiInterface.Mission;
using MementoMori.Ortega.Share.Data.ApiInterface.RentalRaid;
using MementoMori.Ortega.Share.Data.Item;
using Vote = MementoMori.Ortega.Share.Data.ApiInterface.PopularityVote;

namespace MementoMori.Api.Freebies;

// 免费奖励: 都是只领不花的东西, 放在每日任务里默认执行.
// 活动没开时服务器会报错, 这里只记一行 "not available" 就跳过.
// 领到的物品由 Autopilot.RewardTracker 统一记进成长记录.

internal static class Free
{
    public static async Task TryAsync(JobLogger log, long userId, string name, Func<Task> action)
    {
        try { await action(); }
        catch (NetworkManager.ApiErrorException e) { await log.LogAsync(userId, $"{name}: not available ({e.ErrorCode})"); }
    }
}

/// <summary>角色回忆 (记忆): 等级够了就领奖励, 跳过剧情</summary>
[RegisterTransient]
[AutoConstructor]
public partial class CharacterStoryRewardHandler : IGameActionHandler
{
    private readonly JobLogger _log;
    public string ActionName => "Character memories";

    public async Task ExecuteAsync(AccountContext context)
    {
        var nm = context.NetworkManager;
        var userId = context.AccountInfo.UserId;
        var ids = (nm.UserSyncData.UserCharacterBookDtoInfos ?? new())
            .SelectMany(b => CharacterStoryTable.GetListByCharacterId(b.CharacterId)
                .Where(s => s.Level <= b.MaxCharacterLevel && s.EpisodeId > b.MaxEpisodeId))
            .Select(s => s.Id).ToList();
        foreach (var id in ids)
            await Free.TryAsync(_log, userId, "Memory", () =>
                nm.SendRequest<GetCharacterStoryRewardRequest, GetCharacterStoryRewardResponse>(new GetCharacterStoryRewardRequest {IsSkip = true, CharacterStoryIdList = new() {id}}));
        if (ids.Count > 0) await _log.LogAsync(userId, $"Read {ids.Count} character memories");
    }
}

/// <summary>好友任务奖励</summary>
[RegisterTransient]
[AutoConstructor]
public partial class FriendMissionRewardHandler : IGameActionHandler
{
    private readonly JobLogger _log;
    public string ActionName => "Friend mission rewards";

    public async Task ExecuteAsync(AccountContext context)
    {
        var nm = context.NetworkManager;
        var userId = context.AccountInfo.UserId;
        await Free.TryAsync(_log, userId, ActionName, async () =>
        {
            var info = await nm.SendRequest<GetFriendCampaignInfoRequest, GetFriendCampaignInfoResponse>(new GetFriendCampaignInfoRequest());
            foreach (var m in info.UserFriendMissionDtoInfoList ?? new())
            foreach (var missionId in m.MissionStatusHistory?.GetValueOrDefault(MissionStatusType.NotReceived) ?? new())
                await nm.SendRequest<RewardFriendMissionRequest, RewardFriendMissionResponse>(new RewardFriendMissionRequest
                {
                    FriendCampaignId = m.FriendCampaignId, AchievementType = m.AchievementType, FriendMissionId = missionId
                });
        });
    }
}

/// <summary>面板任务 (宾果) 连线奖励: 3x3, 一行/一列的任务都领完了才领</summary>
[RegisterTransient]
[AutoConstructor]
public partial class PanelBingoRewardHandler : IGameActionHandler
{
    private static readonly Dictionary<BingoType, int[]> Lines = new()
    {
        [BingoType.UpperRow] = new[] {0, 1, 2}, [BingoType.CenterRow] = new[] {3, 4, 5}, [BingoType.LowerRow] = new[] {6, 7, 8},
        [BingoType.LeftColumn] = new[] {0, 3, 6}, [BingoType.CenterColumn] = new[] {1, 4, 7}, [BingoType.RightColumn] = new[] {2, 5, 8},
    };

    private readonly JobLogger _log;
    public string ActionName => "Panel bingo rewards";

    public async Task ExecuteAsync(AccountContext context)
    {
        var nm = context.NetworkManager;
        var userId = context.AccountInfo.UserId;
        await Free.TryAsync(_log, userId, ActionName, async () =>
        {
            var resp = await nm.SendRequest<GetMissionInfoRequest, GetMissionInfoResponse>(new GetMissionInfoRequest {TargetMissionGroupList = new() {MissionGroupType.Panel}});
            if (resp.MissionInfoDict?.GetValueOrDefault(MissionGroupType.Panel) is not { } panel) return;
            var done = (panel.UserMissionDtoInfoDict ?? new()).Values.SelectMany(l => l)
                .SelectMany(m => m.MissionStatusHistory?.GetValueOrDefault(MissionStatusType.Received) ?? new()).ToHashSet();
            foreach (var user in panel.UserPanelMissionDtoInfoList ?? new())
            {
                var sheet = PanelMissionTable.GetArray().SelectMany(p => p.PanelMissionSheetInfoList ?? Array.Empty<Ortega.Share.Data.Mission.PanelMissionSheetInfo>())
                    .FirstOrDefault(s => s.SheetNo == user.SheetNo && s.MissionIdList.Count == 9 && s.MissionIdList.Any(done.Contains));
                if (sheet == null) continue;
                foreach (var (type, cells) in Lines)
                {
                    if (user.ReceivedBingoTypeList?.Contains(type) == true || !cells.All(i => done.Contains(sheet.MissionIdList[i]))) continue;
                    await nm.SendRequest<ReceivePanelMissionBingoRewardRequest, ReceivePanelMissionBingoRewardResponse>(
                        new ReceivePanelMissionBingoRewardRequest {SheetNo = user.SheetNo, BingoType = type});
                }
            }
        });
    }
}

/// <summary>人气投票的投票数奖励</summary>
[RegisterTransient]
[AutoConstructor]
public partial class PopularityVoteRewardHandler : IGameActionHandler
{
    private readonly JobLogger _log;
    public string ActionName => "Popularity vote rewards";

    public async Task ExecuteAsync(AccountContext context)
    {
        var nm = context.NetworkManager;
        await Free.TryAsync(_log, context.AccountInfo.UserId, ActionName, async () =>
        {
            var info = await nm.SendRequest<Vote.GetInfoRequest, Vote.GetInfoResponse>(new Vote.GetInfoRequest());
            if (info.PopularityVoteRewardInfoList?.Any(r => !r.IsReceived && r.GoalVoteCount <= info.TotalVoteCount) == true)
                await nm.SendRequest<Vote.RewardVoteItemRequest, Vote.RewardVoteItemResponse>(new Vote.RewardVoteItemRequest {IsAllReward = true});
        });
    }
}

/// <summary>租借讨伐: 别人借用我的角色攒的分享点数奖励</summary>
[RegisterTransient]
[AutoConstructor]
public partial class RentalRaidShareRewardHandler : IGameActionHandler
{
    private readonly JobLogger _log;
    public string ActionName => "Rental raid share rewards";

    public async Task ExecuteAsync(AccountContext context)
    {
        var nm = context.NetworkManager;
        await Free.TryAsync(_log, context.AccountInfo.UserId, ActionName, async () =>
        {
            var info = await nm.SendRequest<GetShareRewardInfoRequest, GetShareRewardInfoResponse>(new GetShareRewardInfoRequest());
            foreach (var r in (info.ShareRewardInfoList ?? new()).Where(r => !r.IsReceived && r.RequiredSharePoint <= info.CurrentSharePoint).OrderBy(r => r.RequiredSharePoint))
                await nm.SendRequest<ReceiveShareRewardRequest, ReceiveShareRewardResponse>(new ReceiveShareRewardRequest {SharePoint = r.RequiredSharePoint});
        });
    }
}

/// <summary>装备同步的任务奖励 (一键领取)</summary>
[RegisterTransient]
[AutoConstructor]
public partial class EquipmentSynchroMissionHandler : IGameActionHandler
{
    private readonly JobLogger _log;
    public string ActionName => "Gear synchro mission rewards";

    public Task ExecuteAsync(AccountContext context) =>
        Free.TryAsync(_log, context.AccountInfo.UserId, ActionName, () =>
            context.NetworkManager.SendRequest<EquipmentSynchroRewardMissionRequest, EquipmentSynchroRewardMissionResponse>(new EquipmentSynchroRewardMissionRequest {IsBulk = true}));
}
