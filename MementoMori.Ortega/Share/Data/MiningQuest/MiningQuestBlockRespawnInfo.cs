using System.ComponentModel;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.MiningQuest
{
	[MessagePackObject(true)]
	public class MiningQuestBlockRespawnInfo
	{
		[Description("ブロック再生成抽選枠のID")]
		public long BlockRespawnId { get; set; }

		[Description("抽選確率")]
		public int LotteryProbability { get; set; }

		[Description("生成ブロック数")]
		public int BlockSpawnCount { get; set; }
	}
}
