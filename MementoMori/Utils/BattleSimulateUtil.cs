using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Enums.Battle.Skill;

namespace MementoMori.Utils;

public static class BattleSimulateUtil
{
    public static string GetName(this EffectGroup effectGroup)
    {
        var effectGroupMb = EffectGroupTable.GetById(effectGroup.EffectGroupId);
        return TextResourceTable.Get(effectGroupMb.NameKey);
    }

    public static List<string> GetEffectNames(this EffectGroup effectGroup)
    {
        return effectGroup.Effects.Select(d => d.GetEffectDesc()).ToList();
    }

    private static string GetEffectDesc(this Effect effect)
    {
        var desc = effect.EffectType.GetDesc();
        if (effect.EffectType < EffectType.SpeedUp) return "None";
        if (effect.EffectType < EffectType.HitRateUp) return $"{desc} {effect.EffectValue}";

        if (effect.EffectType < EffectType.DamageGuard) return $"{desc} {effect.EffectValue}%";

        if (effect.EffectType < EffectType.ActiveSkill1Enhance) return desc;

        if (effect.EffectType < EffectType.SpeedDown) return desc;

        if (effect.EffectType < EffectType.HitRateDown) return $"{desc} {effect.EffectValue}";
        if (effect.EffectType < EffectType.Stun) return $"{desc} {effect.EffectValue}%";
        if (effect.EffectType < EffectType.HpRecoveryForbidden) return desc;

        return desc;

        //
        //
        // return effect.EffectType switch
        // {
        //     EffectType.AttackPowerUp => $"攻击力提升: {effect.EffectValue}",
        //     EffectType.DefenseDown => $"防御力降低: {effect.EffectValue}",
        //     EffectType.CriticalResistRateDown => $"抗暴率降低: {effect.EffectValue}%",
        //     EffectType.ReceiveDamageUp => $"承受伤害增加: {effect.EffectValue}%",
        //     EffectType.GiveDamageDown => $"造成伤害降低: {effect.EffectValue}%",
        //     EffectType.AvoidanceForbidden => "无法回避",
        //     EffectType.Shield1 => $"护盾: {effect.EffectValue}",
        //     EffectType.SpeedUp => $"速度提升: {effect.EffectValue}",
        //     EffectType.CriticalRateUp=> $"暴击率提升: {effect.EffectValue}%",
        //     EffectType.DefensePenetrationUp=> $"防御穿透提升: {effect.EffectValue}",
        //     EffectType.GiveDamageUp=> $"输出伤害提升: {effect.EffectValue}%",
        //     EffectType.DefenseUp=> $"防御提升: {effect.EffectValue}%",
        //     EffectType.DebuffGuard=> $"弱化效果免疫",
        //     EffectType.AvoidanceRateUp=> $"闪避率提升: {effect.EffectValue}%",
        //     EffectType.CriticalResistRateUp=> $"抗爆率提升: {effect.EffectValue}%",
        //     _ => $"{effect.EffectType}: {effect.EffectValue}"
        // };
    }


    public static UnitData GetUnitData(this BattleFieldCharacter battleFieldCharacter)
    {
        switch (battleFieldCharacter.UnitType)
        {
            case UnitType.Character:
            {
                var characterMb = CharacterTable.GetById(battleFieldCharacter.UnitId);
                var characterName = TextResourceTable.Get(characterMb.NameKey);
                return new UnitData {Name = characterName, Level = battleFieldCharacter.CharacterLevel, IsAttacker = battleFieldCharacter.DefaultPosition.IsAttacker};
                break;
            }
            // case UnitType.AutoBattleEnemy:
            //     break;
            // case UnitType.DungeonBattleEnemy:
            //     break;
            // case UnitType.GuildRaidBoss:
            //     break;
            case UnitType.BossBattleEnemy:
            {
                var bossBattleEnemyMb = BossBattleEnemyTable.GetById(battleFieldCharacter.UnitId);
                var bossBattleEnemyName = TextResourceTable.Get(bossBattleEnemyMb.NameKey);
                return new UnitData {Name = bossBattleEnemyName, Level = bossBattleEnemyMb.EnemyRank, IsAttacker = battleFieldCharacter.DefaultPosition.IsAttacker};
                break;
            }
            // case UnitType.TowerBattleEnemy:
            //     break;
            // case UnitType.LocalRaidEnemy:
            //     break;
            default:
                throw new ArgumentOutOfRangeException("battleFieldCharacter.UnitType");
        }
    }

    public class UnitData
    {
        public string Name { get; set; }
        public long Level { get; set; }
        public bool IsAttacker { get; set; }
    }
}