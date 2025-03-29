using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.GuildTower;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildTower
{
	[MessagePackObject(true)]
	public class GetGuildTowerInfoResponse : ApiResponseBase, IGuildSyncApiResponse, IUserSyncApiResponse
	{
		public int ClearGaugeProgress { get; set; }

		public Dictionary<int, long> ClearPartyBattlePowerCoefficientMap { get; set; }

		public GuildTowerComboData ComboData { get; set; }

		public long CurrentFloorMBId { get; set; }

		public Dictionary<int, long> EnemyRankCoefficientMap { get; set; }

		public List<GuildTowerEntryCharacter> GuildTowerEntryCharacterList { get; set; }

		public bool IsAlreadyEntryToday { get; set; }

		public bool IsChangeDayFromFirstWin { get; set; }

		public bool IsContinueEntry { get; set; }

		public int LastSelectedDifficulty { get; set; }

		public int LastWinEntryPassedDays { get; set; }

		public List<GuildTowerReinforcementJobData> ReinforcementJobDataList { get; set; }

		public Dictionary<int, long> ReinforcementMaterialDropCoefficientMap { get; set; }

		public int TodayWinCount { get; set; }

        public long LastReinforceLocalTimeStamp { get; set; }

        public GuildSyncData GuildSyncData { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
