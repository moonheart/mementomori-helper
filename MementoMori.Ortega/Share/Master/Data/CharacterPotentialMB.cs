using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("キャラクター潜在能力")]
	[MessagePackObject(true)]
	public class CharacterPotentialMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("キャラクターレベル")]
		public int CharacterLevel
		{
			get;
		}

		[Description("キャラクターサブレベル")]
		[PropertyOrder(2)]
		public int CharacterSubLevel
		{
			get;
		}

		[Description("潜在能力合計値")]
		[PropertyOrder(3)]
		public long TotalBaseParameter
		{
			get;
		}

		[SerializationConstructor]
		public CharacterPotentialMB(long id, bool? isIgnore, string memo, int characterLevel, int characterSubLevel, long totalBaseParameter)
			:base(id, isIgnore, memo)
		{
			CharacterLevel = characterLevel;
			CharacterSubLevel = characterSubLevel;
			TotalBaseParameter = totalBaseParameter;
		}

		public CharacterPotentialMB() : base(0, false, "")
		{
		}
	}
}
