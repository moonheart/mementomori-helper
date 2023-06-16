using System.Reactive.Subjects;
using MementoMori.Battle;
using MementoMori.Common;
using MementoMori.Friend;
using MementoMori.LoginBonus;
using MementoMori.Present;
using MementoMori.User;
using MementoMori.Vip;
using MessagePack;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MementoMori;

public class MementoMoriFuncs
{
    private const string URL_AUTH_LOGIN = "https://prd1-auth.mememori-boi.com/api/auth/login";
    private const string URL_AUTH_GETSERVERHOST = "https://prd1-auth.mememori-boi.com/api/auth/getServerHost";
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
        var reqBody = new AuthLoginReq
        {
            ClientKey = _authOption.ClientKey,
            DeviceToken = _authOption.DeviceToken,
            AppVersion = _authOption.AppVersion,
            OSVersion = _authOption.OSVersion,
            ModelName = _authOption.ModelName,
            AdverisementId = _authOption.AdverisementId,
            UserId = _authOption.UserId
        };
        var authLoginResp = await GetResponse<AuthLoginReq, AuthLoginResp>(URL_AUTH_LOGIN, reqBody);
        var playerDataInfo = authLoginResp.PlayerDataInfoList.FirstOrDefault();
        if (playerDataInfo == null) throw new Exception("playerDataInfo is null");

        // get server host
        await AuthGetServerHost(playerDataInfo.WorldId);
        
        // do login
        var loginPlayerResp = await UserLoginPlayer(playerDataInfo.PlayerId, playerDataInfo.Password);

        var userSyncData = (await UserGetUserData()).UserSyncData;
        _userSyncDataSubject.OnNext(userSyncData);
    }

    private async Task AuthGetServerHost(int worldId)
    {
        var req = new GetServerHost.Req {WorldId = worldId};
        var resp = await GetResponse<GetServerHost.Req, GetServerHost.Resp>(URL_AUTH_GETSERVERHOST, req);
        _apiHost = new Uri(resp.ApiHost);
        _runtimeInfo.ApiHost = resp.ApiHost;
        _configSubject.OnNext(_runtimeInfo);
    }

    private async Task<LoginPlayer.Resp> UserLoginPlayer(long playerId, string password)
    {
        var loginUri = new Uri(_apiHost, URL_USER_LOGINPLAYER);
        var req = new LoginPlayer.Req {PlayerId = playerId, Password = password};
        return await GetResponse<LoginPlayer.Req, LoginPlayer.Resp>(loginUri, req);
    }

    public async Task<GetUserData.Resp> UserGetUserData()
    {
        var loginUri = new Uri(_apiHost, URL_USER_GETUSERDATA);
        var req = new GetUserData.Req { };
        return await GetResponse<GetUserData.Req, GetUserData.Resp>(loginUri, req);
    }

    public async Task<GetMonthlyLoginBonusInfo.Resp> LoginBonusGetMonthlyLoginBonusInfo()
    {
        var loginUri = new Uri(_apiHost, URL_LOGINBONUS_GETMONTHLYLOGINBONUSINFO);
        var req = new GetMonthlyLoginBonusInfo.Req();
        return await GetResponse<GetMonthlyLoginBonusInfo.Req, GetMonthlyLoginBonusInfo.Resp>(loginUri, req);
    }

    /// <summary>
    /// 获取每日登陆奖励
    /// </summary>
    /// <param name="receiveDay"></param>
    /// <returns></returns>
    public async Task<ReceiveDailyLoginBonus.Resp> LoginBonusReceiveDailyLoginBonus(int receiveDay)
    {
        var loginUri = new Uri(_apiHost, URL_LOGINBONUS_RECEIVEDAILYLOGINBONUS);
        var req = new ReceiveDailyLoginBonus.Req() {ReceiveDay = receiveDay};
        return await GetResponse<ReceiveDailyLoginBonus.Req, ReceiveDailyLoginBonus.Resp>(loginUri, req);
    }
    
    /// <summary>
    /// 获取VIP每日奖励
    /// </summary>
    /// <returns></returns>
    public async Task<GetDailyGift.Resp> VipGetDailyGift()
    {
        var loginUri = new Uri(_apiHost, URL_VIP_GETDAILYGIFT);
        var req = new GetDailyGift.Req();
        return await GetResponse<GetDailyGift.Req, GetDailyGift.Resp>(loginUri, req);
    }
    
    /// <summary>
    /// 一键发送、接收友情点
    /// </summary>
    /// <returns></returns>
    public async Task<BulkTransferFriendPoint.Resp> FriendBulkTransferFriendPoint()
    {
        var loginUri = new Uri(_apiHost, URL_FRIEND_BULKTRANSFERFRIENDPOINT);
        var req = new BulkTransferFriendPoint.Req();
        return await GetResponse<BulkTransferFriendPoint.Req, BulkTransferFriendPoint.Resp>(loginUri, req);
    }
    
    /// <summary>
    /// 获取自动战斗的奖励
    /// </summary>
    /// <returns></returns>
    public async Task<RewardAutoBattle.Resp> BattleRewardAutoBattle()
    {
        var loginUri = new Uri(_apiHost, URL_BATTLE_REWARDAUTOBATTLE);
        var req = new RewardAutoBattle.Req();
        return await GetResponse<RewardAutoBattle.Req, RewardAutoBattle.Resp>(loginUri, req);
    }
    
    /// <summary>
    /// 战斗扫荡
    /// </summary>
    /// <returns></returns>
    public async Task<BossQuick.Resp> BattleBossQuick(int questId)
    {
        var loginUri = new Uri(_apiHost, URL_BATTLE_BOSSQUICK);
        var req = new BossQuick.Req(){QuestId = questId};
        return await GetResponse<BossQuick.Req, BossQuick.Resp>(loginUri, req);
    }

    /// <summary>
    /// 高速战斗
    /// </summary>
    /// <returns></returns>
    public async Task<Quick.Resp> BattleQuick(int questQuickExecuteType, int quickCount)
    {
        var reqUri = new Uri(_apiHost, URL_BATTLE_QUICK);
        var req = new Quick.Req(){QuestQuickExecuteType = questQuickExecuteType, QuickCount = quickCount};
        return await GetResponse<Quick.Req, Quick.Resp>(reqUri, req);
    }

    public async Task<ReceiveItem.Resp> PresentReceiveItem()
    {
        var uri = new Uri(_apiHost, URL_PRESENT_RECEIVEITEM);
        var req = new ReceiveItem.Req();
        return await GetResponse<ReceiveItem.Req, ReceiveItem.Resp>(uri, req);
    } 

    private async Task<TResp> GetResponse<TReq, TResp>(string url, TReq req)
    {
        return await GetResponse<TReq, TResp>(new Uri(url), req);
    }

    private async Task<TResp> GetResponse<TReq, TResp>(Uri url, TReq req)
    {
        var reqMap = JsonConvert.DeserializeObject<Dictionary<object, object>>(JsonConvert.SerializeObject(req));
        var bytes = MessagePackSerializer.Serialize(reqMap);
        var respMsg = await _httpClient.PostAsync(url,
            new ByteArrayContent(bytes) {Headers = {{"content-type", "application/json"}}});
        var respBytes = await respMsg.Content.ReadAsByteArrayAsync();
        var tmp = MessagePackSerializer.Deserialize<object>(respBytes);
        return JsonConvert.DeserializeObject<TResp>(JsonConvert.SerializeObject(tmp));
    }
}