using MessagePack;

namespace MementoMori.Ortega.ServerLib.Models.MySql.Dto
{
    [MessagePackObject(true)]
    public class UserShopAchievementPackDtoInfo
    {
        public long ChapterId { get; set; }

        public long ShopAchievementPackId { get; set; }

        public UserShopAchievementPackDtoInfo()
        {
        }
    }
}