using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Gacha;

[MessagePackObject(true)]
public class GachaRarityRate
{
    public CharacterRarityFlags CharacterRarityFlags { get; set; }

    public double LotteryRate { get; set; }
}