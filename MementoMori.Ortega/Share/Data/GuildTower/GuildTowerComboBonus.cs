using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildTower
{
	[MessagePackObject(true)]
	public class GuildTowerComboBonus
	{
		public int ComboCount { get; set; }

		public int ComboBonus { get; set; }
	}
}
