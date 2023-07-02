using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Equipment
{
	[OrtegaApi("equipment/training", true, false)]
	[MessagePackObject(true)]
	public class TrainingRequest : ApiRequestBase
	{
		public string EquipmentGuid { get; set; }

		public List<BaseParameterType> ParameterLockedList { get; set; }
	}
}
