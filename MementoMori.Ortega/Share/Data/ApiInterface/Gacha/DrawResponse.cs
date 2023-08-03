using MementoMori.Ortega.Share.Data.Gacha;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Gacha
{
    [MessagePackObject(true)]
    public class DrawResponse : ApiResponseBase, IUserSyncApiResponse
    {
        public List<GachaResultItem> BonusRewardItemList { get; set; }

        public List<UserItem> CharacterReleaseItemList { get; set; }

        public List<GachaCaseInfo> GachaCaseInfoList { get; set; }

        public List<GachaDestinyLogInfo> GachaDestinyLogInfoList { get; set; }

        public List<UserItem> GachaRewardAddItemList { get; set; }

        public List<GachaResultItem> GachaRewardItemList { get; set; }

        public bool IsFreeChangeRelicGacha { get; set; }

        public bool IsGetFirstBonusRelicGacha { get; set; }

        public bool IsGetSecondBonusRelicGacha { get; set; }

        public UserSyncData UserSyncData { get; set; }
    }
}