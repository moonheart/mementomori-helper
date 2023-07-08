using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Enums.Battle.Skill;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
    [MessagePackObject(true)]
    public class PassiveSubSetSkillInfo : IDeepCopy<PassiveSubSetSkillInfo>
    {
        [Description("パッシブトリガー")]
        public PassiveTrigger PassiveTrigger { get; set; }

        [Description("スキルクールタイム(MB : 初期スキルクールタイム)")]
        public long SkillCoolTime { get; set; }

        [Description("スキルMAXクールタイム")]
        public long SkillMaxCoolTime { get; set; }

        [Description("同じパッシブスキルのクールタイムグループ")]
        public long PassiveGroupId { get; set; }

        [Description("サブセットスキルId")]
        public long SubSetSkillId { get; set; }

        public PassiveSubSetSkillInfo DeepCopy()
        {
            var passiveSubSetSkillInfo = new PassiveSubSetSkillInfo();
            var passiveTrigger = PassiveTrigger;
            passiveSubSetSkillInfo.PassiveTrigger = passiveTrigger;
            var num = SkillCoolTime;
            passiveSubSetSkillInfo.SkillCoolTime = num;
            var num2 = SkillMaxCoolTime;
            passiveSubSetSkillInfo.SkillMaxCoolTime = num2;
            passiveSubSetSkillInfo.PassiveGroupId = PassiveGroupId;
            var num3 = SubSetSkillId;
            passiveSubSetSkillInfo.SubSetSkillId = num3;
            return passiveSubSetSkillInfo;
        }

        public PassiveSubSetSkillInfo()
        {
        }
    }
}