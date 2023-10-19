using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Master.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Enums.Battle.Skill;

namespace MementoMori.Utils;

public static class BattleSimulateUtil
{
    public static string GetName(this EffectGroup effectGroup)
    {
        var effectGroupMb = Masters.EffectGroupTable.GetById(effectGroup.EffectGroupId);
        return Masters.TextResourceTable.Get(effectGroupMb.NameKey);
    }

    public static List<string> GetEffectNames(this EffectGroup effectGroup)
    {
        return effectGroup.Effects.Select(d => d.GetEffectDesc()).ToList();
    }

    private static string GetEffectDesc(this Effect effect)
    {
        return effect.EffectType switch
        {
            EffectType.AttackPowerUp => $"{"攻击力提升"}: {effect.EffectValue}",
            EffectType.DefenseDown => $"{"防御力降低"}: {effect.EffectValue}",
            EffectType.CriticalResistRateDown => $"{"抗暴率降低"}: {effect.EffectValue}%",
            EffectType.ReceiveDamageUp => $"{"承受伤害增加"}: {effect.EffectValue}%",
            EffectType.GiveDamageDown => $"{"造成伤害降低"}: {effect.EffectValue}%",
            EffectType.AvoidanceForbidden => $"{"无法回避"}",
            EffectType.Shield1 => $"{"护盾"}: {effect.EffectValue}",
            _ => $"{effect.EffectType}: {effect.EffectValue}"
        };
    }

    public class UnitData
    {
        public string Name { get; set; }
        public long Level { get; set; }
        public bool IsAttacker { get; set; }
    }


    public static UnitData GetUnitData(this BattleFieldCharacter battleFieldCharacter)
    {
        switch (battleFieldCharacter.UnitType)
        {
            case UnitType.Character:
            {
                var characterMb = Masters.CharacterTable.GetById(battleFieldCharacter.UnitId);
                var characterName = Masters.TextResourceTable.Get(characterMb.NameKey);
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
                var bossBattleEnemyMb = Masters.BossBattleEnemyTable.GetById(battleFieldCharacter.UnitId);
                var bossBattleEnemyName = Masters.TextResourceTable.Get(bossBattleEnemyMb.NameKey);
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
}