using System.Reactive.Subjects;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Data.ApiInterface;
using MementoMori.Ortega.Share.Data.ApiInterface.Auth;
using MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle;
using MementoMori.Ortega.Share.Data.ApiInterface.Equipment;
using MementoMori.Ortega.Share.Data.ApiInterface.Friend;
using MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus;
using MementoMori.Ortega.Share.Data.ApiInterface.Present;
using MementoMori.Ortega.Share.Data.ApiInterface.User;
using MementoMori.Ortega.Share.Data.ApiInterface.Vip;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master;
using MementoMori.Ortega.Share.Master.Data;
using MessagePack;
using Microsoft.Extensions.Options;

namespace MementoMori;

public class MementoMoriFuncs
{
    private Uri _apiHost;
    private Uri _apiAuth = new("https://prd1-auth.mememori-boi.com/api/");

    private readonly BehaviorSubject<RuntimeInfo> _runtimeInfoSubject = new(new RuntimeInfo());
    public IObservable<RuntimeInfo> RuntimeInfoSubject => _runtimeInfoSubject;

    private readonly BehaviorSubject<UserSyncData> _userSyncDataSubject = new(new UserSyncData());
    public IObservable<UserSyncData> UserSyncData => _userSyncDataSubject;

    private readonly RuntimeInfo _runtimeInfo = new();
    private readonly MeMoriHttpClientHandler _meMoriHttpClientHandler;
    private readonly HttpClient _httpClient;
    private readonly HttpClient _unityHttpClient;


    private readonly AuthOption _authOption;
    private readonly GameConfig _gameConfig;

    public MementoMoriFuncs(IOptions<AuthOption> authOption, IOptions<GameConfig> gameConfig)
    {
        _authOption = authOption.Value;
        _gameConfig = gameConfig.Value;
        AccountXml();
        _meMoriHttpClientHandler = new MeMoriHttpClientHandler(_authOption.Headers);
        _meMoriHttpClientHandler.OrtegaAccessToken.Subscribe(token =>
        {
            _runtimeInfo.OrtegaAccessToken = token;
            _runtimeInfoSubject.OnNext(_runtimeInfo);
        });
        _meMoriHttpClientHandler.OrtegaMasterVersion.Subscribe(version =>
        {
            _runtimeInfo.OrtegaMasterVersion = version;
            _runtimeInfoSubject.OnNext(_runtimeInfo);
        });

        _httpClient = new HttpClient(_meMoriHttpClientHandler);
        _unityHttpClient = new HttpClient();
        _unityHttpClient.DefaultRequestHeaders.Add("User-Agent",
            new[] {"UnityPlayer/2021.3.10f1 (UnityWebRequest/1.0, libcurl/7.80.0-DEV)"});
        _unityHttpClient.DefaultRequestHeaders.Add("X-Unity-Version", new[] {"2021.3.10f1"});

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
        var loginPlayerResp = await UserLoginPlayer(playerDataInfo.PlayerId, playerDataInfo.Password);

        await DownloadMasterCatalog();

        var userSyncData = (await UserGetUserData()).UserSyncData;
        _userSyncDataSubject.OnNext(userSyncData);
    }

    private async Task AuthGetServerHost(long worldId)
    {
        var req = new GetServerHostRequest() {WorldId = worldId};
        var resp = await GetResponse<GetServerHostRequest, GetServerHostResponse>(req);
        _apiHost = new Uri(resp.ApiHost);
        _runtimeInfo.ApiHost = resp.ApiHost;
        _runtimeInfoSubject.OnNext(_runtimeInfo);
    }

    private async Task DownloadMasterCatalog()
    {
        var url =
            $"https://cdn-mememori.akamaized.net/master/prd1/version/{_runtimeInfo.OrtegaMasterVersion}/master-catalog";
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

            var mbUrl =
                $"https://cdn-mememori.akamaized.net/master/prd1/version/{_runtimeInfo.OrtegaMasterVersion}/{name}";
            var fileBytes = await _unityHttpClient.GetByteArrayAsync(mbUrl);
            await File.WriteAllBytesAsync(localPath, fileBytes);
        }

