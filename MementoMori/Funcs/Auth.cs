using MementoMori.Ortega.Share.Data.ApiInterface.Auth;
using MementoMori.Ortega.Share.Data.Auth;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    private readonly IHttpClientFactory _httpClientFactory;

    public async Task AuthLogin(PlayerDataInfo playerDataInfo)
    {
        _lastPlayerDataInfo = playerDataInfo;
        await NetworkManager.Login(playerDataInfo.WorldId, AddLog);
        LoginOk = true;
        await UserGetUserData();
        await _timeZoneAwareJobRegister.RegisterJobs(UserId);
    }

    public async Task AutoLogin(bool manual = false)
    {
        var accountInfo = _accountManager.GetAccountInfo(UserId);
        if (!manual && !accountInfo.AutoLogin) return;
        AddLog(ResourceStrings.AutoLoginonStartup);
        var playerDataInfos = await GetPlayerDataInfo();
        var playerDataInfo = accountInfo.AutoLoginWorldId > 0
            ? playerDataInfos.Find(d => d.WorldId == accountInfo.AutoLoginWorldId)
            : playerDataInfos.MaxBy(d => d.LastLoginTime);
        if (playerDataInfo == null) return;
        await Login(playerDataInfo, !manual);
    }

    public async Task Logout()
    {
        await _timeZoneAwareJobRegister.DeregisterJobs(UserId);
        LoginOk = false;
    }

    public async Task Login(PlayerDataInfo playerDataInfo = null, bool autoLoginThisWorld = false)
    {
        Logining = true;
        try
        {
            if (playerDataInfo == null) playerDataInfo = _lastPlayerDataInfo;
            if (playerDataInfo == null) throw new Exception("playerDataInfo is null");
            _AuthOption.Update(d =>
            {
                var account = d.Accounts.Find(x => x.UserId == UserId);
                if (account != null) account.AutoLoginWorldId = autoLoginThisWorld ? playerDataInfo.WorldId : 0;
            });
            await AuthLogin(playerDataInfo);
        }
        catch (Exception e)
        {
            AddLog(e.ToString());
            return;
        }
        finally
        {
            Logining = false;
        }

        await UserGetUserData();
        await GetMyPage();
        await GetMissionInfo();
        await GetBountyRequestInfo();
        // await GetMonthlyLoginBonusInfo(); 
    }

    public async Task<string> GetClientKey(string password)
    {
        var authToken = await GetAuthToken();
        var createUserResponse = await GetResponse<CreateUserRequest, CreateUserResponse>(new CreateUserRequest
        {
            AdverisementId = Guid.NewGuid().ToString("D"),
            AppVersion = AuthOption.AppVersion,
            CountryCode = "CN",
            DeviceToken = "",
            ModelName = AuthOption.ModelName,
            DisplayLanguage = NetworkManager.LanguageType,
            OSVersion = AuthOption.OSVersion,
            SteamTicket = "",
            AuthToken = authToken
        });
        var clientKey = createUserResponse.ClientKey;
        // var accessTokenResponse = await GetResponse<CreateAccessTokenRequest, CreateAccessTokenResponse>(new CreateAccessTokenRequest()
        // {
        //     ClientKey = clientKey, UserId = createUserResponse.UserId
        // });
        var getComebackUserDataResponse = await GetResponse<GetComebackUserDataRequest, GetComebackUserDataResponse>(new GetComebackUserDataRequest
        {
            FromUserId = createUserResponse.UserId, Password = password, SnsType = SnsType.OrtegaId, UserId = UserId, AuthToken = authToken
        });
        var comebackUserResponse = await GetResponse<ComebackUserRequest, ComebackUserResponse>(new ComebackUserRequest
        {
            FromUserId = createUserResponse.UserId, OneTimeToken = getComebackUserDataResponse.OneTimeToken, ToUserId = UserId
        });
        return comebackUserResponse.ClientKey;
    }

    private async Task<int> GetAuthToken()
    {
        try
        {
            var json = await _httpClientFactory.CreateClient().GetStringAsync("https://list.moonheart.dev/d/public/mmtm/AddressableLocalAssets/ScriptableObjects/AuthToken/AuthTokenData.json?v=" + DateTimeOffset.Now.ToUnixTimeSeconds());
            return JObject.Parse(json)["_authToken"]?.Value<int>() ?? 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "failed to get authToken");
            return 0;
        }
    }
}