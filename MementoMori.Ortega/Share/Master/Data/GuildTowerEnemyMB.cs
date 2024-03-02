using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("ギルドツリー敵データ")]
    public class GuildTowerEnemyMB : MasterBookBase, IBattleEnemy
    {
        [SerializationConstructor]
        public GuildTowerEnemyMB(long id, bool? isIgnore, string memo, string nameKey, long unitIconId, UnitIconType unitIconType, long battlePower, long enemyRank, JobFlags jobFlags,
            ElementType elementType, CharacterRarityFlags characterRarityFlags, BaseParameter baseParameter, BattleParameter battleParameter, long normalSkillId, IReadOnlyList<long> activeSkillIds,
            IReadOnlyList<long> passiveSkillIds, long enemyEquipmentId, long battleEnemyCharacterId, long enemyAdjustId, EquipmentRarityFlags exclusiveEquipmentRarityFlags)
            : base(id, isIgnore, memo)
        {
            NameKey = nameKey;
            UnitIconId = unitIconId;
            UnitIconType = unitIconType;
            BattlePower = battlePower;
            EnemyRank = enemyRank;
            JobFlags = jobFlags;
            ElementType = elementType;
            CharacterRarityFlags = characterRarityFlags;
            BaseParameter = baseParameter;
            BattleParameter = battleParameter;
            NormalSkillId = normalSkillId;
            ActiveSkillIds = activeSkillIds;
            PassiveSkillIds = passiveSkillIds;
            EnemyEquipmentId = enemyEquipmentId;
            BattleEnemyCharacterId = battleEnemyCharacterId;
            EnemyAdjustId = enemyAdjustId;
            ExclusiveEquipmentRarityFlags = exclusiveEquipmentRarityFlags;
        }

        public GuildTowerEnemyMB() : base(default, default, default)
        {
        }

        [PropertyOrder(12)]
        [Description("アクティブスキルIDのリスト")]
        public IReadOnlyList<long> ActiveSkillIds { get; }

        [Nest(false, 0)]
        [PropertyOrder(9)]
        [Description("ベースパラメータ")]
        public BaseParameter BaseParameter { get; }

        [PropertyOrder(14)]
        [Description("敵キャラクターID")]
        public long BattleEnemyCharacterId { get; }

        [Nest(false, 0)]
        [PropertyOrder(10)]
        [Description("バトルパラメータ")]
        public BattleParameter BattleParameter { get; }

        [PropertyOrder(5)]
        [Description("戦闘力")]
        public long BattlePower { get; }

        [PropertyOrder(8)]
        [Description("レアリティ")]
        public CharacterRarityFlags CharacterRarityFlags { get; }

        [PropertyOrder(7)]
        [Description("属性")]
        public ElementType ElementType { get; }

        [PropertyOrder(17)]
        [Description("敵調整ID")]
        public long EnemyAdjustId { get; }

        [PropertyOrder(15)]
        [Description("敵武具ID")]
        public long EnemyEquipmentId { get; }

        [PropertyOrder(4)]
        [Description("敵のランク")]
        public long EnemyRank { get; }

        [PropertyOrder(16)]
        [Description("専用武器レアリティ")]
        public EquipmentRarityFlags ExclusiveEquipmentRarityFlags { get; }

        [PropertyOrder(6)]
        [Description("職業")]
        public JobFlags JobFlags { get; }

        [PropertyOrder(3)]
        [Description("名称キー")]
        public string NameKey { get; }

        [PropertyOrder(11)]
        [Description("通常攻撃として使ってくるスキルID")]
        public long NormalSkillId { get; }

        [PropertyOrder(13)]
        [Description("パッシブスキルIDのリスト")]
        public IReadOnlyList<long> PassiveSkillIds { get; }

        [PropertyOrder(2)]
        [Description("ユニットアイコンID")]
        public long UnitIconId { get; }

        [PropertyOrder(1)]
        [Description("ユニットアイコンタイプ")]
        public UnitIconType UnitIconType { get; }
    }
}