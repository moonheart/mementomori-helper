using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("定期自動更新タイプ")]
	public enum TradeShopAutoUpdateType
	{
		[Description("定期自動更新なし")]
		None,
		[Description("1日1回、4時に更新")]
		OneDay,
		[Description("1週間に1回、4時に更新")]
		OneWeek,
		[Description("1カ月に1回、4時に更新")]
		OneMonth,
		[Description("1日に複数回、指定時刻に更新")]
		SetTime
	}
}
