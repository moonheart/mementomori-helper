using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
	[MessagePackObject(true)]
	public class UserGuildRaidDtoInfo
	{
		public string BattleLogAtMaxDamageJson { get; set; }

		public string BossGuid { get; set; }

		public int ChallengeCount { get; set; }

		public string DropItemJson { get; set; }

		public long MaxDamage { get; set; }

		public long TotalDamage { get; set; }
	}
}
