using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Player;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
	[MessagePackObject(true)]
	public class LegendLeagueRankingPlayerInfo
	{
		public int ConsecutiveVictoryCount { get; set; }

		public long CurrentPoint { get; set; }

		public long CurrentRank { get; set; }

		public long DefenseBattlePower { get; set; }

		public Dictionary<string, BaseParameter> DefenseCharacterBaseParameterMap{ get; set; }

		public Dictionary<string, BattleParameter> DefenseCharacterBattleParameterMap{ get; set; }

		public Dictionary<string, List<UserEquipmentDtoInfo>> DefenseEquipmentDtoInfoListMap{ get; set; }

		public PlayerInfo PlayerInfo{ get; set; }

		public List<UserCharacterDtoInfo> UserCharacterDtoInfoList{ get; set; }

		public List<long> GetBattleCharacterIds()
        {
            throw new NotImplementedException();
        }
	}
}
