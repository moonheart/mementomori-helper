using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Utils;

namespace MementoMori.Ortega.Share.Data.Item.Model
{
	public class UserQuestQuickTicket : IUserItem
	{
		public UserQuestQuickTicket(QuestQuickTicketType ticketType, long itemCount)
		{
			this.ItemId = (long)ticketType;
			this.ItemCount = itemCount;
		}

		public long ItemCount { get; }

		public long ItemId { get; }

		public ItemType ItemType { get; } = (ItemType)((ulong)10L);

		public int GetQuestQuickTicketTime()
		{
			return QuestQuickTicketUtil.GetQuestQuickTicketTime((QuestQuickTicketType)this.ItemId);
		}
	}
}
