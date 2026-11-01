using MementoMori.Ortega.Share.Data.Chat;
using MessagePack;
using MementoMori.Ortega.Share.Data;

namespace MementoMori.Ortega.Share.Data.ApiInterface.User
{
    [MessagePackObject(true)]
    public class LoginPlayerResponse : ApiResponseBase, IUserSyncApiResponse
    {
        public string AuthTokenOfMagicOnion { get; set; }

        public BanChatInfo BanChatInfo { get; set; }

        public UserSyncData UserSyncData { get; set; }

        public GuildSyncData GuildSyncData { get; set; }

        public LoginPlayerResponse()
        {
        }
    }
}