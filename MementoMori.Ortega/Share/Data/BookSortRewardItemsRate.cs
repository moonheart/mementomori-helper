using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data
{
	[MessagePackObject(true)]
	public class BookSortRewardItemsRate
	{
        public List<UserItem> ItemList { get; set; }

        public double LotteryRate { get; set; }
	}
}
