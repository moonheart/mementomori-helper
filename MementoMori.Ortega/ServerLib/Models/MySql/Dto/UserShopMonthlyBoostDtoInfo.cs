using MessagePack;

namespace MementoMori.Ortega.ServerLib.Models.MySql.Dto
{
    [MessagePackObject(true)]
    public class UserShopMonthlyBoostDtoInfo
    {
        public long ExpirationTimeStamp { get; set; }

        public bool IsPrePurchased { get; set; }

        public int LatestReceivedDate { get; set; }

        public long PlayerId { get; set; }

        public int PrevReceivedDate { get; set; }

        public long ShopMonthlyBoostId { get; set; }

        public UserShopMonthlyBoostDtoInfo()
        {
        }
    }
}