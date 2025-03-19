using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Music
{
	[MessagePackObject(true)]
	public class MusicPlayerPlayEventData
	{
		public int Position { get; set; }

		public MusicPlayerPlayEventType EventType { get; set; }
	}
}
