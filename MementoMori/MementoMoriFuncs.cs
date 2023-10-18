using System.Reactive.Linq;
using System.Xml;
using MementoMori.Common.Localization;
using MementoMori.Exceptions;
using MementoMori.Jobs;
using MementoMori.Option;
using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Data.ApiInterface;
using MementoMori.Ortega.Share.Data.ApiInterface.Auth;
using MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle;
using MementoMori.Ortega.Share.Data.ApiInterface.Equipment;
using MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus;
using MementoMori.Ortega.Share.Data.ApiInterface.User;
using MementoMori.Ortega.Share.Data.Auth;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Data.Mission;
using MementoMori.Ortega.Share.Data.Notice;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
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

    private readonly AuthOption _authOption;
    private GameConfig GameConfig => _writableGameConfig.Value;
    private readonly ILogger<MementoMoriFuncs> _logger;

    private readonly MementoNetworkManager _networkManager;
    private readonly TimeZoneAwareJobRegister _timeZoneAwareJobRegister;
    private readonly TimeManager _timeManager;
    private readonly IWritableOptions<GameConfig> _writableGameConfig;

    private T ReadFromJson<T>(string jsonPath) where T : new()
    {
        if (!File.Exists(jsonPath)) return new T();

        var json = File.ReadAllText(jsonPath);
        return JsonConvert.DeserializeObject<T>(json) ?? new T();
    }

    private void WriteToJson<T>(string jsonPath, T value)
    {
        if (value == null) return;

        File.WriteAllText(jsonPath, JsonConvert.SerializeObject(value, Formatting.Indented));
    }

    public MementoMoriFuncs(IOptions<AuthOption> authOption, ILogger<MementoMoriFuncs> logger, MementoNetworkManager networkManager,
        TimeZoneAwareJobRegister timeZoneAwareJobRegister, TimeManager timeManager, IWritableOptions<GameConfig> writableGameConfig)
    {
        _logger = logger;
        _networkManager = networkManager;
        _timeZoneAwareJobRegister = timeZoneAwareJobRegister;
        _timeManager = timeManager;
        _writableGameConfig = writableGameConfig;
        _authOption = authOption.Value;
        AccountXml();

        Mypage = new GetMypageResponse();
        NoticeInfoList = new List<NoticeInfo>();

        UserSyncData = ReadFromJson<UserSyncData>("usersyncdata.json");

        Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10)).Where(_ => !IsQuickActionExecuting).Subscribe(t => { WriteToJson("usersyncdata.json", UserSyncData); });
    }

    private void AccountXml()
    {
        if (File.Exists("account.xml"))
            try
            {
                var doc = new XmlDocument();
                doc.Load("account.xml");
                var userId = doc.SelectSingleNode("/map/string[@name='0_Userid']").FirstChild.Value;
                var clientKey = doc.SelectSingleNode("/map/string[@name='0_ClientKey']").FirstChild.Value.Replace("%22", "");
                _authOption.UserId = long.Parse(userId);
                _authOption.ClientKey = clientKey;
            }
            catch (XmlException)
            {
                _logger.LogInformation("account.xml format error");
            }
    }

    public async Task<List<PlayerDataInfo>> GetPlayerDataInfo()
    {
        var reqBody = new LoginRequest()
        {
            ClientKey = _authOption.ClientKey,
            DeviceToken = _authOption.DeviceToken,
            AppVersion = _authOption.AppVersion,
            OSVersion = _authOption.OSVersion,
            ModelName = _authOption.ModelName,
            AdverisementId = Guid.NewGuid().ToString("D"),
            UserId = _authOption.UserId
        };
        return await _networkManager.GetPlayerDataInfoList(reqBody, AddLog);
    }

    public async Task AuthLogin(PlayerDataInfo playerDataInfo)
    {
        _lastPlayerDataInfo = playerDataInfo;
        await _networkManager.Login(playerDataInfo.WorldId, AddLog);
        await UserGetUserData();
        await _timeZoneAwareJobRegister.RegisterJobs();
    }

    public async Task AutoDungeonBattle(Action<string> log, CancellationToken cancellationToken)
    {
        // 脱装备进副本，然后穿装备
        var equips = UserSyncData.UserEquipmentDtoInfos.Where(d => !string.IsNullOrEmpty(d.CharacterGuid)).GroupBy(d => d.CharacterGuid).ToList();
        foreach (var g in equips)
        {
            var characterDto = UserSyncData.UserCharacterDtoInfos.Find(d => d.Guid == g.Key);
            var name = Masters.TextResourceTable.Get(Masters.CharacterTable.GetById(characterDto.CharacterId).NameKey);
            log(string.Format(ResourceStrings.RemoveEquipmentOfCharacter, name, characterDto.Level));

            // 脱装备
            var removeEquipmentResponse = await GetResponse<RemoveEquipmentRequest, RemoveEquipmentResponse>(new RemoveEquipmentRequest()
            {
                UserCharacterGuid = g.Key,
                EquipmentSlotTypes = g.Select(d => Masters.EquipmentTable.GetById(d.EquipmentId).SlotType).ToList()
            });
        }

        log($"{ResourceStrings.Enter} {Masters.TextResourceTable.Get("[CommonHeaderDungeonBattleLabel]")}");
        // 进副本
        var battleInfoResponse = await GetResponse<GetDungeonBattleInfoRequest, GetDungeonBattleInfoResponse>(new GetDungeonBattleInfoRequest());
        foreach (var g in equips)
        {
            var characterDto = UserSyncData.UserCharacterDtoInfos.Find(d => d.Guid == g.Key);
            var name = Masters.TextResourceTable.Get(Masters.CharacterTable.GetById(characterDto.CharacterId).NameKey);
            log(string.Format(ResourceStrings.PutOnEquipmentOfCharacter, name, characterDto.Level));
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

        if (battleInfoResponse.UserDungeonDtoInfo.IsDoneRewardClearLayer(3))
        {
            log($"{Masters.TextResourceTable.Get("[CommonHeaderDungeonBattleLabel]")} {ResourceStrings.Finished}");
            return;
        }

        while (!cancellationToken.IsCancellationRequested)
        {
            // 获取副本信息
            battleInfoResponse =
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
            log(string.Format(ResourceStrings.CaveCurrentState, layer, currentGrid.Grid.X, currentGrid.Grid.Y, state, type, currentGrid.Power));

            async Task DoBattle()
            {
                // 检查用户编队
                var battleCharacterGuids = new List<string>();
                var userDeckDtoInfo = UserSyncData.UserDeckDtoInfos.Find(d => d.DeckUseContentType == DeckUseContentType.DungeonBattle);
                foreach (var characterGuid in userDeckDtoInfo.GetUserCharacterGuids())
                {
                    var characterDtoInfo = battleInfoResponse.UserDungeonBattleCharacterDtoInfos.Find(d => d.Guid == characterGuid);
                    if (characterDtoInfo == null || characterDtoInfo.CurrentHpPerMill == 0)
                        // 没有角色或者角色挂掉了
                        continue;

                    battleCharacterGuids.Add(characterGuid);
                }

                // 检查是否需要补充角色
                var emptyPosition = 5 - battleCharacterGuids.Count;
                // 按照战力排序
                battleCharacterGuids.AddRange(battleInfoResponse.UserDungeonBattleCharacterDtoInfos
                    .Where(d => d.CurrentHpPerMill != 0)
                    .OrderByDescending(d => BattlePowerCalculatorUtil.GetUserCharacterBattlePower(d.Guid))
                    .Select(d => d.Guid).Take(emptyPosition));

                // 全灭, 如果已使用的苹果没有超过限制
                if (battleCharacterGuids.Count == 0 && battleInfoResponse.UserDungeonDtoInfo.UseDungeonRecoveryItemCount < GameConfig.DungeonBattle.MaxUseRecoveryItem)
                {
                    var useRecoveryItemResponse = await GetResponse<UseRecoveryItemRequest, UseRecoveryItemResponse>(new() {CurrentTermId = battleInfoResponse.CurrentTermId});
                }

                var execBattleResponse = await GetResponse<ExecBattleRequest, ExecBattleResponse>(
                    new ExecBattleRequest()
                    {
                        CurrentTermId = battleInfoResponse.CurrentTermId,
                        DungeonGridGuid = currentGrid.Grid.DungeonGridGuid,
                        CharacterGuids = battleCharacterGuids
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
                    var nextGrid = grids.FirstOrDefault(d => false);
                    // 先看下一行有没有商店节点,并且有目标物品
                    if (GameConfig.DungeonBattle.ShopTargetItems.Count > 0)
                        nextGrid = grids.FirstOrDefault(d => d.Grid.Y == currentGrid.Grid.Y + 1 // 下一行
                                                             && d.GridMb.DungeonGridType == DungeonBattleGridType.Shop
                                                             && GameConfig.DungeonBattle.ShopTargetItems.Any(x =>
                                                                 battleInfoResponse.UserDungeonBattleShopDtoInfos.Find(y => y.GridGuid == d.Grid.DungeonGridGuid).TradeShopItemList.Any(y => // 商店有目标物品
                                                                     y.SalePercent >= x.MinDiscountPercent && y.GiveItem.ItemType == x.ItemType && y.GiveItem.ItemId == x.ItemId))); // 商店的
                    // todo: 递归计算下一节点是否有宝箱

                    // 然后看有没有宝箱节点
                    if (GameConfig.DungeonBattle.PreferTreasureChest)
                        nextGrid ??= grids.FirstOrDefault(d => d.Grid.Y == currentGrid.Grid.Y + 1 && d.GridMb.DungeonGridType == DungeonBattleGridType.TreasureChest);

                    // 然后选择战斗节点
                    nextGrid ??= grids.Where(d => d.Grid.Y == currentGrid.Grid.Y + 1 // 下一行
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

                    // 然后选择回复节点
                    nextGrid ??= grids.FirstOrDefault(d => d.Grid.Y == currentGrid.Grid.Y + 1 && d.GridMb.DungeonGridType == DungeonBattleGridType.Recovery);
                    // 然后选择复活节点
                    nextGrid ??= grids.FirstOrDefault(d => d.Grid.Y == currentGrid.Grid.Y + 1 && d.GridMb.DungeonGridType == DungeonBattleGridType.Revival);
                    // 然后其他
                    nextGrid ??= grids.FirstOrDefault(d => d.Grid.Y == currentGrid.Grid.Y + 1);

                    if (nextGrid == null)
                    {
                        if (!battleInfoResponse.UserDungeonDtoInfo.IsDoneRewardClearLayer(battleInfoResponse.CurrentDungeonBattleLayer.LayerCount))
                        {
                            // 获取当前层奖励
                            var rewardClearLayerResponse = await GetResponse<RewardClearLayerRequest, RewardClearLayerResponse>(new RewardClearLayerRequest()
                            {
                                ClearedLayer = battleInfoResponse.CurrentDungeonBattleLayer.LayerCount,
                                CurrentTermId = battleInfoResponse.CurrentTermId,
                                DungeonBattleDifficultyType = battleInfoResponse.CurrentDungeonBattleLayer
                                    .DungeonDifficultyType
                            });
                        }

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
                                    DungeonDifficultyType = diff
                                });
                        }
                    }
                    else
                    {
                        switch (nextGrid.GridMb.DungeonGridType)
                        {
                            case DungeonBattleGridType.JoinCharacter:
                            {
                                var infos = battleInfoResponse.UserDungeonBattleGuestCharacterDtoInfos.OrderByDescending(d => d.BattlePower).ToList();
                                foreach (var info in infos)
                                    try
                                    {
                                        var execGuestResponse = await GetResponse<ExecGuestRequest, ExecGuestResponse>(new ExecGuestRequest()
                                        {
                                            DungeonGridGuid = nextGrid.Grid.DungeonGridGuid,
                                            GuestMBId = info.CharacterId,
                                            CurrentTermId = battleInfoResponse.CurrentTermId
                                        });
                                        break;
                                    }
                                    catch (ApiErrorException e)
                                    {
                                        log(string.Format(ResourceStrings.CaveErrorSelectSupport, e.Message));
                                    }

                                break;
                            }
                            default:
                            {
                                var selectGridResponse = await GetResponse<SelectGridRequest, SelectGridResponse>(new SelectGridRequest()
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
                            var tradeShopItemList = battleInfoResponse.UserDungeonBattleShopDtoInfos.Find(d => d.GridGuid == currentGrid.Grid.DungeonGridGuid).TradeShopItemList;
                            if (GameConfig.DungeonBattle.ShopTargetItems.Count > 0)
                                foreach (var tradeShopItem in tradeShopItemList)
                                    if (GameConfig.DungeonBattle.ShopTargetItems.Any(d => d.ItemType == tradeShopItem.GiveItem.ItemType && d.ItemId == tradeShopItem.GiveItem.ItemId))
                                    {
                                        // 购买
                                        var execShopResponse = await GetResponse<ExecShopRequest, ExecShopResponse>(new ExecShopRequest
                                        {
                                            CurrentTermId = battleInfoResponse.CurrentTermId,
                                            DungeonGridGuid = currentGrid.Grid.DungeonGridGuid,
                                            TradeShopItemId = tradeShopItem.TradeShopItemId
                                        });
                                        log(string.Format(ResourceStrings.CaveBuyItemSuccess, ItemUtil.GetItemName(tradeShopItem.GiveItem), tradeShopItem.GiveItem.ItemCount));
                                    }

                            var leaveShopResponse = await GetResponse<LeaveShopRequest, LeaveShopResponse>(
                                new LeaveShopRequest()
                                {
                                    CurrentTermId = battleInfoResponse.CurrentTermId,
                                    DungeonGridGuid = currentGrid.Grid.DungeonGridGuid
                                });
                            break;
                        case DungeonBattleGridType.RelicReinforce:
                            var execReinforceRelicResponse =
                                await GetResponse<ExecReinforceRelicRequest, ExecReinforceRelicResponse>(
                                    new ExecReinforceRelicRequest()
                                    {
                                        CurrentTermId = battleInfoResponse.CurrentTermId,
                                        DungeonGridGuid = currentGrid.Grid.DungeonGridGuid
                                    });
                            break;
                        case DungeonBattleGridType.BattleAndRelicReinforce:
                        {
                            await DoBattle();
                            await RewardBattleReinforceRelic(battleInfoResponse.UserDungeonDtoInfo.RelicIds, battleInfoResponse.UserDungeonDtoInfo.RelicIds);
                           
                            break;
                        }
                        case DungeonBattleGridType.TreasureChest:
                            await DoBattle();
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
                        await RewardBattleReinforceRelic(battleInfoResponse.UserDungeonDtoInfo.RelicIds, battleInfoResponse.UserDungeonDtoInfo.RelicIds);
                    }
                    else
                    {
                        // 选择加成奖励
                        var relicId = 0L;

                        foreach (var info in GameConfig.DungeonBattleRelicSort)
                            if (battleInfoResponse.RewardRelicIds.Contains(info.Id))
                            {
                                relicId = info.Id;
                                break;
                            }

                        var rewardBattleReceiveRelicResponse =
                            await GetResponse<RewardBattleReceiveRelicRequest, RewardBattleReceiveRelicResponse>(
                                new RewardBattleReceiveRelicRequest()
                                {
                                    CurrentTermId = battleInfoResponse.CurrentTermId,
                                    DungeonGridGuid = currentGrid.Grid.DungeonGridGuid,
                                    SelectedRelicId = relicId
                                });
                    }

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            async Task RewardBattleReinforceRelic(List<long> newRelicIds,List<long> currentRelicIds)
            {
                // 选择加成奖励
                var relicId = 0L;
                var upgradableRelics = newRelicIds.Where(d =>
                {
                    var mb = Masters.DungeonBattleRelicTable.GetByReinforceFrom(d);
                    if (mb == null)
                        // 不可升级
                        return false;

                    if (currentRelicIds.Contains(mb.Id))
                        // 升级后的已经存在了
                        return false;

                    return true;
                }).ToList();
                foreach (var info in GameConfig.DungeonBattleRelicSort)
                    if (upgradableRelics.Contains(info.Id))
                    {
                        relicId = info.Id;
                        try
                        {
                            var rewardBattleReceiveRelicResponse = await GetResponse<RewardBattleReinforceRelicRequest, RewardBattleReinforceRelicResponse>(
                                new RewardBattleReinforceRelicRequest()
                                {
                                    CurrentTermId = battleInfoResponse.CurrentTermId,
                                    DungeonGridGuid = currentGrid.Grid.DungeonGridGuid,
                                    SelectedRelicId = relicId
                                });
                            break;
                        }
                        catch (ApiErrorException e) when (e.ErrorCode == ErrorCode.DungeonBattleAlreadyHaveRelic)
                        {
                            log($"{Masters.TextResourceTable.GetErrorCodeMessage(e.ErrorCode)}, {ResourceStrings.CaveErrorRelicExist}");
                        }
                    }
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

    private async Task<bool> IsValidMonthlyBoost()
    {
        return UserSyncData.UserShopMonthlyBoostDtoInfos.Exists(d => d.ExpirationTimeStamp > DateTimeOffset.Now.ToUnixTimeMilliseconds());
        // return false;
    }

    public async Task<TResp> GetResponse<TReq, TResp>(TReq req)
        where TReq : ApiRequestBase
        where TResp : ApiResponseBase
    {
        return await _networkManager.GetResponse<TReq, TResp>(req, AddLog, data =>
        {
            UserSyncData.UserItemEditorMergeUserSyncData(data);
            this.RaisePropertyChanged(nameof(UserSyncData));
        });
    }
}