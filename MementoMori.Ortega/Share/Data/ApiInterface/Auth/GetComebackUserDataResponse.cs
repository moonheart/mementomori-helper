using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Auth;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Auth
{
	[MessagePackObject(true)]
	public class GetComebackUserDataResponse : ApiResponseBase
	{
		public bool IsReservedAccountDeletion { get; set; }

		public PlayerDataInfo LastLoginPlayerDataInfo { get; set; }
		public string OneTimeToken { get; set; }

		public long UserId { get; set; }
	}
}
