using System.ComponentModel;

public enum MiningQuestSpecialAttackType
{
    [Description("None")]
    None,
    [Description("波紋（ランダム位置から出現）")]
    Random,
    [Description("波紋（操作地点から出現）")]
    Specified
}
