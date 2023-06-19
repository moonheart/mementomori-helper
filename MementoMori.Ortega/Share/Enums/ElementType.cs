using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum ElementType
	{
		[Description("なし")]
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
		Dark
	}
}
