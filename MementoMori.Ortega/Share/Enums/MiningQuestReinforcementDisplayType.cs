using System.ComponentModel;

public enum MiningQuestReinforcementDisplayType
{
    [Description("None")]
    None,
    [Description("通常強化の短冊1枠目（回収力）")]
    AttackPower,
    [Description("通常強化の短冊2枠目（回収速度）")]
    AttackFrequency,
    [Description("特殊強化")]
    Special
}
