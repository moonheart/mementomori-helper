using MementoMori.Ortega.Custom;
using MementoMori.Ortega.Share.Data.ApiInterface.Character;
using MementoMori.Ortega.Share.Data.ApiInterface.User;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Data.DtoInfo;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    private static readonly DeckUseContentType[] TeamDecks =
    {
        DeckUseContentType.Boss, DeckUseContentType.Auto, DeckUseContentType.Infinite, DeckUseContentType.BattleLeagueOffense, DeckUseContentType.BattleLeagueDefense,
        DeckUseContentType.LocalRaid, DeckUseContentType.DungeonBattle
    };

    // 把推荐队伍存成 主线/挂机/无穷塔/竞技场/幻影神殿/时空洞窟 的队伍
    public async Task AutoBuildTeam()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var team = GetRecommendedTeam();
            if (team.Count == 0) return;
            var guids = team.Select(c => c.Guid).ToList();
            var before = GetMainTeam().Select(c => c.Guid).ToHashSet();
            foreach (var deck in TeamDecks)
            {
                var existing = UserSyncData.UserDeckDtoInfos?.FirstOrDefault(d => d.DeckUseContentType == deck);
                if (existing != null && existing.GetUserCharacterGuids().ToHashSet().SetEquals(guids)) continue;
                try
                {
                    await GetResponse<SaveUsedDeckRequest, SaveUsedDeckResponse>(new SaveUsedDeckRequest
                    {
                        DeckUseContentType = deck, DeckNo = existing?.DeckNo ?? 1, UserCharacterGuids = guids
                    });
                }
                catch (Exception e)
                {
                    log($"Save {deck} team: {e.Message}");
                }

                await Task.Delay(GameConfig.AutoRequestDelay, token);
            }

            if (!before.SetEquals(guids))
                Track(GrowthKind.Team, "New team: " + string.Join(", ", team.Select(c => $"{CharName(c.CharacterId)} ({GetTier(c.CharacterId) ?? "?"})")));
        });
    }

    // 素材: 强度 C/B 的角色; 没有强度的只允许 R/R+ (没评级的 SR 可能是玩家在练的, 不能吃)
    private bool IsFodderTier(UserCharacterDtoInfo c) => TierScore(c.CharacterId) switch
    {
        1 or 2 => true,
        0 => c.RarityFlags <= CharacterRarityFlags.RPlus,
        _ => false
    };

    private static bool IsSpecialElement(long characterId) => CharacterTable.GetById(characterId)?.ElementType is ElementType.Light or ElementType.Dark;

    // 按游戏的 CharacterRankUpMaterialMB 规则自动升星:
    // 同名素材 (重复卡) 随便用; 同属性素材只用 低强度 + 不在推荐队/主队/等级链接/锁定 的角色
    public async Task AutoRankUpSmart()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var rules = CharacterRankUpMaterialTable.GetArray();
            var done = 0;
            for (var pass = 0; pass < 40 && !token.IsCancellationRequested; pass++)
            {
                var chars = UserSyncData.UserCharacterDtoInfos;
                var protectedGuids = GetRecommendedTeam().Concat(GetMainTeam()).Select(c => c.Guid)
                    .Concat(UserSyncData.UserLevelLinkMemberDtoInfos?.Select(m => m.UserCharacterGuid) ?? Enumerable.Empty<string>())
                    .Concat(chars.Where(c => !UserSyncData.IsLevelLinkMember(c.Guid)).OrderByDescending(c => c.Level).Take(5).Select(c => c.Guid))
                    .ToHashSet();
                // 目标优先级: 主队/推荐队 → 强度 → 稀有度
                var targets = chars.OrderByDescending(c => protectedGuids.Contains(c.Guid)).ThenByDescending(c => TierScore(c.CharacterId)).ThenByDescending(c => c.RarityFlags);
                CharacterRankUpMaterialInfo? job = null;
                string desc = "";
                foreach (var target in targets)
                {
                    var cls = IsSpecialElement(target.CharacterId) ? ElementClassificationType.SpecialElement : ElementClassificationType.DefaultElement;
                    var rule = rules.FirstOrDefault(r => r.RankFlags == target.RarityFlags && r.ElementClassification == cls);
                    if (rule == null) continue;
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
                                              && !protectedGuids.Contains(c.Guid) && IsFodderTier(c) && TierScore(c.CharacterId) < TierScore(target.CharacterId) + 1);
                        var m = pool.OrderBy(c => TierScore(c.CharacterId)).ThenBy(c => c.Level).FirstOrDefault();
                        if (m != null) used.Add(m.Guid);
                        return m;
                    }

                    if (rule.UpType == RankUpType.ElementType && !GameConfig.AutoJob.AutoRankUpWithFodder) continue;
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
                    await GetResponse<RankUpRequest, RankUpResponse>(new RankUpRequest {RankUpList = new() {job}});
                    Track(GrowthKind.RankUp, desc);
                    done++;
                }
                catch (Exception e)
                {
                    log($"Rank up failed ({desc}): {e.Message}");
                    break;
                }

                await Task.Delay(GameConfig.AutoRequestDelay, token);
            }

            if (done == 0) log("Rank up: nothing to merge");
        });
    }
}
