using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
    [Description("職業")]
    [Flags]
    public enum JobFlags
    {
        [Description("None")] None = 0,
        [Description("ウォリアー")] Warrior = 1,
        [Description("スナイパー")] Sniper = 2,
        [Description("ソーサラー")] Sorcerer = 4
    }
}