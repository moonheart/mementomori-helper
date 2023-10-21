using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Mission
{
	[MessagePackObject(true)]
	[OrtegaApi("mission/receivePanelMissionBingoReward", true, false)]
	public class ReceivePanelMissionBingoRewardRequest : ApiRequestBase
	{
		public int SheetNo { get; set; }

		public BingoType BingoType { get; set; }
	}
}
