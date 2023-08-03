using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Guild
{
	[MessagePackObject(true)]
	public class GuildSystemChatOptionInfo
	{
		public SystemChatMessageIdType Type { get; set; }

		public bool IsValid { get; set; }
	}
}
