using System.ComponentModel;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class ShopProductWebStoreGuidancePanel
	{
		[Description("コピーボタンを表示フラグ")]
		public bool IsDisplayCopyButton { get; set; }

		[Description("お知らせグループId")]
		public long NoticeGroupId { get; set; }
	}
}
