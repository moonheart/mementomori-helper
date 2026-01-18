using System.Collections;
using System.ComponentModel;
using MementoMori.Ortega.Share.Data.BookSortAssistance;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("書庫整理お手伝い派遣")]
	public class BookSortAssistanceMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("書庫整理イベントID")]
		public long BookSortEventId { get; }

		[PropertyOrder(2)]
		[Description("お手伝い派遣終了日時")]
		public string EndTime { get; }

		[PropertyOrder(3)]
		[Description("パック購入先遷移ID")]
		public long ShopTabId { get; }

		[PropertyOrder(4)]
		[Description("パック商品ID")]
		public long ShopProductDefaultId { get; }

		[Nest(false, 0)]
		[PropertyOrder(5)]
		[Description("レアリティ別獲得グレード数リスト")]
		public IReadOnlyList<BookSortAssistanceRewardGrade> BookSortAssistanceRewardGradeList { get; }

		[Nest(false, 0)]
		[PropertyOrder(6)]
		[Description("お手伝いレベル制御リスト")]
		public IReadOnlyList<BookSortAssistanceLv> BookSortAssistanceLvList { get; }

		[Nest(false, 0)]
		[PropertyOrder(7)]
		[Description("追加クエスト枠解放キャラ指定リスト")]
		public IReadOnlyList<BookSortAddAssistanceCondition> BookSortAddAssistanceConditionList { get; }

		[SerializationConstructor]
		public BookSortAssistanceMB(long id, bool? isIgnore, string memo, long bookSortEventId, string endTime, long shopTabId, long shopProductDefaultId, IReadOnlyList<BookSortAssistanceRewardGrade> bookSortAssistanceRewardGradeList, IReadOnlyList<BookSortAssistanceLv> bookSortAssistanceLvList, IReadOnlyList<BookSortAddAssistanceCondition> bookSortAddAssistanceConditionList)
			: base(id, isIgnore, memo)
		{
            this.BookSortEventId = bookSortEventId;
            this.EndTime = endTime;
            this.ShopTabId = shopTabId;
            this.ShopProductDefaultId = shopProductDefaultId;
            this.BookSortAssistanceRewardGradeList = bookSortAssistanceRewardGradeList;
            this.BookSortAssistanceLvList = bookSortAssistanceLvList;
            this.BookSortAddAssistanceConditionList = bookSortAddAssistanceConditionList;
		}

		public BookSortAssistanceMB(): base(default, default, default)
		{
			
		}

		public long GetRewardGrade(CharacterRarityFlags characterRarityFlags)
		{
			// int num;
			// do
			// {
			// 	num = 0;
			// 	IReadOnlyList<BookSortAssistanceRewardGrade> readOnlyList = this.<BookSortAssistanceRewardGradeList>k__BackingField;
			// 	if (typeof(IEnumerator).TypeHandle != 0)
			// 	{
			// 		if (num < typeof(IEnumerator).TypeHandle)
			// 		{
			// 			num += num;
			// 			if (num == typeof(IEnumerator).TypeHandle)
			// 			{
			// 				goto IL_002F;
			// 			}
			// 			num++;
			// 		}
			// 		if (num != 0)
			// 		{
			// 		}
			// 	}
			// 	IL_002F:
			// 	if ("{il2cpp array field local7->}" != (ulong)0L)
			// 	{
			// 	}
			// }
			// while (num != 0);
			// if (num != 0)
			// {
			// }
			// throw new NullReferenceException();
            throw new NotImplementedException();
        }

		public long GetDispatchMilliseconds(long assistanceLv, CharacterRarityFlags characterRarityFlags)
		{
			// BookSortAssistanceLv bookSortAssistanceLv = this.GetBookSortAssistanceLv(assistanceLv);
			// TimeSpan timeSpan = bookSortAssistanceLv.<MaxRequiredTime>k__BackingField.ToTimeSpan();
			// long num;
			// if (bookSortAssistanceLv.<QuestGrade>k__BackingField > num)
			// {
			// }
			// throw new NullReferenceException();
            throw new NotImplementedException();
		}

		public long GetDispatchMilliseconds(BookSortAssistanceLv assistanceLvInfo, CharacterRarityFlags characterRarityFlags)
		{
			// TimeSpan timeSpan = assistanceLvInfo.<MaxRequiredTime>k__BackingField.ToTimeSpan();
			// long num;
			// if (assistanceLvInfo.<QuestGrade>k__BackingField > num)
			// {
			// }
			// throw new NullReferenceException();
            throw new NotImplementedException();
		}

		public BookSortAssistanceLv GetBookSortAssistanceLv(long assistanceLv)
		{
			// new BookSortAssistanceMB.<>c__DisplayClass26_0().assistanceLv = assistanceLv;
			// Func<BookSortAssistanceLv, bool> func;
			// return Enumerable.FirstOrDefault<BookSortAssistanceLv>(this.<BookSortAssistanceLvList>k__BackingField, func);
            throw new NotImplementedException();
		}

		public bool IsInTime(DateTime localDateTime)
		{
			// string text = this.<EndTime>k__BackingField;
			// bool flag;
			// if (flag)
			// {
			// 	bool flag2;
			// 	return flag2;
			// }
            throw new NotImplementedException();
		}

		public long GetRewardItemCount(long assistanceLevel, long rewardGrade, long questGrade)
		{
			// BookSortAssistanceLv bookSortAssistanceLv = this.GetBookSortAssistanceLv(assistanceLevel);
			// if (bookSortAssistanceLv != 0)
			// {
			// 	long num = Math.Min(rewardGrade, assistanceLevel);
			// 	long num2 = bookSortAssistanceLv.<ItemCountList>k__BackingField[(int)assistanceLevel];
			// }
			// throw new NullReferenceException();
            throw new NotImplementedException();
		}
	}
}
