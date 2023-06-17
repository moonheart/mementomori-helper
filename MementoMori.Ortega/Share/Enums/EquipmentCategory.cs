using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
    [Description("武具カテゴリ")]
    public enum EquipmentCategory
    {
        [Description("通常武具")] Normal = 1,
        [Description("セット武具")] Set,
        [Description("専用武器")] Exclusive
    }
}