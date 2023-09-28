using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("パネル図鑑")]
	public class PanelMB : MasterBookBase
	{
		[DateTimeString]
		[PropertyOrder(1)]
		[Description("開始日時")]
		public string StartTime { get; }

		[PropertyOrder(2)]
		[Description("タブタイプ")]
		public PanelTabType TabType { get; }

		[PropertyOrder(3)]
		[Description("パネル静止画ID")]
		public long PanelImageId { get; }

		[PropertyOrder(4)]
		[Description("パネル動画ID")]
		public long PanelMovieId { get; }

		[PropertyOrder(5)]
		[Description("並び順")]
		public long SortOrder { get; }

		[PropertyOrder(6)]
		[Description("イベントグループID")]
		public long EventGroupId { get; }

		[PropertyOrder(7)]
		[Description("グループタイトル")]
		public string GroupTitleKey { get; }

		[PropertyOrder(8)]
		[Description("シナリオタイトル")]
		public string ScenarioTitleKey { get; }

		[PropertyOrder(9)]
		[Description("シナリオテキスト")]
		public string ScenarioTextKey { get; }

		[PropertyOrder(10)]
		[Description("解放判定アイテムID")]
		public long PanelGetJudgmentItemId { get; }

		[PropertyOrder(11)]
		[Description("無条件解放フラグ")]
		public bool IsFree { get; }

		[PropertyOrder(12)]
		[Description("解放時消費解放専用アイテム個数")]
		public long ConsumeUnlockItemCount { get; }

		[PropertyOrder(13)]
		[Description("解放時消費ダイヤ個数")]
		public long ConsumeCurrencyCount { get; }

		[SerializationConstructor]
		public PanelMB(long id, bool? isIgnore, string memo, string startTime, PanelTabType tabType, long panelImageId, long panelMovieId, long sortOrder, long eventGroupId, string groupTitleKey, string scenarioTitleKey, string scenarioTextKey, long panelGetJudgmentItemId, bool isFree, long consumeUnlockItemCount, long consumeCurrencyCount)
			: base(id, isIgnore, memo)
		{
			StartTime = startTime;
            TabType = tabType;
            PanelImageId = panelImageId;
            PanelMovieId = panelMovieId;
            SortOrder = sortOrder;
            EventGroupId = eventGroupId;
            GroupTitleKey = groupTitleKey;
            ScenarioTitleKey = scenarioTitleKey;
            ScenarioTextKey = scenarioTextKey;
            PanelGetJudgmentItemId = panelGetJudgmentItemId;
            IsFree = isFree;
            ConsumeUnlockItemCount = consumeUnlockItemCount;
            ConsumeCurrencyCount = consumeCurrencyCount;
		}

		public PanelMB():base(0L, null, null)
		{
			
		}
	}
}
