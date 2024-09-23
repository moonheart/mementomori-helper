using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("プレイヤーランク")]
    public class PlayerRankMB : MasterBookBase
    {
        [PropertyOrder(1)]
        [Description("ランク")]
        public long Rank { get; }

        [PropertyOrder(2)]
        [Description("必要累計経験値")]
        public long RequiredTotalExp { get; }

        [DateTimeString]
        [PropertyOrder(3)]
        [Description("解放時間")]
        public string StartTimeFixJST { get; }

        [PropertyOrder(4)]
        [Description("キャラパラメータ攻撃力ボーナス")]
        public long AttackPowerBonus { get; }

        [PropertyOrder(5)]
        [Description("キャラパラメータ攻撃力%ボーナス")]
        public long AttackPowerPercentBonus { get; }

        [PropertyOrder(6)]
        [Description("キャラパラメータHPボーナス")]
        public long HpBonus { get; }

        [PropertyOrder(7)]
        [Description("キャラパラメータHP%ボーナス")]
        public long HpPercentBonus { get; }

        [PropertyOrder(8)]
        [Description("キャラパラメータ防御貫通ボーナス")]
        public long DefensePenetrationBonus { get; }

        [PropertyOrder(9)]
        [Description("キャラパラメータ物魔防御貫通ボーナス")]
        public long DamageEnhanceBonus { get; }

        [PropertyOrder(10)]
        [Description("キャラパラメータクリティカルボーナス")]
        public int CriticalBonus { get; }

        [PropertyOrder(11)]
        [Description("キャラパラメータ命中ボーナス")]
        public int HitBonus { get; }

        [PropertyOrder(12)]
        [Description("キャラパラメータ回避ボーナス")]
        public int AvoidanceBonus { get; }

        [PropertyOrder(13)]
        [Description("キャラパラメータ弱体効果命中ボーナス")]
        public int DebuffHitBonus { get; }

        [PropertyOrder(14)]
        [Description("キャラパラメータスピードボーナス")]
        public int SpeedBonus { get; }

        [PropertyOrder(15)]
        [Description("キャラパラメータクリダメ強化ボーナス")]
        public long CriticalDamageEnhanceBonus { get; }

        [PropertyOrder(16)]
        [Description("キャラパラメータカウンタボーナス")]
        public int DamageReflectBonus { get; }

        [PropertyOrder(17)]
        [Description("キャラパラメータHPドレインボーナス")]
        public int HpDrainBonus { get; }

        [PropertyOrder(18)]
        [Description("命中率ボーナス(パラメータX)")]
        public int HitDirectPercentBonus { get; }

        [PropertyOrder(19)]
        [Description("レベルリンク枠最大数")]
        public int LevelLinkMemberMaxCount { get; }

        [SerializationConstructor]
        public PlayerRankMB(long id, bool? isIgnore, string memo, long rank, long requiredTotalExp, string startTimeFixJST, long attackPowerBonus, long attackPowerPercentBonus, long hpBonus, long hpPercentBonus, long defensePenetrationBonus, long damageEnhanceBonus, int criticalBonus, int hitBonus, int avoidanceBonus, int debuffHitBonus, int speedBonus, long criticalDamageEnhanceBonus, int hpDrainBonus, int damageReflectBonus, int hitDirectPercentBonus, int levelLinkMemberMaxCount)
            : base(id, isIgnore, memo)
        {
            Rank = rank;
            RequiredTotalExp = requiredTotalExp;
            StartTimeFixJST = startTimeFixJST;
            AttackPowerBonus = attackPowerBonus;
            AttackPowerPercentBonus = attackPowerPercentBonus;
            HpBonus = hpBonus;
            HpPercentBonus = hpPercentBonus;
            DefensePenetrationBonus = defensePenetrationBonus;
            DamageEnhanceBonus = damageEnhanceBonus;
            CriticalBonus = criticalBonus;
            HitBonus = hitBonus;
            AvoidanceBonus = avoidanceBonus;
            DebuffHitBonus = debuffHitBonus;
            SpeedBonus = speedBonus;
            CriticalDamageEnhanceBonus = criticalDamageEnhanceBonus;
            HpDrainBonus = hpDrainBonus;
            DamageReflectBonus = damageReflectBonus;
            HitDirectPercentBonus = hitDirectPercentBonus;
            LevelLinkMemberMaxCount = levelLinkMemberMaxCount;
        }

        public PlayerRankMB() : base(0L, false, "")
        {
        }
        
        public bool IsInTime(DateTime jstDateTime)
        {
            if (!string.IsNullOrEmpty(this.StartTimeFixJST))
            {
                bool flag = this.StartTimeFixJST.ToDateTime() <= jstDateTime;
                if (!flag)
                {
                    return flag;
                }
            }
            return true;
        }

    }
}