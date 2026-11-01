using MementoMori.Ortega.Share.Extensions;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Data.MiningQuest;

[MessagePackObject(true)]
public class MiningQuestBlockInfo : ILotteryWeight
{
    [Description("出現ブロックのID")]
    public long BlockId { get; set; }

    [Description("ブロック名称")]
    public string NameTextKey { get; set; }

    [Description("耐久値")]
    public long Health { get; set; }

    [Description("ブロックサイズ")]
    public int Size { get; set; }

    [Description("スコア")]
    public int Score { get; set; }

    [Description("抽選weight")]
    public int LotteryWeight { get; set; }

    [Description("参照画像ID")]
    public long ImageId { get; set; }
}
