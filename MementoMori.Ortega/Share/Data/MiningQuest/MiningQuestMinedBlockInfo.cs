using System.ComponentModel;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.MiningQuest
{
	[MessagePackObject(true)]
	public class MiningQuestMinedBlockInfo
	{
		[Description("出現ブロックのId")]
		public long BlockId { get; set; }

		[Description("採掘数")]
		public int Count { get; set; }

		[Description("参照画像ID")]
		public long ImageId { get; set; }
	}
}
