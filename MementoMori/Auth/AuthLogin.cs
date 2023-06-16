using MementoMori.Common;
using MementoMori.User;
using MessagePack;

namespace MementoMori;

public class AuthLoginReq
{
    public string ClientKey { get; set; }
    public string DeviceToken { get; set; }
    public string AppVersion { get; set; }
    public string OSVersion { get; set; }
    public string ModelName { get; set; }
    public string AdverisementId { get; set; }
    public long UserId { get; set; }
}

public class AuthLoginResp
{
    public object AccountMessageInfo { get; set; }
    public bool IsReservedAccountDeletion { get; set; }
    public PlayerDataInfoList[] PlayerDataInfoList { get; set; }
    public int RecommendWorldId { get; set; }
    public int TextLanguageType { get; set; }
    public int VoiceLanguageType { get; set; }
    public object[] WarningMessageInfos { get; set; }
    public int[] WorldIdList { get; set; }
    public UserSyncData UserSyncData { get; set; }
}


public class PlayerDataInfoList
{
    public int CharacterId { get; set; }
    public long LastLoginTime { get; set; }
    public int LegendLeagueClass { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public long PlayerId { get; set; }
    public int PlayerRank { get; set; }
    public int WorldId { get; set; }
}
