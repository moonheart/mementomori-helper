using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class VipGiftInfo
	{
		[Nest(true, 1)]
		[Description("獲得アイテムリスト")]
		public IReadOnlyList<UserItem> GetItemList
		{
			get;
			set;
		}

		[Nest(true, 1)]
		[Description("必要アイテムリスト")]
		public IReadOnlyList<UserItem> RequiredItemList
		{
			get;
			set;
		}

		[Description("ギフトID")]
		public long VipGiftId
		{
			get;
			set;
		}

		public VipGiftInfo()
		{
		}
	}
}
