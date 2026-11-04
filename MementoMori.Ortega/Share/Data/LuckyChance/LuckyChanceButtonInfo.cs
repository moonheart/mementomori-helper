using MessagePack;

namespace MementoMori.Ortega.Share.Data.LuckyChance;

[MessagePackObject(true)]
public class LuckyChanceButtonInfo
{
    public long LuckyChanceButtonId { get; set; }

    public long ImageId { get; set; }

    public string NameTextKey { get; set; }

    public bool IsDisplayDetailButton { get; set; }
}
