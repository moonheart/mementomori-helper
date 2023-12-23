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
        [Description("キャラパラメータ攻撃力ボーナス")]
        [PropertyOrder(4)]
        public long AttackPowerBonus { get; }

        [Description("キャラパラメータクリティカルボーナス")]
        [PropertyOrder(9)]
        public int CriticalBonus { get; }

        [PropertyOrder(13)]
        [Description("キャラパラメータクリダメ強化ボーナス")]
        public long CriticalDamageEnhanceBonus { get; }

        [PropertyOrder(8)]
        [Description("キャラパラメータ物魔防御貫通ボーナス")]
        public long DamageEnhanceBonus { get; }

        [PropertyOrder(14)]
        [Description("キャラパラメータカウンタボーナス")]
        public int DamageReflectBonus { get; }

        [Description("キャラパラメータ弱体効果命中ボーナス")]
        [PropertyOrder(11)]
        public int DebuffHitBonus { get; }

        [PropertyOrder(7)]
        [Description("キャラパラメータ防御貫通ボーナス")]
        public long DefensePenetrationBonus { get; }

        [PropertyOrder(10)]
        [Description("キャラパラメータ命中ボーナス")]
        public int HitBonus { get; }

        [PropertyOrder(5)]
        [Description("キャラパラメータHPボーナス")]
        public long HpBonus { get; }

        [PropertyOrder(6)]
        [Description("キャラパラメータHP%ボーナス")]
        public long HpPercentBonus { get; }

        [Description("キャラパラメータHPドレインボーナス")]
        [PropertyOrder(15)]
        public int HpDrainBonus { get; }

        [PropertyOrder(16)]
        [Description("命中率ボーナス(パラメータX)")]
        public int HitDirectPercentBonus { get; }

        [PropertyOrder(17)]
        [Description("レベルリンク枠最大数")]
        public int LevelLinkMemberMaxCount { get; }

        [PropertyOrder(1)]
        [Description("ランク")]
        public long Rank { get; }

        [Description("必要累計経験値")]
        [PropertyOrder(2)]
        public long RequiredTotalExp { get; }

        [Description("解放時間")]
        [PropertyOrder(3)]
        public string StartTimeFixJST { get; }

        [Description("キャラパラメータスピードボーナス")]
        [PropertyOrder(12)]
        public int SpeedBonus { get; }

        [SerializationConstructor]
        public PlayerRankMB(long id, bool? isIgnore, string memo, long rank, long requiredTotalExp, string startTimeFixJST, long attackPowerBonus, long hpBonus, long hpPercentBonus,
            long defensePenetrationBonus, long damageEnhanceBonus, int criticalBonus, int hitBonus, int debuffHitBonus, int speedBonus, long criticalDamageEnhanceBonus, int hpDrainBonus,
            int damageReflectBonus, int hitDirectPercentBonus, int levelLinkMemberMaxCount)
            : base(id, isIgnore, memo)
        {
            Rank = rank;
            RequiredTotalExp = requiredTotalExp;
            StartTimeFixJST = startTimeFixJST;
            AttackPowerBonus = attackPowerBonus;
            HpBonus = hpBonus;
            HpPercentBonus = hpPercentBonus;
            DefensePenetrationBonus = defensePenetrationBonus;
            DamageEnhanceBonus = damageEnhanceBonus;
            CriticalBonus = criticalBonus;
            HitBonus = hitBonus;
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