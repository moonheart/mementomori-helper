using System.ComponentModel;
using MementoMori.Ortega.Share.Data;
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
		[Description("追加報酬アイテム")]
		[PropertyOrder(20)]
		[Nest(false, 0)]
		public IReadOnlyList<UserItem> AddRewardItems { get; }

		[Description("バナー画像番号")]
		[PropertyOrder(13)]
		public int BannerImageNumber { get; }

		[Description("バナー")]
		[PropertyOrder(1)]
		public string BannerKey { get; }

		[PropertyOrder(21)]
		[Description("天井テキスト")]
		public string CeilingCountFormatKey { get; }

		[Description("レイアウト")]
		[PropertyOrder(12)]
		[Nest(false, 0)]
		public CustomTextLayout CustomTextLayout { get; }

		[Description("詳細ダイアログ 詳細")]
		[PropertyOrder(18)]
		public string DetailDialogDetailKey { get; }

		[PropertyOrder(17)]
		[Description("詳細ダイアログ 見出し")]
		public string DetailDialogHeadingKey { get; }

		[Description("詳細ダイアログ 注意")]
		[PropertyOrder(19)]
		public string DetailDialogNotesKey { get; }

		[Description("武具図鑑タブ番号")]
		[PropertyOrder(15)]
		public int EquipmentPictureBookTabIndex { get; }

		[PropertyOrder(3)]
		[Description("説明補足")]
		public string ExplanationKey { get; }

		[PropertyOrder(16)]
		[Description("ボーナスゲージタイプ")]
		public GachaBonusGaugeType GachaBonusGaugeType { get; }

		[PropertyOrder(7)]
		[Description("ボタンメッセージキー")]
		public string GachaButtonMessageKey { get; }

		[Nest(false, 0)]
		[PropertyOrder(8)]
		[Description("ヘッダーアイテム1")]
		public UserItem HeaderItem1 { get; }

		[Nest(false, 0)]
		[PropertyOrder(9)]
		[Description("ヘッダーアイテム2")]
		public UserItem HeaderItem2 { get; }

		[PropertyOrder(10)]
		[Nest(false, 0)]
		[Description("ヘッダーアイテム3")]
		public UserItem HeaderItem3 { get; }

		[Description("回数報酬表示")]
		[PropertyOrder(11)]
		public bool IsLotteryViewShowBonusItem { get; }

		[PropertyOrder(2)]
		[Description("タイトル")]
		public string NameKey { get; }

		[PropertyOrder(4)]
		[Description("ピックアップされるキャラクター")]
		public long PickUpCharacterId { get; }

		[PropertyOrder(14)]
		[Description("時間テキスト")]
		public string TimeFormatKey { get; }

		[Description("タイトル色")]
		[PropertyOrder(5)]
		public GachaTitleColorType TitleColorType { get; }

		[SerializationConstructor]
		public GachaCaseUiMB(long id, bool? isIgnore, string memo, GachaBonusGaugeType gachaBonusGaugeType, int equipmentPictureBookTabIndex, string timeFormatKey, int bannerImageNumber, CustomTextLayout customTextLayout, GachaTitleColorType titleColorType, string explanationKey, string gachaButtonMessageKey, string nameKey, string bannerKey, long pickUpCharacterId, UserItem headerItem1, UserItem headerItem2, UserItem headerItem3, bool isLotteryViewShowBonusItem, string detailDialogDetailKey, string detailDialogHeadingKey, string detailDialogNotesKey, IReadOnlyList<UserItem> addRewardItems, string ceilingCountFormatKey)
		:base(id,isIgnore, memo)
		{
			this.GachaBonusGaugeType = gachaBonusGaugeType;
			this.EquipmentPictureBookTabIndex = equipmentPictureBookTabIndex;
			this.TimeFormatKey = timeFormatKey;
			this.BannerImageNumber = bannerImageNumber;
			this.CustomTextLayout = customTextLayout;
			this.TitleColorType = titleColorType;
			this.ExplanationKey = explanationKey;
			this.GachaButtonMessageKey = gachaButtonMessageKey;
			this.NameKey = nameKey;
			this.BannerKey = bannerKey;
			this.PickUpCharacterId = pickUpCharacterId;
			this.HeaderItem1 = headerItem1;
			this.HeaderItem2 = headerItem2;
			this.HeaderItem3 = headerItem3;
			this.IsLotteryViewShowBonusItem = isLotteryViewShowBonusItem;
			this.DetailDialogDetailKey = detailDialogDetailKey;
			this.DetailDialogHeadingKey = detailDialogHeadingKey;
			this.DetailDialogNotesKey = detailDialogNotesKey;
			this.AddRewardItems = addRewardItems;
			this.CeilingCountFormatKey = ceilingCountFormatKey;
		}

		public GachaCaseUiMB():base(0,false,null)
		{
			
		}
	}
}
