using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("問い合わせボタンタイプ")]
	public enum InquiryButtonType
	{
		[Description("不明")]
		None,
		[Description("URL")]
		Url,
		[Description("メール")]
		Mail,
		[Description("アカウント削除")]
		AccountDelete
	}
}