        Masters.ItemTable.Load();
        Masters.CharacterTable.Load();
        Masters.TextResourceTable.Load(LanguageType.zhTW);
        Masters.EquipmentTable.Load();
        Masters.SphereTable.Load();
        Masters.DungeonBattleRelicTable.Load();
        Masters.EquipmentSetMaterialTable.Load();
        Masters.TreasureChestTable.Load();
        Masters.LevelLinkTable.Load();
        Masters.DungeonBattleGridTable.Load();
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

    /// <summary>
    /// 自动精炼非D级别装备，然后将所有魔装继承到D级别装备
    /// </summary>
    public async Task AutoEquipmentInheritance()
    {
        while (true)
        {
            // 批量精炼
            var castManyResponse = await GetResponse<CastManyRequest, CastManyResponse>(new CastManyRequest()
            {
                RarityFlags = EquipmentRarityFlags.S | EquipmentRarityFlags.A | EquipmentRarityFlags.B |
                              EquipmentRarityFlags.C
            });
            var usersyncData = await UserGetUserData();
            // 找到所有 等级为S、魔装、未装备 的装备
            var equipments = usersyncData.UserSyncData.UserEquipmentDtoInfos.Select(d => new
            {
                Equipment = d, EquipmentMB = Masters.EquipmentTable.GetById(d.EquipmentId)
            });

            var sEquipments = equipments.Where(d =>
                    d.Equipment.CharacterGuid == "" && // 未装备
                    d.Equipment.MatchlessSacredTreasureLv == 1 && // 魔装等级为 1
                    (d.EquipmentMB.RarityFlags & EquipmentRarityFlags.S) != 0 // 稀有度为 S
            );

            if (sEquipments.Count() == 0) break;

            foreach (var grouping in sEquipments.GroupBy(d => d.EquipmentMB.SlotType))
            {
                // 当前能够接受继承的 D 级别装备
                var currentTypeEquips = equipments.Where(d =>
                {
                    return (d.EquipmentMB.RarityFlags & EquipmentRarityFlags.D) != 0 &&
                           d.EquipmentMB.SlotType == grouping.Key && d.Equipment.MatchlessSacredTreasureLv == 0;
                });
                var processedDEquips = new List<UserItemDtoInfo>();

                // 还缺多少装备
                var needMoreCount = grouping.Count() - currentTypeEquips.Count();
                if (needMoreCount > 0)
                {
                    // 找到未解封的装备物品
                    var equipItems = usersyncData.UserSyncData.UserItemDtoInfo.Where(d =>
                    {
                        if (d.ItemType != ItemType.Equipment) return false;
                        var equipmentMb = Masters.EquipmentTable.GetById(d.ItemId);
                        if (equipmentMb.SlotType != grouping.Key) return false;
                        if ((equipmentMb.RarityFlags & EquipmentRarityFlags.D) == 0) return false;
                        return true;
                    });
                    foreach (var equipItem in equipItems)
                    {
                        if (needMoreCount <= 0) break;

                        var equipmentMb = Masters.EquipmentTable.GetById(equipItem.ItemId);
                        Console.WriteLine(equipmentMb.Memo);
                        // 找到可以装备的一个角色
                        var userCharacterDtoInfo = usersyncData.UserSyncData.UserCharacterDtoInfos.Where(d =>
                        {
                            var characterMb = Masters.CharacterTable.GetById(d.CharacterId);
                            if ((characterMb.JobFlags & equipmentMb.EquippedJobFlags) == 0) return false; // 装备职业

                            if (d.Level >= equipmentMb.EquipmentLv) return true; // 装备等级

                            if (usersyncData.UserSyncData.UserLevelLinkMemberDtoInfos.Exists(x =>
                                    d.Guid == x.UserCharacterGuid)
                                && usersyncData.UserSyncData.UserLevelLinkDtoInfo.PartyLevel >=
                                equipmentMb.EquipmentLv) // 角色在等级链接里面并且等级链接大于装备等级
                            {
                                return true;
                            }

                            return false;
                        }).First();


                        // 获取角色某个位置的准备

                        var replacedEquip = usersyncData.UserSyncData.UserEquipmentDtoInfos.Where(d =>
                        {
                            var byId = Masters.EquipmentTable.GetById(d.EquipmentId);
                            return d.CharacterGuid == userCharacterDtoInfo.Guid &&
                                   byId.SlotType == equipmentMb.SlotType;
                        }).First();

                        // 替换装备
                        var changeEquipmentResponse =
                            await GetResponse<ChangeEquipmentRequest, ChangeEquipmentResponse>(
                                new ChangeEquipmentRequest()
                                {
                                    UserCharacterGuid = userCharacterDtoInfo.Guid,
                                    EquipmentChangeInfos = new List<EquipmentChangeInfo>()
                                    {
                                        new()
                                        {
                                            EquipmentId = equipItem.ItemId,
                                            EquipmentSlotType = equipmentMb.SlotType,
                                            IsInherit = false
                                        }
                                    }
                                });

                        // 恢复装备
                        var changeEquipmentResponse1 =
                            await GetResponse<ChangeEquipmentRequest, ChangeEquipmentResponse>(
                                new ChangeEquipmentRequest()
                                {
                                    UserCharacterGuid = userCharacterDtoInfo.Guid,
                                    EquipmentChangeInfos = new List<EquipmentChangeInfo>()
                                    {
                                        new()
                                        {
                                            EquipmentGuid = replacedEquip.Guid,
                                            EquipmentId = replacedEquip.EquipmentId,
                                            EquipmentSlotType = equipmentMb.SlotType,
                                            IsInherit = false
                                        }
                                    }
                                });

                        needMoreCount--;
                        processedDEquips.Add(equipItem);
                    }
                }


                // 继承            
                foreach (var x1 in grouping)
                {
                    // 同步数据
                    usersyncData = await UserGetUserData();

                    var userEquipmentDtoInfo = usersyncData.UserSyncData.UserEquipmentDtoInfos.Where(d =>
                    {
                        var equipmentMb = Masters.EquipmentTable.GetById(d.EquipmentId);
                        if (d.MatchlessSacredTreasureLv == 0 // 未被继承的装备 
                            && equipmentMb.SlotType == x1.EquipmentMB.SlotType // 同一个位置 
                            && (equipmentMb.RarityFlags & EquipmentRarityFlags.D) != 0 // 稀有度为 D
                           )
                        {
                            return true;
                        }

                        return false;
                    }).First();

                    var inheritanceEquipmentResponse =
                        await GetResponse<InheritanceEquipmentRequest, InheritanceEquipmentResponse>(
                            new InheritanceEquipmentRequest()
                            {
                                InheritanceEquipmentGuid = userEquipmentDtoInfo.Guid,
                                SourceEquipmentGuid = x1.Equipment.Guid
                            });
                    Console.WriteLine($"继承完成 {x1.EquipmentMB.Memo}=>{userEquipmentDtoInfo.Guid}");
                }
            }
        }
    }


