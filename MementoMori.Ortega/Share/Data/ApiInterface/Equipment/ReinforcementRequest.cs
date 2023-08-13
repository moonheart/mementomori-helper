using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Equipment
{
	[MessagePackObject(true)]
	[OrtegaApi("equipment/reinforcement", true, false)]
	public class ReinforcementRequest : ApiRequestBase
	{
		public string EquipmentGuid { get; set; }

		public int NumberOfTimes { get; set; }
	}
}
