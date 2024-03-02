using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildTower
{
	[MessagePackObject(true)]
	public class GuildTowerDebuffParameter
	{
		public int DebuffCount { get; set; }

		public GuildTowerCharacterConditionType CharacterConditionType { get; set; }

		public long AttackPower { get; set; }

		public long HP { get; set; }

		public int Critical { get; set; }

		public int Hit { get; set; }

		public int DebuffHit { get; set; }

		public long CriticalDamageEnhance { get; set; }

		public long Speed { get; set; }
	}
}
