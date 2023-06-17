using MessagePack;

namespace MementoMori.Ortega.ServerLib.Models.MySql.Dto
{
    [MessagePackObject(true)]
    public class UserShopFreeGrowthPackDtoInfo
    {
        public long ShopGrowthPackId { get; set; }

        public bool IsBuff { get; set; }

        public long PlayerId { get; set; }

        public DateTime ReceiveDateTime { get; set; }

        public long ShopProductGrowthPackId { get; set; }

        public UserShopFreeGrowthPackDtoInfo()
        {
        }
    }
}