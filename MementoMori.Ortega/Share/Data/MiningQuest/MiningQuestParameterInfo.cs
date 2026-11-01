using System.ComponentModel;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.MiningQuest
{
	[MessagePackObject(true)]
	public class MiningQuestParameterInfo
	{
		[Description("攻撃力（攻撃頻度が1000を超えた分の補正をかけた値）")]
		public int AttackPower { get; set; }

		[Description("攻撃範囲")]
		public int AttackRange { get; set; }

		[Description("攻撃頻度（上限1000）")]
		public int AttackFrequency { get; set; }

		[Description("攻撃速度")]
		public int AttackSpeed { get; set; }

		[Description("ブロック生成上限数")]
		public int BlockGenerateLimit { get; set; }

		[Description("ブロック接触時移動速度倍率")]
		public int ContactBlockAttackSpeedRatePercent { get; set; }

		[Description("ブロック再生成抽頻度")]
		public int RespawnBlockIntervalMilliseconds { get; set; }
	}
}
