using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Gacha;

[MessagePackObject(true)]
public class GachaItemRate : IUserCharacterItem
{
    public CharacterRarityFlags CharacterRarityFlags { get; set; }

    public UserItem AddItem { get; set; }

    public double LotteryRate { get; set; }

    public UserItem Item { get; set; }

    CharacterRarityFlags IUserCharacterItem.RarityFlags => CharacterRarityFlags;
}