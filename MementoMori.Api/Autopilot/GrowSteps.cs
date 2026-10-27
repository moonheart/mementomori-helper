using MementoMori.Api.Extensions;
using MementoMori.Api.Handlers;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Equipment;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Custom;
using static MementoMori.Api.Autopilot.AutopilotExtensions;
using CharacterApi = MementoMori.Ortega.Share.Data.ApiInterface.Character;
using LevelLinkApi = MementoMori.Ortega.Share.Data.ApiInterface.LevelLink;

namespace MementoMori.Api.Autopilot;

/// <summary>
/// 升级: 组队模式按 LevelLinkMB 一步步升; 普通模式升前5名里最低的那个
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class AutoLevelUpHandler : IGameActionHandler
{
    private readonly GrowthTracker _tracker;
    private readonly JobLogger _log;
    public string ActionName => "Level up";

    public async Task ExecuteAsync(AccountContext context)
    {
        var nm = context.NetworkManager;
        var userId = context.AccountInfo.UserId;
        var data = nm.UserSyncData;
        var link = data.UserLevelLinkDtoInfo;
        if (link is {IsPartyMode: true})
        {
            // 组队模式: 同等级内 addExp, 跨等级 levelUp
            var before = $"{link.PartyLevel}.{link.PartySubLevel}";
            var table = LevelLinkTable.GetArray().OrderBy(d => d.Id).ToList();
            while (true)
            {
                link = nm.UserSyncData.UserLevelLinkDtoInfo;
                if (link.PartyLevel >= link.PartyMaxLevel && link.PartyMaxLevel > 0) break;
                var cur = table.FindIndex(d => d.PartyLevel == link.PartyLevel && d.PartySubLevel == link.PartySubLevel);
                if (cur < 0 || cur + 1 >= table.Count) break;
                var next = table[cur + 1];
                if (!nm.UserSyncData.CanAfford(next.RequiredLevelUpItems)) break;
                if (next.PartyLevel == link.PartyLevel)
                    await nm.SendRequest<LevelLinkApi.AddExpRequest, LevelLinkApi.AddExpResponse>(new LevelLinkApi.AddExpRequest());
                else
                    await nm.SendRequest<LevelLinkApi.LevelUpRequest, LevelLinkApi.LevelUpResponse>(new LevelLinkApi.LevelUpRequest());
            }

            link = nm.UserSyncData.UserLevelLinkDtoInfo;
            var after = $"{link.PartyLevel}.{link.PartySubLevel}";
            if (after != before) await _tracker.TrackAsync(userId, GrowthKind.LevelLink, $"Party level {before} → {after}");
            return;
        }

        // 普通模式: 等级链接等级 = 前5名最低等级, 所以每次升最低的那个
        var start = data.UserCharacterDtoInfos.ToDictionary(d => d.Guid, d => d.Level);
        var linkBefore = link?.PartyLevel ?? 0;
        var skip = new HashSet<string>();
        while (true)
        {
            data = nm.UserSyncData;
            var c = data.UserCharacterDtoInfos
                .Where(d => !data.IsLevelLinkMember(d.Guid))
                .OrderByDescending(d => d.Level).Take(5)
                .Where(d => !skip.Contains(d.Guid))
                .Where(d => d.Level < OrtegaConst.LevelLink.MaxCharacterLevel.GetValueOrDefault(d.RarityFlags, 240))
                .MinBy(d => d.Level);
            if (c == null) break;
            var cost = CharacterLevelTable.GetArray().FirstOrDefault(d => d.Level == c.Level + 1);
            if (cost == null || !data.CanAfford(cost.RequiredItemList)) break;
            try
            {
                await nm.SendRequest<CharacterApi.LevelUpRequest, CharacterApi.LevelUpResponse>(new CharacterApi.LevelUpRequest {UserCharacterGuid = c.Guid});
            }
            catch (Exception e)
            {
                // ponytail: 稀有度上限没精确算, 报错就跳过这个角色
                await _log.LogAsync(userId, $"Level up {CharName(c.CharacterId)}: {e.Message}", "Warning");
                skip.Add(c.Guid);
            }
        }

        data = nm.UserSyncData;
        var changed = data.UserCharacterDtoInfos
            .Where(d => start.TryGetValue(d.Guid, out var lv) && d.Level > lv)
            .Select(d => $"{CharName(d.CharacterId)} {start[d.Guid]}→{d.Level}").ToList();
        if (changed.Count > 0) await _tracker.TrackAsync(userId, GrowthKind.Level, string.Join(", ", changed));
        var linkAfter = data.UserLevelLinkDtoInfo?.PartyLevel ?? 0;
        if (linkAfter > linkBefore) await _tracker.TrackAsync(userId, GrowthKind.LevelLink, $"Level Link {linkBefore} → {linkAfter}");
        if (changed.Count == 0) await _log.LogAsync(userId, "Level up: not enough gold/potions");
    }
}

