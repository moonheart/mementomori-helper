using System.ComponentModel;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.MiningQuest
{
	[MessagePackObject(true)]
	public class MiningQuestSpecialAttackRange
	{
		[Description("範囲段階")]
		public int Step { get; set; }

		[Description("範囲")]
		public int Range { get; set; }

		[Description("解放段階")]
		public int UnlockStage { get; set; }
	}
}
