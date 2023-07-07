using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	public class NextQuestResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public int AutoBattleDropEquipmentPercent { get; set; }

		public List<long> CurrentQuestAutoEnemyIds { get; set; }

		public long RemainNextRankUpTime { get; set; }

		public UserBattleAutoDtoInfo UserBattleAuto { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
