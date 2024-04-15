using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Auth
{
	[MessagePackObject(true)]
	[OrtegaAuth("auth/createWorldPlayer", true, false)]
	public class CreateWorldPlayerRequest : ApiRequestBase
	{
		public long WorldId { get; set; }

		public string Comment { get; set; }

		public string Name { get; set; }
	}
}
