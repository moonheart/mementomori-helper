using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.LocalRaid
{
	[MessagePackObject(true)]
	public class LocalRaidPartyInfo
	{
		public LocalRaidRoomConditionsType ConditionsType { get; set; }

		public long LeaderPlayerId { get; set; }

		public string LeaderPlayerName { get; set; }

		public List<LocalRaidBattleLogPlayerInfo> LocalRaidBattleLogPlayerInfoList { get; set; }

		public int Password { get; set; }

		public long WorldId { get; set; }

		public long QuestId { get; set; }

		public long RequiredBattlePower { get; set; }

		public string RoomId { get; set; }

		public long TotalBattlePower { get; set; }

        public bool IsAutoStart { get; set; }

		public bool IsReady
		{
			get
			{
                if (LocalRaidBattleLogPlayerInfoList == null)
                {
                    return false;
                }
                foreach (var playerInfo in LocalRaidBattleLogPlayerInfoList)
                {
                    if (!playerInfo.IsLeader && !playerInfo.IsReady)
                    {
                        return false;
                    }
                }
				return true;
			}
		}
	}
}
