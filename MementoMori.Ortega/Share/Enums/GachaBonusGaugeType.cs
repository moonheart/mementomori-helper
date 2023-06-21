using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ガチャボーナスゲージ表示タイプ")]
	public enum GachaBonusGaugeType
	{
		[Description("次のボーナスでドローカウントをリセット")]
		SingleReset,
		[Description("ドロー回数を累計表示")]
		SingleSum,
		[Description("ボーナスを複数表示")]
		Multi
	}
}
