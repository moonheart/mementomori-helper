using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Gacha;

[MessagePackObject(true)]
public class GachaElementInfo
{
    public long EndTimeOpenBlue { get; set; }

    public long EndTimeOpenGreen { get; set; }

    public long EndTimeOpenRed { get; set; }

    public long EndTimeOpenYellow { get; set; }

    public ElementType ServerOpenElementType { get; set; }
}