using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Title;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Auth
{
    [MessagePackObject(true)]
    public class CreateUserResponse : ApiResponseBase, IUserSyncApiResponse
    {
        public AccountMessageInfo AccountMessageInfo { get; set; }

        public string ClientKey { get; set; }

        public long RecommendWorldId { get; set; }

        public long UserId { get; set; }

        public List<WarningMessageInfo> WarningMessageInfos { get; set; }

        public List<long> WorldIdList { get; set; }

        public Dictionary<long, string> SpecialWorldDict { get; set; }

        public UserSyncData UserSyncData { get; set; }
    }
}