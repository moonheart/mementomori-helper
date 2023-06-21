using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Flags]
	[Description("Backtraceに送信しないエラー")]
	public enum BacktraceFilterFlags
	{
		[Description(null)]
		None = 0,
		[Description("カスタムメッセージ")]
		Message = 1,
		[Description("ハンドル済みエラー")]
		HandledException = 2,
		[Description("未ハンドルエラー")]
		UnhandledException = 4,
		[Description("ハング")]
		Hang = 8,
		[Description("Debug.LogError")]
		GameError = 16
	}
}
