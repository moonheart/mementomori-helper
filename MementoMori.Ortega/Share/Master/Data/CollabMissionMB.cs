using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Sweepstakes;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("コラボミッション")]
	public class CollabMissionMB : MasterBookBase, IHasStartEndTimeZone
	{
		[PropertyOrder(1)]
		[Description("時間タイプ")]
		public StartEndTimeZoneType StartEndTimeZoneType { get; }

		[PropertyOrder(2)]
		[Description("開始日時")]
		public string StartTime { get; }

		[PropertyOrder(3)]
		[Description("終了日時")]
		public string EndTime { get; }

		[PropertyOrder(6)]
		[Description("メイン画像Id")]
		public int ImageId { get; }

		[PropertyOrder(7)]
		[Description("メイン画像座標X")]
		public float ImageX { get; }

		[PropertyOrder(8)]
		[Description("メイン画像座標Y")]
		public float ImageY { get; }

		[PropertyOrder(9)]
		[Description("メイン画像サイズ")]
		public float ImageSize { get; }

		[PropertyOrder(13)]
		[Description("説明文")]
		public string DescriptionTextKey { get; }

		[PropertyOrder(14)]
		[Description("説明文(応募有り)")]
		public string SweepstakesDescriptionTextKey { get; }

		[PropertyOrder(15)]
		[Description("ミッションタイトル")]
		public string TitleTextKey { get; }

		[PropertyOrder(16)]
		[Description("ミッションタイトル(応募有り)")]
		public string SweepstakesTitleTextKey { get; }

		[PropertyOrder(17)]
		[Description("規約")]
		public string TermsTextKey { get; }

		[PropertyOrder(18)]
		[Description("貢献メダルタイプ")]
		public int ActivityMedalType { get; }

		[PropertyOrder(19)]
		[Description("対象ミッションIDリスト")]
		public IReadOnlyList<long> TargetMissionIdList { get; }

		[PropertyOrder(21)]
		[Description("Url1")]
		public string Url1 { get; }

		[PropertyOrder(22)]
		[Description("Url2")]
		public string Url2 { get; }

		[PropertyOrder(23)]
		[Description("キャラクターID(楽曲開放ミッション用)")]
		public long CharacterId { get; }

		[PropertyOrder(5)]
		[Description("アイコン表示箇所")]
		public MypageIconDisplayLocationType MypageIconDisplayLocationType { get; }

		[PropertyOrder(4)]
		[Description("懸賞応募対象の時間サーバーIdのリスト")]
		public IReadOnlyList<long> SweepstakesTargetTimeServerIdList { get; }

		[PropertyOrder(10)]
		[Description("応募券アイテムID")]
		public long SweepstakesTicketItemId { get; }

		[PropertyOrder(11)]
		[Description("応募上限")]
		public int SweepstakesEntryUpperLimit { get; }

		[Nest(false, 0)]
		[PropertyOrder(12)]
		[Description("応募対象アイテムリスト")]
		public IReadOnlyList<SweepstakesItem> SweepstakesItemList { get; }

		[PropertyOrder(20)]
		[Description("お知らせタブ")]
		public long NoticeGroupId { get; }


        [SerializationConstructor]
        public CollabMissionMB(long id, bool? isIgnore, string memo, StartEndTimeZoneType startEndTimeZoneType, string startTime, string endTime, int imageId, float imageX, float imageY, float imageSize, string descriptionTextKey, string sweepstakesDescriptionTextKey, string titleTextKey, int activityMedalType, string sweepstakesTitleTextKey, string termsTextKey, IReadOnlyList<long> targetMissionIdList, string url1, string url2, long characterId, MypageIconDisplayLocationType mypageIconDisplayLocationType, IReadOnlyList<long> sweepstakesTargetTimeServerIdList, long sweepstakesTicketItemId, int sweepstakesEntryUpperLimit, IReadOnlyList<SweepstakesItem> sweepstakesItemList, long noticeGroupId)
			: base(id, isIgnore, memo)
		{
            StartEndTimeZoneType = startEndTimeZoneType;
            StartTime = startTime;
            EndTime = endTime;
            ImageId = imageId;
            ImageX = imageX;
            ImageY = imageY;
            ImageSize = imageSize;
            DescriptionTextKey = descriptionTextKey;
            SweepstakesDescriptionTextKey = sweepstakesDescriptionTextKey;
            TitleTextKey = titleTextKey;
            SweepstakesTitleTextKey = sweepstakesTitleTextKey;
            TermsTextKey = termsTextKey;
            TargetMissionIdList = targetMissionIdList ?? new List<long>();
            ActivityMedalType = activityMedalType;
            Url1 = url1;
            Url2 = url2;
            CharacterId = characterId;
            MypageIconDisplayLocationType = mypageIconDisplayLocationType;
            SweepstakesTargetTimeServerIdList = sweepstakesTargetTimeServerIdList ?? new List<long>();
            SweepstakesTicketItemId = sweepstakesTicketItemId;
            SweepstakesEntryUpperLimit = sweepstakesEntryUpperLimit;
            SweepstakesItemList = sweepstakesItemList ?? new List<SweepstakesItem>();
            NoticeGroupId = noticeGroupId;
		}

		public CollabMissionMB()
			: base(0L, null, null)
		{
		}
	}
}
