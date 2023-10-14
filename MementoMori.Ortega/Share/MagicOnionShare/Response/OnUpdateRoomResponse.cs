using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.LocalRaid;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Response
{
	[MessagePackObject(false)]
	public class OnUpdateRoomResponse
	{
		[Key(0)]
		public LocalRaidPartyInfo LocalRaidPartyInfo { get; set; }
	}
}
