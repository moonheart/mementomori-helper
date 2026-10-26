using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.User
{
	[MessagePackObject(true)]
	[OrtegaApi("user/saveUsedDeck", true, false)]
	public class SaveUsedDeckRequest : ApiRequestBase
	{
		public long CopyPlayerId { get; set; }
		public DeckUseContentType DeckUseContentType { get; set; }
		public List<string> UserCharacterGuids { get; set; }
		public int DeckNo { get; set; }
		public bool UseLockEquipmentInFriendBattle { get; set; }
	}

	[MessagePackObject(true)]
	public class SaveUsedDeckResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public UserSyncData UserSyncData { get; set; }
	}
}
