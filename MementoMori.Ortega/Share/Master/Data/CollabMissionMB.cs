using System.ComponentModel;
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

		[PropertyOrder(4)]
		[Description("メイン画像Id")]
		public int ImageId { get; }

		[PropertyOrder(5)]
		[Description("メイン画像座標X")]
		public float ImageX { get; }

		[PropertyOrder(6)]
		[Description("メイン画像座標Y")]
		public float ImageY { get; }

		[PropertyOrder(7)]
		[Description("メイン画像サイズ")]
		public float ImageSize { get; }

		[PropertyOrder(8)]
		[Description("説明文")]
		public string DescriptionTextKey { get; }

		[PropertyOrder(9)]
		[Description("ミッションタイトル")]
		public string TitleTextKey { get; }

		[PropertyOrder(10)]
		[Description("対象ミッションIDリスト")]
		public IReadOnlyList<long> TargetMissionIdList { get; }

		[PropertyOrder(11)]
		[Description("Url1")]
		public string Url1 { get; }

		[PropertyOrder(12)]
		[Description("Url2")]
		public string Url2 { get; }

		[PropertyOrder(13)]
		[Description("キャラクターID(楽曲開放ミッション用)")]
		public long CharacterId { get; }

		[PropertyOrder(14)]
		[Description("アイコン表示箇所")]
		public MypageIconDisplayLocationType MypageIconDisplayLocationType { get; }

		[SerializationConstructor]
		public CollabMissionMB(long id, bool? isIgnore, string memo, StartEndTimeZoneType startEndTimeZoneType, string startTime, string endTime, int imageId, float imageX, float imageY, float imageSize, string descriptionTextKey, string titleTextKey, IReadOnlyList<long> targetMissionIdList, string url1, string url2, long characterId, MypageIconDisplayLocationType mypageIconDisplayLocationType)
			: base(id, isIgnore, memo)
		{
            this.StartEndTimeZoneType = startEndTimeZoneType;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.ImageId = imageId;
            this.ImageX = imageX;
            this.ImageY = imageY;
            this.ImageSize = imageSize;
            this.DescriptionTextKey = descriptionTextKey;
            this.TitleTextKey = titleTextKey;
            this.TargetMissionIdList = targetMissionIdList;
            this.Url1 = url1;
            this.Url2 = url2;
            this.CharacterId = characterId;
            this.MypageIconDisplayLocationType = mypageIconDisplayLocationType;
		}

		public CollabMissionMB()
			: base(0L, null, null)
		{
		}
	}
}
