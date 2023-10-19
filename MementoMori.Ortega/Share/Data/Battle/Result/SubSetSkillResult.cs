using MementoMori.Ortega.Share.Enums.Battle.Skill;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle.Result
{
    [MessagePackObject(true)]
    public class SubSetSkillResult
    {
        public List<SubSkillResult> DamageSubSkillResults { get; set; }

        public List<SubSkillResult> EffectSubSkillResults { get; set; }

        public List<SubSkillResult> PassiveSubSkillResults { get; set; }

        public List<SubSkillResult> TempSubSkillResults { get; set; }

        public List<SubSkillResult> SubSkillResults { get; set; }

        public SubSetType SubSetType { get; set; }

        public SubSetSkillResult()
        {
            List<SubSkillResult> list = new List<SubSkillResult>();
            this.DamageSubSkillResults = list;
            List<SubSkillResult> list2 = new List<SubSkillResult>();
            this.EffectSubSkillResults = list2;
            List<SubSkillResult> list3 = new List<SubSkillResult>();
            this.PassiveSubSkillResults = list3;
            List<SubSkillResult> list4 = new List<SubSkillResult>();
            this.TempSubSkillResults = list4;
        }
    }
}