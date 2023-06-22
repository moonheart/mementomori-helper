using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Equipment
{
	[OrtegaApi("equipment/inheritanceEquipment", true, false)]
	[MessagePackObject(true)]
	public class InheritanceEquipmentRequest : ApiRequestBase
	{
		public string InheritanceEquipmentGuid
		{
			get;
			set;
		}

		public string SourceEquipmentGuid
		{
			get;
			set;
		}

		public InheritanceEquipmentRequest()
		{
		}
	}
}
