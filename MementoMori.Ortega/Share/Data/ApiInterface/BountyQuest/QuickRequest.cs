using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest
{
	[MessagePackObject(true)]
	[OrtegaApi("bountyQuest/quick", true, false)]
	public class QuickRequest : ApiRequestBase
	{
		public long BountyQuestId { get; set; }
	}
}
