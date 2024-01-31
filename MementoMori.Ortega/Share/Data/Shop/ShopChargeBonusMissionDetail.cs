using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
    [MessagePackObject(true)]
    public class ShopChargeBonusMissionDetail
    {
        [Description("獲得済み回数")]
        public long AlreadyReceivedCount { get; set; }

        [Description("獲得可能回数")]
        public long CanReceiveCount { get; set; }

        [Description("日数")]
        public int Day { get; set; }

        [Description("獲得制限回数")]
        public long GetLimitCount { get; set; }

        [Description("目標設定値")]
        public long RequiredValue { get; set; }

        [Description("ShopChargeBonusMissionMBのID")]
        public long ShopChargeBonusMissionMBId { get; set; }

        [Description("目標表示テキストキー")]
        public string TextKey { get; set; }

        [Description("その日獲得した有償ダイヤの数")]
        public long TodayGetCurrency { get; set; }

        [Description("報酬リスト")]
        public List<UserItem> UserItemList { get; set; }

        [Description("報酬リスト2 (ギルド特典の場合のギルド報酬など)")]
        public List<UserItem> UserItemList2 { get; set; }
    }
}