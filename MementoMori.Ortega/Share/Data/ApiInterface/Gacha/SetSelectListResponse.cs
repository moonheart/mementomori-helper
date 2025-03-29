using MementoMori.Ortega.Share.Data.Gacha;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Gacha
{
    [MessagePackObject(true)]
    public class SetSelectListResponse : ApiResponseBase, IUserSyncApiResponse
    {
        public GachaSelectListType GachaSelectListType { get; set; }

        public List<long> SetCharacterIdList { get; set; }

        public List<GachaCaseInfo> GachaCaseInfoList { get; set; }

        public UserSyncData UserSyncData { get; set; }
    }
}