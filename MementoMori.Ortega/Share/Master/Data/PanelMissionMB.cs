using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Mission;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("パネルミッション")]
	public class PanelMissionMB : MasterBookBase, IHasJstStartEndTime
	{
		[PropertyOrder(1)]
		[Description("キャンペーンタイトルキー")]
		public string CampaignTitleKey { get; }

		[PropertyOrder(6)]
		[Description("猶予日数")]
		public int DelayDays { get; }

		[PropertyOrder(5)]
		[Description("強制開始時刻(現地時間)")]
		public string ForceStartTime { get; }

		[Nest(false, 0)]
		[PropertyOrder(2)]
		[Description("シート情報")]
		public IReadOnlyList<PanelMissionSheetInfo> PanelMissionSheetInfoList { get; }

		[SerializationConstructor]
		public PanelMissionMB(long id, bool? isIgnore, string memo, string campaignTitleKey, IReadOnlyList<PanelMissionSheetInfo> panelMissionSheetInfoList, string startTimeFixJST, string endTimeFixJST, string forceStartTime, int delayDays)
			: base(id, isIgnore, memo)
		{
			CampaignTitleKey = campaignTitleKey;
            PanelMissionSheetInfoList = panelMissionSheetInfoList;
            StartTimeFixJST = startTimeFixJST;
            EndTimeFixJST = endTimeFixJST;
            ForceStartTime = forceStartTime;
            DelayDays = delayDays;
		}

		public PanelMissionMB():base(0L, null, null)
		{
		}

		[PropertyOrder(4)]
		[Description("終了時刻(JST)")]
		public string EndTimeFixJST { get; }

		[PropertyOrder(3)]
		[Description("開始時刻(JST)")]
		public string StartTimeFixJST { get; }
	}
}
