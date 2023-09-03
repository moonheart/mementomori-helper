using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Auth
{
	[MessagePackObject(true)]
	[OrtegaAuth("auth/createAccessToken", false, true)]
	public class CreateAccessTokenRequest : ApiRequestBase
	{
		public string ClientKey { get; set; }

		public long UserId { get; set; }
	}
}
