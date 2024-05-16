using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildTower
{
	[MessagePackObject(true)]
	public class EntryCharacterResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<string> EntryCharacterGuidList { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
