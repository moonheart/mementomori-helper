using MementoMori.Api.Extensions;
using MementoMori.Api.Handlers;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Data.ApiInterface.Character;
using MementoMori.Ortega.Share.Data.ApiInterface.Gacha;
using MementoMori.Ortega.Share.Data.ApiInterface.User;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Gacha;
using MementoMori.Ortega.Share.Data.Item;
using static MementoMori.Api.Autopilot.AutopilotExtensions;

namespace MementoMori.Api.Autopilot;

/// <summary>
/// 把推荐队伍存成 主线/挂机/无穷塔/竞技场/幻影神殿/时空洞窟 的队伍
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class AutoBuildTeamHandler : IGameActionHandler
{
    private static readonly DeckUseContentType[] Decks =
    {
        DeckUseContentType.Boss, DeckUseContentType.Auto, DeckUseContentType.Infinite, DeckUseContentType.BattleLeagueOffense, DeckUseContentType.BattleLeagueDefense,
        DeckUseContentType.LocalRaid, DeckUseContentType.DungeonBattle
    };

    private readonly GrowthTracker _tracker;
    private readonly TierListService _tiers;
    private readonly JobLogger _log;
    public string ActionName => "Build best team";

    public async Task ExecuteAsync(AccountContext context)
    {
        var nm = context.NetworkManager;
        var userId = context.AccountInfo.UserId;
        var team = _tiers.GetRecommendedTeam(nm.UserSyncData);
        if (team.Count == 0) return;
        var guids = team.Select(c => c.Guid).ToList();
        var before = nm.UserSyncData.GetMainTeam().Select(c => c.Guid).ToHashSet();
        foreach (var deck in Decks)
        {
            var existing = nm.UserSyncData.UserDeckDtoInfos?.FirstOrDefault(d => d.DeckUseContentType == deck);
            if (existing != null && existing.GetUserCharacterGuids().ToHashSet().SetEquals(guids)) continue;
            try
            {
                await nm.SendRequest<SaveUsedDeckRequest, SaveUsedDeckResponse>(new SaveUsedDeckRequest
                {
                    DeckUseContentType = deck, DeckNo = existing?.DeckNo ?? 1, UserCharacterGuids = guids
                });
            }
            catch (Exception e)
            {
                await _log.LogAsync(userId, $"Save {deck} team: {e.Message}", "Warning");
            }
        }

        if (!before.SetEquals(guids))
            await _tracker.TrackAsync(userId, GrowthKind.Team, "New team: " + string.Join(", ", team.Select(c => $"{CharName(c.CharacterId)} ({_tiers.GetTier(c.CharacterId) ?? "?"})")));
    }
}

