using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildRaid
{
	[MessagePackObject(true)]
	public class GuildRaidQuestClearEquipmentLvList
	{
		public long EquipmentLv { get; set; }

		public long QuestClearValue { get; set; }
	}
}
