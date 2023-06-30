using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[MessagePackObject(true)]
	public class ExecReinforceRelicResponse : ApiResponseBase
	{
		public long AddedRelicId { get; set; }

		public List<long> ReinforcedAfterRelicIds{ get; set; }

		public UserDungeonBattleDtoInfo UserDungeonBattleDtoInfo{ get; set; }

		public ExecReinforceRelicResponse()
		{
			this.ReinforcedAfterRelicIds = new List<long>();
		}
	}
}
