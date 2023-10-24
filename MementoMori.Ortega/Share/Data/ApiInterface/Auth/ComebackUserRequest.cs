using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Auth
{
	[MessagePackObject(true)]
	[OrtegaAuth("auth/comebackUser", false, true)]
	public class ComebackUserRequest : ApiRequestBase, IHasSteamTicketApiRequest
	{
		public long FromUserId { get; set; }

		public string OneTimeToken { get; set; }

		public long ToUserId { get; set; }

		public string SteamTicket { get; set; }
	}
}
