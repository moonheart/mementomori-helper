using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildTower
{
	[MessagePackObject(true)]
	public class GuildTowerReinforcementJobData
	{
		public List<UserItem> ConsumedMaterialItemList { get; set; }

		public JobFlags JobFlags { get; set; }

		public int Level { get; set; }

		public long GetConsumedMaterialCount(ItemType itemType, long itemId)
		{
			return ConsumedMaterialItemList.Find(d=>d.ItemType == itemType && d.ItemId == itemId)?.ItemCount ?? 0;
		}
	}
}
