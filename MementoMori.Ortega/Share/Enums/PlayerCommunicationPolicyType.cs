using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum PlayerCommunicationPolicyType
	{
		[Description("指定なし")]
		None,
		[Description("無言")]
		Silence,
		[Description("たまに雑談")]
		SmallTalk,
		[Description("おしゃべり")]
		Conversationalist
	}
}
