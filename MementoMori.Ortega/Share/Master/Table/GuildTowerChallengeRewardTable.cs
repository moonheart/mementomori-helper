using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class GuildTowerChallengeRewardTable : TableBase<GuildTowerChallengeRewardMB>
	{
		public GuildTowerChallengeRewardMB GetByFloorAndDifficulty(int floor, int difficulty)
		{
			return _datas.FirstOrDefault(d => d.Floor == floor && d.Difficulty == difficulty);
		}
	}
}
