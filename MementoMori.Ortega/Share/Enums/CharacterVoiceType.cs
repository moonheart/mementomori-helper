using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("キャラクターの声の種類")]
	public enum CharacterVoiceType
	{
		[Description("攻撃")]
		Attack,
		[Description("ダメージ")]
		Damage,
		[Description("トドメ")]
		Todome,
		[Description("力尽きる")]
		Death
	}
}
