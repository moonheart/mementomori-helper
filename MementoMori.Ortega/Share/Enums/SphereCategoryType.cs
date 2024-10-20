using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums;

[Description("ルーンの種別")]
public enum SphereCategoryType
{
    [Description("不明")] None,
    [Description("腕力のルーン")] Muscle,
    [Description("技力のルーン")] Energy,
    [Description("魔力のルーン")] Intelligence,
    [Description("攻撃力のルーン")] AttackPower,
    [Description("物魔防御貫通のルーン")] DamageEnhance,
    [Description("命中のルーン")] Hit,
    [Description("クリティカルのルーン")] Critical,
    [Description("弱体効果命中のルーン")] DebuffHit,
    [Description("スピードのルーン")] Speed,
    [Description("耐久力のルーン")] Health,
    [Description("HPのルーン")] Hp,
    [Description("物理防御力のルーン")] PhysicalDamageRelax,
    [Description("魔法防御力のルーン")] MagicDamageRelax,
    [Description("回避のルーン")] Avoidance,
    [Description("クリティカル耐性のルーン")] CriticalResist,
    [Description("弱体効果耐性のルーン")] DebuffResist
}