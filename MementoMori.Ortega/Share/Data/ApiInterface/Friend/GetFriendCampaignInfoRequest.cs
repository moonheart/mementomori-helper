using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	[OrtegaApi("friend/getFriendCampaignInfo", true, false)]
	public class GetFriendCampaignInfoRequest : ApiRequestBase
	{
	}
}
