using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("楽曲再生時の操作イベント種別")]
	public enum MusicPlayerPlayEventType
	{
		[Description("不明")]
		None,
		[Description("シーク操作_操作前の位置")]
		SeekFrom,
		[Description("シーク操作_操作後の位置")]
		SeekTo,
		[Description("一時停止の位置")]
		Pause,
		[Description("再開の位置")]
		Resume,
		[Description("次の楽曲にスキップした位置")]
		Skip
	}
}
