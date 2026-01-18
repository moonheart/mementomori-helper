using MessagePack;

namespace MementoMori.Ortega.Share.Data.IconEffect
{
	[MessagePackObject(true)]
	public class IconEffectInfo
	{
		public long IconId { get; set; }

		public long IconEffectId { get; set; }
	}
}
