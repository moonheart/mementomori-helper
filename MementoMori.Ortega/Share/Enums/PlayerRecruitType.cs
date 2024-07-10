using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum PlayerRecruitType
	{
		[Description("希望しない")]
		None,
		[Description("希望する")]
		Desire,
		[Description("興味がある")]
		Interested
	}
}
