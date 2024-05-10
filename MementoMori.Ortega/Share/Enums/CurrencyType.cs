using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ダイヤ種別")]
	public enum CurrencyType
	{
		[Description("無償ダイヤ")]
		Free,
		[Description("有償ダイヤ(IOS)")]
		PaidIOS,
		[Description("有償ダイヤ(Android)")]
		PaidAndroid,
		[Description("有償ダイヤ(DMM)")]
		PaidDMM = 5,
		[Description("有償ダイヤ(Apk)")]
		PaidApk = 7
	}
}
