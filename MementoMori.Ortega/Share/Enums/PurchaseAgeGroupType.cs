using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("課金制限区分")]
	public enum PurchaseAgeGroupType
	{
		[Description("未設定")]
		None,
		[Description("16歳未満")]
		Under16YearsOld,
		[Description("16歳から17歳まで")]
		Between16To17YearsOld,
		[Description("18歳以上")]
		Over18YearsOld
	}
}
