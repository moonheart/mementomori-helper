using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("交換所スフィア")]
	[MessagePackObject(true)]
	public class TradeShopSphereMB : MasterBookBase
	{
		[Description("スフィアのLv")]
		[PropertyOrder(1)]
		public int SphereLevel
		{
			get;
		}

		[Description("消費アイテム1")]
		[Nest(false, 0)]
		[PropertyOrder(2)]
		public UserItem ConsumeItem1
		{
			get;
		}

		[Description("消費アイテム2")]
		[PropertyOrder(3)]
		[Nest(false, 0)]
		public UserItem ConsumeItem2
		{
			get;
		}

		[SerializationConstructor]
		public TradeShopSphereMB(long id, bool? isIgnore, string memo, int sphereLevel, UserItem consumeItem1, UserItem consumeItem2)
			:base(id, isIgnore, memo)
		{
			SphereLevel = sphereLevel;
			ConsumeItem1 = consumeItem1;
			ConsumeItem2 = consumeItem2;
		}

		public TradeShopSphereMB() :base(0L, false, ""){}
	}
}
