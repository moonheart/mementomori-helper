using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Data.TradeShop;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("交換所タブ")]
	public class TradeShopTabMB : MasterBookBase, IHasStartEndTime
	{
		[PropertyOrder(14)]
		[Description("定期自動更新(タイプ４の更新時間)")]
		public IReadOnlyList<long> AutoUpdateTimes
		{
			get;
		}

		[Description("定期自動更新タイプ")]
		[PropertyOrder(13)]
		public TradeShopAutoUpdateType AutoUpdateType
		{
			get;
		}

		[Nest(false, 0)]
		[PropertyOrder(17)]
		[Description("消費アイテムリスト(表示用)")]
		public IReadOnlyList<ConsumeItemInfo> ConsumeItemInfos
		{
			get;
		}

		[Nest(false, 0)]
		[PropertyOrder(6)]
		[Description("レイアウト")]
		public CustomTextLayout CustomTextLayout
		{
			get;
		}

		[Description("デコレーション色")]
		[PropertyOrder(5)]
		public string DecorationColor
		{
			get;
		}

		[Description("デコレーションId")]
		[PropertyOrder(3)]
		public long DecorationId
		{
			get;
		}

		[Description("デコレーションスペシャルId")]
		[PropertyOrder(4)]
		public long DecorationSpecialId
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("アイコンId")]
		public long IconId
		{
			get;
		}

		[PropertyOrder(9)]
		[Description("強制非表示フラグ")]
		public bool IsHide
		{
			get;
		}

		[PropertyOrder(11)]
		[Description("未解放時に非表示にするフラグ")]
		public bool IsHideNotOpen
		{
			get;
		}

		[Description("手動更新可能フラグ")]
		[PropertyOrder(15)]
		public bool IsManualUpdate
		{
			get;
		}

		[Description("手動更新に必要なダイヤ数")]
		[PropertyOrder(16)]
		public int ManualUpdateCurrencyCount
		{
			get;
		}

		[Description("コンテンツ実行タイプ")]
		[PropertyOrder(18)]
		public OpenCommandType OpenCommandType
		{
			get;
		}

		[PropertyOrder(8)]
		[Description("並び順")]
		public int SortOrder
		{
			get;
		}

		[Description("タブ名")]
		[PropertyOrder(7)]
		public string TabNameKey
		{
			get;
		}

		[Description("店舗景品数")]
		[PropertyOrder(17)]
		public int TradeShopItemCount
		{
			get;
		}

		[PropertyOrder(12)]
		[Description("交換所種類")]
		public TradeShopType TradeShopType
		{
			get;
		}

		[SerializationConstructor]
		public TradeShopTabMB(long id, bool? isIgnore, string memo, OpenCommandType openCommandType, long iconId, long decorationId, long decorationSpecialId, string decorationColor, string tabNameKey, int sortOrder, bool isHide, bool isHideNotOpen, TradeShopType tradeShopType, TradeShopAutoUpdateType autoUpdateType, IReadOnlyList<long> autoUpdateTimes, bool isManualUpdate, int manualUpdateCurrencyCount, IReadOnlyList<ConsumeItemInfo> consumeItemInfos, int tradeShopItemCount, CustomTextLayout customTextLayout, string endTime, string startTime)
			:base(id, isIgnore, memo)
		{ 
			this.OpenCommandType = openCommandType;
			this.IconId = iconId;
			this.DecorationId = decorationId;
			this.DecorationSpecialId = decorationSpecialId;
			this.DecorationColor = decorationColor;
			this.TabNameKey = tabNameKey;
			this.SortOrder = sortOrder;
			this.IsHide = isHide;
			this.IsHideNotOpen = isHideNotOpen;
			this.TradeShopType = tradeShopType;
			this.AutoUpdateType = autoUpdateType;
			this.AutoUpdateTimes = autoUpdateTimes;
			this.IsManualUpdate = isManualUpdate;
			this.ManualUpdateCurrencyCount = manualUpdateCurrencyCount;
			this.ConsumeItemInfos = consumeItemInfos;
			this.TradeShopItemCount = tradeShopItemCount;
			this.CustomTextLayout = customTextLayout;
			this.EndTime = endTime;
			this.StartTime = startTime;
		}

		public TradeShopTabMB() :base(0L, false, ""){}

		[Description("終了日時")]
		[PropertyOrder(20)]
		public string EndTime
		{
			get;
			set;
		}

		[Description("開始日時")]
		[PropertyOrder(19)]
		public string StartTime
		{
			get;
			set;
		}

		public bool IsUpdateMode()
		{
			throw new NotImplementedException();
			// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
		}

		public bool HasPeriod()
		{
			if (string.IsNullOrEmpty(this.StartTime))
			{
			}
			return !string.IsNullOrEmpty(this.EndTime);
		}
	}
}
