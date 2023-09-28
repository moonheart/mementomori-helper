using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Mission
{
	[MessagePackObject(true)]
	[Description("パネルミッションのシート情報")]
	public class PanelMissionSheetInfo
	{
		[Nest(true, 1)]
		[Description("ビンゴ報酬情報リスト")]
		public List<BingoRewardInfo> BingoRewardInfoList { get; set; }

		[Description("対象ミッションIDリスト")]
		public List<long> MissionIdList{ get; set; }

		[Description("パネル図鑑MBのID")]
		public long PanelMBId { get; set; }

		[Description("シート番号")]
		public int SheetNo { get; set; }

		[Description("X座標")]
		public float X { get; set; }

		[Description("Y座標")]
		public float Y { get; set; }
	}
}
