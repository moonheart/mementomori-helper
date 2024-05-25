using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
    [MessagePackObject(true)]
    public class ShopProductGuerrillaPack
    {
        [Description("ダイアログベース画像ID")]
        public long DialogImageId { get; set; }

        [Description("割引率")]
        public int DiscountRate { get; set; }

        [Description("終了日時")]
        public long EndTime { get; set; }

        [Description("商品名キー")]
        public string NameKey { get; set; }

        [Description("ProductIdのDictonary")]
        public Dictionary<DeviceType, string> ProductIdDict { get; set; }

        [Description("ランク種別")]
        public ShopGuerrillaPackRankType ShopGuerrillaPackRankType { get; set; }

        [Description("商品値段")]
        public int ShopProductPrice { get; set; }

        [Description("ゲリラパックID")]
        public long ShopGuerrillaPackId { get; set; }

        [Description("ゲリラパック解放タイプ")]
        public ShopGuerrillaPackOpenType ShopGuerrillaPackOpenType { get; set; }

        [Description("ゲリラパック解放値")]
        public int ShopGuerrillaPackOpenValue { get; set; }

        [Description("訴求文言キー")]
        public string TextKey { get; set; }

        [Description("報酬リスト")]
        public List<UserItem> UserItemList { get; set; }
    }
}