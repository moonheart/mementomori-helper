using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Player;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Ranking
{
	[MessagePackObject(true)]
	public class AchieveRankingPlayerInfo
	{
		public long AchieveLocalTimeStamp { get; set; }

		public PlayerInfo PlayerInfo { get; set; }

		public int Rank { get; set; }
	}
}
