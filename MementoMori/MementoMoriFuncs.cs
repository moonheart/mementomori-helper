using System.Reactive.Subjects;
using System.Reflection;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Data.ApiInterface;
using MementoMori.Ortega.Share.Data.ApiInterface.Auth;
using MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using MementoMori.Ortega.Share.Data.ApiInterface.Friend;
using MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus;
using MementoMori.Ortega.Share.Data.ApiInterface.Present;
using MementoMori.Ortega.Share.Data.ApiInterface.User;
using MementoMori.Ortega.Share.Data.ApiInterface.Vip;
using MementoMori.Ortega.Share.Enums;
using MessagePack;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MementoMori;

public class MementoMoriFuncs
{
    private const string URL_AUTH_LOGIN = "https://prd1-auth.mememori-boi.com/api/auth/login";
    private const string URL_AUTH_GETSERVERHOST = "https://prd1-auth.mememori-boi.com/api/auth/getServerHost";
    private const string URL_AUTH_GETDATAURI = "https://prd1-auth.mememori-boi.com/api/auth/getDataUri";
    private const string URL_USER_LOGINPLAYER = "user/loginPlayer";
    private const string URL_USER_GETUSERDATA = "user/getUserData";
    private const string URL_LOGINBONUS_GETMONTHLYLOGINBONUSINFO = "loginBonus/getMonthlyLoginBonusInfo";
    private const string URL_LOGINBONUS_RECEIVEDAILYLOGINBONUS = "loginBonus/receiveDailyLoginBonus";
    private const string URL_VIP_GETDAILYGIFT = "vip/getDailyGift";
    private const string URL_FRIEND_BULKTRANSFERFRIENDPOINT = "friend/bulkTransferFriendPoint";
    private const string URL_BATTLE_REWARDAUTOBATTLE = "battle/rewardAutoBattle";
    private const string URL_BATTLE_BOSSQUICK = "battle/bossQuick";
    private const string URL_BATTLE_QUICK = "battle/quick";
    private const string URL_PRESENT_RECEIVEITEM = "present/receiveItem";

    private Uri _apiHost;
    private Uri _apiAuth = new Uri("https://prd1-auth.mememori-boi.com/api/");

    private readonly BehaviorSubject<RuntimeInfo> _configSubject = new(new RuntimeInfo());
    public IObservable<RuntimeInfo> ConfigSubject => _configSubject;

    private readonly BehaviorSubject<UserSyncData> _userSyncDataSubject = new(new UserSyncData());
    public IObservable<UserSyncData> UserSyncData => _userSyncDataSubject;

    private readonly RuntimeInfo _runtimeInfo = new();
    private readonly MeMoriHttpClientHandler _meMoriHttpClientHandler;
    private readonly HttpClient _httpClient;


    private readonly AuthOption _authOption;
    public MementoMoriFuncs(IOptions<AuthOption> authOption)
    {
        _authOption = authOption.Value;
        _meMoriHttpClientHandler = new MeMoriHttpClientHandler(_authOption.Headers);
        _meMoriHttpClientHandler.OrtegaAccessToken.Subscribe(token =>
        {
            _runtimeInfo.OrtegaAccessToken = token;
            _configSubject.OnNext(_runtimeInfo);
        });

        _httpClient = new HttpClient(_meMoriHttpClientHandler);
    }

    public async Task AuthLogin()
    {
        var reqBody = new LoginRequest()
        {
            ClientKey = _authOption.ClientKey,
            DeviceToken = _authOption.DeviceToken,
            AppVersion = _authOption.AppVersion,
            OSVersion = _authOption.OSVersion,
            ModelName = _authOption.ModelName,
            AdverisementId = _authOption.AdverisementId,
            UserId = _authOption.UserId
        };
        var authLoginResp = await GetResponse<LoginRequest, LoginResponse>(reqBody);
        var playerDataInfo = authLoginResp.PlayerDataInfoList.FirstOrDefault();
        if (playerDataInfo == null) throw new Exception("playerDataInfo is null");

        // get server host
        await AuthGetServerHost(playerDataInfo.WorldId);
        
        // do login
        var loginPlayerResp = await UserLoginPlayer(playerDataInfo.PlayerId, playerDataInfo.Password);

        var userSyncData = (await UserGetUserData()).UserSyncData;
        _userSyncDataSubject.OnNext(userSyncData);
    }

    private async Task AuthGetServerHost(long worldId)
    {
        var req = new GetServerHostRequest() {WorldId = worldId};
        var resp = await GetResponse<GetServerHostRequest, GetServerHostResponse>(req);
        _apiHost = new Uri(resp.ApiHost);
        _runtimeInfo.ApiHost = resp.ApiHost;
        _configSubject.OnNext(_runtimeInfo);
    }
    public async Task<GetDataUriResponse> AuthGetDataUri(string countryCode, long userId)
    {
        var req = new GetDataUriRequest() {CountryCode = countryCode, UserId = userId};
        return await GetResponse<GetDataUriRequest, GetDataUriResponse>(req);
    }

