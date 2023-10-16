using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega;

public interface IBattleEnemy
{
    IReadOnlyList<long> ActiveSkillIds { get; }

    BaseParameter BaseParameter { get; }

    long BattleEnemyCharacterId { get; }

    BattleParameter BattleParameter { get; }

    long BattlePower { get; }

    CharacterRarityFlags CharacterRarityFlags { get; }

    ElementType ElementType { get; }

    long EnemyAdjustId { get; }

    long EnemyEquipmentId { get; }

    EquipmentRarityFlags ExclusiveEquipmentRarityFlags { get; }

    long EnemyRank { get; }

    JobFlags JobFlags { get; }

    string NameKey { get; }

    long NormalSkillId { get; }

    IReadOnlyList<long> PassiveSkillIds { get; }

    long UnitIconId { get; }

    UnitIconType UnitIconType { get; }
}
