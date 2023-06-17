using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("無窮の塔タイプ")]
	public enum TowerType
	{
		None,
		[Description("無窮の塔")]
		Infinite,
		[Description("愁（しゅう）")]
		Blue,
		[Description("業（ごう）")]
		Red,
		[Description("心（しん）")]
		Green,
		[Description("渇（かつ）")]
		Yellow
	}
}
