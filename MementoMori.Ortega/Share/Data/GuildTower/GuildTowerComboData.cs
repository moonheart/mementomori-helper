using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildTower
{
	[MessagePackObject(true)]
	public class GuildTowerComboData
	{
		public int ComboCount { get; set; }

		public long EndLocalTimeStamp { get; set; }
	}
}
