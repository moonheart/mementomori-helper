using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	[OrtegaApi("friend/updateBlockList", true, false)]
	public class UpdateBlockListRequest : ApiRequestBase
	{
		public bool IsBlock { get; set; }

		public long TargetPlayerId { get; set; }
	}
}
