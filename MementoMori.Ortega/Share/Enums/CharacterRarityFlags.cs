using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
    [Flags]
    [Description("キャラクターのレアリティ")]
    public enum CharacterRarityFlags
    {
        [Description("None")] None = 0,
        [Description("N")] N = 1,
        [Description("R")] R = 2,
        [Description("R+")] RPlus = 4,
        [Description("SR")] SR = 8,
        [Description("SR+")] SRPlus = 16,
        [Description("SSR")] SSR = 32,
        [Description("SSR+")] SSRPlus = 64,
        [Description("UR")] UR = 128,
        [Description("UR+")] URPlus = 256,
        [Description("LR")] LR = 512,
        [Description("LR+")] LRPlus = 1024,
        [Description("LR+2")] LRPlus2 = 2048,
        [Description("LR+3")] LRPlus3 = 4096,
        [Description("LR+4")] LRPlus4 = 8192,
        [Description("LR+5")] LRPlus5 = 16384,
        [Description("LR+6")] LRPlus6 = 32768,
        [Description("LR+7")] LRPlus7 = 65536,
        [Description("LR+8")] LRPlus8 = 131072,
        [Description("LR+9")] LRPlus9 = 262144,
        [Description("LR+10")] LRPlus10 = 524288
    }
}