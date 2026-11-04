using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Data.Shop;

[MessagePackObject(true)]
[Description("レアリティアイテム")]
public class RarityItem : IUserCharacterItem
{
    public CharacterRarityFlags RarityFlags { get; set; }

    [Nest(true, 1)]
    public UserItem Item { get; set; }
}
