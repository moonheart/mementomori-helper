using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
    [MessagePackObject(true)]
    public class BattleActiveSkill
    {
        public long ActiveSkillId { get; set; }

        public int SkillOrderNumber { get; set; }

        public int SkillMaxCoolTime { get; set; }

        public int SkillCoolTime { get; set; }

        public List<long> SubSetSkillIds { get; set; }

        public BattleActiveSkill()
        {
        }
    }
}