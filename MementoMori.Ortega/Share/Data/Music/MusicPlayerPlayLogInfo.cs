using MessagePack;

namespace MementoMori.Ortega.Share.Data.Music
{
	[MessagePackObject(true)]
	public class MusicPlayerPlayLogInfo
	{
		public long MusicMBId { get; set; }

		public int TotalPlaySeconds { get; set; }

		public string PlayListGuid{ get; set; }

		public bool IsShuffle { get; set; }

		public bool IsMySelect { get; set; }

		public bool IsComplete { get; set; }

		public List<MusicPlayerPlayEventData> EventDataList{ get; set; }
	}
}
