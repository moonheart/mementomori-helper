using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("専属スキル説明文")]
	[MessagePackObject(true)]
	public class EquipmentExclusiveSkillDescriptionMB : MasterBookBase
	{
		[Description("説明文1キー")]
		[PropertyOrder(1)]
		public string Description1Key
		{
			get;
		}

		[Description("説明文2キー")]
		[PropertyOrder(2)]
		public string Description2Key
		{
			get;
		}

		[Description("説明文3キー")]
		[PropertyOrder(3)]
		public string Description3Key
		{
			get;
		}

		[SerializationConstructor]
		public EquipmentExclusiveSkillDescriptionMB(long id, bool? isIgnore, string memo, string description1Key, string description2Key, string description3Key)
			:base(id, isIgnore, memo)
		{
			Description1Key = description1Key;
			Description2Key = description2Key;
			Description3Key = description3Key;
		}

		public EquipmentExclusiveSkillDescriptionMB() : base(0, false, "")
		{
		}
	}
}
