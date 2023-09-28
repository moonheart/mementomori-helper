using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
	[MessagePackObject(true)]
	public class UserPanelMissionDtoInfo
	{
		public int SheetNo { get; set; }

		public List<BingoType> ReceivedBingoTypeList{ get; set; }
	}
}
