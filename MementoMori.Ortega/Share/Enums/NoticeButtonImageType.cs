using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("お知らせボタンのシンボル画像タイプ")]
	public enum NoticeButtonImageType
	{
		[Description("表示なし")]
		None,
		[Description("インフォメーション")]
		Information,
		[Description("アップデート")]
		Update,
		[Description("メンテナンス")]
		Maintenance,
		[Description("不具合")]
		Bug,
		[Description("イベント")]
		Event,
		[Description("ガチャ")]
		Gacha,
		[Description("キャンペーン")]
		Campaign,
		[Description("その他")]
		Other,
		[Description("アンケート")]
		Survey
	}
}
