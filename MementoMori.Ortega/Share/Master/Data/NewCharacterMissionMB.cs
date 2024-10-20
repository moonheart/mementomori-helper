using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("新キャラミッション")]
    public class NewCharacterMissionMB : MasterBookBase, IHasJstStartEndTime
    {
        [PropertyOrder(1)]
        [Description("開始日時")]
        public string StartTimeFixJST { get; }

        [PropertyOrder(2)]
        [Description("終了日時")]
        public string EndTimeFixJST { get; }

        [DateTimeString]
        [PropertyOrder(3)]
        [Description("強制開始時間")]
        public string ForceStartTime { get; }

        [PropertyOrder(4)]
        [Description("キャラ画像Id")]
        public int CharacterImageId { get; }

        [PropertyOrder(5)]
        [Description("キャラ画像座標X")]
        public float CharacterImageX { get; }

        [PropertyOrder(6)]
        [Description("キャラ画像座標Y")]
        public float CharacterImageY { get; }

        [PropertyOrder(7)]
        [Description("キャラ画像サイズ")]
        public float CharacterImageSize { get; }

        [PropertyOrder(8)]
        [Description("タイトル")]
        public string TitleTextKey { get; }

        [PropertyOrder(9)]
        [Description("対象ミッションID")]
        public IReadOnlyList<long> TargetMissionIdList { get; }

        [PropertyOrder(10)]
        [Description("YouTube")]
        public string YouTubeUrl { get; }

        [PropertyOrder(11)]
        [Description("Twitter")]
        public string TwitterUrl { get; }

        [PropertyOrder(12)]
        [Description("アイコン表示箇所")]
        public MypageIconDisplayLocationType MypageIconDisplayLocationType { get; }

        [SerializationConstructor]
        public NewCharacterMissionMB(long id, bool? isIgnore, string memo, string startTimeFixJST, string endTimeFixJST, string forceStartTime, int characterImageId, float characterImageX,
            float characterImageY, float characterImageSize, string titleTextKey, IReadOnlyList<long> targetMissionIdList, string youTubeUrl, string twitterUrl,
            MypageIconDisplayLocationType mypageIconDisplayLocationType)
            : base(id, isIgnore, memo)
        {
            this.StartTimeFixJST = startTimeFixJST;
            this.EndTimeFixJST = endTimeFixJST;
            this.ForceStartTime = forceStartTime;
            this.CharacterImageId = characterImageId;
            this.CharacterImageX = characterImageX;
            this.CharacterImageY = characterImageY;
            this.CharacterImageSize = characterImageSize;
            this.TitleTextKey = titleTextKey;
            this.TargetMissionIdList = targetMissionIdList;
            this.YouTubeUrl = youTubeUrl;
            this.TwitterUrl = twitterUrl;
            this.MypageIconDisplayLocationType = mypageIconDisplayLocationType;
        }

        public NewCharacterMissionMB()
            : base(0L, null, null)
        {
        }
    }
}
