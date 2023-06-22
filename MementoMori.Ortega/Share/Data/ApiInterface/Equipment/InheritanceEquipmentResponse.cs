using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Equipment
{
	[MessagePackObject(true)]
	public class InheritanceEquipmentResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public UserSyncData UserSyncData
		{
			get;
			set;
		}

		public InheritanceEquipmentResponse()
		{
		}
	}
}
