using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item.Model;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Equipment
{
	[MessagePackObject(true)]
	[OrtegaApi("equipment/cast", true, false)]
	public class CastRequest : ApiRequestBase
	{
		public string Guid { get; set; }

        public UserEquipment UserEquipment { get; set; }
    }
}
