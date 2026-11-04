using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Equipment
{
	[OrtegaApi("equipment/composeManySphere", true, false)]
	[MessagePackObject(true)]
	public class ComposeManySphereRequest : ApiRequestBase
	{
		public List<long> CategoryIdList { get; set; }
	}

	[MessagePackObject(true)]
	public class ComposeManySphereResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public bool IsSucceeded { get; set; }

		public List<UserItem> ResultItemList { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
