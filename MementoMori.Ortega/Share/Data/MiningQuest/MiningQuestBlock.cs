using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Data.MiningQuest;

[MessagePackObject(true)]
public class MiningQuestBlock
{
    [Description("出現ブロックのId")]
    public long BlockId { get; set; }

    [Description("ブロック名称")]
    public string NameTextKey { get; set; }

    [Description("基礎耐久値")]
    public long BaseHealth { get; set; }

    [Description("ブロックサイズ")]
    public int Size { get; set; }

    [Description("報酬アイテム獲得量")]
    public long RewardItemCount { get; set; }

    [Description("スコア")]
    public int BaseScore { get; set; }

    [Description("基礎抽選weight")]
    public int BaseLotteryWeight { get; set; }

    [Description("参照画像ID")]
    public long ImageId { get; set; }
}
