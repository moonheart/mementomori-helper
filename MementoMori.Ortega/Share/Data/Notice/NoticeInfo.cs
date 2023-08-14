using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Notice
{
	[MessagePackObject(true)]
	[Description("お知らせ情報")]
	public class NoticeInfo
	{
		[Description("バナーID")]
		public long BannerId { get; set; }

		[Description("ボタン画像タイプ")]
		public NoticeButtonImageType ButtonImageType { get; set; }

		[Description("ボタン上のタイトル")]
		public string ButtonTitle { get; set; }

		[Description("カテゴリー")]
		public NoticeCategoryType CategoryType { get; set; }

		[Description("グループID")]
		public long GroupId { get; set; }

		[Description("NoticeMBのId")]
		public long Id { get; set; }

		[Description("言語種別")]
		public LanguageType LanguageType { get; set; }

		[Description("本文")]
		public string MainText { get; set; }

		[Description("本文上のタイトル")]
		public string Title { get; set; }
	}
}