/// <summary>
/// 给主队每个位置换上能穿的战力最高的装备 (继承强化等级)
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class AutoEquipBestHandler : IGameActionHandler
{
    private readonly GrowthTracker _tracker;
    private readonly JobLogger _log;
    public string ActionName => "Equip best gear";

    public async Task ExecuteAsync(AccountContext context)
    {
        var nm = context.NetworkManager;
        var userId = context.AccountInfo.UserId;
        foreach (var c in nm.UserSyncData.GetMainTeam())
        {
            var data = nm.UserSyncData;
            var job = CharacterTable.GetById(c.CharacterId).JobFlags;
            var level = data.GetEffectiveLevel(c);
            var changes = new List<EquipmentChangeInfo>();
            var descriptions = new List<string>();
            foreach (var (slot, current) in data.GetEquipmentBySlot(c.Guid))
            {
                var currentPower = current == null ? -1 : EquipmentTable.GetById(current.EquipmentId).PerformancePoint;
                bool Fits(EquipmentMB mb) => mb.SlotType == slot && (mb.EquippedJobFlags & job) != 0 && mb.EquipmentLv <= level && mb.PerformancePoint > currentPower;

                // 背包里的: 未强化的叠在 item 里, 强化过的是没主人的 EquipmentDtoInfo
                var bestItem = data.UserItemDtoInfo
                    .Where(d => d.ItemType == ItemType.Equipment && d.ItemCount > 0)
                    .Select(d => EquipmentTable.GetById(d.ItemId)).Where(mb => mb != null && Fits(mb))
                    .MaxBy(mb => mb.PerformancePoint);
                var bestDto = data.UserEquipmentDtoInfos
                    .Where(d => string.IsNullOrEmpty(d.CharacterGuid))
                    .Select(d => (dto: d, mb: EquipmentTable.GetById(d.EquipmentId))).Where(x => x.mb != null && Fits(x.mb))
                    .OrderByDescending(x => x.mb.PerformancePoint).FirstOrDefault();
                if (bestItem == null && bestDto.dto == null) continue;

                var useDto = bestDto.dto != null && (bestItem == null || bestDto.mb.PerformancePoint >= bestItem.PerformancePoint);
                var mb = useDto ? bestDto.mb : bestItem!;
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
                descriptions.Add(current == null ? $"{slot}: {EquipName(mb.Id)}" : $"{slot}: {EquipName(current.EquipmentId)} → {EquipName(mb.Id)}");
            }

            if (changes.Count == 0) continue;
            try
            {
                await nm.SendRequest<ChangeEquipmentRequest, ChangeEquipmentResponse>(new ChangeEquipmentRequest
                {
                    UserCharacterGuid = c.Guid, EquipmentChangeInfos = changes
                });
                await _tracker.TrackAsync(userId, GrowthKind.Equip, $"{CharName(c.CharacterId)} — {string.Join("; ", descriptions)}");
            }
            catch (Exception e)
            {
                await _log.LogAsync(userId, $"Equip {CharName(c.CharacterId)}: {e.Message}", "Warning");
            }
        }
    }
}

