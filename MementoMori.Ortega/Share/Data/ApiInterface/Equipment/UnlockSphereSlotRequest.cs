using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Equipment
{
	[OrtegaApi("equipment/unlockSphereSlot", true, false)]
	[MessagePackObject(true)]
	public class UnlockSphereSlotRequest : ApiRequestBase
	{
		public string EquipmentGuid { get; set; }

		public int UnlockCount { get; set; }
	}

	[MessagePackObject(true)]
	public class UnlockSphereSlotResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public UserSyncData UserSyncData { get; set; }
	}
}
