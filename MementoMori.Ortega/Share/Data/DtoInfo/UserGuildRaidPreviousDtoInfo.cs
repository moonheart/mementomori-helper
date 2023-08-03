using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
	[MessagePackObject(true)]
	public class UserGuildRaidPreviousDtoInfo
	{
		public string BattleLogJson { get; set; }

		public long Damage { get; set; }

		public long DropItemCount { get; set; }

		public GuildRaidBossType GuildRaidBossType { get; set; }
	}
}
