using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Item
{
	[MessagePackObject(true)]
	[OrtegaApi("item/useAutoBattleRewardItem", true, false)]
	public class UseAutoBattleRewardItemRequest : ApiRequestBase
	{
		public QuestQuickTicketType ItemType { get; set; }

		public int UseCount { get; set; }
	}
}
