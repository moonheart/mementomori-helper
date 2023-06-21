using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("パッシブスキル")]
	[MessagePackObject(true)]
	public class PassiveSkillMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("名称キー")]
		public string NameKey
		{
			get;
		}

		[Nest(true, 1)]
		[Description("サブセットスキルリスト")]
		[PropertyOrder(2)]
		public IReadOnlyList<PassiveSkillInfo> PassiveSkillInfos
		{
			get;
		}

		[SerializationConstructor]
		public PassiveSkillMB(long id, bool? isIgnore, string memo, string nameKey, IReadOnlyList<PassiveSkillInfo> passiveSkillInfos)
			:base(id, isIgnore, memo)
		{
			NameKey = nameKey;
			PassiveSkillInfos = passiveSkillInfos;
		}

		public PassiveSkillMB() :base(0L, false, ""){}
	}
}