    public async Task AutoDungeonBattle(Action<string> log)
    {
        // todo 脱装备进副本，然后穿装备
        var deckDtoInfo = _userSyncDataSubject.Value.UserDeckDtoInfos.First(d=>d.DeckUseContentType == DeckUseContentType.DungeonBattle).GetUserCharacterGuids();
        var equips = _userSyncDataSubject.Value.UserEquipmentDtoInfos.Where(d=>!string.IsNullOrEmpty(d.CharacterGuid)).GroupBy(d=>d.CharacterGuid);
        foreach (var g in equips)
        {
            log($"脱下装备 {g.Key}");
            // 脱装备
            var removeEquipmentResponse = await GetResponse<RemoveEquipmentRequest, RemoveEquipmentResponse>(new RemoveEquipmentRequest()
            {
                UserCharacterGuid = g.Key, EquipmentSlotTypes = new List<EquipmentSlotType>(){EquipmentSlotType.Armor, EquipmentSlotType.Gauntlet, EquipmentSlotType.Helmet, EquipmentSlotType.Shoes, EquipmentSlotType.Sub, EquipmentSlotType.Weapon}
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
            log($"当前第 {layer}层，坐标 {currentGrid.Grid.X},{currentGrid.Grid.Y}，状态 {state}, {memo} {type} 敌人战斗力 {currentGrid.Power}");
            Console.WriteLine($"当前第 {layer}层，坐标 {currentGrid.Grid.X},{currentGrid.Grid.Y}，状态 {state}, {memo} {type} 敌人战斗力 {currentGrid.Power}");

            async Task DoBattle()
            {
                var userSyncData = (await UserGetUserData()).UserSyncData;
                // battleInfoResponse.UserDungeonBattleCharacterDtoInfos.Where(d =>
                // {
                //     // todo 选择出战斗力最高的5个角色
                //     var characterMb = Masters.CharacterTable.GetById(d.CharacterId);
                //     return d.CurrentHpPerMill>0 && characterMb.
                // })
                var userDeckDtoInfo = userSyncData.UserDeckDtoInfos.First(d =>
                    d.DeckUseContentType == DeckUseContentType.DungeonBattle);
                // todo 处理角色挂掉的情况
                var execBattleResponse = await GetResponse<ExecBattleRequest, ExecBattleResponse>(
                    new ExecBattleRequest()
                    {
                        CurrentTermId = battleInfoResponse.CurrentTermId,
                        DungeonGridGuid = currentGrid.Grid.DungeonGridGuid,
                        CharacterGuids = new List<string>()
                        {
                            userDeckDtoInfo.UserCharacterGuid1,
                            userDeckDtoInfo.UserCharacterGuid2,
                            userDeckDtoInfo.UserCharacterGuid3,
                            userDeckDtoInfo.UserCharacterGuid4,
                            userDeckDtoInfo.UserCharacterGuid5,
                        }
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
                                                    && (d.GridMb.DungeonGridType ==
                                                        DungeonBattleGridType.BattleNormal ||
                                                        d.GridMb.DungeonGridType == DungeonBattleGridType.BattleElite ||
                                                        d.GridMb.DungeonGridType == DungeonBattleGridType.BattleBoss ||
                                                        d.GridMb.DungeonGridType ==
                                                        DungeonBattleGridType.BattleBossNoRelic ||
                                                        d.GridMb.DungeonGridType ==
                                                        DungeonBattleGridType.BattleAndRelicReinforce
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
                                var info = battleInfoResponse.UserDungeonBattleGuestCharacterDtoInfos.First();
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
                        {await DoBattle();
                            
                            var relicId = 0L;
                            var canUpgradeRelics = battleInfoResponse.UserDungeonDtoInfo.RelicIds.Where(d=>Masters.DungeonBattleRelicTable.GetById(d).DungeonRelicRarityType != DungeonBattleRelicRarityType.SSR).ToList();
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
                        case DungeonBattleGridType.EventBattleNormal:
                            break;
                        case DungeonBattleGridType.EventBattleElite:
                            break;
                        case DungeonBattleGridType.EventBattleSpecial:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                case DungeonBattleGridState.Reward:
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

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public async Task<GetDataUriResponse> AuthGetDataUri(string countryCode, long userId)
    {
        var req = new GetDataUriRequest() {CountryCode = countryCode, UserId = userId};
        return await GetResponse<GetDataUriRequest, GetDataUriResponse>(req);
    }

    private async Task<LoginPlayerResponse> UserLoginPlayer(long playerId, string password)
    {
        var req = new LoginPlayerRequest {PlayerId = playerId, Password = password};
        return await GetResponse<LoginPlayerRequest, LoginPlayerResponse>(req);
    }

    public async Task<GetUserDataResponse> UserGetUserData()
    {
        var req = new GetUserDataRequest { };
        var data = await GetResponse<GetUserDataRequest, GetUserDataResponse>(req);
        _userSyncDataSubject.OnNext(data.UserSyncData);
        return data;
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
    /// 高速战斗
    /// </summary>
    /// <returns></returns>
    public async Task<QuickResponse> BattleQuick(QuestQuickExecuteType questQuickExecuteType, int quickCount)
    {
        var req = new QuickRequest() {QuestQuickExecuteType = questQuickExecuteType, QuickCount = quickCount};
        return await GetResponse<QuickRequest, QuickResponse>(req);
    }

    public async Task<ReceiveItemResponse> PresentReceiveItem()
    {
        var req = new ReceiveItemRequest() {LanguageType = LanguageType.zhTW};
        return await GetResponse<ReceiveItemRequest, ReceiveItemResponse>(req);
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