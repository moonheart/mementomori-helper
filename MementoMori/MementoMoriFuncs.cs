using System.Net;
using System.Reactive.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using MementoMori.Exceptions;
using MementoMori.Extensions;
using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Data.ApiInterface;
using MementoMori.Ortega.Share.Data.ApiInterface.Auth;
using MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle;
using MementoMori.Ortega.Share.Data.ApiInterface.Equipment;
using MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus;
using MementoMori.Ortega.Share.Data.ApiInterface.User;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Data.Mission;
using MementoMori.Ortega.Share.Data.Notice;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share.Master;
using MessagePack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using BountyQuestGetListResponse = MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest.GetListResponse;
using Formatting = Newtonsoft.Json.Formatting;

namespace MementoMori;

public partial class MementoMoriFuncs
{
    private Uri _apiAuth = new("https://prd1-auth.mememori-boi.com/api/");

    [Reactive]
    public RuntimeInfo RuntimeInfo { get; private set; }

    [Reactive]
    public UserSyncData UserSyncData { get; private set; }

    [Reactive]
    public Dictionary<MissionGroupType, MissionInfo> MissionInfoDict { get; set; }

    [Reactive]
    public GetMypageResponse Mypage { get; private set; }

    [Reactive]
    public BountyQuestGetListResponse BountyQuestResponseInfo { get; private set; }

    [Reactive]
    public GetMonthlyLoginBonusInfoResponse MonthlyLoginBonusInfo { get; private set; }

    [Reactive]
    public List<NoticeInfo> NoticeInfoList { get; set; }

    [Reactive]
    public List<NoticeInfo> EventInfoList { get; set; }

    [Reactive]
    public bool IsNotClearDungeonBattleMap { get; set; }

    private readonly MeMoriHttpClientHandler _meMoriHttpClientHandler;
    private readonly HttpClient _httpClient;
    private readonly HttpClient _unityHttpClient;


    private readonly AuthOption _authOption;
    private readonly GameConfig _gameConfig;
    private readonly ILogger<MementoMoriFuncs> _logger;

    private T ReadFromJson<T>(string jsonPath) where T : new()
    {
        if (!File.Exists(jsonPath))
        {
            return new T();
        }

        var json = File.ReadAllText(jsonPath);
        return JsonConvert.DeserializeObject<T>(json) ?? new T();
    }

    private void WriteToJson<T>(string jsonPath, T value)
    {
        if (value == null)
        {
            return;
        }

        File.WriteAllText(jsonPath, JsonConvert.SerializeObject(value, Formatting.Indented));
    }

