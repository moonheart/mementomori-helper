using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
	[MessagePackObject(true)]
	public class UserDungeonBattleMissedCountDtoInfo
	{
		public long LatestChallengeTermId
		{
			get;
			set;
		}

		public long MissedCount
		{
			get;
			set;
		}

		public long PlayerId
		{
			get;
			set;
		}

		public UserDungeonBattleMissedCountDtoInfo()
		{
		}
	}
}
