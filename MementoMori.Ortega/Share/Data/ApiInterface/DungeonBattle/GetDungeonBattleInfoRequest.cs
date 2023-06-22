using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[MessagePackObject(true)]
	[OrtegaApi("dungeonBattle/getDungeonBattleInfo", true, false)]
	public class GetDungeonBattleInfoRequest : ApiRequestBase
	{
		public GetDungeonBattleInfoRequest()
		{
		}
	}
}
