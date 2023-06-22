using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Equipment
{
	[MessagePackObject(true)]
	[OrtegaApi("equipment/castMany", true, false)]
	public class CastManyRequest : ApiRequestBase
	{
		public EquipmentRarityFlags RarityFlags
		{
			get;
			set;
		}

		public CastManyRequest()
		{
		}
	}
}
