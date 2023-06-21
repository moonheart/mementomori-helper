using System.ComponentModel;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Data.Global;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("ガチャ筐体描画用")]
	[MessagePackObject(true)]
	public class GachaCaseUiMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("バナー")]
		[Nest(false, 0)]
		public TranslatedText Banner
		{
			get;
		}

		[PropertyOrder(13)]
		[Description("バナー画像番号")]
		public int BannerImageNumber
		{
			get;
		}

		[PropertyOrder(12)]
		[Nest(false, 0)]
		[Description("レイアウト")]
		public CustomTextLayout CustomTextLayout
		{
			get;
		}

		[Description("詳細ダイアログ 詳細")]
		[Nest(false, 0)]
		[PropertyOrder(18)]
		public TranslatedText DetailDialogDetail
		{
			get;
		}

		[Description("詳細ダイアログ 見出し")]
		[PropertyOrder(17)]
		[Nest(false, 0)]
		public TranslatedText DetailDialogHeading
		{
			get;
		}

		[Description("詳細ダイアログ 注意")]
		[PropertyOrder(19)]
		[Nest(false, 0)]
		public TranslatedText DetailDialogNotes
		{
			get;
		}

		[Description("ガチャ詳細URL")]
		[PropertyOrder(6)]
		[Nest(false, 0)]
		public TranslatedText DetailURL
		{
			get;
		}

		[Description("武具図鑑タブ番号")]
		[PropertyOrder(15)]
		public int EquipmentPictureBookTabIndex
		{
			get;
		}

		[Description("説明補足")]
		[PropertyOrder(3)]
		[Nest(false, 0)]
		public TranslatedText Explanation
		{
			get;
		}

		[PropertyOrder(16)]
		[Description("ボーナスゲージタイプ")]
		public GachaBonusGaugeType GachaBonusGaugeType
		{
			get;
		}

		[PropertyOrder(7)]
		[Nest(false, 0)]
		[Description("ボタンメッセージキー")]
		public TranslatedText GachaButtonMessage
		{
			get;
		}

		[Description("ヘッダーアイテム1")]
		[PropertyOrder(8)]
		[Nest(false, 0)]
		public UserItem HeaderItem1
		{
			get;
		}

		[Description("ヘッダーアイテム2")]
		[PropertyOrder(9)]
		[Nest(false, 0)]
		public UserItem HeaderItem2
		{
			get;
		}

		[Nest(false, 0)]
		[PropertyOrder(10)]
		[Description("ヘッダーアイテム3")]
		public UserItem HeaderItem3
		{
			get;
		}

		[Description("回数報酬表示")]
		[PropertyOrder(11)]
		public bool IsLotteryViewShowBonusItem
		{
			get;
		}

		[Nest(false, 0)]
		[PropertyOrder(2)]
		[Description("タイトル")]
		public TranslatedText Name
		{
			get;
		}

		[Description("ピックアップされるキャラクター")]
		[PropertyOrder(4)]
		public long PickUpCharacterId
		{
			get;
		}

		[Description("時間テキスト")]
		[PropertyOrder(14)]
		[Nest(false, 0)]
		public TranslatedText TimeFormat
		{
			get;
		}

		[Description("タイトル色")]
		[PropertyOrder(5)]
		public GachaTitleColorType TitleColorType
		{
			get;
		}

		[Description("追加報酬アイテム")]
		[PropertyOrder(20)]
		[Nest(false, 0)]
		public IReadOnlyList<UserItem> AddRewardItems
		{
			get;
		}

		[PropertyOrder(21)]
		[Description("天井テキスト")]
		public string CeilingCountFormatKey
		{
			get;
		}

		[SerializationConstructor]
		public GachaCaseUiMB(long id, bool? isIgnore, string memo, GachaBonusGaugeType gachaBonusGaugeType, int equipmentPictureBookTabIndex, TranslatedText timeFormat, int bannerImageNumber, CustomTextLayout customTextLayout, GachaTitleColorType titleColorType, TranslatedText detailURL, TranslatedText explanation, TranslatedText gachaButtonMessage, TranslatedText name, TranslatedText banner, long pickUpCharacterId, UserItem headerItem1, UserItem headerItem2, UserItem headerItem3, bool isLotteryViewShowBonusItem, TranslatedText detailDialogDetail, TranslatedText detailDialogHeading, TranslatedText detailDialogNotes, IReadOnlyList<UserItem> addRewardItems, string ceilingCountFormatKey)
			:base(id, isIgnore, memo)
		{
			this.GachaBonusGaugeType = gachaBonusGaugeType;
			this.EquipmentPictureBookTabIndex = equipmentPictureBookTabIndex;
			this.TimeFormat = timeFormat;
			this.BannerImageNumber = bannerImageNumber;
			this.CustomTextLayout = customTextLayout;
			this.TitleColorType = titleColorType;
			this.DetailURL = detailURL;
			this.Explanation = explanation;
			this.GachaButtonMessage = gachaButtonMessage;
			this.Name = name;
			this.Banner = banner;
			this.PickUpCharacterId = pickUpCharacterId;
			this.HeaderItem1 = headerItem1;
			this.HeaderItem2 = headerItem2;
			this.HeaderItem3 = headerItem3;
			this.IsLotteryViewShowBonusItem = isLotteryViewShowBonusItem;
			this.DetailDialogDetail = detailDialogDetail;
			this.DetailDialogHeading = detailDialogHeading;
			this.DetailDialogNotes = detailDialogNotes;
			this.AddRewardItems = addRewardItems;
			this.CeilingCountFormatKey = ceilingCountFormatKey;
		}

		public GachaCaseUiMB() : base(0L, null, null)
		{
		}
	}
}
