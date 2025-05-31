using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Chat
{
	[MessagePackObject(false)]
	public class ChatBattleInfo
	{
		[Key(0)]
		public ChatBattlePropertyInfo ChatBattlePropertyInfo { get; set; }

		[Key(1)]
		public List<ChatBattlePartyInfo> ChatBattlePartyInfoList { get; set; }
	}
}
