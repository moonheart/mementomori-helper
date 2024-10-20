using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [Description("期間限定ミッション")]
    [MessagePackObject(true)]
    public class LimitedMissionMB : MasterBookBase, IHasStartEndTime
    {
        [PropertyOrder(1)]
        [Description("開始日時")]
        public string StartTime { get; }

        [Description("終了日時")]
        [PropertyOrder(2)]
        public string EndTime { get; }

        [PropertyOrder(3)]
        [Description("強制開始日時")]
        public string ForceStartTime { get; }

        [PropertyOrder(4)]
        [Description("猶予日数")]
        public int DelayDays { get; }

        [PropertyOrder(5)]
        [Description("キャラ画像Id")]
        public int CharacterImageId { get; }

        [PropertyOrder(6)]
        [Description("キャラ画像座標X")]
        public float CharacterImageX { get; }

        [PropertyOrder(7)]
        [Description("キャラ画像座標Y")]
        public float CharacterImageY { get; }

        [PropertyOrder(8)]
        [Description("キャラ画像サイズ")]
        public float CharacterImageSize { get; }

        [PropertyOrder(9)]
        [Description("タイトル")]
        public string TitleTextKey { get; }

        [PropertyOrder(10)]
        [Description("訴求文言")]
        public string AppealTextKey { get; }

        [PropertyOrder(11)]
        [Description("対象ミッションID")]
        public IReadOnlyList<long> TargetMissionIdList { get; }

        [PropertyOrder(12)]
        [Description("アイコン表示箇所")]
        public MypageIconDisplayLocationType MypageIconDisplayLocationType { get; }

        [SerializationConstructor]
        public LimitedMissionMB(long id, bool? isIgnore, string memo, string startTime, string endTime, string forceStartTime, int delayDays, int characterImageId, float characterImageX, float characterImageY, float characterImageSize, string titleTextKey, string appealTextKey, IReadOnlyList<long> targetMissionIdList, MypageIconDisplayLocationType mypageIconDisplayLocationType)
            : base(id, isIgnore, memo)
        {
            StartTime = startTime;
            EndTime = endTime;
            ForceStartTime = forceStartTime;
            DelayDays = delayDays;
            CharacterImageId = characterImageId;
            CharacterImageX = characterImageX;
            CharacterImageY = characterImageY;
            CharacterImageSize = characterImageSize;
            TitleTextKey = titleTextKey;
            AppealTextKey = appealTextKey;
            TargetMissionIdList = targetMissionIdList;
            MypageIconDisplayLocationType = mypageIconDisplayLocationType;
        }

        public LimitedMissionMB() : base(0L, false, "")
        {
        }
    }
}