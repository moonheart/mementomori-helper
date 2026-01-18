using MessagePack;

namespace MementoMori.Ortega.Share.Data
{
	[MessagePackObject(true)]
	public class WinningLotteryCoefficient
	{
		public int CellThreshold { get; set; }

		public int Coefficient { get; set; }
	}
}
