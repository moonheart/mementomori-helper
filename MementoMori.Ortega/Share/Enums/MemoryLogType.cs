using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("メモリーログタイプ")]
	public enum MemoryLogType
	{
		[Description("初回報酬受取")]
		FirstReward = 1,
		[Description("無報酬")]
		NoReward,
		[Description("ガチャからの閲覧")]
		Gacha
	}
}
