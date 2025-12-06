using MementoMori.Ortega.Share.Data.Music;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.User;

[OrtegaApi("user/loginPlayer", false, true)]
[MessagePackObject(true)]
public class LoginPlayerRequest : ApiRequestBase, IHasSteamTicketApiRequest
{
    public string Password { get; set; }

    public long PlayerId { get; set; }

    public List<ErrorLogInfo> ErrorLogInfoList { get; set; }

    public List<MusicPlayerPlayLogInfo> MusicPlayerPlayLogInfoList { get; set; }

    public List<MusicPlayerPlayLogInfo> CustomMusicSettingPlayLogInfoList { get; set; }

    public string SteamTicket { get; set; }
}