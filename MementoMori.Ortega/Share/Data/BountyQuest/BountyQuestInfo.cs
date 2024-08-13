using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.BountyQuest;

[MessagePackObject(true)]
public class BountyQuestInfo
{
    [Description("懸賞カウンタークエストID")]
    public long BountyQuestId { get; set; }

    [Description("懸賞カウンタークエスト名キー")]
    public string BountyQuestNameKey { get; set; }

    [Description("懸賞カウンタータイプ")]
    public BountyQuestType BountyQuestType { get; set; }

    [Description("懸賞カウンタークエストレアリティ")]
    public BountyQuestRarityFlags BountyQuestRarity { get; set; }

    [Description("設定可能なキャラー数")]
    public int CharacterMaxCount { get; set; }

    [Description("懸賞カウンター制限時間(ミリ秒)")]
    public long BountyQuestLimitTime { get; set; }

    [Description("懸賞カウンタークリアタイム(ミリ秒)")]
    public long BountyQuestClearTime { get; set; }

    [Description("懸賞カウンター条件リスト")]
    public List<BountyQuestConditionInfo> BountyQuestConditionInfos { get; set; }

    [Description("懸賞カウンター報酬リスト")]
    public List<UserItem> RewardItems { get; set; }

    [Description("高速完了初期必要ダイヤ")]
    public long InitialRequireCurrencyForQuick { get; set; }
}