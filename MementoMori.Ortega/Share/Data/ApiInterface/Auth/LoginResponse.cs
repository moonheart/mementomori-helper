using MementoMori.Ortega.Share.Data.Auth;
using MementoMori.Ortega.Share.Data.Title;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Auth
{
    [MessagePackObject(true)]
    public class LoginResponse : ApiResponseBase, IUserSyncApiResponse
    {
        public AccountMessageInfo AccountMessageInfo { get; set; }

        public bool IsReservedAccountDeletion { get; set; }

        public List<RemoteNotificationType> IgnoreTypes { get; set; }

        public List<PlayerDataInfo> PlayerDataInfoList { get; set; }

        public long RecommendWorldId { get; set; }

        public LanguageType TextLanguageType { get; set; }

        public LanguageType VoiceLanguageType { get; set; }

        public List<WarningMessageInfo> WarningMessageInfos { get; set; }

        public List<long> WorldIdList { get; set; }

        public UserSyncData UserSyncData { get; set; }

        public LoginResponse()
        {
        }
    }
}