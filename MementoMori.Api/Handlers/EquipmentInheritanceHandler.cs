using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.Equipment;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 圣装/魔装继承 (manual tool, not in the daily/hourly lists). Port of the old MementoMori/Funcs/EquipmentInheritance.cs:
/// every unequipped S gear at Legend (or Matchless) lv 1 passes its level to a D gear of the same slot. Missing D gear is
/// taken out of the inventory by briefly equipping it on a character that can wear it, then putting the old piece back.
/// ponytail: the old Matchless loop also bulk-smelted S/A/B/C gear (CastMany) to make more; dropped, it eats unused gear
/// without asking. Add it behind its own switch if someone asks.
/// </summary>
public abstract class EquipmentInheritanceHandlerBase : IGameActionHandler
{
    protected abstract ILogger Logger { get; }
    protected abstract JobLogger JobLogger { get; }
    protected abstract Func<UserEquipmentDtoInfo, long> SourceLevel { get; }

    public abstract string ActionName { get; }

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;
        try
        {
            var sources = nm.UserSyncData.UserEquipmentDtoInfos
                .Where(d => string.IsNullOrEmpty(d.CharacterGuid) && SourceLevel(d) == 1 && IsRarity(d, EquipmentRarityFlags.S))
                .ToList();
            if (sources.Count == 0)
            {
                await JobLogger.LogAsync(userId, Text.T("InheritNothing", ActionName));
                return;
            }

            var done = 0;
            foreach (var slot in sources.GroupBy(d => Masters.EquipmentTable.GetById(d.EquipmentId).SlotType))
            {
                var missing = slot.Count() - Targets(nm, slot.Key).Count();
                if (missing > 0) await UnsealDGearAsync(nm, slot.Key, missing);

                foreach (var source in slot)
                {
                    var target = Targets(nm, slot.Key).FirstOrDefault();
                    if (target == null)
                    {
                        await JobLogger.LogAsync(userId, Text.T("InheritNoTarget", Masters.EquipmentTable.GetById(source.EquipmentId).Memo));
                        break;
                    }

                    await nm.SendRequest<InheritanceEquipmentRequest, InheritanceEquipmentResponse>(new InheritanceEquipmentRequest
                        { InheritanceEquipmentGuid = target.Guid, SourceEquipmentGuid = source.Guid });
                    await JobLogger.LogAsync(userId, Text.T("InheritDone",
                        Masters.EquipmentTable.GetById(source.EquipmentId).Memo, Masters.EquipmentTable.GetById(target.EquipmentId).Memo));
                    done++;
                }
            }

            await JobLogger.LogAsync(userId, Text.T("InheritFinished", ActionName, done));
        }
        catch (Exception ex)
        {
            Logger.LogWarning(ex, "{Action} failed for user {UserId}", ActionName, userId);
            await JobLogger.LogAsync(userId, $"{ActionName}: {ex.Message}", "Error");
        }
    }

    private static bool IsRarity(UserEquipmentDtoInfo d, EquipmentRarityFlags flag) =>
        (Masters.EquipmentTable.GetById(d.EquipmentId).RarityFlags & flag) != 0;

    private static bool IsTargetMb(EquipmentMB mb, EquipmentSlotType slot) =>
        (mb.RarityFlags & EquipmentRarityFlags.D) != 0 && mb.EquipmentLv != 1 && mb.SlotType == slot;

    /// <summary>D gear (not lv 1) of the slot that hasn't received any Legend/Matchless level yet.</summary>
    private static IEnumerable<UserEquipmentDtoInfo> Targets(NetworkManager nm, EquipmentSlotType slot) =>
        nm.UserSyncData.UserEquipmentDtoInfos.Where(d =>
            d.LegendSacredTreasureLv == 0 && d.MatchlessSacredTreasureLv == 0 && IsTargetMb(Masters.EquipmentTable.GetById(d.EquipmentId), slot));

    /// <summary>Sealed D gear sits in the inventory as items; equipping it once turns it into a gear instance.</summary>
    private static async Task UnsealDGearAsync(NetworkManager nm, EquipmentSlotType slot, int count)
    {
        var items = nm.UserSyncData.UserItemDtoInfo
            .Where(d => d.ItemType == ItemType.Equipment && IsTargetMb(Masters.EquipmentTable.GetById(d.ItemId), slot))
            .ToList();
        foreach (var item in items)
        {
            for (var i = 0; i < item.ItemCount && count > 0; i++, count--)
            {
                var mb = Masters.EquipmentTable.GetById(item.ItemId);
                var character = nm.UserSyncData.UserCharacterDtoInfos.FirstOrDefault(c => CanWear(nm, c, mb));
                if (character == null) return;

                var previous = nm.UserSyncData.UserEquipmentDtoInfos.FirstOrDefault(d =>
                    d.CharacterGuid == character.Guid && Masters.EquipmentTable.GetById(d.EquipmentId).SlotType == slot);

                await nm.SendRequest<ChangeEquipmentRequest, ChangeEquipmentResponse>(new ChangeEquipmentRequest
                {
                    UserCharacterGuid = character.Guid,
                    EquipmentChangeInfos = [new EquipmentChangeInfo { EquipmentId = item.ItemId, EquipmentSlotType = slot, IsInherit = false }]
                });

                if (previous == null)
                    await nm.SendRequest<RemoveEquipmentRequest, RemoveEquipmentResponse>(new RemoveEquipmentRequest
                        { UserCharacterGuid = character.Guid, EquipmentSlotTypes = [slot] });
                else
                    await nm.SendRequest<ChangeEquipmentRequest, ChangeEquipmentResponse>(new ChangeEquipmentRequest
                    {
                        UserCharacterGuid = character.Guid,
                        EquipmentChangeInfos = [new EquipmentChangeInfo
                            { EquipmentGuid = previous.Guid, EquipmentId = previous.EquipmentId, EquipmentSlotType = slot, IsInherit = false }]
                    });
            }
        }
    }

    private static bool CanWear(NetworkManager nm, UserCharacterDtoInfo c, EquipmentMB mb)
    {
        if ((Masters.CharacterTable.GetById(c.CharacterId).JobFlags & mb.EquippedJobFlags) == 0) return false;
        if (c.Level >= mb.EquipmentLv) return true;
        return nm.UserSyncData.UserLevelLinkMemberDtoInfos?.Exists(x => x.UserCharacterGuid == c.Guid) == true
               && nm.UserSyncData.UserLevelLinkDtoInfo?.PartyLevel >= mb.EquipmentLv;
    }
}

[RegisterTransient]
[AutoConstructor]
public partial class LegendInheritanceHandler : EquipmentInheritanceHandlerBase
{
    private readonly ILogger<LegendInheritanceHandler> _logger;
    private readonly JobLogger _jobLogger;
    protected override ILogger Logger => _logger;
    protected override JobLogger JobLogger => _jobLogger;
    protected override Func<UserEquipmentDtoInfo, long> SourceLevel => d => d.LegendSacredTreasureLv;
    public override string ActionName => Text.T("ActionLegendInheritance");
}

[RegisterTransient]
[AutoConstructor]
public partial class MatchlessInheritanceHandler : EquipmentInheritanceHandlerBase
{
    private readonly ILogger<MatchlessInheritanceHandler> _logger;
    private readonly JobLogger _jobLogger;
    protected override ILogger Logger => _logger;
    protected override JobLogger JobLogger => _jobLogger;
    protected override Func<UserEquipmentDtoInfo, long> SourceLevel => d => d.MatchlessSacredTreasureLv;
    public override string ActionName => Text.T("ActionMatchlessInheritance");
}
