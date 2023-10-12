using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GlobalGvg
{
	[MessagePackObject(true)]
	[OrtegaApi("globalGvg/receiveGlobalGvgReward", true, false)]
	public class ReceiveGlobalGvgRewardRequest : ApiRequestBase
	{
		public List<long> CastleIdList { get; set; }
	}
}
