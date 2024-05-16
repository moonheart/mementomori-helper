using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Player;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildTower
{
	[MessagePackObject(true)]
	public class GuildTowerReinforcementJobRankingPlayerData
	{
		public PlayerInfo PlayerInfo { get; set; }

		public int Rank { get; set; }

		public long TotalConsumeItemCount { get; set; }
	}
}
