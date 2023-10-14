using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.LocalRaid;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Response
{
	[MessagePackObject(false)]
	public class OnGetRoomListResponse
	{
		[Key(0)]
		public List<LocalRaidPartyInfo> LocalRaidPartyInfoList { get; set; }
	}
}
