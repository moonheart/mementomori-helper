using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
    [Description("武具の種類")]
    public enum EquipmentSlotType
    {
        [Description("武器")] Weapon = 1,
        [Description("装飾品")] Sub,
        [Description("アーム")] Gauntlet,
        [Description("メット")] Helmet,
        [Description("メイル")] Armor,
        [Description("ブーツ")] Shoes
    }
}