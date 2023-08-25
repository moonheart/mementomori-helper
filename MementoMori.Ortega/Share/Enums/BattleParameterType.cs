using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
    [Description("バトルパラメータの種類")]
    public enum BattleParameterType
    {
        [Description("HP")] Hp = 1,
        [Description("攻撃力")] AttackPower = 2,
        [Description("物理ダメージ緩和")] PhysicalDamageRelax = 3,
        [Description("魔法ダメージ緩和")] MagicDamageRelax = 4,
        [Description("命中")] Hit = 5,
        [Description("回避")] Avoidance = 6,
        [Description("クリティカル")] Critical = 7,
        [Description("クリティカル耐性")] CriticalResist = 8,
        [Description("クリダメ強化")] CriticalDamageEnhance = 9,
        [Description("物理クリダメ緩和")] PhysicalCriticalDamageRelax = 10,
        [Description("魔法クリダメ緩和")] MagicCriticalDamageRelax = 11,
        [Description("防御貫通力")] DefensePenetration = 12,
        [Description("防御力")] Defense = 13,
        [Description("物魔防御貫通")] DamageEnhance = 14,
        [Description("弱体効果命中")] DebuffHit = 15,
        [Description("弱体効果耐性")] DebuffResist = 16,
        [Description("リフレクト")] DamageReflect = 17,
        [Description("HP吸収")] HpDrain = 18,
        [Description("スピード")] Speed = 19
    }
}