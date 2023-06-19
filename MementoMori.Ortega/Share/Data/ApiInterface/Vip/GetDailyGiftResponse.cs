using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Vip
{
	[MessagePackObject(true)]
	public class GetDailyGiftResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<UserItem> ItemList { get; set; }

		public UserSyncData UserSyncData{ get; set; }

		public GetDailyGiftResponse()
		{
		}
	}
}
