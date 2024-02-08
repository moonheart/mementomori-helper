using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle.Result
{
    [MessagePackObject(true)]
    public class ActiveSkillData
    {
        public TransientEffectResult TransientEffectResult { get; set; }

        public long ActiveSkillId { get; set; }

        public List<SubSetSkillResult> SubSetSkillResults { get; set; }
        
        public List<SubSkillResult> ActionStartSubSkillResults { get; set; }
        
        public List<SubSkillResult> ActionEndSubSkillResults { get; set; }

        public List<SubSkillResult> TurnEndSubSkillResults { get; set; }

        public bool IsNonActionStance { get; set; }

        public int FromGuid { get; set; }
    }
}