/// <summary>
/// 主队装备轮流 +1 直到没钱/材料 (装备等级是强化上限)
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class AutoReinforceTeamHandler : IGameActionHandler
{
    private readonly GrowthTracker _tracker;
    public string ActionName => "Reinforce team gear";

    public async Task ExecuteAsync(AccountContext context)
    {
        var nm = context.NetworkManager;
        var team = nm.UserSyncData.GetMainTeam().ToDictionary(c => c.Guid, c => c.CharacterId);
        bool OnTeam(UserEquipmentDtoInfo d) => team.ContainsKey(d.CharacterGuid ?? "");
        var start = nm.UserSyncData.UserEquipmentDtoInfos.Where(OnTeam).ToDictionary(d => d.Guid, d => d.ReinforcementLv);
        while (true)
        {
            var e = nm.UserSyncData.UserEquipmentDtoInfos
                .Where(d => OnTeam(d) && EquipmentTable.GetById(d.EquipmentId).EquipmentLv > d.ReinforcementLv)
                .MinBy(d => d.ReinforcementLv);
            if (e == null) break;
            var mat = EquipmentReinforcementMaterialTable.GetArray().FirstOrDefault(d => d.ReinforcementLevel == e.ReinforcementLv + 1);
            if (mat == null) break;
            var cost = EquipmentTable.GetById(e.EquipmentId).SlotType == EquipmentSlotType.Weapon ? mat.WeaponRequiredItemList : mat.OthersRequiredItemList;
            if (!nm.UserSyncData.CanAfford(cost)) break;
            await nm.SendRequest<ReinforcementRequest, ReinforcementResponse>(new ReinforcementRequest {EquipmentGuid = e.Guid, NumberOfTimes = 1});
        }

        var perChar = nm.UserSyncData.UserEquipmentDtoInfos
            .Where(d => OnTeam(d) && start.TryGetValue(d.Guid, out var lv) && d.ReinforcementLv > lv)
            .GroupBy(d => d.CharacterGuid)
            .Select(g => $"{CharName(team[g.Key])}: " + string.Join(", ", g.Select(d => $"{EquipmentTable.GetById(d.EquipmentId).SlotType} +{start[d.Guid]}→+{d.ReinforcementLv}")))
            .ToList();
        if (perChar.Count > 0) await _tracker.TrackAsync(context.AccountInfo.UserId, GrowthKind.Reinforce, string.Join(" | ", perChar));
    }
}

/// <summary>
/// 符石: 合成重复的低级符石 → 主队装备开第一个孔 → 空孔塞最高级的 (同一件装备不放同类)
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class AutoSphereHandler : IGameActionHandler
{
    // ponytail: 合成只花金币 (Lv1-5), 金币低于这个数就不合成, 优先留给升级
    private const long ComposeGoldReserve = 1_000_000;

    private readonly GrowthTracker _tracker;
    private readonly JobLogger _log;
    public string ActionName => "Socket runes";

    public async Task ExecuteAsync(AccountContext context)
    {
        var nm = context.NetworkManager;
        var userId = context.AccountInfo.UserId;
        var team = nm.UserSyncData.GetMainTeam().ToDictionary(c => c.Guid, c => c.CharacterId);
        long Gold() => nm.UserSyncData.GetUserItemCount(ItemType.Gold, 1);
        List<(long id, long count)> Owned() => nm.UserSyncData.UserItemDtoInfo
            .Where(d => d.ItemType == ItemType.Sphere && d.ItemCount > 0)
            .Select(d => (d.ItemId, d.ItemCount)).ToList();

        var cats = Owned().Where(s => s.count >= 2 && SphereTable.GetById(s.id).Lv <= 5)
            .Select(s => SphereTable.GetById(s.id).CategoryId).Distinct().ToList();
        if (cats.Count > 0 && Gold() >= ComposeGoldReserve)
        {
            try
            {
                var resp = await nm.SendRequest<ComposeManySphereRequest, ComposeManySphereResponse>(new ComposeManySphereRequest {CategoryIdList = cats});
                if (resp.IsSucceeded)
                    await _tracker.TrackAsync(userId, GrowthKind.Equip, "[Rune] Combined: " + string.Join(", ", resp.ResultItemList.Select(i => $"{TextResourceTable.Get(SphereTable.GetById(i.ItemId).NameKey)} ×{i.ItemCount}")));
            }
            catch (Exception e)
            {
                await _log.LogAsync(userId, $"Rune combine: {e.Message}", "Warning");
            }
        }

        var placed = new List<string>();
        foreach (var eq in nm.UserSyncData.UserEquipmentDtoInfos.Where(d => team.ContainsKey(d.CharacterGuid ?? "")).ToList())
        {
            if (Owned().Count == 0) break;
            var mb = EquipmentTable.GetById(eq.EquipmentId);
            var who = $"{CharName(team[eq.CharacterGuid])} {mb.SlotType}";
            if (eq.SphereUnlockedCount == 0)
            {
                if (mb.GoldRequiredToOpeningFirstSphereSlot <= 0 || Gold() < mb.GoldRequiredToOpeningFirstSphereSlot) continue;
                try
                {
                    await nm.SendRequest<UnlockSphereSlotRequest, UnlockSphereSlotResponse>(new UnlockSphereSlotRequest {EquipmentGuid = eq.Guid, UnlockCount = 1});
                }
                catch (Exception e)
                {
                    await _log.LogAsync(userId, $"Rune slot {who}: {e.Message}", "Warning");
                    continue;
                }
            }

            var current = nm.UserSyncData.UserEquipmentDtoInfos.First(d => d.Guid == eq.Guid);
            var slots = new[] {current.SphereId1, current.SphereId2, current.SphereId3, current.SphereId4};
            var used = slots.Where(id => id > 0).Select(id => SphereTable.GetById(id).CategoryId).ToHashSet();
            var pool = Owned().SelectMany(s => Enumerable.Repeat(s.id, (int) Math.Min(s.count, 4))).ToList();
            var added = new List<string>();
            // 攻击类符石只能放 武器/饰品/手, 防御类只能放 头/甲/鞋 (ErrorCode 232001)
            var attackSlot = mb.SlotType is EquipmentSlotType.Weapon or EquipmentSlotType.Sub or EquipmentSlotType.Gauntlet;
            for (var i = 0; i < current.SphereUnlockedCount && i < 4; i++)
            {
                if (slots[i] > 0) continue;
                var best = pool.Where(id => !used.Contains(SphereTable.GetById(id).CategoryId) && SphereTable.GetById(id).IsAttackType == attackSlot)
                    .OrderByDescending(id => SphereTable.GetById(id).Lv).FirstOrDefault();
                if (best == 0) break;
                slots[i] = best;
                pool.Remove(best);
                used.Add(SphereTable.GetById(best).CategoryId);
                added.Add($"{who}: {TextResourceTable.Get(SphereTable.GetById(best).NameKey)}");
            }

            if (added.Count == 0) continue;
            try
            {
                await nm.SendRequest<ChangeSphereRequest, ChangeSphereResponse>(new ChangeSphereRequest
                {
                    SphereChangeInfos = new() {new SphereChangeInfo {EquipmentGuid = eq.Guid, SphereId1 = slots[0], SphereId2 = slots[1], SphereId3 = slots[2], SphereId4 = slots[3]}}
                });
                placed.AddRange(added);
            }
            catch (Exception e)
            {
                await _log.LogAsync(userId, $"Rune equip {who}: {e.Message}", "Warning");
            }
        }

        if (placed.Count > 0) await _tracker.TrackAsync(userId, GrowthKind.Equip, "[Rune] " + string.Join(" | ", placed));
    }
}

