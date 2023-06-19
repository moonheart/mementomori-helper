using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("アカウント停止タイプ")]
	public enum AccountSuspensionType
	{
		[Description("解除")]
		Lift,
		[Description("永久停止")]
		Permanent,
		[Description("時限停止")]
		Period
	}
}
