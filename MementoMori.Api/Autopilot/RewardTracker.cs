using MementoMori.Api.Infrastructure;
using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Share.Data.Item;
using Battle = MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using BountyQuest = MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest;
using Friend = MementoMori.Ortega.Share.Data.ApiInterface.Friend;
using LoginBonus = MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus;
using Mission = MementoMori.Ortega.Share.Data.ApiInterface.Mission;
using Present = MementoMori.Ortega.Share.Data.ApiInterface.Present;
using Shop = MementoMori.Ortega.Share.Data.ApiInterface.Shop;
using Vip = MementoMori.Ortega.Share.Data.ApiInterface.Vip;
using Character = MementoMori.Ortega.Share.Data.ApiInterface.Character;
using RentalRaid = MementoMori.Ortega.Share.Data.ApiInterface.RentalRaid;
using Vote = MementoMori.Ortega.Share.Data.ApiInterface.PopularityVote;
using Synchro = MementoMori.Ortega.Share.Data.ApiInterface.EquipmentSynchro;
using TowerBattle = MementoMori.Ortega.Share.Data.ApiInterface.TowerBattle;

namespace MementoMori.Api.Autopilot;

/// <summary>
/// 监听所有游戏响应, 把领到的奖励和好友操作记进成长记录
/// (不管是定时任务还是界面上手动点的都会记)
/// </summary>
public class RewardTracker : IHostedService
{
    private readonly GrowthTracker _tracker;
    private readonly ILogger<RewardTracker> _logger;

    public RewardTracker(GrowthTracker tracker, ILogger<RewardTracker> logger)
    {
        _tracker = tracker;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        NetworkManager.ResponseReceived += OnResponse;
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        NetworkManager.ResponseReceived -= OnResponse;
        return Task.CompletedTask;
    }

    private static string T(string key) => TextResourceTable.Get(key);

    private void OnResponse(long userId, object request, object response)
    {
        var entry = response switch
        {
            Battle.RewardAutoBattleResponse r => Rewards(T("[AutoBattleRewardInfoRewardLabel]"), r.AutoBattleRewardResult?.BattleRewardResult?.FixedItemList, r.AutoBattleRewardResult?.BattleRewardResult?.DropItemList),
            BountyQuest.RewardResponse r => Rewards(T("[CommonHeaderBountyQuestLabel]"), r.RewardItems),
            LoginBonus.ReceiveDailyLoginBonusResponse r => Rewards(T("[MyPageButtonLoginBonusLabel]"), r.RewardItemList),
            LoginBonus.ReceiveLoginCountBonusResponse r => Rewards(T("[MyPageButtonLoginBonusLabel]"), r.RewardItemList),
            LoginBonus.ReceiveDailyLimitedLoginBonusResponse r => Rewards(T("[MyPageButtonLoginBonusLabel]"), r.RewardItemList?.Select(d => d.Item)),
            LoginBonus.ReceiveSpecialLimitedLoginBonusResponse r => Rewards(T("[MyPageButtonLoginBonusLabel]"), r.RewardItemList?.Select(d => d.Item)),
            Mission.RewardMissionResponse r => Rewards(T("[MyPageMenuButtonMissionLabel]"), r.RewardInfo?.ItemList),
            Mission.RewardMissionActivityResponse r => Rewards(T("[MyPageMenuButtonMissionLabel]"), r.RewardInfo?.ItemList),
            Present.ReceiveItemResponse r => Rewards(T("[MyPagePresentBoxButtonTitle]"), r.ResultItems?.Select(d => d.Item)),
            Shop.ReceiveRewardResponse r => Rewards(T("[CommonMonthlyBoosterLabel]"), r.RewardInfo?.ItemList, r.RewardInfo?.BonusItemList),
            Vip.GetDailyGiftResponse r => Rewards("VIP", r.ItemList),
            Character.GetCharacterStoryRewardResponse r => Rewards("Memories", r.RewardItemList),
            Friend.RewardFriendMissionResponse r => Rewards("Friend missions", r.RewardItemList),
            Mission.ReceivePanelMissionBingoRewardResponse r => Rewards("Panel bingo", r.RewardInfo?.ItemList),
            Vote.RewardVoteItemResponse r => Rewards("Popularity vote", r.RewardItemList),
            RentalRaid.ReceiveShareRewardResponse r => Rewards("Rental raid", r.ReceiveItems),
            Synchro.EquipmentSynchroRewardMissionResponse r => Rewards("Gear synchro", r.RewardItem == null ? null : new[] {r.RewardItem}),
            TowerBattle.TowerBattleQuickResponse r => Rewards(T("[TowerTypeInfinite]"), r.BattleRewardResult?.FixedItemList, r.BattleRewardResult?.DropItemList),
            Friend.BulkApplyFriendsResponse { AppliedPlayerIdList.Count: > 0 } r => (GrowthKind.Friend, $"Sent {r.AppliedPlayerIdList.Count} friend requests"),
            Friend.ReplyAllFriendResponse { ProcessedNum: > 0 } r => (GrowthKind.Friend, $"Accepted {r.ProcessedNum} friend requests"),
            Friend.RemoveFriendResponse when request is Friend.RemoveFriendRequest req => (GrowthKind.Friend, $"Removed inactive friend (player {req.TargetPlayerId})"),
            _ => default((GrowthKind, string)?)
        };
        if (entry is not { } e) return;

        // 不阻塞请求线程
        _ = Task.Run(async () =>
        {
            try { await _tracker.TrackAsync(userId, e.Item1, e.Item2); }
            catch (Exception ex) { _logger.LogWarning(ex, "track reward failed"); }
        });
    }

    // "登录奖励: 金币 ×1,000, 钻石 ×20"
    private static (GrowthKind, string)? Rewards(string source, params IEnumerable<IUserItem>?[] lists)
    {
        var items = lists.Where(l => l != null).SelectMany(l => l!).Where(i => i is {ItemCount: > 0})
            .GroupBy(i => (i.ItemType, i.ItemId))
            .Select(g => $"{ItemUtil.GetItemName(g.Key.ItemType, g.Key.ItemId)} ×{g.Sum(i => i.ItemCount):N0}")
            .ToList();
        return items.Count == 0 ? null : (GrowthKind.Reward, $"{source}: {string.Join(", ", items)}");
    }
}
