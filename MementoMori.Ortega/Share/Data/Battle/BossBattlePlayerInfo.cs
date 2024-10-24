using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Data.Player;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle;

[MessagePackObject(true)]
public class BossBattlePlayerInfo
{
    public long BattlePower { get; set; }

    public PlayerInfo PlayerInfo { get; set; }

    public List<UserCharacterInfo> UserCharacterInfoList { get; set; }
}