using MementoMori.Exceptions;
using MementoMori.Ortega.Share.Data.ApiInterface.Equipment;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Master.Data;

// ReSharper disable PossibleMultipleEnumeration

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    /// <summary>
    ///     魔装继承
    /// </summary>
    public async Task AutoEquipmentMatchlessInheritance()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            while (!token.IsCancellationRequested)
            {
                // 批量精炼
                var equipments = UserSyncData.UserEquipmentDtoInfos.Select(d => new
                {
                    Equipment = d, EquipmentMB = EquipmentTable.GetById(d.EquipmentId)
                });

                // 找到所有 等级为S、魔装、未装备 的装备
                var sEquipments = equipments.Where(d =>
                        d.Equipment.CharacterGuid == "" && // 未装备
                        d.Equipment.MatchlessSacredTreasureLv == 1 && // 魔装等级为 1
                        (d.EquipmentMB.RarityFlags & EquipmentRarityFlags.S) != 0 // 稀有度为 S
                ).ToList();

                if (sEquipments.Count == 0)
                {
                    log(TextResourceTable.Get("[ItemBoxButtonBulkCasting]"));

                    try
                    {
                        await GetResponse<CastManyRequest, CastManyResponse>(new CastManyRequest
                        {
                            RarityFlags = EquipmentRarityFlags.S | EquipmentRarityFlags.A | EquipmentRarityFlags.B |
                                          EquipmentRarityFlags.C
                        });
                    }
                    catch (ApiErrorException e) when (e.ErrorCode == ErrorCode.EquipmentMissingEquipment)
                    {
                        // 没有可以精炼的装备了
                        log(ResourceStrings.NothingToSmelt);
                        break;
                    }

                    sEquipments = equipments.Where(d =>
                            d.Equipment.CharacterGuid == "" && // 未装备
                            d.Equipment.MatchlessSacredTreasureLv == 1 && // 魔装等级为 1
                            (d.EquipmentMB.RarityFlags & EquipmentRarityFlags.S) != 0 // 稀有度为 S
                    ).ToList();
                }

                // 按照装备位置进行分组
                foreach (var grouping in sEquipments.GroupBy(d => d.EquipmentMB.SlotType))
                {
                    // 当前能够接受继承的 D 级别装备
                    var currentTypeEquips = equipments.Where(d =>
                    {
                        return (d.EquipmentMB.RarityFlags & EquipmentRarityFlags.D) != 0 &&
                               d.EquipmentMB.EquipmentLv != 1 &&
                               d.EquipmentMB.SlotType == grouping.Key &&
                               d.Equipment.LegendSacredTreasureLv == 0 &&
                               d.Equipment.MatchlessSacredTreasureLv == 0;
                    });
                    var processedDEquips = new List<UserItemDtoInfo>();

                    // 还缺多少装备
                    var needMoreCount = grouping.Count() - currentTypeEquips.Count();
                    var slotType = grouping.Key;
                    if (needMoreCount > 0) await GetInheritatableEquipments(slotType, needMoreCount, log, processedDEquips);

                    // 继承            
                    foreach (var x1 in grouping)
                    {
                        var mb = x1.EquipmentMB;
                        var info = x1.Equipment;
                        await InheritantEquipment(mb, info, log);
                    }
                }
            }
        });
    }

    private async Task GetInheritatableEquipments(EquipmentSlotType slotType, int needMoreCount, Action<string> log, List<UserItemDtoInfo> processedDEquips)
    {
        // 找到未解封的装备物品
        var equipItems = UserSyncData.UserItemDtoInfo.Where(d =>
        {
            if (d.ItemType != ItemType.Equipment) return false;
            var equipmentMb = EquipmentTable.GetById(d.ItemId);
            if (equipmentMb.EquipmentLv == 1) return false;
            if (equipmentMb.SlotType != slotType) return false;
            if ((equipmentMb.RarityFlags & EquipmentRarityFlags.D) == 0) return false;
            return true;
        }).ToList();
        foreach (var equipItem in equipItems)
        {
            if (needMoreCount <= 0) break;

            for (var i = 0; i < equipItem.ItemCount; i++)
            {
                if (needMoreCount <= 0) break;
                var equipmentMb = EquipmentTable.GetById(equipItem.ItemId);
                log($"{ResourceStrings.FindEquipmentCharacter} {equipmentMb.Memo}");
                // 找到可以装备的一个角色
                var userCharacterDtoInfo = UserSyncData.UserCharacterDtoInfos.Where(d =>
                {
                    var characterMb = CharacterTable.GetById(d.CharacterId);
                    if ((characterMb.JobFlags & equipmentMb.EquippedJobFlags) == 0) return false; // 装备职业

                    if (d.Level >= equipmentMb.EquipmentLv) return true; // 装备等级

                    if (UserSyncData.UserLevelLinkMemberDtoInfos.Exists(x =>
                            d.Guid == x.UserCharacterGuid)
                        && UserSyncData.UserLevelLinkDtoInfo.PartyLevel >=
                        equipmentMb.EquipmentLv) // 角色在等级链接里面并且等级链接大于装备等级
                        return true;

                    return false;
                }).First();


                // 获取角色某个位置的装备, 可能没有装备
                var replacedEquip = UserSyncData.UserEquipmentDtoInfos.Where(d =>
                {
                    var byId = EquipmentTable.GetById(d.EquipmentId);
                    return d.CharacterGuid == userCharacterDtoInfo.Guid &&
                           byId.SlotType == equipmentMb.SlotType;
                }).FirstOrDefault();

                // 替换装备
                await GetResponse<ChangeEquipmentRequest, ChangeEquipmentResponse>(
                    new ChangeEquipmentRequest
                    {
                        UserCharacterGuid = userCharacterDtoInfo.Guid,
                        EquipmentChangeInfos = new List<EquipmentChangeInfo>
                        {
                            new()
                            {
                                EquipmentId = equipItem.ItemId,
                                EquipmentSlotType = equipmentMb.SlotType,
                                IsInherit = false
                            }
                        }
                    });

                // 恢复装备
                if (replacedEquip == null)
                {
                    await GetResponse<RemoveEquipmentRequest, RemoveEquipmentResponse>(new RemoveEquipmentRequest
                    {
                        EquipmentSlotTypes = new List<EquipmentSlotType> {equipmentMb.SlotType},
                        UserCharacterGuid = userCharacterDtoInfo.Guid
                    });
                }
                else
                {
                    await GetResponse<ChangeEquipmentRequest, ChangeEquipmentResponse>(
                        new ChangeEquipmentRequest
                        {
                            UserCharacterGuid = userCharacterDtoInfo.Guid,
                            EquipmentChangeInfos = new List<EquipmentChangeInfo>
                            {
                                new()
                                {
                                    EquipmentGuid = replacedEquip.Guid,
                                    EquipmentId = replacedEquip.EquipmentId,
                                    EquipmentSlotType = equipmentMb.SlotType,
                                    IsInherit = false
                                }
                            }
                        });
                }

                needMoreCount--;
                processedDEquips.Add(equipItem);
            }
        }
    }

    /// <summary>
    ///     圣装继承
    /// </summary>
    public async Task AutoEquipmentLegendInheritance()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            while (!token.IsCancellationRequested)
            {
                var usersyncData = await UserGetUserData();
                // 找到所有 等级为S、魔装、未装备 的装备
                var equipments = usersyncData.UserSyncData.UserEquipmentDtoInfos.Select(d => new
                {
                    Equipment = d, EquipmentMB = EquipmentTable.GetById(d.EquipmentId)
                });
                var sEquipments = equipments.Where(d =>
                        d.Equipment.CharacterGuid == "" && // 未装备
                        d.Equipment.LegendSacredTreasureLv == 1 && // 圣装等级为 1
                        (d.EquipmentMB.RarityFlags & EquipmentRarityFlags.S) != 0 // 稀有度为 S
                ).ToList();

                if (sEquipments.Count == 0)
                {
                    log(ResourceStrings.NoLegendEquip);
                    break;
                }

                // 按照装备位置进行分组
                foreach (var grouping in sEquipments.GroupBy(d => d.EquipmentMB.SlotType))
                {
                    // 当前能够接受继承的 D 级别装备
                    var currentTypeEquips = equipments.Where(d =>
                    {
                        return (d.EquipmentMB.RarityFlags & EquipmentRarityFlags.D) != 0 &&
                               d.EquipmentMB.EquipmentLv != 1 &&
                               d.EquipmentMB.SlotType == grouping.Key &&
                               d.Equipment.LegendSacredTreasureLv == 0 &&
                               d.Equipment.MatchlessSacredTreasureLv == 0;
                    });
                    var processedDEquips = new List<UserItemDtoInfo>();

                    // 还缺多少装备
                    var needMoreCount = grouping.Count() - currentTypeEquips.Count();
                    if (needMoreCount > 0) await GetInheritatableEquipments(grouping.Key, needMoreCount, log, processedDEquips);

                    // 继承            
                    foreach (var x1 in grouping)
                    {
                        var mb = x1.EquipmentMB;
                        var info = x1.Equipment;
                        await InheritantEquipment(mb, info, log);
                    }
                }
            }
        });
    }

    private async Task InheritantEquipment(EquipmentMB mb, UserEquipmentDtoInfo info, Action<string> log)
    {
        // 同步数据
        await UserGetUserData();

        var userEquipmentDtoInfo = UserSyncData.UserEquipmentDtoInfos.Where(d =>
        {
            var equipmentMb = EquipmentTable.GetById(d.EquipmentId);
            if (d.LegendSacredTreasureLv == 0 // 未被继承的装备
                && d.MatchlessSacredTreasureLv == 0
                && equipmentMb.EquipmentLv != 1
                && equipmentMb.SlotType == mb.SlotType // 同一个位置 
                && (equipmentMb.RarityFlags & EquipmentRarityFlags.D) != 0 // 稀有度为 D
               )
                return true;

            return false;
        }).FirstOrDefault();

        if (userEquipmentDtoInfo != null)
        {
            await GetResponse<InheritanceEquipmentRequest, InheritanceEquipmentResponse>(
                new InheritanceEquipmentRequest
                {
                    InheritanceEquipmentGuid = userEquipmentDtoInfo.Guid,
                    SourceEquipmentGuid = info.Guid
                });
            log($"{TextResourceTable.Get("[EquipmentInheritanceButton]")}{ResourceStrings.Finished} {mb.Memo}=>{EquipmentTable.GetById(userEquipmentDtoInfo.EquipmentId).Memo}");
        }
        else
            log(ResourceStrings.NoInheritableDRarityEquip);
    }
}