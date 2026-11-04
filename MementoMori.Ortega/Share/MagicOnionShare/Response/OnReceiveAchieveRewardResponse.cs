using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Response
{
	[MessagePackObject(false)]
	public class OnReceiveAchieveRewardResponse
	{
		[Key(0)]
		public long WorldId { get; set; }

		[Key(1)]
		public RankingDataType RankingDataType { get; set; }

		[Key(3)]
		public long PlayerId { get; set; }

		[Key(2)]
		public string PlayerName { get; set; }
	}
}
