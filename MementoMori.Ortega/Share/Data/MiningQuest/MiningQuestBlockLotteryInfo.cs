using MementoMori.Ortega.Share.Extensions;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Data.MiningQuest;

[MessagePackObject(true)]
public class MiningQuestBlockLotteryInfo : ILotteryWeight
{
    [Description("出現ブロックのID")]
    public long BlockId { get; set; }

    [Description("ブロック名称")]
    public string NameTextKey { get; set; }

    [Description("抽選weight")]
    public int LotteryWeight { get; set; }
}
