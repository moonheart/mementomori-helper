using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
	[MessagePackObject(true)]
	public class BattlePassiveSkill
	{
		public long PassiveSkillId
		{
			get;
			set;
		}

		public List<PassiveSubSetSkillInfo> PassiveSubSetSkillInfos
		{
			get;
			set;
		}

		public BattlePassiveSkill()
		{
		}
	}
}
