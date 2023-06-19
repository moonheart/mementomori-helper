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
		public PassiveTrigger PassiveTrigger
		{
			get;
			set;
		}

		[Description("スキルクールタイム(MB : 初期スキルクールタイム)")]
		public long SkillCoolTime
		{
			get;
			set;
		}

		[Description("スキルMAXクールタイム")]
		public long SkillMaxCoolTime
		{
			get;
			set;
		}

		[Description("サブセットスキルId")]
		public long SubSetSkillId
		{
			get;
			set;
		}

		public PassiveSubSetSkillInfo DeepCopy()
		{
			PassiveSubSetSkillInfo passiveSubSetSkillInfo = new PassiveSubSetSkillInfo();
			PassiveTrigger passiveTrigger = this.PassiveTrigger;
			passiveSubSetSkillInfo.PassiveTrigger = passiveTrigger;
			long num = this.SkillCoolTime;
			passiveSubSetSkillInfo.SkillCoolTime = num;
			long num2 = this.SkillMaxCoolTime;
			passiveSubSetSkillInfo.SkillMaxCoolTime = num2;
			long num3 = this.SubSetSkillId;
			passiveSubSetSkillInfo.SubSetSkillId = num3;
			return passiveSubSetSkillInfo;
		}

		public PassiveSubSetSkillInfo()
		{
		}
	}
}
