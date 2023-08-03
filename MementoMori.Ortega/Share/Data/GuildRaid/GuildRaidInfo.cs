using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildRaid
{
	[MessagePackObject(true)]
	public class GuildRaidInfo
	{
		public List<UserItem> DropDiamondLotteryItemList{ get; set; }

		public GuildRaidBossInfo GuildRaidBossInfo{ get; set; }

		public GuildRaidDtoInfo GuildRaidDtoInfo{ get; set; }

		public List<GuildRaidUserRankingInfo> GuildRaidUserRankingInfos{ get; set; }

		public bool IsOpen { get; set; }

		public bool IsExistWorldDamageReward { get; set; }

		public List<UserItem> ObtainableEquipmentList{ get; set; }

		public UserGuildRaidDtoInfo UserGuildRaidDtoInfo{ get; set; }

		public UserGuildRaidPreviousDtoInfo UserGuildRaidPreviousDtoInfo{ get; set; }
	}
}
