using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("ラッキーチャンス")]
	public class LuckyChanceMB : MasterBookBase, IHasStartEndTimeZone
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

        [DateTimeString]
		[PropertyOrder(4)]
		[Description("入力フォーム終了日時")]
		public string InputFormEndTime { get; }

		[PropertyOrder(5)]
		[Description("表示タイプ")]
		public MypageIconDisplayLocationType MypageIconDisplayLocationType { get; }

		[PropertyOrder(6)]
		[Description("タイトルキー")]
		public string TitleTextKey { get; }

		[PropertyOrder(8)]
		[Description("同一ユーザーの抽選上限回数")]
		public int LimitUserDrawCount { get; }

		[Nest(false, 0)]
		[PropertyOrder(9)]
		[Description("消費アイテム")]
		public UserItem ConsumeItem { get; }

        [DateTimeString]
		[PropertyOrder(10)]
		[Description("個人情報が削除可能になる日時")]
		public string CanDeletePersonalInfoTime { get; }

		[SerializationConstructor]
        public LuckyChanceMB(long id, bool? isIgnore, string memo, StartEndTimeZoneType startEndTimeZoneType, string startTime, string endTime, string inputFormEndTime, MypageIconDisplayLocationType mypageIconDisplayLocationType, string titleTextKey, int limitUserDrawCount, UserItem consumeItem, string canDeletePersonalInfoTime)
			: base(id, isIgnore, memo)
		{
            this.StartEndTimeZoneType = startEndTimeZoneType;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.InputFormEndTime = inputFormEndTime;
            this.MypageIconDisplayLocationType = mypageIconDisplayLocationType;
            this.TitleTextKey = titleTextKey;
            this.LimitUserDrawCount = limitUserDrawCount;
            this.ConsumeItem = consumeItem;
            this.CanDeletePersonalInfoTime = canDeletePersonalInfoTime;
		}

		public LuckyChanceMB()
			: base(0L, null, null)
		{
		}

		public DateTime GetInputFormEndTime(OrtegaTimeManager timeManager)
		{
			return default(DateTime);
		}

		public DateTime GetCanDeletePersonalInfoTime(OrtegaTimeManager timeManager)
		{
			return default(DateTime);
		}

		public bool IsDisplayInMypage()
		{
			return false;
		}

		public bool IsDisplayEventPortal()
		{
			return false;
		}
	}
}
