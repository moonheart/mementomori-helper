using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Equipment
{
	[MessagePackObject(true)]
	[OrtegaApi("equipment/removeEquipment", true, false)]
	public class RemoveEquipmentRequest : ApiRequestBase
	{
		public string UserCharacterGuid { get; set; }

		public List<EquipmentSlotType> EquipmentSlotTypes{ get; set; }
	}
}
