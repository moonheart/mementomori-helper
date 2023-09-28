using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest
{
	[MessagePackObject(true)]
	[OrtegaApi("bountyQuest/remake", true, false)]
	public class RemakeRequest : ApiRequestBase
	{
	}
}
