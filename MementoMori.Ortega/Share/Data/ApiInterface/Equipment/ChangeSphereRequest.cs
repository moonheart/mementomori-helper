using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Equipment
{
	[OrtegaApi("equipment/changeSphere", true, false)]
	[MessagePackObject(true)]
	public class ChangeSphereRequest : ApiRequestBase
	{
		public List<SphereChangeInfo> SphereChangeInfos { get; set; }

		public bool IsFromSphereEquipmentListView { get; set; }
	}

	[MessagePackObject(true)]
	public class ChangeSphereResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public UserSyncData UserSyncData { get; set; }
	}
}
