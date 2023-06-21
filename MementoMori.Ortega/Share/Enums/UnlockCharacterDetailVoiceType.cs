using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("視聴可能ボイスの解放条件")]
	public enum UnlockCharacterDetailVoiceType
	{
		[Description("条件無し")]
		None,
		[Description("ランクアップボイス1")]
		RankUp1,
		[Description("ランクアップボイス2")]
		RankUp2,
		[Description("ランクアップボイス3")]
		RankUp3,
		[Description("ランクアップボイス4")]
		RankUp4,
		[Description("ランクアップボイス5")]
		RankUp5,
		[Description("ランクアップボイス6")]
		RankUp6,
		[Description("誕生日")]
		Birthday,
		[Description("メモリー全視聴")]
		MemoryComplete,
		[Description("クエストクリア")]
		QuestClear
	}
}
