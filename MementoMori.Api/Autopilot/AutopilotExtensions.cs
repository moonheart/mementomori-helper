using MementoMori.Api.Extensions;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Utils;

namespace MementoMori.Api.Autopilot;

public static class AutopilotExtensions
{
    public static Dictionary<EquipmentSlotType, UserEquipmentDtoInfo?> GetEquipmentBySlot(this UserSyncData data, string characterGuid)
    {
        var result = EnumUtil.GetValueList<EquipmentSlotType>().ToDictionary(s => s, _ => (UserEquipmentDtoInfo?) null);
        foreach (var e in data.UserEquipmentDtoInfos.Where(d => d.CharacterGuid == characterGuid))
            result[EquipmentTable.GetById(e.EquipmentId).SlotType] = e;
        return result;
    }

    public static List<UserCharacterDtoInfo> GetDeckCharacters(this UserSyncData data, DeckUseContentType deck)
    {
        var info = data.UserDeckDtoInfos?.Find(d => d.DeckUseContentType == deck);
        if (info == null) return new();
        var guids = info.GetUserCharacterGuids();
        return data.UserCharacterDtoInfos.Where(d => guids.Contains(d.Guid)).ToList();
    }

    /// <summary>主队: Boss 队 (主线用的) → Auto 队, 都没有就用等级最高的5个</summary>
    public static List<UserCharacterDtoInfo> GetMainTeam(this UserSyncData data)
    {
        if (data?.UserCharacterDtoInfos == null) return new();
        var team = data.GetDeckCharacters(DeckUseContentType.Boss);
        if (team.Count == 0) team = data.GetDeckCharacters(DeckUseContentType.Auto);
        return team.Count > 0 ? team : data.UserCharacterDtoInfos.OrderByDescending(d => d.Level).Take(5).ToList();
    }

    public static long GetEffectiveLevel(this UserSyncData data, UserCharacterDtoInfo c) =>
        data.IsLevelLinkMember(c.Guid) ? data.GetLevelLinkLevel(c.CharacterId) : c.Level;

    public static bool CanAfford(this UserSyncData data, IEnumerable<UserItem> items) =>
        items.All(i => i.ItemCount <= 0 || data.GetUserItemCount(i.ItemType, i.ItemId) >= i.ItemCount);

    /// <summary>只要名字, 不带称号</summary>
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
}
