using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Player;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildTower
{
	[MessagePackObject(true)]
	public class GuildTowerTotalStarRankingInfo
	{
		public PlayerInfo PlayerInfo { get; set; }

		public long TotalStarCount { get; set; }
	}
}
