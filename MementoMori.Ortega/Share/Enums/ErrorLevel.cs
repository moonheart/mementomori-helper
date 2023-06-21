using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("エラーレベル")]
	public enum ErrorLevel
	{
		[Description("デバッグ")]
		Debug,
		[Description("情報")]
		Info,
		[Description("警告")]
		Warning,
		[Description("エラー")]
		Error
	}
}
