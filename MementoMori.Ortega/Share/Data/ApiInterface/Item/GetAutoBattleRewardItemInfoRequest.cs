using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Item
{
	[MessagePackObject(true)]
	[OrtegaApi("item/getAutoBattleRewardItemInfo", true, false)]
	public class GetAutoBattleRewardItemInfoRequest : ApiRequestBase
	{
		public QuestQuickTicketType ItemType { get; set; }
	}
}