/// <summary>
/// 按 CharacterRankUpMaterialMB 规则自动升星:
/// 同名素材 (重复卡) 随便用; 同属性素材只用 低强度 + 不在推荐队/主队/等级链接/锁定 的角色
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class SmartRankUpHandler : IGameActionHandler
{
    private readonly GrowthTracker _tracker;
    private readonly TierListService _tiers;
    private readonly PlayerSettingService _settings;
    private readonly JobLogger _log;
    public string ActionName => "Smart rank up";

    private static bool IsSpecialElement(long characterId) => CharacterTable.GetById(characterId)?.ElementType is ElementType.Light or ElementType.Dark;

    public async Task ExecuteAsync(AccountContext context)
    {
        var nm = context.NetworkManager;
        var userId = context.AccountInfo.UserId;
        var config = await _settings.GetSettingAsync<AutopilotConfig>(userId, AutopilotJob.SettingKey) ?? new();
        var rules = CharacterRankUpMaterialTable.GetArray();
        var done = 0;
        for (var pass = 0; pass < 40; pass++)
        {
            var data = nm.UserSyncData;
            var chars = data.UserCharacterDtoInfos;
            var protectedGuids = _tiers.GetRecommendedTeam(data).Concat(data.GetMainTeam()).Select(c => c.Guid)
                .Concat(data.UserLevelLinkMemberDtoInfos?.Select(m => m.UserCharacterGuid) ?? Enumerable.Empty<string>())
                .Concat(chars.Where(c => !data.IsLevelLinkMember(c.Guid)).OrderByDescending(c => c.Level).Take(5).Select(c => c.Guid))
                .ToHashSet();
            // 目标优先级: 主队/推荐队 → 强度 → 稀有度
            var targets = chars.OrderByDescending(c => protectedGuids.Contains(c.Guid)).ThenByDescending(c => _tiers.Score(c.CharacterId)).ThenByDescending(c => c.RarityFlags);
            CharacterRankUpMaterialInfo? job = null;
            var desc = "";
            foreach (var target in targets)
            {
                var cls = IsSpecialElement(target.CharacterId) ? ElementClassificationType.SpecialElement : ElementClassificationType.DefaultElement;
                var rule = rules.FirstOrDefault(r => r.RankFlags == target.RarityFlags && r.ElementClassification == cls);
                if (rule == null) continue;
                if (rule.UpType == RankUpType.ElementType && !config.RankUpWithFodder) continue;
                var element = CharacterTable.GetById(target.CharacterId).ElementType;
                var used = new HashSet<string> {target.Guid};

                UserCharacterDtoInfo? Pick(CharacterRarityFlags rarity)
                {
                    if (rarity == CharacterRarityFlags.None) return null;
                    var pool = chars.Where(c => !used.Contains(c.Guid) && c.RarityFlags == rarity && !c.IsLocked);
                    pool = rule.UpType == RankUpType.SameName
                        ? pool.Where(c => c.CharacterId == target.CharacterId && !protectedGuids.Contains(c.Guid))
                        // 同属性: 不能吃好角色, 也不能吃同名 (同名留着自己升)
                        : pool.Where(c => c.CharacterId != target.CharacterId && CharacterTable.GetById(c.CharacterId).ElementType == element
                                          && !protectedGuids.Contains(c.Guid) && _tiers.Score(c.CharacterId) <= 2 && _tiers.Score(c.CharacterId) < _tiers.Score(target.CharacterId) + 1);
                    var m = pool.OrderBy(c => _tiers.Score(c.CharacterId)).ThenBy(c => c.Level).FirstOrDefault();
                    if (m != null) used.Add(m.Guid);
                    return m;
                }

                var m1 = Pick(rule.MaterialRankFlags);
                if (m1 == null) continue;
                var m2 = rule.MaterialRankSecondFlags == CharacterRarityFlags.None ? null : Pick(rule.MaterialRankSecondFlags);
                if (rule.MaterialRankSecondFlags != CharacterRarityFlags.None && m2 == null) continue;
                job = new CharacterRankUpMaterialInfo {TargetGuid = target.Guid, MaterialGuid1 = m1.Guid, MaterialGuid2 = m2?.Guid};
                var fodder = rule.UpType == RankUpType.SameName ? "duplicates" : string.Join(" + ", new[] {m1, m2}.Where(x => x != null).Select(x => CharName(x!.CharacterId)));
                desc = $"{CharName(target.CharacterId)} {target.RarityFlags} → {rule.RankUpResultRarityFlags} (used {fodder})";
                break;
            }

            if (job == null) break;
            try
            {
                await nm.SendRequest<RankUpRequest, RankUpResponse>(new RankUpRequest {RankUpList = new() {job}});
                await _tracker.TrackAsync(userId, GrowthKind.RankUp, desc);
                done++;
            }
            catch (Exception e)
            {
                await _log.LogAsync(userId, $"Rank up failed ({desc}): {e.Message}", "Warning");
                break;
            }
        }

        if (done == 0) await _log.LogAsync(userId, "Rank up: nothing to merge");
    }
}

public record BannerAdvice(long GachaCaseId, string Name, long PickupCharacterId, string? PickupTier,
    long? DiamondButtonId, long DiamondCost, long? TicketButtonId, string Verdict, bool Worth,
    string? TicketName, long TicketsOwned, long TicketCost, long FreeDiamonds);

/// <summary>
/// 卡池评估: 有 S/SS 限定 UP 才值得砸免费钻石, 券随便用; 付费钻石永远不碰
/// </summary>
[RegisterSingleton]
[AutoConstructor]
public partial class BannerAdvisor
{
    private readonly TierListService _tiers;
    private readonly GrowthTracker _tracker;

    public async Task<List<BannerAdvice>> EvaluateAsync(NetworkManager nm, AutopilotConfig config)
    {
        var list = await nm.SendRequest<GetListRequest, GetListResponse>(new GetListRequest());
        var data = nm.UserSyncData;
        var diamonds = data.GetUserItemCount(ItemType.CurrencyFree); // 只算免费钻石
        var advice = new List<BannerAdvice>();
        foreach (var c in list.GachaCaseInfoList.Where(c => c.GachaCategoryType == GachaCategoryType.Character && c.GachaGroupType != GachaGroupType.HolyAngel))
        {
            var ui = GachaCaseUiTable.GetById(c.GachaCaseUiId);
            var pickup = ui?.PickUpCharacterId ?? 0;
            var name = ui == null ? c.GachaCaseId.ToString() : TextResourceTable.Get(ui.NameKey);
            var diamondBtn = c.GachaButtonInfoList?.Where(b => b.ConsumeUserItem?.ItemType == ItemType.CurrencyFree).MaxBy(b => b.LotteryCount);
            var ticketBtn = c.GachaButtonInfoList?.Where(b => b.ConsumeUserItem?.ItemType == ItemType.GachaTicket
                                                              && data.GetUserItemCount(ItemType.GachaTicket, b.ConsumeUserItem.ItemId) >= b.ConsumeUserItem.ItemCount)
                .MaxBy(b => b.LotteryCount);
            // 显示用: 这个卡池的券有多少 (不管够不够抽一次)
            var anyTicket = c.GachaButtonInfoList?.Where(b => b.ConsumeUserItem?.ItemType == ItemType.GachaTicket).MinBy(b => b.LotteryCount)?.ConsumeUserItem;
            var tier = pickup > 0 ? _tiers.GetTier(pickup) : null;
            var worth = pickup > 0 && _tiers.Score(pickup) >= config.GachaMinPickupTier;
            var verdict = pickup > 0
                ? worth ? $"Pull: {CharName(pickup)} is {tier}-tier" : $"Skip: {CharName(pickup)} is {tier ?? "unranked"} — save diamonds"
                : ticketBtn != null ? "Use tickets (free value)" : "Standard pool — not worth diamonds";
            var cost = diamondBtn?.ConsumeUserItem.ItemCount ?? 0;
            if (diamondBtn != null && worth && diamonds - cost < config.GachaDiamondReserve)
                verdict += $" (need {cost + config.GachaDiamondReserve - diamonds:N0} more diamonds above reserve)";
            advice.Add(new BannerAdvice(c.GachaCaseId, name, pickup, tier, diamondBtn?.GachaButtonId, cost, ticketBtn?.GachaButtonId, verdict, worth,
                anyTicket == null ? null : ItemUtil.GetItemName(ItemType.GachaTicket, anyTicket.ItemId),
                anyTicket == null ? 0 : data.GetUserItemCount(ItemType.GachaTicket, anyTicket.ItemId), anyTicket?.ItemCount ?? 0, diamonds));
        }

        return advice.OrderByDescending(a => a.Worth).ThenByDescending(a => a.TicketButtonId != null).ToList();
    }

