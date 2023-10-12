using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LocalGvg
{
	[MessagePackObject(true)]
	[OrtegaApi("localGvg/receiveLocalGvgReward", true, false)]
	public class ReceiveLocalGvgRewardRequest : ApiRequestBase
	{
		public List<long> CastleIdList{ get; set; }
	}
}
