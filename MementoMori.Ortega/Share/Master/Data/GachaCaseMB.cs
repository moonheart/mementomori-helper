using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("ガチャ筐体")]
	[MessagePackObject(true)]
	public class GachaCaseMB : MasterBookBase, IHasJstStartEndTime
	{
		[PropertyOrder(3)]
		[Description("表示順(降順)")]
		public int DisplayOrder
		{
			get;
		}

		[Description("属性(属性ガチャ用)")]
		[PropertyOrder(5)]
		public ElementType ElementType
		{
			get;
		}

		[Description("GachaCaseUiMBのID")]
		[PropertyOrder(1)]
		public long GachaCaseUiId
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("カテゴリー")]
		public GachaCategoryType GachaCategoryType
		{
			get;
		}

		[Description("運命(運命ガチャ用)")]
		[PropertyOrder(7)]
		public GachaDestinyType GachaDestinyType
		{
			get;
		}

		[Description("ガチャグループタイプ")]
		[PropertyOrder(4)]
		public GachaGroupType GachaGroupType
		{
			get;
		}

		[Description("聖遺物(聖天使の信託ガチャ用)")]
		[PropertyOrder(6)]
		public GachaRelicType GachaRelicType
		{
			get;
		}

		[Description("表示用残り時間初期化タイプ")]
		[PropertyOrder(9)]
		public GachaResetType GachaResetType
		{
			get;
			set;
		}

		[Description("セレクトリストタイプ")]
		[PropertyOrder(8)]
		public GachaSelectListType GachaSelectListType
		{
			get;
		}

		[PropertyOrder(10)]
		[Description("ガチャ天井表示用フラグ")]
		public GachaCaseFlags GachaCaseFlags
		{
			get;
		}

		[PropertyOrder(13)]
		[Description("ガチャの日数制限(プレイヤー生成時から計算、０は無視)")]
		public int GachaLimitDayFromCreatePlayer
		{
			get;
		}

		[SerializationConstructor]
		public GachaCaseMB(long id, bool? isIgnore, string memo, int displayOrder, ElementType elementType, long gachaCaseUiId, GachaCategoryType gachaCategoryType, GachaGroupType gachaGroupType, GachaRelicType gachaRelicType, GachaResetType gachaResetType, GachaDestinyType gachaDestinyType, GachaSelectListType gachaSelectListType, GachaCaseFlags gachaCaseFlags, string startTimeFixJST, string endTimeFixJST, int gachaLimitDayFromCreatePlayer)
			:base(id, isIgnore, memo)
		{
			DisplayOrder = displayOrder;
			ElementType = elementType;
			GachaCaseUiId = gachaCaseUiId;
			GachaCategoryType = gachaCategoryType;
			GachaGroupType = gachaGroupType;
			GachaRelicType = gachaRelicType;
			GachaResetType = gachaResetType;
			GachaDestinyType = gachaDestinyType;
			GachaSelectListType = gachaSelectListType;
			GachaCaseFlags = gachaCaseFlags;
			StartTimeFixJST = startTimeFixJST;
			EndTimeFixJST = endTimeFixJST;
			GachaLimitDayFromCreatePlayer = gachaLimitDayFromCreatePlayer;
		}

		public GachaCaseMB() : base(0, false, "")
		{
		}

		[Description("新ガチャの開始時間(JST)")]
		[PropertyOrder(11)]
		public string StartTimeFixJST
		{
			get;
		}

		[Description("新ガチャの終了時間(JST)")]
		[PropertyOrder(12)]
		public string EndTimeFixJST
		{
			get;
		}
	}
}
