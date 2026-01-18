using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.BookSortAssistance
{
	[MessagePackObject(true)]
	public class BookSortAssistanceLv
	{
		[Description("お手伝いレベル")]
		public long AssistanceLv { get; set; }

		[Description("必要派遣回数")]
		public long RequiredDispatchCount { get; set; }

		[Description("基本お手伝い枠数")]
		public long BaseAssistanceCount { get; set; }

		[Description("一括派遣解放")]
		public bool IsAvailableBulkDispatch { get; set; }

		[Description("出現クエストグレード数")]
		public long QuestGrade { get; set; }

		[Description("出現クエスト表示名")]
		public string QuestNameKey { get; set; }

		[Description("クエスト最大完了タイム")]
		public string MaxRequiredTime { get; set; }

		[Description("報酬アイテムのアイテムid")]
		public long RewardItemId { get; set; }

		[Description("報酬アイテムのアイテムtype")]
		public ItemType RewardItemType { get; set; }

		[Description("達成グレード数ごとの報酬アイテム数量")]
		public List<long> ItemCountList { get; set; }

		public long GetRewardItemCount(long grade)
		{
			// int size = this.<ItemCountList>k__BackingField._size;
			// long num = grade - 1L;
			// return this.<ItemCountList>k__BackingField[(int)num];
            throw new NotImplementedException();
        }
	}
}
