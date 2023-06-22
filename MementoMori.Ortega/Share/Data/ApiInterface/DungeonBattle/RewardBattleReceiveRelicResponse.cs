using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[MessagePackObject(true)]
	public class RewardBattleReceiveRelicResponse : ApiResponseBase
	{
		public long AddedRelicId
		{
			get;
			set;
		}

		public UserDungeonBattleDtoInfo UserDungeonBattleDtoInfo
		{
			get;
			set;
		}

		public RewardBattleReceiveRelicResponse()
		{
		}
	}
}
