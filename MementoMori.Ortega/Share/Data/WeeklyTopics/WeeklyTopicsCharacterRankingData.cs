using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Data.Player;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.WeeklyTopics;

[MessagePackObject(true)]
public class WeeklyTopicsCharacterRankingData
{
    public PlayerInfo PlayerInfo { get; set; }

    public int Rank { get; set; }

    public long CharacterId { get; set; }

    public CharacterDetailInfo CharacterDetailInfo { get; set; }
}