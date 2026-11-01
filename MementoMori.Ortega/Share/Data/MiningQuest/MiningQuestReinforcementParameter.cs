using System.ComponentModel;
using MessagePack;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.MiningQuest
{
	[MessagePackObject(true)]
	public class MiningQuestReinforcementParameter
	{
		[Description("強化パラメータtype")]
		public MiningQuestReinforcementParameterType ParameterType { get; set; }

		[Description("強化パラメータID")]
		public long ParameterId { get; set; }

		[Description("パラメータ値")]
		public int Value { get; set; }

		[Description("表示順")]
		public int SortOrder { get; set; }
	}
}
