using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Gacha
{
	[MessagePackObject(true)]
	public class GachaStarsGuidanceLogInfo
	{
		public string Name { get; set; }

		public UserItem UserItem{ get; set; }
	}
}
