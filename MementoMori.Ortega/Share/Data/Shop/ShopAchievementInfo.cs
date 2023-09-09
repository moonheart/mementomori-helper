using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class ShopAchievementInfo
	{
		[Description("報酬解放章")]
		public int RequiredChapterId { get; set; }

		[Nest(true, 1)]
		[Description("報酬リスト")]
		public List<UserItem> UserItemList { get; set; }
    }
}
