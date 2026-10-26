using MementoMori.Ortega.Custom;
using MementoMori.Ortega.Share.Data.ApiInterface.Equipment;
using MementoMori.Ortega.Share.Data.Equipment;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    // 合成只花金币 (Lv1-5), 金币低于这个数就不合成, 优先留给升级
    private const long SphereComposeGoldReserve = 1_000_000;

    // 符石: 合成重复的低级符石 -> 主队装备开第一个孔 -> 空孔塞最高级的符石 (同一件装备不放同类)
    public async Task AutoSphere()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var team = GetMainTeam().ToDictionary(c => c.Guid, c => c.CharacterId);
            var gold = () => UserSyncData.GetUserItemCount(ItemType.Gold, 1);
            List<(long id, long count)> Owned() => UserSyncData.UserItemDtoInfo
                .Where(d => d.ItemType == ItemType.Sphere && d.ItemCount > 0)
                .Select(d => (d.ItemId, d.ItemCount)).ToList();

            var cats = Owned().Where(s => s.count >= 2 && SphereTable.GetById(s.id).Lv <= 5)
                .Select(s => SphereTable.GetById(s.id).CategoryId).Distinct().ToList();
            if (cats.Count > 0 && gold() >= SphereComposeGoldReserve)
            {
                try
                {
                    var resp = await GetResponse<ComposeManySphereRequest, ComposeManySphereResponse>(new ComposeManySphereRequest {CategoryIdList = cats});
                    if (resp.IsSucceeded) Track(GrowthKind.Equip, "[Rune] Combined: " + string.Join(", ", resp.ResultItemList.Select(i => $"{TextResourceTable.Get(SphereTable.GetById(i.ItemId).NameKey)} ×{i.ItemCount}")));
                }
                catch (Exception e)
                {
                    log($"Rune combine: {e.Message}");
                }
            }

            var placed = new List<string>();
            foreach (var eq in UserSyncData.UserEquipmentDtoInfos.Where(d => team.ContainsKey(d.CharacterGuid ?? "")).ToList())
            {
                if (token.IsCancellationRequested) break;
                if (Owned().Count == 0) break;
                var mb = EquipmentTable.GetById(eq.EquipmentId);
                if (eq.SphereUnlockedCount == 0)
                {
                    if (mb.GoldRequiredToOpeningFirstSphereSlot <= 0 || gold() < mb.GoldRequiredToOpeningFirstSphereSlot) continue;
                    try
                    {
                        await GetResponse<UnlockSphereSlotRequest, UnlockSphereSlotResponse>(new UnlockSphereSlotRequest {EquipmentGuid = eq.Guid, UnlockCount = 1});
                    }
                    catch (Exception e)
                    {
                        log($"Rune slot {CharName(team[eq.CharacterGuid])} {mb.SlotType}: {e.Message}");
                        continue;
                    }
                }

                var current = UserSyncData.UserEquipmentDtoInfos.First(d => d.Guid == eq.Guid);
                var slots = new[] {current.SphereId1, current.SphereId2, current.SphereId3, current.SphereId4};
                var used = slots.Where(id => id > 0).Select(id => SphereTable.GetById(id).CategoryId).ToHashSet();
                var pool = Owned().SelectMany(s => Enumerable.Repeat(s.id, (int) Math.Min(s.count, 4))).ToList();
                var added = new List<string>();
                // 攻击类符石只能放 武器/饰品/手, 防御类只能放 头/甲/鞋 (ErrorCode 232001)
                var attackSlot = mb.SlotType is EquipmentSlotType.Weapon or EquipmentSlotType.Sub or EquipmentSlotType.Gauntlet;
                for (var i = 0; i < current.SphereUnlockedCount && i < 4; i++)
                {
                    if (slots[i] > 0) continue;
                    var best = pool.Where(id => !used.Contains(SphereTable.GetById(id).CategoryId) && SphereTable.GetById(id).IsAttackType == attackSlot).OrderByDescending(id => SphereTable.GetById(id).Lv).FirstOrDefault();
                    if (best == 0) break;
                    slots[i] = best;
                    pool.Remove(best);
                    used.Add(SphereTable.GetById(best).CategoryId);
                    added.Add($"{CharName(team[eq.CharacterGuid])} {mb.SlotType}: {TextResourceTable.Get(SphereTable.GetById(best).NameKey)}");
                }

                if (added.Count == 0) continue;
                try
                {
                    await GetResponse<ChangeSphereRequest, ChangeSphereResponse>(new ChangeSphereRequest
                    {
                        SphereChangeInfos = new() {new SphereChangeInfo {EquipmentGuid = eq.Guid, SphereId1 = slots[0], SphereId2 = slots[1], SphereId3 = slots[2], SphereId4 = slots[3]}}
                    });
                    placed.AddRange(added);
                }
                catch (Exception e)
                {
                    log($"Rune equip {CharName(team[eq.CharacterGuid])} {mb.SlotType}: {e.Message}");
                }

                await Task.Delay(GameConfig.AutoRequestDelay, token);
            }

            if (placed.Count > 0) Track(GrowthKind.Equip, "[Rune] " + string.Join(" | ", placed));
        });
    }
}
