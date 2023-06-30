using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Debug
{
	[OrtegaApi("debug/giveAllSrCharacter", true, false)]
	[MessagePackObject(true)]
	public class GiveAllSrCharacterRequest : ApiRequestBase
	{
	}
}
