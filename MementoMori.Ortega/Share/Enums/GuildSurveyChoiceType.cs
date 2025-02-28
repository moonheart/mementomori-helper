using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ギルドアンケートの選択肢種別")]
	public enum GuildSurveyChoiceType
	{
		[Description("不明")]
		None,
		[Description("選択肢1")]
		First,
		[Description("選択肢2")]
		Second,
		[Description("選択肢3")]
		Third,
		[Description("選択肢4")]
		Fourth
	}
}
