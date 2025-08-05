using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
	[MessagePackObject(true)]
	public class DamageBar
	{
		public int DamageBarCount { get; set; }

		public long DamageBarMaxValue { get; set; }
	}
}
