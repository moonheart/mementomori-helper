using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle;

[MessagePackObject(true)]
[OrtegaApi("badge/getCompetitionInfo", true, false)]
public class GetCompetitionInfoRequest : ApiRequestBase
{
}