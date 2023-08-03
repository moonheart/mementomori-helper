using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Gvg;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Guild
{
	[MessagePackObject(true)]
	public class GuildGvgInfo
	{
		public bool IsOpen { get; set; }

		public long CurrentRanking { get; set; }

		public long TodayRanking { get; set; }

		public int RemainingDeclarationCount { get; set; }

		public long RewardLimitTime { get; set; }

		public List<CastleRewardInfo> CanGetCastleRewardInfoList{ get; set; }

		public List<CastleRewardInfo> GotCastleRewardInfoList{ get; set; }

		public int CastleCountLarge { get; set; }

		public int CastleCountMedium { get; set; }

		public int CastleCountSmall { get; set; }
	}
}
