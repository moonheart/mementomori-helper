using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("誘導タイプ")]
	public enum WorldGuidanceType
	{
		[Description("なし")]
		None,
		[Description("汎用新ワールド")]
		GenericNewWorld,
		[Description("VIPワールド")]
		VIPWorld
	}
}
