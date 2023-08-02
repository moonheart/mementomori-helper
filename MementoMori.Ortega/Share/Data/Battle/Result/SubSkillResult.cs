using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Enums.Battle.Skill;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle.Result
{
    [MessagePackObject(true)]
    public class SubSkillResult
    {
        public SubSkillResultType SubSkillResultType { get; set; }
        public int SubSkillIndex { get; set; }

        public SkillDisplayType SkillDisplayType { get; set; }

        public int AttackUnitGuid { get; set; }

        public int TargetUnitGuid { get; set; }

        public List<EffectGroup> AddEffectGroups { get; set; }

        public List<EffectGroup> RemoveEffectGroups { get; set; }

        public HitType HitType { get; set; }

        public long ChangeHp { get; set; }

        public long TargetRemainHp { get; set; }

        public SubSkillResult()
        {
            this.AddEffectGroups = new List<EffectGroup>();
            this.RemoveEffectGroups = new List<EffectGroup>();
        }
    }
}