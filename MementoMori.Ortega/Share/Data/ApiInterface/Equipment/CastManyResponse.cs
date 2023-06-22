using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Data.Item.Model;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Equipment
{
	[MessagePackObject(true)]
	public class CastManyResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<UserEquipment> ResultEquipmentList
		{
			get;
			set;
		}

		public List<UserItem> ResultItemList
		{
			get;
			set;
		}

		public UserSyncData UserSyncData
		{
			get;
			set;
		}

		public CastManyResponse()
		{
		}
	}
}
