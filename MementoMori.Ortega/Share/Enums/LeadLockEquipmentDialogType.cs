using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
    [Description("装備固定誘導ダイアログタイプ")]
    public enum LeadLockEquipmentDialogType
    {
        [Description("ダイアログ表示無し")] None,
        [Description("新キャラ入手")] NewCharacter,
        [Description("最後の更新またはキャンセルから一定期間経過")] PassedDays
    }
}
