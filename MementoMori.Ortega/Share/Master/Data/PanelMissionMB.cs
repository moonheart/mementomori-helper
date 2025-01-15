using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Mission;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("パネルミッション")]
    public class PanelMissionMB : MasterBookBase, IHasStartEndTimeZone
    {
        [PropertyOrder(1)]
        [Description("キャンペーンタイトルキー")]
        public string CampaignTitleKey { get; }

        [PropertyOrder(7)]
        [Description("猶予日数")]
        public int DelayDays { get; }

        [DateTimeString]
        [PropertyOrder(6)]
        [Description("強制開始時刻(現地時間)")]
        public string ForceStartTime { get; }

        [Nest(false, 0)]
        [PropertyOrder(2)]
        [Description("シート情報")]
        public IReadOnlyList<PanelMissionSheetInfo> PanelMissionSheetInfoList { get; }

        [PropertyOrder(12)]
        [Description("アイコン表示箇所")]
        public MypageIconDisplayLocationType MypageIconDisplayLocationType { get; }

        [SerializationConstructor]
        public PanelMissionMB(long id, bool? isIgnore, string memo, string campaignTitleKey, IReadOnlyList<PanelMissionSheetInfo> panelMissionSheetInfoList, StartEndTimeZoneType startEndTimeZoneType, string startTime, string endTime, string forceStartTime, int delayDays, MypageIconDisplayLocationType mypageIconDisplayLocationType)
            : base(id, isIgnore, memo)
        {
            CampaignTitleKey = campaignTitleKey;
            PanelMissionSheetInfoList = panelMissionSheetInfoList;
            StartEndTimeZoneType = startEndTimeZoneType;
            StartTime = startTime;
            EndTime = endTime;
            ForceStartTime = forceStartTime;
            DelayDays = delayDays;
            MypageIconDisplayLocationType = mypageIconDisplayLocationType;
        }

        public PanelMissionMB() : base(0L, null, null)
        {
        }

        [PropertyOrder(5)]
        [Description("終了日時")]
        public string EndTime { get; }

        [PropertyOrder(4)]
        [Description("開始日時")]
        public string StartTime { get; }

        [PropertyOrder(3)]
        [Description("時間タイプ")]
        public StartEndTimeZoneType StartEndTimeZoneType { get; }
    }
}