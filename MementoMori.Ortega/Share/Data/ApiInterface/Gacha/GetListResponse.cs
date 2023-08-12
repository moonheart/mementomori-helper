using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Gacha;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Gacha
{
    [MessagePackObject(true)]
    public class GetListResponse : ApiResponseBase, IUserSyncApiResponse
    {
        public List<GachaCaseInfo> GachaCaseInfoList { get; set; }

        public List<GachaDestinyLogInfo> GachaDestinyLogInfoList { get; set; }

        public GachaElementInfo GachaElementInfo { get; set; }

        public bool IsGetFirstBonusRelicGacha { get; set; }

        public bool IsGetSecondBonusRelicGacha { get; set; }

        public bool IsFreeChangeRelicGacha { get; set; }

        public UserSyncData UserSyncData { get; set; }
    }
}