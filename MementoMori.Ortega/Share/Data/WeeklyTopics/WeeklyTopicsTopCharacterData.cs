using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.WeeklyTopics;

[MessagePackObject(true)]
public class WeeklyTopicsTopCharacterData
{
    public string PlayerName { get; set; }

    public long CharacterId { get; set; }

    public ElementType ElementType { get; set; }

    public CharacterRarityFlags RarityFlags { get; set; }

    public long BattlePower { get; set; }
}