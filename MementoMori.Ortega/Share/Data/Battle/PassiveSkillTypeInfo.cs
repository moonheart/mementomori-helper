using System.ComponentModel;
using MementoMori.Ortega.Share.Enums.Battle.Skill;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
	[MessagePackObject(true)]
	public class PassiveSkillTypeInfo
	{
		[Description("パッシブタイプ")]
		public PassiveType PassiveType
		{
			get;
			set;
		}

		[Description("パッシブスキルID")]
		public long PassiveSkillId
		{
			get;
			set;
		}

		public PassiveSkillTypeInfo()
		{
		}
	}
}
