using MessagePack;

namespace MementoMori.Ortega.Share.Data
{
	[MessagePackObject(true)]
	public class UnlockedGridCellInfo
	{
		public int GridCellIndex { get; set; }

		public bool IsWin { get; set; }

		public bool IsKey { get; set; }
	}
}
