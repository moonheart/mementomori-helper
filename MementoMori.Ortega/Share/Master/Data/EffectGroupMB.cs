using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("スキル効果グループ")]
	[MessagePackObject(true)]
	public class EffectGroupMB : MasterBookBase
	{
		[Description("付与者アイコンID")]
		[PropertyOrder(6)]
		public long CasterIconId
		{
			get;
		}

		[Description("付与者アイコンタイプ")]
		[PropertyOrder(5)]
		public EffectGroupIconType CasterIconType
		{
			get;
		}

		[Description("説明文キー")]
		[PropertyOrder(2)]
		public string DescriptionKey
		{
			get;
		}

		[PropertyOrder(4)]
		[Description("効果アイコンID")]
		public long IconId
		{
			get;
		}

		[Description("効果アイコンタイプ")]
		[PropertyOrder(3)]
		public EffectGroupIconType IconType
		{
			get;
		}

		[Description("効果名キー")]
		[PropertyOrder(1)]
		public string NameKey
		{
			get;
		}

		[Description("強制非表示フラグ")]
		[PropertyOrder(7)]
		public bool IsHide
		{
			get;
		}

		[PropertyOrder(8)]
		[Description("ターン数非表示フラグ")]
		public bool IsTurnHide
		{
			get;
		}

		[SerializationConstructor]
		public EffectGroupMB(long id, bool? isIgnore, string memo, string nameKey, string descriptionKey, EffectGroupIconType iconType, long iconId, EffectGroupIconType casterIconType, long casterIconId, bool isHide, bool isTurnHide)
			:base(id, isIgnore, memo)
		{
			NameKey = nameKey;
			DescriptionKey = descriptionKey;
			IconType = iconType;
			IconId = iconId;
			CasterIconType = casterIconType;
			CasterIconId = casterIconId;
			IsHide = isHide;
			IsTurnHide = isTurnHide;
		}

		public EffectGroupMB() : base(0, false, "")
		{
		}
	}
}
