using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("ショップポイント比率情報")]
	public class ShopPointRateMB : MasterBookBase, IHasStartEndTime
	{
		[PropertyOrder(1)]
		[Description("貯めるポイント比率")]
		public int SavePointRate
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("支払いタイプ")]
		public ShopPayType ShopPayType
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("開始日時")]
		public string StartTime
		{
			get;
		}

		[PropertyOrder(4)]
		[Description("終了日時")]
		public string EndTime
		{
			get;
		}

		[SerializationConstructor]
		public ShopPointRateMB(long id, bool? isIgnore, string memo, int savePointRate, ShopPayType shopPayType, string startTime, string endTime)
			:base(id, isIgnore, memo)
		{
			SavePointRate = savePointRate;
			ShopPayType = shopPayType;
			StartTime = startTime;
			EndTime = endTime;
		}

		public ShopPointRateMB():base(0, false, "")
		{
		}
	}
}
