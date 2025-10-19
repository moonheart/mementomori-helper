using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Player;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	public class GetPlayerInfoListResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<long> AlreadyReceiveFriendPointPlayerIdList { get; set; }

		public List<long> CanReceiveFriendPointPlayerIdList { get; set; }

		public List<long> CanSendFriendPointPlayerIdList { get; set; }

		public int CurrentTypePlayerNum { get; set; }

		public int FriendNum { get; set; }

		public List<NewFriendInfo> NewFriendInfoList { get; set; }

		public List<PlayerInfo> PlayerInfoList { get; set; }

		public int ReceivedFriendPointCount { get; set; }
        
        public Dictionary<long, string> FriendBattleDefenseDeckCommentMap { get; set; }

        public UserSyncData UserSyncData { get; set; }
	}
}
