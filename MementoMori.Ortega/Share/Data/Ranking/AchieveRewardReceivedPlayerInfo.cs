using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Ranking
{
	[MessagePackObject(true)]
	public class AchieveRewardReceivedPlayerInfo
	{
		public long PlayerId { get; set; }

		public string PlayerName { get; set; }
	}
}
