using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
    [Description("アイテムのレアリティ")]
    [Flags]
    public enum ItemRarityFlags
    {
        [Description("None")] None = 0,
        [Description("D")] D = 1,
        [Description("C")] C = 2,
        [Description("B")] B = 4,
        [Description("A")] A = 8,
        [Description("S")] S = 16,
        [Description("R")] R = 32,
        [Description("SR")] SR = 64,
        [Description("SSR")] SSR = 128,
        [Description("UR")] UR = 256,
        [Description("LR")] LR = 512
    }
}