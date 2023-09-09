using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
    [MessagePackObject(true)]
    public class ShopProductCurrency
    {
        [Description("お得バッジ画像ID")]
        public long BadgeImageId { get; set; }

        [Description("告知用大画像ID")]
        public long BigImageId { get; set; }

        [Description("ボーナス率")]
        public string BonusTextKey { get; set; }

        [Description("画像ID")]
        public long ImageId { get; set; }

        [Description("商品名キー")]
        public string NameKey { get; set; }

        [Description("ProductId")]
        public string ProductId { get; set; }

        [Description("商品値段")]
        public int ShopProductPrice { get; set; }

        [Description("ボーナス報酬")]
        public UserItem UserItemBonus { get; set; }

        [Description("報酬リスト")]
        public IReadOnlyList<UserItem> UserItemList { get; set; }
    }
}