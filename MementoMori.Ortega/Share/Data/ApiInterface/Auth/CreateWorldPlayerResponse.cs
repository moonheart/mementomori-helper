using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Auth
{
	[MessagePackObject(true)]
	public class CreateWorldPlayerResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public string ApiHost { get; set; }

		public bool CanSkipTutorial { get; set; }

		public string MagicOnionHost { get; set; }

		public int MagicOnionPort { get; set; }

		public long PlayerId { get; set; }

		public string Password { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
