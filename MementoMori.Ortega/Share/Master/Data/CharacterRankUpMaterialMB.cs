using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("キャラクターランクアップ")]
	[MessagePackObject(true)]
	public class CharacterRankUpMaterialMB : MasterBookBase
	{
		[Description("属性の分類")]
		[PropertyOrder(1)]
		public ElementClassificationType ElementClassification
		{
			get;
			set;
		}

		[PropertyOrder(4)]
		[Description("Material")]
		public CharacterRarityFlags MaterialRankFlags
		{
			get;
			set;
		}

		[Description("Material2")]
		[PropertyOrder(5)]
		public CharacterRarityFlags MaterialRankSecondFlags
		{
			get;
			set;
		}

		[Description("RankFlags")]
		[PropertyOrder(3)]
		public CharacterRarityFlags RankFlags
		{
			get;
			set;
		}

		[Description("ランクアップを完了すると適用されるランク")]
		[PropertyOrder(6)]
		public CharacterRarityFlags RankUpResultRarityFlags
		{
			get;
			set;
		}

		[Description("ランクアップ時の方法")]
		[PropertyOrder(2)]
		public RankUpType UpType
		{
			get;
			set;
		}

		[SerializationConstructor]
		public CharacterRankUpMaterialMB(long id, bool? isIgnore, string memo, ElementClassificationType elementClassification, RankUpType upType, CharacterRarityFlags rankFlags, CharacterRarityFlags materialRankFlags, CharacterRarityFlags materialRankSecondFlags, CharacterRarityFlags rankUpResultRarityFlags)
			:base(id, isIgnore, memo)
		{
			ElementClassification = elementClassification;
			UpType = upType;
			RankFlags = rankFlags;
			MaterialRankFlags = materialRankFlags;
			MaterialRankSecondFlags = materialRankSecondFlags;
			RankUpResultRarityFlags = rankUpResultRarityFlags;
		}

		public CharacterRankUpMaterialMB() : base(0, false, "")
		{
		}

		public CharacterRarityFlags GetMaterialRarityFlag(int index)
		{
			// if (index == 0)
			// {
			// 	return this.MaterialRankFlags;
			// }
			// if (index == 1)
			// {
			// 	return this.MaterialRankSecondFlags;
			// }
			throw new NotImplementedException();
		}

		public int GetMaterialCount()
		{
			// int num = 0;
			// bool flag = this.MaterialRankFlags != (CharacterRarityFlags)num;
			// bool flag2 = flag + true;
			// if (this.MaterialRankSecondFlags == CharacterRarityFlags.None)
			// {
			// 	return;
			// }
			throw new NotImplementedException();
		}
	}
}
