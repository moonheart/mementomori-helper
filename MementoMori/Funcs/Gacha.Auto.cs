using MementoMori.Ortega.Custom;
using MementoMori.Common.Localization;
using MementoMori.Ortega.Share.Data.ApiInterface.Gacha;
using MementoMori.Ortega.Share.Data.Gacha;
using ReactiveUI;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    public record BannerAdvice(GachaCaseInfo Case, string Name, long PickupCharacterId, string? PickupTier, GachaButtonInfo? DiamondButton, GachaButtonInfo? TicketButton, string Verdict, bool Worth);

    public List<BannerAdvice> LastBannerAdvice { get; private set; } = new();

    // 评估当前卡池: 有 S/SS 限定 UP 就值得砸钻石, 券随便用
    public async Task<List<BannerAdvice>> EvaluateBanners()
    {
        var list = await GetResponse<GetListRequest, GetListResponse>(new GetListRequest());
        var diamonds = UserSyncData.GetUserItemCount(ItemType.CurrencyFree); // 只算免费钻石, 不碰付费钻石
        var advice = new List<BannerAdvice>();
        foreach (var c in list.GachaCaseInfoList.Where(c => c.GachaCategoryType == GachaCategoryType.Character && c.GachaGroupType != GachaGroupType.HolyAngel))
        {
            var ui = GachaCaseUiTable.GetById(c.GachaCaseUiId);
            var pickup = ui?.PickUpCharacterId ?? 0;
            var name = ui == null ? c.GachaCaseId.ToString() : TextResourceTable.Get(ui.NameKey);
            var diamondBtn = c.GachaButtonInfoList?.Where(b => b.ConsumeUserItem?.ItemType == ItemType.CurrencyFree).MaxBy(b => b.LotteryCount);
            var ticketBtn = c.GachaButtonInfoList?.Where(b => b.ConsumeUserItem?.ItemType == ItemType.GachaTicket
                                                              && UserSyncData.GetUserItemCount(ItemType.GachaTicket, b.ConsumeUserItem.ItemId) >= b.ConsumeUserItem.ItemCount)
                .MaxBy(b => b.LotteryCount);
            var tier = pickup > 0 ? GetTier(pickup) : null;
            var worth = pickup > 0 && TierScore(pickup) >= GameConfig.AutoJob.GachaMinPickupTier;
            var verdict = pickup > 0
                ? worth ? string.Format(ResourceStrings.GachaVerdictPull, CharName(pickup), tier) : string.Format(ResourceStrings.GachaVerdictSkip, CharName(pickup), tier ?? ResourceStrings.GachaUnranked)
                : ticketBtn != null ? ResourceStrings.GachaVerdictTickets : ResourceStrings.GachaVerdictStandard;
            if (diamondBtn != null && worth && diamonds - diamondBtn.ConsumeUserItem.ItemCount < GameConfig.AutoJob.GachaDiamondReserve)
                verdict += " " + string.Format(ResourceStrings.GachaVerdictNeedMore, (diamondBtn.ConsumeUserItem.ItemCount + GameConfig.AutoJob.GachaDiamondReserve - diamonds).ToString("N0"));
            advice.Add(new BannerAdvice(c, name, pickup, tier, diamondBtn, ticketBtn, verdict, worth));
        }

        LastBannerAdvice = advice.OrderByDescending(a => a.Worth).ThenByDescending(a => a.TicketButton != null).ToList();
        this.RaisePropertyChanged(nameof(LastBannerAdvice));
        return LastBannerAdvice;
    }

    public async Task DrawBanner(long gachaButtonId, string bannerName)
    {
        var resp = await GetResponse<DrawRequest, DrawResponse>(new DrawRequest {GachaButtonId = gachaButtonId});
        var got = (resp.GachaRewardItemList ?? new()).Concat(resp.BonusRewardItemList ?? new())
            .Where(i => i.ItemType == ItemType.Character)
            .Select(i => $"{CharName(i.ItemId)} {i.CharacterRarityFlags}{(GetTier(i.ItemId) is { } t ? $" [{t}]" : "")}")
            .ToList();
        Track(GrowthKind.Gacha, $"{bannerName}: {(got.Count == 0 ? ResourceStrings.GachaNoCharacters : string.Join(", ", got))}");
    }

    public async Task AutoPullGacha()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            for (var round = 0; round < 20 && !token.IsCancellationRequested; round++)
            {
                var advice = await EvaluateBanners();
                var diamonds = UserSyncData.GetUserItemCount(ItemType.CurrencyFree); // 只算免费钻石, 不碰付费钻石
                // 1) 券: 白金/属性/命运券 全部用掉
                var ticket = advice.FirstOrDefault(a => a.TicketButton != null);
                // 2) 钻石: 只砸 S/SS 限定 UP, 并保留储备
                var pull = advice.FirstOrDefault(a => a.Worth && a.DiamondButton != null
                                                      && diamonds - a.DiamondButton.ConsumeUserItem.ItemCount >= GameConfig.AutoJob.GachaDiamondReserve);
                var (btn, name) = ticket != null ? (ticket.TicketButton!, ticket.Name) : pull != null ? (pull.DiamondButton!, pull.Name) : (null, "");
                if (btn == null) break;
                try
                {
                    await DrawBanner(btn.GachaButtonId, name);
                }
                catch (Exception e)
                {
                    log($"Gacha {name}: {e.Message}");
                    break;
                }

                await Task.Delay(Math.Max(GameConfig.AutoRequestDelay, 1000), token);
            }
        });
    }
}
