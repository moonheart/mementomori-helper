using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum ShopChargeBonusMissionType
	{
		[Description("買い切りダイヤ商品購入")]
		Currency = 1,
		[Description("有償ダイヤ購入")]
		CurrencySum,
		[Description("有償ダイヤ連日購入")]
		CurrencySumDays,
        [Description("有償ダイヤ購入 ギルド特典")]
        CurrencySumGuild
	}
}
