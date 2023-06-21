using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("キャラクターの血液型")]
	public enum CharacterBloodType
	{
		[Description("不明")]
		None,
		[Description("A型")]
		A,
		[Description("B型")]
		B,
		[Description("O型")]
		O,
		[Description("AB型")]
		AB
	}
}
