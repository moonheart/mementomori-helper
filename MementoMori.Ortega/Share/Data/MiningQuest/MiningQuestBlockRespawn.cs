using System.ComponentModel;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.MiningQuest
{
	[MessagePackObject(true)]
	public class MiningQuestBlockRespawn
	{
		[Description("ブロック再生成抽選枠のID")]
		public long BlockRespawnId { get; set; }

		[Description("基礎抽選確率")]
		public int BaseLotteryProbability { get; set; }

		[Description("基礎生成ブロック数")]
		public int BaseBlockSpawnCount { get; set; }
	}
}
