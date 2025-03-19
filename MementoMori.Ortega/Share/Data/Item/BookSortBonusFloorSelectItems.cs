using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Item
{
	[MessagePackObject(true)]
	public class BookSortBonusFloorSelectItems
	{
		[Nest(true, 1)]
		public IReadOnlyList<UserItem> ItemList{ get; set; }

		public long StartMaxClearQuestId { get; set; }

		public long EndMaxClearQuestId { get; set; }
	}
}
