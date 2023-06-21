using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("交換所種類")]
	public enum TradeShopType
	{
		[Description("一般")]
		Normal,
		[Description("通常武具合成")]
		NormalEquipmentComposite,
		[Description("パターン2")]
		Fix,
		[Description("店舗")]
		Store,
		[Description("スフィア")]
		Sphere
	}
}
