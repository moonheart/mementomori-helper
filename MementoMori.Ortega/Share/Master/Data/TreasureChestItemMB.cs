using System.ComponentModel;
using MementoMori.Ortega.Share.Data.TreatureChest;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("宝箱から得られるアイテム")]
	[MessagePackObject(true)]
	public class TreasureChestItemMB : MasterBookBase
	{
		[Description("固定アイテムリスト")]
		[PropertyOrder(4)]
		[Nest(true, 1)]
		public IReadOnlyList<TreasureChestFixItem> FixItemList
		{
			get;
		}

		[Description("抽選報酬ID")]
		[PropertyOrder(2)]
		public long LotteryRewardId
		{
			get;
		}

		[Description("選択アイテムリスト")]
		[PropertyOrder(3)]
		[Nest(true, 0)]
		public IReadOnlyList<TreasureChestSelectItem> SelectItemList
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("ItemListの参照先")]
		public TreasureChestItemListType TreasureChestItemListType
		{
			get;
		}

		[SerializationConstructor]
		public TreasureChestItemMB(long id, bool? isIgnore, string memo, long lotteryRewardId, TreasureChestItemListType treasureChestItemListType, IReadOnlyList<TreasureChestFixItem> fixItemList, IReadOnlyList<TreasureChestSelectItem> selectItemList)
			:base(id, isIgnore, memo)
		{
			LotteryRewardId = lotteryRewardId;
			TreasureChestItemListType = treasureChestItemListType;
			FixItemList = fixItemList;
			SelectItemList = selectItemList;
		}

		public TreasureChestItemMB() :base(0L, false, ""){}
	}
}
