using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Chat;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Response
{
	[MessagePackObject(false)]
	public class OnChangeChatOptionResponse
	{
		[Key(0)]
		public List<ChangeChatOptionInfo> ChangeChatOptionInfoList { get; set; }

		[Key(1)]
		public long AnnounceChatEndIntervalTimestamp { get; set; }
	}
}
