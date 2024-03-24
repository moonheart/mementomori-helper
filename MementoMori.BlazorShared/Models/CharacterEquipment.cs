using MementoMori.Ortega.Share.Data.DtoInfo;

namespace MementoMori.BlazorShared.Models;

public class CharacterEquipment
{
    public string CharacterGuid { get; set; }
    public string CharacterName { get; set; }
    public string EquipmentGuid { get; set; }
    public string EquipmentName { get; set; }
    public UserEquipmentDtoInfo UserEquipmentDtoInfo { get; set; }
}