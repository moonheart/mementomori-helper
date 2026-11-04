using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission
{
	[MessagePackObject(true)]
	[OrtegaApi("unionMission/buyAcceptCount", true, false)]
	public class UnionMissionBuyAcceptCountRequest : ApiRequestBase
	{
		public int BuyCount { get; set; }
	}
}
