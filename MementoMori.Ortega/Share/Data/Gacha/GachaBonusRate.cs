using MessagePack;

namespace MementoMori.Ortega.Share.Data.Gacha;

[MessagePackObject(true)]
public class GachaBonusRate
{
    public int GachaCount { get; set; }

    public List<GachaItemRate> GachaItemRateList { get; set; }

    public int Index { get; set; }
}