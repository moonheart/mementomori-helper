using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("キャラクターレアリティ係数")]
	[MessagePackObject(true)]
	public class CharacterPotentialCoefficientMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("初期レアリティ")]
		public CharacterRarityFlags InitialRarityFlags { get; }

		[Description("レアリティ係数")]
		[PropertyOrder(3)]
		[Nest(false, 0)]
		public CharacterRarityCoefficientInfo RarityCoefficientInfo { get; }

		[PropertyOrder(2)]
		[Description("レアリティ")]
		public CharacterRarityFlags RarityFlags { get; }

		[SerializationConstructor]
		public CharacterPotentialCoefficientMB(long id, bool? isIgnore, string memo,
			CharacterRarityFlags initialRarityFlags, CharacterRarityFlags rarityFlags,
			CharacterRarityCoefficientInfo rarityCoefficientInfo)
			: base(id, isIgnore, memo)
		{
			InitialRarityFlags = initialRarityFlags;
			RarityFlags = rarityFlags;
			RarityCoefficientInfo = rarityCoefficientInfo;
		}

		public CharacterPotentialCoefficientMB() : base(0, false, "")
		{
		}
	}
}