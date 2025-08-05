using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Gacha;

[MessagePackObject(true)]
public class GachaCaseInfo
{
    public int DisplayOrder { get; set; }

    public ElementType ElementType { get; set; }

    public long DrawStartTime { get; set; }

    public long EndTime { get; set; }

    public int GachaBonusDrawCount { get; set; }

    public List<GachaBonusInfo> GachaBonusInfoList { get; set; }

    public List<GachaButtonInfo> GachaButtonInfoList { get; set; }

    public long GachaCaseId { get; set; }

    public long GachaCaseUiId { get; set; }

    public GachaCategoryType GachaCategoryType { get; set; }

    public GachaGroupType GachaGroupType { get; set; }

    public GachaRelicType GachaRelicType { get; set; }

    public List<long> GachaSelectCharacterIdList { get; set; }

    public GachaSelectListType GachaSelectListType { get; set; }

    public int MaxDrawGold { get; set; }

    public int RemainingDrawGold { get; set; }

    public int GachaDrawCount { get; set; }

    public int GachaCeilingCount { get; set; }

    public GachaCaseFlags GachaCaseFlags { get; set; }
}