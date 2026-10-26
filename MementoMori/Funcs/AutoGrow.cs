using MementoMori.Common.Localization;
using MementoMori.Ortega.Share.Data.ApiInterface.Equipment;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Custom;
using CharLevelUp = MementoMori.Ortega.Share.Data.ApiInterface.Character;
using LevelLinkApi = MementoMori.Ortega.Share.Data.ApiInterface.LevelLink;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    // 先把等级链接空位填满, 再升级前5名
    public async Task AutoLevelUp()
    {
        await AutoFillLevelLink();
        await AutoLevelUpCharacter();
    }

    // 主队换最好的装备, 再把主队装备强化到上限
    public async Task AutoGear()
    {
        await AutoEquipBest();
        await AutoReinforceTeamEquipment();
    }

    private bool CanAfford(IEnumerable<UserItem> items) =>
        items.All(i => i.ItemCount <= 0 || UserSyncData.GetUserItemCount(i.ItemType, i.ItemId) >= i.ItemCount);

    // 主队角色 (Boss deck → Auto deck), 没有就用等级最高的5个
    public List<UserCharacterDtoInfo> GetMainTeam()
    {
        if (UserSyncData?.UserCharacterDtoInfos == null) return new();
        // battle/boss 只传 QuestId, 服务器用的是 Boss 队
        var team = UserSyncData.GetDeckUserCharacterDtoInfos(DeckUseContentType.Boss);
        if (team.Count == 0) team = UserSyncData.GetDeckUserCharacterDtoInfos(DeckUseContentType.Auto);
        return team.Count > 0 ? team : UserSyncData.UserCharacterDtoInfos.OrderByDescending(d => d.Level).Take(5).ToList();
    }

    public long GetEffectiveLevel(UserCharacterDtoInfo c) =>
        UserSyncData.IsLevelLinkMember(c.Guid) ? UserSyncData.GetLevelLinkLevel(c.CharacterId) : c.Level;

    // 只要名字, 不带称号 ("Lunalynn" 而不是 "LunalynnThe Witch of ...")
    public static string CharName(long characterId)
    {
        var mb = CharacterTable.GetById(characterId);
        return mb == null ? characterId.ToString() : TextResourceTable.Get(mb.NameKey);
    }

    public static string EquipName(long equipmentId)
    {
        var mb = EquipmentTable.GetById(equipmentId);
        return mb == null ? equipmentId.ToString() : $"{TextResourceTable.Get(mb.NameKey)} ({mb.RarityFlags} Lv{mb.EquipmentLv})";
    }

    public async Task AutoLevelUpCharacter()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var link = UserSyncData.UserLevelLinkDtoInfo;
            if (link is {IsPartyMode: true})
            {
                // 组队模式: 每个 LevelLinkMB 是一步, 同等级内 addExp, 跨等级 levelUp
                var before = $"{link.PartyLevel}.{link.PartySubLevel}";
                var table = LevelLinkTable.GetArray().OrderBy(d => d.Id).ToList();
                while (!token.IsCancellationRequested)
                {
                    link = UserSyncData.UserLevelLinkDtoInfo;
                    if (link.PartyLevel >= link.PartyMaxLevel && link.PartyMaxLevel > 0) break;
                    var cur = table.FindIndex(d => d.PartyLevel == link.PartyLevel && d.PartySubLevel == link.PartySubLevel);
                    if (cur < 0 || cur + 1 >= table.Count) break;
                    var next = table[cur + 1];
                    if (!CanAfford(next.RequiredLevelUpItems)) break;
                    if (next.PartyLevel == link.PartyLevel)
                        await GetResponse<LevelLinkApi.AddExpRequest, LevelLinkApi.AddExpResponse>(new LevelLinkApi.AddExpRequest());
                    else
                        await GetResponse<LevelLinkApi.LevelUpRequest, LevelLinkApi.LevelUpResponse>(new LevelLinkApi.LevelUpRequest());
                    await Task.Delay(GameConfig.AutoRequestDelay, token);
                }

                link = UserSyncData.UserLevelLinkDtoInfo;
                var after = $"{link.PartyLevel}.{link.PartySubLevel}";
                if (after != before) Track(GrowthKind.LevelLink, string.Format(ResourceStrings.ActPartyLevel, before, after));
                return;
            }

            // 普通模式: 只升不在等级链接里的前5名, 每次升最低的那个 (等级链接等级 = 前5名最低等级)
            var start = UserSyncData.UserCharacterDtoInfos.ToDictionary(d => d.Guid, d => d.Level);
            var linkBefore = link?.PartyLevel ?? 0;
            var skip = new HashSet<string>();
            while (!token.IsCancellationRequested)
            {
                var c = UserSyncData.UserCharacterDtoInfos
                    .Where(d => !UserSyncData.IsLevelLinkMember(d.Guid))
                    .OrderByDescending(d => d.Level).Take(5)
                    .Where(d => !skip.Contains(d.Guid))
                    .Where(d => d.Level < OrtegaConst.LevelLink.MaxCharacterLevel.GetValueOrDefault(d.RarityFlags, 240))
                    .MinBy(d => d.Level);
                if (c == null) break;
                var cost = CharacterLevelTable.GetArray().FirstOrDefault(d => d.Level == c.Level + 1);
                if (cost == null || !CanAfford(cost.RequiredItemList)) break;
                try
                {
                    await GetResponse<CharLevelUp.LevelUpRequest, CharLevelUp.LevelUpResponse>(new CharLevelUp.LevelUpRequest {UserCharacterGuid = c.Guid});
                }
                catch (Exception e)
                {
                    // 稀有度等级上限没精确算, 服务器报错就跳过这个角色
                    log($"LevelUp {CharName(c.CharacterId)}: {e.Message}");
                    skip.Add(c.Guid);
                }

                await Task.Delay(GameConfig.AutoRequestDelay, token);
            }

            var changed = UserSyncData.UserCharacterDtoInfos
                .Where(d => start.TryGetValue(d.Guid, out var lv) && d.Level > lv)
                .Select(d => $"{CharName(d.CharacterId)} {start[d.Guid]}→{d.Level}").ToList();
            if (changed.Count > 0) Track(GrowthKind.Level, string.Join(", ", changed));
            var linkAfter = UserSyncData.UserLevelLinkDtoInfo?.PartyLevel ?? 0;
            if (linkAfter > linkBefore) Track(GrowthKind.LevelLink, string.Format(ResourceStrings.ActLevelLink, linkBefore, linkAfter));
            if (changed.Count == 0) log("Level up: not enough gold/potions");
        });
    }

    // 把空的等级链接格子填满 (主队优先, 然后稀有度高的), 不动前5名基础角色
    public async Task AutoFillLevelLink()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var link = UserSyncData.UserLevelLinkDtoInfo;
            if (link == null) return;
            var members = UserSyncData.UserLevelLinkMemberDtoInfos ?? new();
            var baseFive = UserSyncData.UserCharacterDtoInfos.Where(d => !UserSyncData.IsLevelLinkMember(d.Guid))
                .OrderByDescending(d => d.Level).Take(5).Select(d => d.Guid).ToHashSet();
            var team = GetMainTeam().Select(d => d.Guid).ToHashSet();
            var candidates = UserSyncData.UserCharacterDtoInfos
                .Where(d => !baseFive.Contains(d.Guid) && !UserSyncData.IsLevelLinkMember(d.Guid))
                .OrderByDescending(d => team.Contains(d.Guid)).ThenByDescending(d => d.RarityFlags).ThenByDescending(d => d.Level)
                .Select(d => d.CharacterId).Distinct().ToList(); // 同角色只放一个
            var freeCells = Enumerable.Range(1, link.MemberMaxCount)
                .Where(n => !members.Any(m => m.CellNo == n && !string.IsNullOrEmpty(m.UserCharacterGuid))).ToList();
            foreach (var (cell, charId) in freeCells.Zip(candidates))
            {
                if (token.IsCancellationRequested) break;
                if (members.Any(m => m.CharacterId == charId)) continue;
                var c = UserSyncData.UserCharacterDtoInfos.Where(d => d.CharacterId == charId && !baseFive.Contains(d.Guid)).MaxBy(d => d.RarityFlags);
                if (c == null) continue;
                try
                {
                    await GetResponse<LevelLinkApi.SetMemberRequest, LevelLinkApi.SetMemberResponse>(new LevelLinkApi.SetMemberRequest {LevelLinkCellNo = cell, UserCharacterGuid = c.Guid});
                    Track(GrowthKind.LevelLink, string.Format(ResourceStrings.ActLevelLinkJoin, CharName(c.CharacterId), cell));
                }
                catch (Exception e)
                {
                    log($"Level Link slot {cell}: {e.Message}"); // 冷却中的格子会报错, 跳过
                }

                await Task.Delay(GameConfig.AutoRequestDelay, token);
            }
        });
    }

    // 给主队每个位置换上能穿的战力最高的装备 (继承强化等级)
    public async Task AutoEquipBest()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            foreach (var c in GetMainTeam())
            {
                var job = CharacterTable.GetById(c.CharacterId).JobFlags;
                var level = GetEffectiveLevel(c);
                var equipped = UserSyncData.GetUserEquipmentDtoInfoSlotTypeDictionaryByCharacterGuid(c.Guid);
                var changes = new List<EquipmentChangeInfo>();
                var descriptions = new List<string>();
                foreach (var (slot, current) in equipped)
                {
                    var currentPower = current == null ? -1 : EquipmentTable.GetById(current.EquipmentId).PerformancePoint;
                    bool Fits(EquipmentMB mb) => mb.SlotType == slot && (mb.EquippedJobFlags & job) != 0 && mb.EquipmentLv <= level && mb.PerformancePoint > currentPower;

                    // 背包里的 (未强化的叠在 item 里, 强化过的是没主人的 EquipmentDtoInfo)
                    var bestItem = UserSyncData.UserItemDtoInfo
                        .Where(d => d.ItemType == ItemType.Equipment && d.ItemCount > 0)
                        .Select(d => EquipmentTable.GetById(d.ItemId)).Where(mb => mb != null && Fits(mb))
                        .MaxBy(mb => mb.PerformancePoint);
                    var bestDto = UserSyncData.UserEquipmentDtoInfos
                        .Where(d => string.IsNullOrEmpty(d.CharacterGuid))
                        .Select(d => (dto: d, mb: EquipmentTable.GetById(d.EquipmentId))).Where(x => x.mb != null && Fits(x.mb))
                        .OrderByDescending(x => x.mb.PerformancePoint).FirstOrDefault();
                    if (bestItem == null && bestDto.dto == null) continue;

                    var useDto = bestDto.dto != null && (bestItem == null || bestDto.mb.PerformancePoint >= bestItem.PerformancePoint);
                    var mb = useDto ? bestDto.mb : bestItem;
                    changes.Add(new EquipmentChangeInfo
                    {
                        EquipmentGuid = useDto ? bestDto.dto.Guid : null,
                        EquipmentId = mb.Id,
                        EquipmentSlotType = slot,
                        // 新装备没强化过才继承, 免得覆盖
                        IsInherit = current != null && (!useDto || bestDto.dto.ReinforcementLv == 0),
                        // null = 全部继承 (选择性继承要解锁后才能用, 否则 232061)
                        EquipmentInheritanceTypeList = null
                    });
                    descriptions.Add(current == null ? $"{TextResourceTable.Get(slot)}: {EquipName(mb.Id)}" : $"{TextResourceTable.Get(slot)}: {EquipName(current.EquipmentId)} → {EquipName(mb.Id)}");
                }

                if (changes.Count == 0) continue;
                try
                {
                    await GetResponse<ChangeEquipmentRequest, ChangeEquipmentResponse>(new ChangeEquipmentRequest
                    {
                        UserCharacterGuid = c.Guid, EquipmentChangeInfos = changes
                    });
                    Track(GrowthKind.Equip, $"{CharName(c.CharacterId)} — {string.Join("; ", descriptions)}");
                }
                catch (Exception e)
                {
                    log($"Equip {CharName(c.CharacterId)}: {e.Message}");
                }

                await Task.Delay(GameConfig.AutoRequestDelay, token);
            }
        });
    }

    // 主队装备轮流 +1 直到没钱/材料 (装备等级是强化上限)
    public async Task AutoReinforceTeamEquipment()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var team = GetMainTeam().ToDictionary(c => c.Guid, c => c.CharacterId);
            var start = UserSyncData.UserEquipmentDtoInfos.Where(d => team.ContainsKey(d.CharacterGuid ?? "")).ToDictionary(d => d.Guid, d => d.ReinforcementLv);
            while (!token.IsCancellationRequested)
            {
                var e = UserSyncData.UserEquipmentDtoInfos
                    .Where(d => team.ContainsKey(d.CharacterGuid ?? "") && EquipmentTable.GetById(d.EquipmentId).EquipmentLv > d.ReinforcementLv)
                    .MinBy(d => d.ReinforcementLv);
                if (e == null) break;
                var mat = EquipmentReinforcementMaterialTable.GetArray().FirstOrDefault(d => d.ReinforcementLevel == e.ReinforcementLv + 1);
                if (mat == null) break;
                var cost = EquipmentTable.GetById(e.EquipmentId).SlotType == EquipmentSlotType.Weapon ? mat.WeaponRequiredItemList : mat.OthersRequiredItemList;
                if (!CanAfford(cost)) break;
                await GetResponse<ReinforcementRequest, ReinforcementResponse>(new ReinforcementRequest {EquipmentGuid = e.Guid, NumberOfTimes = 1});
                await Task.Delay(GameConfig.AutoRequestDelay, token);
            }

            var perChar = UserSyncData.UserEquipmentDtoInfos
                .Where(d => team.ContainsKey(d.CharacterGuid ?? "") && start.TryGetValue(d.Guid, out var lv) && d.ReinforcementLv > lv)
                .GroupBy(d => d.CharacterGuid)
                .Select(g => $"{CharName(team[g.Key])}: " + string.Join(", ", g.Select(d => $"{TextResourceTable.Get(EquipmentTable.GetById(d.EquipmentId).SlotType)} +{start[d.Guid]}→+{d.ReinforcementLv}")))
                .ToList();
            if (perChar.Count > 0) Track(GrowthKind.Reinforce, string.Join(" | ", perChar));
        });
    }
}
