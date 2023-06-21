using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("キャラクターリセット")]
	[MessagePackObject(true)]
	public class CharacterResetMB : MasterBookBase
	{
		[Description("リセット対象のレアリティ")]
		[PropertyOrder(2)]
		public CharacterRarityFlags CharacterRarityFlags
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("リセット対象の属性の分類")]
		public ElementClassificationType ElementClassificationType
		{
			get;
		}

		[Description("同名のSR+のLv1キャラ返還数")]
		[PropertyOrder(3)]
		public int ReturnCharacterCount
		{
			get;
		}

		[Description("魔女の手紙返還数")]
		[PropertyOrder(4)]
		public long ReturnWitchLetterCount
		{
			get;
		}

		[SerializationConstructor]
		public CharacterResetMB(long id, bool? isIgnore, string memo, ElementClassificationType elementClassificationType, CharacterRarityFlags characterRarityFlags, int returnCharacterCount, long returnWitchLetterCount)
			:base(id, isIgnore, memo)
		{
			ElementClassificationType = elementClassificationType;
			CharacterRarityFlags = characterRarityFlags;
			ReturnCharacterCount = returnCharacterCount;
			ReturnWitchLetterCount = returnWitchLetterCount;
		}

		public CharacterResetMB() : base(0, false, "")
		{
		}
	}
}
