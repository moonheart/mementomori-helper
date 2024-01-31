using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
    [MessagePackObject(true)]
    public class ShopProductChargeBonus
    {
        [Description("ShopChargeBonusMBのID")]
        public long ShopChargeBonusMBId { get; set; }

        [Description("詳細ダイアログベース画像ID")]
        public long DialogImageId { get; set; }

        [Description("開始日時")]
        public string StartDateTime { get; set; }

        [Description("終了日時")]
        public string EndDateTime { get; set; }

        [Description("商品パネルのベース画像ID")]
        public long ImageId { get; set; }

        [Description("商品パネルのメッセージキー")]
        public string MessageKey { get; set; }

        [Description("表示名キー")]
        public string NameKey { get; set; }

        [Description("目標リスト")]
        public List<ShopChargeBonusMissionDetail> ShopChargeBonusMissionDetailList { get; set; }

        [Description("チャージ特典条件タイプ")]
        public ShopChargeBonusMissionType ShopChargeBonusMissionType { get; set; }

        [Description("詳細ダイアログ概要説明キー")]
        public string SummaryKey { get; set; }

        [Description("シンボル画像ID")]
        public long SymbolImageId { get; set; }
    }
}