using MementoMori.Ortega.Share.Enums;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Data.Shop;

[MessagePackObject(true)]
public class ShopBannerInfo
{
    [Description("ショップバナーの表示タイプ")]
    public ShopBannerDisplayType ShopBannerDisplayType { get; set; }

    [Description("表示テキストキー")]
    public string DisplayTextKey { get; set; }

    [Description("テキスト座標設定")]
    public CustomTextSimpleLayoutInfo CustomTextSimpleLayoutInfo { get; set; }

    [Description("テキストアライメント設定")]
    public int TextAlignment { get; set; }

    [Description("遷移タイプ")]
    public TransferSpotType TransferSpotType { get; set; }

    [Description("遷移情報")]
    public string TransferSpotInfo { get; set; }

    [Description("お知らせグループId")]
    public long NoticeGroupId { get; set; }

    [Description("コピーボタンを表示フラグ")]
    public bool IsDisplayCopyButton { get; set; }
}
