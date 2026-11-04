using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission
{
	[MessagePackObject(true)]
	[OrtegaApi("unionMission/entryUnionMission", true, false)]
	public class EntryUnionMissionRequest : ApiRequestBase
	{
	}
}
