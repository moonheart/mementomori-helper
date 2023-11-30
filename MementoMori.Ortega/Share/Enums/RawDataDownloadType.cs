using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("RawDataダウンロードタイプ")]
	public enum RawDataDownloadType
	{
		[Description("なし")]
		None,
		[Description("タイトルダウンロード")]
		Title,
		[Description("タイトルダウンロード(JP)")]
		TitleLanguageJP,
		[Description("タイトルダウンロード(US)")]
		TitleLanguageUS,
		[Description("タイトルダウンロード(KR)")]
		TitleLanguageKR,
		[Description("タイトルダウンロード(TW)")]
		TitleLanguageTW,
		[Description("タイトルダウンロード(FR)")]
		TitleLanguageFR,
		[Description("タイトルダウンロード(CN)")]
		TitleLanguageCN,
		[Description("タイトルダウンロード(ES)")]
		TitleLanguageES,
		[Description("タイトルダウンロード(PT)")]
		TitleLanguagePT,
		[Description("タイトルダウンロード(TH)")]
		TitleLanguageTH,
		[Description("タイトルダウンロード(ID)")]
		TitleLanguageID,
		[Description("タイトルダウンロード(VN)")]
		TitleLanguageVN,
		[Description("タイトルダウンロード(RU)")]
		TitleLanguageRU,
		[Description("タイトルダウンロード(DE)")]
		TitleLanguageDE
	}
}