    public async Task DrawAsync(long userId, NetworkManager nm, long gachaButtonId, string bannerName)
    {
        var resp = await nm.SendRequest<DrawRequest, DrawResponse>(new DrawRequest {GachaButtonId = gachaButtonId});
        var got = (resp.GachaRewardItemList ?? new()).Concat(resp.BonusRewardItemList ?? new())
            .Where(i => i.ItemType == ItemType.Character)
            .Select(i => $"{CharName(i.ItemId)} {i.CharacterRarityFlags}{(_tiers.GetTier(i.ItemId) is { } t ? $" [{t}]" : "")}")
            .ToList();
        await _tracker.TrackAsync(userId, GrowthKind.Gacha, $"{bannerName}: {(got.Count == 0 ? "no characters" : string.Join(", ", got))}");
    }
}

/// <summary>
/// 自动抽卡: 先用光所有券, 再只对 S/SS 限定 UP 用免费钻石, 保留储备
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class AutoPullGachaHandler : IGameActionHandler
{
    private readonly BannerAdvisor _advisor;
    private readonly PlayerSettingService _settings;
    private readonly TierListService _tiers;
    private readonly JobLogger _log;
    public string ActionName => "Pull gacha";

    public async Task ExecuteAsync(AccountContext context)
    {
        var nm = context.NetworkManager;
        var userId = context.AccountInfo.UserId;
        var config = await _settings.GetSettingAsync<AutopilotConfig>(userId, AutopilotJob.SettingKey) ?? new();
        var closed = new HashSet<long>(); // 没开的卡池 (GachaNotOpen) 跳过, 换下一个
        for (var round = 0; round < 20; round++)
        {
            var advice = (await _advisor.EvaluateAsync(nm, config))
                .Where(a => !closed.Contains(a.TicketButtonId ?? -1) && !closed.Contains(a.DiamondButtonId ?? -1)).ToList();
            var diamonds = nm.UserSyncData.GetUserItemCount(ItemType.CurrencyFree);
            var ticket = advice.FirstOrDefault(a => a.TicketButtonId != null);
            var pull = advice.FirstOrDefault(a => a.Worth && a.DiamondButtonId != null && diamonds - a.DiamondCost >= config.GachaDiamondReserve);
            // 余量: 钻石多于 GachaSurplusAbove 时, 多的部分抽其他卡池 (限定角色强度高的优先)
            var surplus = config.GachaSurplusAbove > 0
                ? advice.Where(a => a.DiamondButtonId != null && diamonds - a.DiamondCost >= config.GachaSurplusAbove)
                    .OrderByDescending(a => a.PickupCharacterId > 0 ? _tiers.Score(a.PickupCharacterId) : 0).FirstOrDefault()
                : null;
            var choice = ticket != null ? (ticket.TicketButtonId, ticket.Name) : pull != null ? (pull.DiamondButtonId, pull.Name) : surplus != null ? (surplus.DiamondButtonId, surplus.Name) : (null, "");
            var (btn, name) = choice;
            if (btn == null) break;
            var spent = advice.First(a => a.TicketButtonId == btn || a.DiamondButtonId == btn);
            await _log.LogAsync(userId, btn == spent.TicketButtonId
                ? $"Gacha {name}: using tickets ({spent.TicketName} ×{spent.TicketsOwned:N0} owned)"
                : $"Gacha {name}: spending {spent.DiamondCost:N0} of {diamonds:N0} free diamonds");
            try
            {
                await _advisor.DrawAsync(userId, nm, btn.Value, name);
            }
            catch (NetworkManager.ApiErrorException e) when (e.ErrorCode == ErrorCode.GachaNotOpen)
            {
                closed.Add(btn.Value);
                continue;
            }
            catch (Exception e)
            {
                await _log.LogAsync(userId, $"Gacha {name}: {e.Message}", "Warning");
                break;
            }

            await Task.Delay(1000);
        }
    }
}
