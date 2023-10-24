using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Auth
{
	[MessagePackObject(true)]
	public class ComebackUserResponse : ApiResponseBase
	{
		public string ClientKey { get; set; }
	}
}
