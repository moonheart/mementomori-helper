using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("書庫整理ボーナスフロア報酬")]
	public class BookSortBonusFloorRewardMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("開始フロア")]
		public int StartFloor { get; }

		[PropertyOrder(2)]
		[Description("終了フロア")]
		public int EndFloor { get; }

		[Nest(false, 0)]
		[PropertyOrder(3)]
		[Description("選択報酬ラインナップリスト")]
		public IReadOnlyList<BookSortBonusFloorSelectItems> SelectItemsList { get; }

		[SerializationConstructor]
		public BookSortBonusFloorRewardMB(long id, bool? isIgnore, string memo, int startFloor, int endFloor, IReadOnlyList<BookSortBonusFloorSelectItems> selectItemsList)
			: base(id, isIgnore, memo)
		{
            this.StartFloor = startFloor;
            this.EndFloor = endFloor;
            this.SelectItemsList = selectItemsList;
		}

		public BookSortBonusFloorRewardMB()
			: base(0L, null, null)
		{
		}

		public List<BookSortBonusFloorSelectItems> GetItemsByMaxClearQuestId(long maxClearQuestId)
		{
			return null;
		}

		public List<int> GetSelectableIndexList(long maxClearQuestId)
		{
			return null;
		}
	}
}