    private async Task<LoginPlayerResponse> UserLoginPlayer(long playerId, string password)
    {
        var req = new LoginPlayerRequest{PlayerId = playerId, Password = password};
        return await GetResponse<LoginPlayerRequest, LoginPlayerResponse>(req);
    }

    public async Task<GetUserDataResponse> UserGetUserData()
    {
        var req = new GetUserDataRequest { };
        return await GetResponse<GetUserDataRequest, GetUserDataResponse>(req);
    }

    public async Task<GetMonthlyLoginBonusInfoResponse> LoginBonusGetMonthlyLoginBonusInfo()
    {
        var req = new GetMonthlyLoginBonusInfoRequest();
        return await GetResponse<GetMonthlyLoginBonusInfoRequest, GetMonthlyLoginBonusInfoResponse>(req);
    }

    /// <summary>
    /// 获取每日登陆奖励
    /// </summary>
    /// <param name="receiveDay"></param>
    /// <returns></returns>
    public async Task<ReceiveDailyLoginBonusResponse> LoginBonusReceiveDailyLoginBonus(int receiveDay)
    {
        var req = new ReceiveDailyLoginBonusRequest() {ReceiveDay = receiveDay};
        return await GetResponse<ReceiveDailyLoginBonusRequest, ReceiveDailyLoginBonusResponse>(req);
    }
    
    /// <summary>
    /// 获取VIP每日奖励
    /// </summary>
    /// <returns></returns>
    public async Task<GetDailyGiftResponse> VipGetDailyGift()
    {
        var req = new GetDailyGiftRequest();
        return await GetResponse<GetDailyGiftRequest, GetDailyGiftResponse>(req);
    }
    
    /// <summary>
    /// 一键发送、接收友情点
    /// </summary>
    /// <returns></returns>
    public async Task<BulkTransferFriendPointResponse> FriendBulkTransferFriendPoint()
    {
        var req = new BulkTransferFriendPointRequest();
        return await GetResponse<BulkTransferFriendPointRequest, BulkTransferFriendPointResponse>(req);
    }
    
    /// <summary>
    /// 获取自动战斗的奖励
    /// </summary>
    /// <returns></returns>
    public async Task<RewardAutoBattleResponse> BattleRewardAutoBattle()
    {
        var req = new RewardAutoBattleRequest();
        return await GetResponse<RewardAutoBattleRequest, RewardAutoBattleResponse>(req);
    }
    
    /// <summary>
    /// 战斗扫荡
    /// </summary>
    /// <returns></returns>
    public async Task<BossQuickResponse> BattleBossQuick(int questId)
    {
        var req = new BossQuickRequest(){QuestId = questId};
        return await GetResponse<BossQuickRequest, BossQuickResponse>(req);
    }

    /// <summary>
    /// 高速战斗
    /// </summary>
    /// <returns></returns>
    public async Task<QuickResponse> BattleQuick(QuestQuickExecuteType questQuickExecuteType, int quickCount)
    {
        var req = new QuickRequest(){QuestQuickExecuteType = questQuickExecuteType, QuickCount = quickCount};
        return await GetResponse<QuickRequest, QuickResponse>(req);
    }

    public async Task<ReceiveItemResponse> PresentReceiveItem()
    {
        var req = new ReceiveItemRequest();
        return await GetResponse<ReceiveItemRequest, ReceiveItemResponse>(req);
    } 
    
    public async Task<TResp> GetResponse<TReq, TResp>(TReq req) 
        where TReq: ApiRequestBase 
        where TResp: ApiResponseBase
    {
        var authAttr = typeof(TReq).GetCustomAttribute<OrtegaAuthAttribute>();
        var apiAttr = typeof(TReq).GetCustomAttribute<OrtegaApiAttribute>();
        Uri uri;
        if (authAttr != null)
        {
            uri = new Uri(_apiAuth, authAttr.Uri);
        }
        else if (apiAttr != null)
        {
            uri = new Uri(_apiHost, apiAttr.Uri);
        }
        else
        {
            throw new NotSupportedException();
        }

        // var reqMap = JsonConvert.DeserializeObject<Dictionary<object, object>>(JsonConvert.SerializeObject(req));
        var bytes = MessagePackSerializer.Serialize(req);
        var respMsg = await _httpClient.PostAsync(uri,
            new ByteArrayContent(bytes) {Headers = {{"content-type", "application/json"}}});
        var respBytes = await respMsg.Content.ReadAsByteArrayAsync();
        return MessagePackSerializer.Deserialize<TResp>(respBytes);
        // return JsonConvert.DeserializeObject<TResp>(JsonConvert.SerializeObject(tmp));
    }
}