using MementoMori.Ortega.Share.Data.Player;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.LocalRaid
{
	[MessagePackObject(true)]
	public class LocalRaidBattleLogPlayerInfo
	{
		public bool IsLeader { get; set; }

		public bool IsReady { get; set; }

		public bool IsInvite { get; set; }

		public PlayerInfo PlayerInfo { get; set; }
	}
}
