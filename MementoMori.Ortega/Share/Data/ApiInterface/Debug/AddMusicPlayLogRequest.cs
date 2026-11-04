using MementoMori.Ortega.Share.Data.Music;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Debug;

[MessagePackObject(true)]
[OrtegaApi("debug/addMusicPlayLog", true, false)]
public class AddMusicPlayLogRequest : ApiRequestBase
{
    public List<MusicPlayerPlayLogInfo> MusicPlayerPlayLogInfoList { get; set; }
}
