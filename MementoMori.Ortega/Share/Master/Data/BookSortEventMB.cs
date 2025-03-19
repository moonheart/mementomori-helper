using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("書庫整理イベント")]
	public class BookSortEventMB : MasterBookBase, IHasStartEndTimeZone
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
		[Description("対象ミッションIDリスト")]
		public IReadOnlyList<long> TargetMissionIdList { get; }

		[PropertyOrder(5)]
		[Description("ショップボタン遷移先ID")]
		public long TransferShopTabId { get; }

		[PropertyOrder(6)]
		[Description("ボーナスフロア繰り返し設定")]
		public int BonusFloorRepeat { get; }

		[PropertyOrder(7)]
		[Description("ボーナスフロア個別設定")]
		public IReadOnlyList<int> BonusFloorList { get; }

		[PropertyOrder(8)]
		[Description("アイコン表示箇所")]
		public MypageIconDisplayLocationType MypageIconDisplayLocationType { get; }

		[SerializationConstructor]
		public BookSortEventMB(long id, bool? isIgnore, string memo, StartEndTimeZoneType startEndTimeZoneType, string startTime, string endTime, IReadOnlyList<long> targetMissionIdList, long transferShopTabId, int bonusFloorRepeat, IReadOnlyList<int> bonusFloorList, MypageIconDisplayLocationType mypageIconDisplayLocationType)
			: base(id, isIgnore, memo)
		{
            this.StartEndTimeZoneType = startEndTimeZoneType;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.TargetMissionIdList = targetMissionIdList;
            this.TransferShopTabId = transferShopTabId;
            this.BonusFloorRepeat = bonusFloorRepeat;
            this.BonusFloorList = bonusFloorList;
            this.MypageIconDisplayLocationType = mypageIconDisplayLocationType;
		}

		public BookSortEventMB()
			: base(0L, null, null)
		{
		}

		public bool IsDisplayInMypage()
		{
			return false;
		}

		public bool IsDisplayEventPortal()
		{
			return false;
		}

		public bool IsBonusFloor(int floor)
		{
			return false;
		}
	}
}