    public MementoMoriFuncs(IOptions<AuthOption> authOption, IOptions<GameConfig> gameConfig, ILogger<MementoMoriFuncs> logger)
    {
        _logger = logger;
        _authOption = authOption.Value;
        _gameConfig = gameConfig.Value;
        _meMoriHttpClientHandler = new MeMoriHttpClientHandler(_authOption.Headers);
        _meMoriHttpClientHandler.OrtegaAccessToken.Subscribe(token => { RuntimeInfo.OrtegaAccessToken = token; });
        _meMoriHttpClientHandler.OrtegaMasterVersion.Subscribe(version => { RuntimeInfo.OrtegaMasterVersion = version; });
        _httpClient = new HttpClient(_meMoriHttpClientHandler);
        _unityHttpClient = new HttpClient();
        _unityHttpClient.DefaultRequestHeaders.Add("User-Agent",
            new[] {"UnityPlayer/2021.3.10f1 (UnityWebRequest/1.0, libcurl/7.80.0-DEV)"});
        _unityHttpClient.DefaultRequestHeaders.Add("X-Unity-Version", new[] {"2021.3.10f1"});
        AccountXml();

        Mypage = new GetMypageResponse();
        NoticeInfoList = new List<NoticeInfo>();

        // RuntimeInfo = new RuntimeInfo();
        RuntimeInfo = ReadFromJson<RuntimeInfo>("runtimeinfo.json");
        // UserSyncData = new UserSyncData();
        UserSyncData = ReadFromJson<UserSyncData>("usersyncdata.json");

        Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10)).Where(_ => !IsQuickActionExecuting).Subscribe(t =>
        {
            WriteToJson("runtimeinfo.json", RuntimeInfo);
            WriteToJson("usersyncdata.json", UserSyncData);
        });
    }

    private void AccountXml()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load("account.xml");
        var userId = doc.SelectSingleNode("/map/string[@name='0_Userid']").FirstChild.Value;
        var clientKey = doc.SelectSingleNode("/map/string[@name='0_ClientKey']").FirstChild.Value.Replace("%22", "");
        var deviceToken = doc.SelectSingleNode("/map/string[@name='KeyPrefix_0_NotificationDeviceToken']").FirstChild.Value.Replace("%22", "").Replace("%3A", ":");
        _authOption.UserId = long.Parse(userId);
        _authOption.ClientKey = clientKey;
        _authOption.DeviceToken = deviceToken;
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
        var loginPlayerResp = await GetResponse<LoginPlayerRequest, LoginPlayerResponse>(new LoginPlayerRequest
        {
            PlayerId = playerDataInfo.PlayerId, Password = playerDataInfo.Password
        });

        UserSyncData = (await UserGetUserData()).UserSyncData;
    }

    private async Task AuthGetServerHost(long worldId)
    {
        var req = new GetServerHostRequest() {WorldId = worldId};
        var resp = await GetResponse<GetServerHostRequest, GetServerHostResponse>(req);
        RuntimeInfo.ApiHost = resp.ApiHost;
    }

    public async Task DownloadMasterCatalog()
    {
        _logger.LogInformation("下载 master 目录中...");
        var dataUriResponse = await GetResponse<GetDataUriRequest, GetDataUriResponse>(new GetDataUriRequest() {CountryCode = "CN", UserId = 0});

        var url = string.Format(dataUriResponse.MasterUriFormat, RuntimeInfo.OrtegaMasterVersion, "master-catalog");
        var bytes = await _unityHttpClient.GetByteArrayAsync(url);
        var masterBookCatalog = MessagePackSerializer.Deserialize<MasterBookCatalog>(bytes);
        Directory.CreateDirectory("./Master");
        foreach (var (name, info) in masterBookCatalog.MasterBookInfoMap)
        {
            var localPath = $"./Master/{name}";
            if (File.Exists(localPath))
            {
                var md5 = await CalcFileMd5(localPath);
                if (md5 == info.Hash) continue;
                File.Delete(localPath);
            }

            var mbUrl = string.Format(dataUriResponse.MasterUriFormat, RuntimeInfo.OrtegaMasterVersion, name);
            var fileBytes = await _unityHttpClient.GetByteArrayAsync(mbUrl);
            await File.WriteAllBytesAsync(localPath, fileBytes);
        }

        Masters.TextResourceTable.SetLanguageType(LanguageType.zhTW);
        Masters.LoadAllMasters();
        _logger.LogInformation("下载 master 目录完成");
    }

    private async Task<string> CalcFileMd5(string path)
    {
        byte[] retVal;
        using (FileStream file = new FileStream(path, FileMode.Open))
        {
            MD5 md5 = MD5.Create();
            retVal = await md5.ComputeHashAsync(file);
            file.Close();
        }

        StringBuilder sb = new StringBuilder();
        foreach (byte t in retVal)
        {
            sb.Append(t.ToString("x2"));
        }

        return sb.ToString();
    }

    public async Task AutoDungeonBattle(Action<string> log)
    {
        // todo 脱装备进副本，然后穿装备
        var deckDtoInfo = UserSyncData.UserDeckDtoInfos.First(d => d.DeckUseContentType == DeckUseContentType.DungeonBattle).GetUserCharacterGuids();
        var equips = UserSyncData.UserEquipmentDtoInfos.Where(d => !string.IsNullOrEmpty(d.CharacterGuid)).GroupBy(d => d.CharacterGuid).ToList();
        foreach (var g in equips)
        {
            log($"脱下装备 {g.Key}");
            // 脱装备
            var removeEquipmentResponse = await GetResponse<RemoveEquipmentRequest, RemoveEquipmentResponse>(new RemoveEquipmentRequest()
            {
                UserCharacterGuid = g.Key,
                EquipmentSlotTypes = new List<EquipmentSlotType>()
                    {EquipmentSlotType.Armor, EquipmentSlotType.Gauntlet, EquipmentSlotType.Helmet, EquipmentSlotType.Shoes, EquipmentSlotType.Sub, EquipmentSlotType.Weapon}
            });
        }

        log("进入副本");
        // 进副本
        var battleInfoResponse1 =
            await GetResponse<GetDungeonBattleInfoRequest, GetDungeonBattleInfoResponse>(
                new GetDungeonBattleInfoRequest());
        foreach (var g in equips)
        {
            log($"穿上装备 {g.Key}");
            // 穿装备
            var changeInfos = g.Select(d =>
            {
                var equipmentMb = Masters.EquipmentTable.GetById(d.EquipmentId);
                return new EquipmentChangeInfo()
                {
                    EquipmentGuid = d.Guid,
                    EquipmentId = d.EquipmentId,
                    EquipmentSlotType = equipmentMb.SlotType,
                    IsInherit = false
                };
            });
            var changeEquipmentResponse = await GetResponse<ChangeEquipmentRequest, ChangeEquipmentResponse>(new ChangeEquipmentRequest()
            {
                UserCharacterGuid = g.Key, EquipmentChangeInfos = changeInfos.ToList()
            });
        }

        if (battleInfoResponse1.UserDungeonDtoInfo.IsDoneRewardClearLayer(3))
        {
            log("时空洞窟已通关");
            return;
        }

        while (true)
        {
            // 获取副本信息
            var battleInfoResponse =
                await GetResponse<GetDungeonBattleInfoRequest, GetDungeonBattleInfoResponse>(
                    new GetDungeonBattleInfoRequest());
            var grids = battleInfoResponse.CurrentDungeonBattleLayer.DungeonGrids.Select(d =>
            {
                var dungeonBattleGridMb = Masters.DungeonBattleGridTable.GetById(d.DungeonGridId);
                var power = battleInfoResponse.GridBattlePowerDict.TryGetValue(d.DungeonGridGuid, out var n) ? n : 0;
                return new
                {
                    Grid = d, GridMb = dungeonBattleGridMb, Power = power
                };
            }).ToList();
            // 当前节点状态
            var currentGrid = grids.First(d =>
                d.Grid.DungeonGridGuid == battleInfoResponse.UserDungeonDtoInfo.CurrentGridGuid);
            var layer = battleInfoResponse.CurrentDungeonBattleLayer.LayerCount;
            var state = battleInfoResponse.UserDungeonDtoInfo.CurrentGridState;
            var memo = currentGrid.GridMb.Memo;
            var type = currentGrid.GridMb.DungeonGridType;
            log($"当前第 {layer}层，坐标 {currentGrid.Grid.X},{currentGrid.Grid.Y}，状态 {state}, {Masters.TextResourceTable.Get(type)} 敌人战斗力 {currentGrid.Power}");
            Console.WriteLine($"当前第 {layer}层，坐标 {currentGrid.Grid.X},{currentGrid.Grid.Y}，状态 {state}, {Masters.TextResourceTable.Get(type)} 敌人战斗力 {currentGrid.Power}");

            async Task DoBattle()
            {
                // 选择出战斗力最高的5个角色
                var characters = battleInfoResponse.UserDungeonBattleCharacterDtoInfos.Where(d => d.CurrentHpPerMill != 0).OrderByDescending(d =>
                {
                    return BattlePowerCalculatorUtil.GetUserCharacterBattlePower(d.Guid);
                }).ToList();
                // todo 处理角色挂掉的情况
                var execBattleResponse = await GetResponse<ExecBattleRequest, ExecBattleResponse>(
                    new ExecBattleRequest()
                    {
                        CurrentTermId = battleInfoResponse.CurrentTermId,
                        DungeonGridGuid = currentGrid.Grid.DungeonGridGuid,
                        CharacterGuids = new List<string>()
                        {
                            characters[0].Guid,
                            characters[1].Guid,
                            characters[2].Guid,
                            characters[3].Guid,
                            characters[4].Guid,
                        }.Where(d => !d.IsNullOrEmpty()).ToList()
                    });
                var finishBattleResponse = await GetResponse<FinishBattleRequest, FinishBattleResponse>(
                    new FinishBattleRequest()
                    {
                        DungeonGridGuid = currentGrid.Grid.DungeonGridGuid,
                        CurrentTermId = battleInfoResponse.CurrentTermId,
                        VisitDungeonCount = 0
                    });
            }

            switch (state)
            {
                case DungeonBattleGridState.Done:

                    // 当前已完成，选择下一个节点
                    var nextGrid = grids.Where(d => d.Grid.Y == currentGrid.Grid.Y + 1 // 下一行
                                                    && (d.GridMb.DungeonGridType == DungeonBattleGridType.BattleNormal ||
                                                        d.GridMb.DungeonGridType == DungeonBattleGridType.BattleElite ||
                                                        d.GridMb.DungeonGridType == DungeonBattleGridType.BattleBoss ||
                                                        d.GridMb.DungeonGridType == DungeonBattleGridType.BattleBossNoRelic ||
                                                        d.GridMb.DungeonGridType == DungeonBattleGridType.EventBattleElite ||
                                                        d.GridMb.DungeonGridType == DungeonBattleGridType.EventBattleNormal ||
                                                        d.GridMb.DungeonGridType == DungeonBattleGridType.EventBattleSpecial ||
                                                        d.GridMb.DungeonGridType == DungeonBattleGridType.BattleAndRelicReinforce
                                                    ) // 战斗类型的
                    ).MinBy(d => d.Power);
                    if (nextGrid == null)
                    {
                        // 没有战斗类型的节点
                        nextGrid = grids.FirstOrDefault(d => d.Grid.Y == currentGrid.Grid.Y + 1);
                    }

                    if (nextGrid == null)
                    {
                        // 获取当前层奖励
                        var rewardClearLayerResponse =
                            await GetResponse<RewardClearLayerRequest, RewardClearLayerResponse>(
                                new RewardClearLayerRequest()
                                {
                                    ClearedLayer = battleInfoResponse.CurrentDungeonBattleLayer.LayerCount,
                                    CurrentTermId = battleInfoResponse.CurrentTermId,
                                    DungeonBattleDifficultyType = battleInfoResponse.CurrentDungeonBattleLayer
                                        .DungeonDifficultyType,
                                });
                        if (battleInfoResponse.CurrentDungeonBattleLayer.LayerCount == 3)
                        {
                            // 结束
                            return;
                        }
                        else
                        {
                            var diff = battleInfoResponse.CurrentDungeonBattleLayer.LayerCount == 2
                                ? DungeonBattleDifficultyType.Hard
                                : DungeonBattleDifficultyType.Normal;
                            var proceedLayerResponse = await GetResponse<ProceedLayerRequest, ProceedLayerResponse>(
                                new ProceedLayerRequest()
                                {
                                    CurrentTermId = battleInfoResponse.CurrentTermId,
                                    DungeonDifficultyType = diff,
                                });
                        }
                    }
                    else
                    {
                        switch (nextGrid.GridMb.DungeonGridType)
                        {
                            case DungeonBattleGridType.JoinCharacter:
                            {
                                var info = battleInfoResponse.UserDungeonBattleGuestCharacterDtoInfos.OrderByDescending(d=>d.BattlePower).First();
                                var execGuestResponse = await GetResponse<ExecGuestRequest, ExecGuestResponse>(
                                    new ExecGuestRequest()
                                    {
                                        DungeonGridGuid = nextGrid.Grid.DungeonGridGuid,
                                        GuestMBId = info.CharacterId,
                                        CurrentTermId = battleInfoResponse.CurrentTermId
                                    });
                                break;
                            }
                            default:
                            {
                                var selectGridResponse = await GetResponse<SelectGridRequest, SelectGridResponse>(
                                    new SelectGridRequest()
                                    {
                                        CurrentTermId = battleInfoResponse.CurrentTermId,
                                        DungeonGridGuid = nextGrid.Grid.DungeonGridGuid
                                    });
                                break;
                            }
                        }
                    }

                    break;
                case DungeonBattleGridState.Selected:
                    switch (type)
                    {
                        case DungeonBattleGridType.Start:
                            break;
                        case DungeonBattleGridType.BattleNormal:
                        case DungeonBattleGridType.BattleElite:
                        case DungeonBattleGridType.BattleBoss:
                        case DungeonBattleGridType.BattleBossNoRelic:
                        case DungeonBattleGridType.EventBattleNormal:
                        case DungeonBattleGridType.EventBattleElite:
                        case DungeonBattleGridType.EventBattleSpecial:
                            await DoBattle();
                            break;
                        case DungeonBattleGridType.Recovery:
                            var execRecoveryResponse = await GetResponse<ExecRecoveryRequest, ExecRecoveryResponse>(
                                new ExecRecoveryRequest()
                                {
                                    CurrentTermId = battleInfoResponse.CurrentTermId,
                                    DungeonGridGuid = currentGrid.Grid.DungeonGridGuid,
                                    IsHealed = true
                                });
                            break;
                        case DungeonBattleGridType.JoinCharacter:
                            break;
                        case DungeonBattleGridType.Shop:
                            var leaveShopResponse = await GetResponse<LeaveShopRequest, LeaveShopResponse>(
                                new LeaveShopRequest()
                                {
                                    CurrentTermId = battleInfoResponse.CurrentTermId,
                                    DungeonGridGuid = currentGrid.Grid.DungeonGridGuid,
                                });
                            break;
                        case DungeonBattleGridType.RelicReinforce:
                            var execReinforceRelicResponse =
                                await GetResponse<ExecReinforceRelicRequest, ExecReinforceRelicResponse>(
                                    new ExecReinforceRelicRequest()
                                    {
                                        CurrentTermId = battleInfoResponse.CurrentTermId,
                                        DungeonGridGuid = currentGrid.Grid.DungeonGridGuid,
                                    });
                            break;
                        case DungeonBattleGridType.BattleAndRelicReinforce:
                        {
                            await DoBattle();

                            var relicId = 0L;
                            var canUpgradeRelics = battleInfoResponse.UserDungeonDtoInfo.RelicIds
                                .Where(d => Masters.DungeonBattleRelicTable.GetById(d).DungeonRelicRarityType != DungeonBattleRelicRarityType.SSR).ToList();
                            foreach (var info in _gameConfig.DungeonBattleRelicSort)
                            {
                                if (canUpgradeRelics.Contains(info.Id))
                                {
                                    relicId = info.Id;
                                    break;
                                }
                            }

                            var response = await GetResponse<RewardBattleReinforceRelicRequest, RewardBattleReinforceRelicResponse>(
                                new RewardBattleReinforceRelicRequest()
                                {
                                    SelectedRelicId = relicId,
                                    CurrentTermId = battleInfoResponse.CurrentTermId,
                                    DungeonGridGuid = currentGrid.Grid.DungeonGridGuid,
                                });
                            break;
                        }
                        case DungeonBattleGridType.TreasureChest:
                            break;
                        case DungeonBattleGridType.Revival:
                            var execReviveResponse = await GetResponse<ExecReviveRequest, ExecReviveResponse>(
                                new ExecReviveRequest()
                                {
                                    CurrentTermId = battleInfoResponse.CurrentTermId,
                                    DungeonGridGuid = currentGrid.Grid.DungeonGridGuid,
                                    IsRevived = true
                                });
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                case DungeonBattleGridState.Reward:
                {
                    if (type == DungeonBattleGridType.BattleAndRelicReinforce)
                    {
                        // 选择加成奖励
                        var relicId = 0L;
                        var upgradableRelics = battleInfoResponse.UserDungeonDtoInfo.RelicIds.Where(d =>
                        {
                            var mb = Masters.DungeonBattleRelicTable.GetByReinforceFrom(d);
                            if (mb == null)
                            {
                                // 不可升级
                                return false;
                            }

                            if (battleInfoResponse.UserDungeonDtoInfo.RelicIds.Contains(mb.Id))
                            {
                                // 升级后的已经存在了
                                return false;
                            }

                            return true;
                        }).ToList();
                        foreach (var info in _gameConfig.DungeonBattleRelicSort)
                        {
                            if (upgradableRelics.Contains(info.Id))
                            {
                                // relicId = Masters.DungeonBattleRelicTable.GetByReinforceFrom(info.Id).Id;
                                relicId = info.Id;
                                break;
                            }
                        }

                        var rewardBattleReceiveRelicResponse =
                            await GetResponse<RewardBattleReinforceRelicRequest, RewardBattleReinforceRelicResponse>(
                                new RewardBattleReinforceRelicRequest()
                                {
                                    CurrentTermId = battleInfoResponse.CurrentTermId,
                                    DungeonGridGuid = currentGrid.Grid.DungeonGridGuid,
                                    SelectedRelicId = relicId,
                                });
                    }
                    else
                    {
                        // 选择加成奖励
                        var relicId = 0L;

                        foreach (var info in _gameConfig.DungeonBattleRelicSort)
                        {
                            if (battleInfoResponse.RewardRelicIds.Contains(info.Id))
                            {
                                relicId = info.Id;
                                break;
                            }
                        }

                        var rewardBattleReceiveRelicResponse =
                            await GetResponse<RewardBattleReceiveRelicRequest, RewardBattleReceiveRelicResponse>(
                                new RewardBattleReceiveRelicRequest()
                                {
                                    CurrentTermId = battleInfoResponse.CurrentTermId,
                                    DungeonGridGuid = currentGrid.Grid.DungeonGridGuid,
                                    SelectedRelicId = relicId,
                                });
                    }

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public async Task<GetUserDataResponse> UserGetUserData()
    {
        var req = new GetUserDataRequest { };
        var data = await GetResponse<GetUserDataRequest, GetUserDataResponse>(req);
        UserSyncData = data.UserSyncData;
        IsNotClearDungeonBattleMap = data.IsNotClearDungeonBattleMap;
        return data;
    }


    public async Task<TResp> GetResponse<TReq, TResp>(TReq req)
        where TReq : ApiRequestBase
        where TResp : ApiResponseBase
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
            uri = new Uri(new Uri(RuntimeInfo.ApiHost), apiAttr.Uri);

            if (RuntimeInfo.OrtegaAccessToken.IsNullOrEmpty())
            {
                await AuthLogin();
            }
        }
        else
        {
            throw new NotSupportedException();
        }


        // var reqMap = JsonConvert.DeserializeObject<Dictionary<object, object>>(JsonConvert.SerializeObject(req));
        var bytes = MessagePackSerializer.Serialize(req);
        var respMsg = await _httpClient.PostAsync(uri,
            new ByteArrayContent(bytes) {Headers = {{"content-type", "application/json"}}});
        if (!respMsg.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(respMsg.ToString());
        }

        var respBytes = await respMsg.Content.ReadAsByteArrayAsync();
        if (respMsg.Headers.TryGetValues("ortegastatuscode", out var headers2))
        {
            var ortegastatuscode = headers2.FirstOrDefault() ?? "";
            if (ortegastatuscode != "0")
            {
                AddLog(uri.ToString());
                var apiErrResponse = MessagePackSerializer.Deserialize<ApiErrorResponse>(respBytes);
                var errorCodeMessage = Masters.TextResourceTable.GetErrorCodeMessage(apiErrResponse.ErrorCode);
                Console.WriteLine(errorCodeMessage);
                AddLog($"{errorCodeMessage}");
                AddLog(req.ToJson());
                AddLog(apiErrResponse.ToJson());
                Console.WriteLine(apiErrResponse.ToJson());
                throw new ApiErrorException(apiErrResponse.ErrorCode);
            }
        }

        var response = MessagePackSerializer.Deserialize<TResp>(respBytes);
        if (response is IUserSyncApiResponse userSyncApiResponse)
        {
            UserSyncData.UserItemEditorMergeUserSyncData(userSyncApiResponse.UserSyncData);
            this.RaisePropertyChanged(nameof(UserSyncData));
        }

        return response;
        // return JsonConvert.DeserializeObject<TResp>(JsonConvert.SerializeObject(tmp));
    }
}