using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ゲリラクエスト報酬用キャラクターの過半数属性")]
	public enum RewardMostElementType
	{
		None,
		[Description("愁（しゅう）")]
		Blue,
		[Description("業（ごう）")]
		Red,
		[Description("心（しん）")]
		Green,
		[Description("渇（かつ）")]
		Yellow,
		[Description("天（てん） ")]
		Light,
		[Description("冥（めい） ")]
		Dark,
		[Description("全違い")]
		Even
	}
}
