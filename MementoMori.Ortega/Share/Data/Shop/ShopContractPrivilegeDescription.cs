using System.ComponentModel;
using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class ShopContractPrivilegeDescription
	{
		[Description("説明キー")]
		public string DescriptionKey { get; set; }

        [Description("表示番号")]
		public int DisplayNumber { get; set; }
	}
}
