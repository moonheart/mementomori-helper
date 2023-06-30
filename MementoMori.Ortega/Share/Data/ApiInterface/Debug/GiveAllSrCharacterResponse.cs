using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Debug
{
	[MessagePackObject(true)]
	public class GiveAllSrCharacterResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public UserSyncData UserSyncData
		{
			get;
			set;
		}
	}
}