/// <summary>
/// 把空的等级链接格子填满 (主队优先, 然后稀有度高的), 不动前5名基础角色
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class AutoFillLevelLinkHandler : IGameActionHandler
{
    private readonly GrowthTracker _tracker;
    private readonly JobLogger _log;
    public string ActionName => "Fill Level Link";

    public async Task ExecuteAsync(AccountContext context)
    {
        var nm = context.NetworkManager;
        var userId = context.AccountInfo.UserId;
        var data = nm.UserSyncData;
        var link = data.UserLevelLinkDtoInfo;
        if (link == null) return;
        var members = data.UserLevelLinkMemberDtoInfos ?? new();
        var baseFive = data.UserCharacterDtoInfos.Where(d => !data.IsLevelLinkMember(d.Guid))
            .OrderByDescending(d => d.Level).Take(5).Select(d => d.Guid).ToHashSet();
        var team = data.GetMainTeam().Select(d => d.Guid).ToHashSet();
        var candidates = data.UserCharacterDtoInfos
            .Where(d => !baseFive.Contains(d.Guid) && !data.IsLevelLinkMember(d.Guid))
            .OrderByDescending(d => team.Contains(d.Guid)).ThenByDescending(d => d.RarityFlags).ThenByDescending(d => d.Level)
            .Select(d => d.CharacterId).Distinct().ToList(); // 同角色只放一个
        var freeCells = Enumerable.Range(1, link.MemberMaxCount)
            .Where(n => !members.Any(m => m.CellNo == n && !string.IsNullOrEmpty(m.UserCharacterGuid))).ToList();
        foreach (var (cell, charId) in freeCells.Zip(candidates))
        {
            if (members.Any(m => m.CharacterId == charId)) continue;
            var c = data.UserCharacterDtoInfos.Where(d => d.CharacterId == charId && !baseFive.Contains(d.Guid)).MaxBy(d => d.RarityFlags);
            if (c == null) continue;
            try
            {
                await nm.SendRequest<LevelLinkApi.SetMemberRequest, LevelLinkApi.SetMemberResponse>(new LevelLinkApi.SetMemberRequest {LevelLinkCellNo = cell, UserCharacterGuid = c.Guid});
                await _tracker.TrackAsync(userId, GrowthKind.LevelLink, $"{CharName(c.CharacterId)} joined Level Link (slot {cell})");
            }
            catch (Exception e)
            {
                await _log.LogAsync(userId, $"Level Link slot {cell}: {e.Message}", "Warning"); // 冷却中的格子会报错, 跳过
            }
        }
    }
}
