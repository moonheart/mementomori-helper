using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Gacha;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Gacha
{
	[MessagePackObject(true)]
	public class ChangeGachaRelicResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<GachaCaseInfo> GachaCaseInfoList { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
