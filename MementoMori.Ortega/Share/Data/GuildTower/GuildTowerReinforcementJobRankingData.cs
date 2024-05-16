using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildTower
{
	[MessagePackObject(true)]
	public class GuildTowerReinforcementJobRankingData
	{
		public int AfterJobLevel { get; set; }

		public List<GuildTowerReinforcementJobRankingPlayerData> RankingPlayerDataList { get; set; }
	}
}
