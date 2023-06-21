using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("宝箱天井設定")]
	[MessagePackObject(true)]
	public class TreasureChestCeilingMB : MasterBookBase
	{
		[Description("天井条件回数")]
		[PropertyOrder(2)]
		public int CeilingCount
		{
			get;
			set;
		}

		[Description("天井を適用するTreasureChestItemのId")]
		[PropertyOrder(1)]
		public long TreasureChestItemId
		{
			get;
		}

		[SerializationConstructor]
		public TreasureChestCeilingMB(long id, bool? isIgnore, string memo, long treasureChestItemId, int ceilingCount)
			:base(id, isIgnore, memo)
		{
			TreasureChestItemId = treasureChestItemId;
			CeilingCount = ceilingCount;
		}

		public TreasureChestCeilingMB() :base(0L, false, ""){}
	}
}
