using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using MementoMori.Exceptions;
using MementoMori.Extensions;
using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Custom;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest;
using MementoMori.Ortega.Share.Data.ApiInterface.Character;
using MementoMori.Ortega.Share.Data.ApiInterface.Equipment;
using MementoMori.Ortega.Share.Data.ApiInterface.Friend;
using MementoMori.Ortega.Share.Data.ApiInterface.Gacha;
using MementoMori.Ortega.Share.Data.ApiInterface.Guild;
using MementoMori.Ortega.Share.Data.ApiInterface.GuildRaid;
using MementoMori.Ortega.Share.Data.ApiInterface.Item;
using MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus;
using MementoMori.Ortega.Share.Data.ApiInterface.Mission;
using MementoMori.Ortega.Share.Data.ApiInterface.Notice;
using MementoMori.Ortega.Share.Data.ApiInterface.Present;
using MementoMori.Ortega.Share.Data.ApiInterface.Quest;
using MementoMori.Ortega.Share.Data.ApiInterface.TowerBattle;
using MementoMori.Ortega.Share.Data.ApiInterface.User;
using MementoMori.Ortega.Share.Data.ApiInterface.Vip;
using MementoMori.Ortega.Share.Data.BountyQuest;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Data.Gacha;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Data.Item.Model;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Utils;
using MoreLinq.Extensions;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using TowerBattleStartRequest = MementoMori.Ortega.Share.Data.ApiInterface.TowerBattle.StartRequest;
using TowerBattleStartResponse = MementoMori.Ortega.Share.Data.ApiInterface.TowerBattle.StartResponse;
using BountyQuestStartRequest = MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest.StartRequest;
using BountyQuestStartResponse = MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest.StartResponse;
using BountyQuestGetListRequest = MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest.GetListRequest;
using BountyQuestGetListResponse = MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest.GetListResponse;
using GachaGetListRequest = MementoMori.Ortega.Share.Data.ApiInterface.Gacha.GetListRequest;
using GachaGetListResponse = MementoMori.Ortega.Share.Data.ApiInterface.Gacha.GetListResponse;
using PresentGetListRequest = MementoMori.Ortega.Share.Data.ApiInterface.Present.GetListRequest;
using PresentGetListResponse = MementoMori.Ortega.Share.Data.ApiInterface.Present.GetListResponse;
using ShopGetListRequest = MementoMori.Ortega.Share.Data.ApiInterface.Shop.GetListRequest;
using ShopGetListResponse = MementoMori.Ortega.Share.Data.ApiInterface.Shop.GetListResponse;
using TradeShopGetListRequest = MementoMori.Ortega.Share.Data.ApiInterface.TradeShop.GetListRequest;
using TradeShopGetListResponse = MementoMori.Ortega.Share.Data.ApiInterface.TradeShop.GetListResponse;
using System.Xml.Linq;
using MementoMori.Common.Localization;
using MementoMori.MagicOnion;
using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Network.MagicOnion.Client;
using MementoMori.Ortega.Share.Data.ApiInterface.GlobalGvg;
using MementoMori.Ortega.Share.Data.ApiInterface.LocalGvg;
using MementoMori.Ortega.Share.Data.ApiInterface.LocalRaid;
using MementoMori.Ortega.Share.Data.ApiInterface.Shop;
using MementoMori.Ortega.Share.Data.ApiInterface.TradeShop;
using MementoMori.Ortega.Share.Data.Auth;
using MementoMori.Ortega.Share.Data.TradeShop;
using static MementoMori.Ortega.Share.Masters;
using DynamicData;
using MementoMori.Ortega.Share.Data.Battle;

namespace MementoMori;

public partial class MementoMoriFuncs : ReactiveObject
{
    [Reactive]
    public bool Logining { get; private set; }

    [Reactive]
    public bool IsQuickActionExecuting { get; private set; }

    [Reactive]
    public string EquipmentId { get; set; }

    [Reactive]
    public string EquipmentTrainingTargetType { get; set; }

    [Reactive]
    public long EquipmentTrainingTargetValue { get; set; }

    [Reactive]
    public TowerType SelectedAutoTowerType { get; set; }

    [Reactive]
    public bool ShowDebugInfo { get; set; }

    [Reactive]
    public bool BountyRequestForceAll { get; set; }

    [Reactive]
    public bool LoginOk { get; set; }

    private CancellationTokenSource _cancellationTokenSource;

    public ObservableCollection<string> MesssageList { get; } = new();

    private const int Max_Err_Count = 20;

    private PlayerDataInfo _lastPlayerDataInfo;

    public long UserId { get; set; }

    public async Task Login(PlayerDataInfo playerDataInfo = null)
    {
        Logining = true;
        try
        {
            if (playerDataInfo == null) playerDataInfo = _lastPlayerDataInfo;
            if (playerDataInfo == null) throw new Exception("playerDataInfo is null");
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

    public async Task AutoLogin(bool manual = false)
    {
        if (!manual && !_accountManager.GetAccountInfo(UserId).AutoLogin) return;
        AddLog(ResourceStrings.AutoLoginonStartup);
        var playerDataInfos = await GetPlayerDataInfo();
        var playerDataInfo = Enumerable.MaxBy(playerDataInfos, d => d.LastLoginTime);
        if (playerDataInfo == null) return;
        await Login(playerDataInfo);
    }

    public async Task Logout()
    {
        await _timeZoneAwareJobRegister.DeregisterJobs(UserId);
        LoginOk = false;
    }

    public async Task SyncUserData()
    {
        await UserGetUserData();
    }

    private void AddLog(string message)
    {
        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{_lastPlayerDataInfo?.Name}(Lv{_lastPlayerDataInfo?.PlayerRank})] {message}");
        lock (MesssageList)
        {
            MesssageList.Insert(0, message);
            if (MesssageList.Count > 100) MesssageList.RemoveAt(MesssageList.Count - 1);
        }
    }

    private ConcurrentQueue<Func<Action<string>, CancellationToken, Task>> _funcs = new();

    private List<Task> _tasks = new();

    public async Task ExecuteQuickAction(Func<Action<string>, CancellationToken, Task> func)
    {
        if (!IsQuickActionExecuting)
        {
            IsQuickActionExecuting = true;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        var task = func(AddLog, _cancellationTokenSource.Token);
        _tasks.Add(task);
        _ = task.ContinueWith(t =>
        {
            if (_tasks.TrueForAll(d => d.IsCompleted)) IsQuickActionExecuting = false;
        });
        try
        {
            await task;
        }
        catch (Exception e)
        {
            AddLog(e.Message);
        }
    }

    public void CancelQuickAction()
    {
        if (_cancellationTokenSource == null) return;
        _cancellationTokenSource.Cancel();
    }

    public async Task GetLoginBonus()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            await GetMonthlyLoginBonusInfo();
            var day = DateTimeOffset.Now.ToOffset(TimeSpan.FromHours(1)).Day;
            if (!MonthlyLoginBonusInfo.ReceivedDailyRewardDayList.Contains(day))
            {
                if (TimeManager.ServerNow.Hour >= 4)
                {
                    var bonus = await GetResponse<ReceiveDailyLoginBonusRequest, ReceiveDailyLoginBonusResponse>(new ReceiveDailyLoginBonusRequest() {ReceiveDay = TimeManager.ServerNow.Day});
                    log($"{TextResourceTable.Get("[MyPageButtonLoginBonusLabel]")}:\n");
                    bonus.RewardItemList.PrintUserItems(log);
                }

                await GetMonthlyLoginBonusInfo();
            }
            else
            {
                log(TextResourceTable.GetErrorCodeMessage(ErrorCode.LoginBonusAlreadyReceivedDailyReward));
            }

            var monthlyLoginBonusMb = MonthlyLoginBonusTable.GetById(MonthlyLoginBonusInfo.MonthlyLoginBonusId);
            var monthlyLoginBonusRewardListMb = MonthlyLoginBonusRewardListTable.GetById(monthlyLoginBonusMb.RewardListId);
            foreach (var loginCountRewardInfo in monthlyLoginBonusRewardListMb.LoginCountRewardList)
                // 登录次数达到 && 没有领取过
                if (loginCountRewardInfo.DayCount <= MonthlyLoginBonusInfo.ReceivedDailyRewardDayList.Count &&
                    !MonthlyLoginBonusInfo.ReceivedLoginCountRewardDayList.Contains(loginCountRewardInfo.DayCount))
                {
                    var resp = await GetResponse<ReceiveLoginCountBonusRequest, ReceiveLoginCountBonusResponse>(new ReceiveLoginCountBonusRequest
                    {
                        ReceiveDayCount = loginCountRewardInfo.DayCount
                    });
                    log($"{TextResourceTable.Get("[LoginBonusCountFormat]", loginCountRewardInfo.DayCount, 30)}");
                    resp.RewardItemList.PrintUserItems(log);
                }

            await GetMonthlyLoginBonusInfo();

            var iconInfo = Mypage.MypageInfo.MypageIconInfos.FirstOrDefault(d => d.TransferDetailInfo.TransferSpotType == TransferSpotType.LimitedLoginBonus);
            if (iconInfo != null)
            {
                var limitedLoginBonusId = iconInfo.TransferDetailInfo.NumberInfo1;
                var limitedLoginBonusInfoResponse = await GetResponse<GetLimitedLoginBonusInfoRequest, GetLimitedLoginBonusInfoResponse>(new GetLimitedLoginBonusInfoRequest
                {
                    LimitedLoginBonusId = limitedLoginBonusId
                });

                var limitedLoginBonusMb = LimitedLoginBonusTable.GetById(limitedLoginBonusId);
                var name = TextResourceTable.Get(limitedLoginBonusMb.TitleTextKey);
                var loginBonusRewardListMb = LimitedLoginBonusRewardListTable.GetById(limitedLoginBonusId);
                foreach (var dailyLimitedLoginBonusItem in loginBonusRewardListMb.DailyRewardList)
                {
                    if (limitedLoginBonusInfoResponse.ReceivedDateList.Contains(dailyLimitedLoginBonusItem.Date)) continue;
                    if (limitedLoginBonusInfoResponse.TotalLoginCount < dailyLimitedLoginBonusItem.Date) continue;
                    log(name);
                    var resp = await GetResponse<ReceiveDailyLimitedLoginBonusRequest, ReceiveDailyLimitedLoginBonusResponse>(new ReceiveDailyLimitedLoginBonusRequest
                    {
                        LimitedLoginBonusId = limitedLoginBonusId, ReceiveDate = dailyLimitedLoginBonusItem.Date
                    });
                    resp.RewardItemList.PrintUserItems(log);
                }

                if (loginBonusRewardListMb.ExistSpecialReward && limitedLoginBonusInfoResponse.TotalLoginCount >= loginBonusRewardListMb.SpecialRewardItem.Date &&
                    !limitedLoginBonusInfoResponse.IsReceivedSpecialReward)
                {
                    log(name);
                    var response = await GetResponse<ReceiveSpecialLimitedLoginBonusRequest, ReceiveSpecialLimitedLoginBonusResponse>(new ReceiveSpecialLimitedLoginBonusRequest()
                    {
                        LimitedLoginBonusId = limitedLoginBonusId
                    });
                    response.RewardItemList.PrintUserItems(log);
                }
            }
        });
    }

    public async Task GetVipGift()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            if (UserSyncData.ExistVipDailyGift == true)
            {
                var bonus = await GetResponse<GetDailyGiftRequest, GetDailyGiftResponse>(new GetDailyGiftRequest());
                log($"{TextResourceTable.Get("[VipDailyRewardLabelFormat]", UserSyncData.UserStatusDtoInfo.Vip)}\n");
                bonus.ItemList.PrintUserItems(log);
            }
            else
            {
                log($"{TextResourceTable.GetErrorCodeMessage(ErrorCode.VipGetDailyGiftAlreadyGet)}");
            }
        });
    }

    public async Task ReceiveMonthlyBoost()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var listResponse = await GetResponse<ShopGetListRequest, ShopGetListResponse>(new ShopGetListRequest());
            var shopProductInfo = listResponse.ShopTabInfoList.SelectMany(d => d.ShopProductInfoList).FirstOrDefault(d => d.ShopProductType == ShopProductType.MonthlyBoost);
            if (shopProductInfo != null && shopProductInfo.ShopProductMonthlyBoost.ExpirationTimeStamp >= DateTimeOffset.Now.ToUnixTimeMilliseconds())
            {
                if (shopProductInfo.ShopProductMonthlyBoost.IsAlreadyReceive)
                {
                    log($"{TextResourceTable.Get("[CommonMonthlyBoosterLabel]")} {TextResourceTable.Get("[ShopMonthlyBoostRewardDetailReceivedMessage]")}");
                }
                else
                {
                    var receiveRewardResponse = await GetResponse<ReceiveRewardRequest, ReceiveRewardResponse>(new ReceiveRewardRequest
                        {MBId = shopProductInfo.MbId, ProductId = shopProductInfo.ShopProductMonthlyBoost.ProductId, ShopProductType = ShopProductType.MonthlyBoost});
                    log($"{TextResourceTable.Get("[CommonMonthlyBoosterLabel]")} {TextResourceTable.Get("[ShopMonthlyBoostRewardDetailReceivedMessage]")}");
                    receiveRewardResponse.RewardInfo.ItemList.PrintUserItems(log);
                    receiveRewardResponse.RewardInfo.BonusItemList.PrintUserItems(log);
                    receiveRewardResponse.RewardInfo.CharacterList.PrintCharacterDtos(log);
                }
            }
        });
    }

    public async Task GetAutoBattleReward()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var mapInfoResponse = await GetResponse<MapInfoRequest, MapInfoResponse>(new MapInfoRequest {IsUpdateOtherPlayerInfo = true});
            var autoResponse = await GetResponse<AutoRequest, AutoResponse>(new AutoRequest());
            var bonus = await GetResponse<RewardAutoBattleRequest, RewardAutoBattleResponse>(new RewardAutoBattleRequest());
            log(TextResourceTable.Get("[AutoBattleRewardInfoTitle]"));
            log($"{TextResourceTable.Get("[CommonWinLabel]")} {bonus.AutoBattleRewardResult.BattleCountWin}");
            log($"{TextResourceTable.Get("[CommonLoseLabel]")} {bonus.AutoBattleRewardResult.BattleCountAll - bonus.AutoBattleRewardResult.BattleCountWin}");
            log($"{TextResourceTable.Get("[AutoBattleRewardInfoBattleTotalTimeLabel]")} {TimeSpan.FromMilliseconds(bonus.AutoBattleRewardResult.BattleTotalTime)}");
            log(TextResourceTable.Get("[AutoBattleRewardInfoPopulationLabel]"));
            log($"{TextResourceTable.Get("[ItemName5]")} {bonus.AutoBattleRewardResult.GoldByPopulation}");
            log($"{TextResourceTable.Get("[ItemName11]")} {bonus.AutoBattleRewardResult.PotentialJewelByPopulation}");
            log(TextResourceTable.Get("[AutoBattleRewardInfoRewardLabel]"));
            bonus.AutoBattleRewardResult.BattleRewardResult.FixedItemList.PrintUserItems(log);
            bonus.AutoBattleRewardResult.BattleRewardResult.DropItemList.PrintUserItems(log);
        });
    }

    public async Task BulkTransferFriendPoint()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            try
            {
                var resp = await GetResponse<BulkTransferFriendPointRequest, BulkTransferFriendPointResponse>(new BulkTransferFriendPointRequest());
                log($"{TextResourceTable.Get("[ItemName9]")} {TextResourceTable.Get("[BulkReceiveAndSend]")} {ResourceStrings.Finished}");
            }
            catch (ApiErrorException e) when (e.ErrorCode == ErrorCode.FriendAlreadyMaxReceived)
            {
                log(e.Message);
            }
        });
    }

    public async Task PresentReceiveItem()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var usedItem = false;
            do
            {
                var getListResponse = await GetResponse<PresentGetListRequest, PresentGetListResponse>(new PresentGetListRequest {LanguageType = LanguageType.zhTW});
                if (getListResponse.userPresentDtoInfos.Any(d => !d.IsReceived))
                    try
                    {
                        var resp = await GetResponse<ReceiveItemRequest, ReceiveItemResponse>(new ReceiveItemRequest() {LanguageType = LanguageType.zhTW});
                        usedItem = false;
                        log($"{TextResourceTable.Get("[MyPagePresentBoxButtonTitle]")} {TextResourceTable.Get("[MyPagePresentBoxButtonAllReceive]")}");
                        resp.ResultItems.Select(d => d.Item).PrintUserItems(log);
                    }
                    catch (ApiErrorException e) when (e.ErrorCode == ErrorCode.PresentReceiveOverLimitCountPresent)
                    {
                        log(e.Message);
                        foreach (var presentItem in getListResponse.userPresentDtoInfos.SelectMany(d => d.ItemList).GroupBy(d => new {d.Item.ItemType, d.Item.ItemId}))
                        {
                            if (presentItem.Key.ItemType == ItemType.QuestQuickTicket)
                            {
                                var count = UserSyncData.UserItemDtoInfo.FirstOrDefault(d => d.ItemType == presentItem.Key.ItemType && d.ItemId == presentItem.Key.ItemId)?.ItemCount ?? 0;
                                var itemMb = ItemTable.GetByItemTypeAndItemId(presentItem.Key.ItemType, presentItem.Key.ItemId);
                                var maxItemCount = itemMb.MaxItemCount;
                                if (count < maxItemCount) continue;

                                var name = TextResourceTable.Get(itemMb.NameKey);
                                var useCount = (int) Math.Floor(maxItemCount * 0.1);
                                log($"{ResourceStrings.UseOverLimitItem}: {name}×{useCount}, {count}/{maxItemCount}");
                                var response = await GetResponse<UseAutoBattleRewardItemRequest, UseAutoBattleRewardItemResponse>(new UseAutoBattleRewardItemRequest()
                                {
                                    ItemType = (QuestQuickTicketType) itemMb.ItemType,
                                    UseCount = useCount
                                });
                                response.RewardItemList.PrintUserItems(log);
                                usedItem = true;
                                break;
                            }

                            if (presentItem.Key.ItemType == ItemType.Equipment)
                            {
                                var count = UserSyncData.UserItemDtoInfo.FirstOrDefault(d => d.ItemType == presentItem.Key.ItemType && d.ItemId == presentItem.Key.ItemId)?.ItemCount ?? 0;
                                var maxItemCount = 999;
                                if (count < maxItemCount) continue;

                                var name = TextResourceTable.Get(EquipmentTable.GetById(presentItem.Key.ItemId).NameKey);
                                var useCount = (int) Math.Floor(maxItemCount * 0.1);
                                log($"{ResourceStrings.UseOverLimitItem}: {name}×{useCount}, {count}/{maxItemCount}");
                                var response = await GetResponse<CastRequest, CastResponse>(new CastRequest {UserEquipment = new UserEquipment(presentItem.Key.ItemId, useCount)});
                                response.ResultItemList.PrintUserItems(log);
                                usedItem = true;
                                break;
                            }
                        }
                    }
                else
                    log(TextResourceTable.GetErrorCodeMessage(ErrorCode.PresentReceiveAlreadyReceivedPresent));
            } while (usedItem);
        });
    }

    public async Task BattleBossQuick()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            try
            {
                if (IsBossBattleQuickAvailable)
                {
                    var bossQuickResponse = await GetResponse<BossQuickRequest, BossQuickResponse>(
                        new BossQuickRequest()
                        {
                            QuestId = UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId,
                            QuickCount = 3
                        });
                    if (bossQuickResponse.BattleRewardResult == null) return;

                    log($"{TextResourceTable.Get("[AutoBattleButtonQuickForward]")}:\n");
                    bossQuickResponse.BattleRewardResult.FixedItemList.PrintUserItems(log);
                    bossQuickResponse.BattleRewardResult.DropItemList.PrintUserItems(log);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        var bossQuickResponse = await GetResponse<BossRequest, BossResponse>(
                            new BossRequest()
                            {
                                QuestId = UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId
                            });
                        if (bossQuickResponse.BattleRewardResult == null) return;

                        log($"{TextResourceTable.Get("[AutoBattleButtonQuickForward]")}:\n");
                        bossQuickResponse.BattleRewardResult.FixedItemList.PrintUserItems(log);
                        bossQuickResponse.BattleRewardResult.DropItemList.PrintUserItems(log);
                    }
                }
            }
            catch (ApiErrorException e) when (e.ErrorCode == ErrorCode.BattleBossNotEnoughBossChallengeCount)
            {
                log(e.Message);
            }
        });
    }

    private bool IsBossBattleQuickAvailable
    {
        get
        {
            var vip = VipTable.GetByLevel(UserSyncData.UserStatusDtoInfo.Vip);
            var isQuickAvailable = vip.IsQuickBossBattleAvailable;
            if (!isQuickAvailable)
            {
                isQuickAvailable = UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId >= OpenContentTable.GetByOpenCommandType(OpenCommandType.BossBattleQuick).OpenContentValue;
            }

            return isQuickAvailable;
        }
    }

    private bool IsGuildRaidQuickAvailable
    {
        get
        {
            var vip = VipTable.GetByLevel(UserSyncData.UserStatusDtoInfo.Vip);
            var isQuickAvailable = vip.IsQuickStartGuildRaidAvailable;
            if (!isQuickAvailable)
            {
                isQuickAvailable = UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId >= OpenContentTable.GetByOpenCommandType(OpenCommandType.GuildRaidQuick).OpenContentValue;
            }

            return isQuickAvailable;
        }
    }

    public async Task InfiniteTowerQuick()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            try
            {
                var tower = UserSyncData.UserTowerBattleDtoInfos.First(d => d.TowerType == TowerType.Infinite);
                log($"{TextResourceTable.Get("[CommonHeaderTowerBattleLabel]")}:\n");

                if (IsBossBattleQuickAvailable)
                {
                    var bossQuickResponse = await GetResponse<TowerBattleQuickRequest, TowerBattleQuickResponse>(
                        new TowerBattleQuickRequest()
                        {
                            TargetTowerType = TowerType.Infinite, TowerBattleQuestId = tower.MaxTowerBattleId, QuickCount = 3
                        });
                    if (bossQuickResponse.BattleRewardResult != null)
                    {
                        bossQuickResponse.BattleRewardResult.FixedItemList.PrintUserItems(log);
                        bossQuickResponse.BattleRewardResult.DropItemList.PrintUserItems(log);
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        var bossQuickResponse = await GetResponse<TowerBattleStartRequest, TowerBattleStartResponse>(
                            new TowerBattleStartRequest()
                            {
                                TargetTowerType = TowerType.Infinite, TowerBattleQuestId = tower.MaxTowerBattleId
                            });
                        if (bossQuickResponse.BattleRewardResult != null)
                        {
                            bossQuickResponse.BattleRewardResult.FixedItemList.PrintUserItems(log);
                            bossQuickResponse.BattleRewardResult.DropItemList.PrintUserItems(log);
                        }
                    }
                }
            }
            catch (ApiErrorException e) when (e.ErrorCode == ErrorCode.TowerBattleNotEnoughChallengeCount)
            {
                log(e.Message);
            }
        });
    }

    public async Task PvpAuto()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            log($"{TextResourceTable.Get("[CommonHeaderLocalPvpLabel]")}:\n");
            var count = 100;
            while (!token.IsCancellationRequested && count-- > 0)
            {
                try
                {
                    var pvpInfoResponse = await GetResponse<GetPvpInfoRequest, GetPvpInfoResponse>(
                        new GetPvpInfoRequest());

                    if (UserSyncData.UserBattlePvpDtoInfo.PvpTodayCount >= OrtegaConst.BattlePvp.MaxPvpBattleFreeCount)
                    {
                        log(TextResourceTable.GetErrorCodeMessage(ErrorCode.BattlePvpOverLegendLeagueChallengeMaxCount));
                        return;
                    }

                    List<(long PlayerId, long DefenseBattlePower, List<(long characterId, Func<Task<BattleParameter>> battleParameter)> characterDetailInfos)> list = new();

                    var characterDetailInfoDict = new Dictionary<long, List<CharacterDetailInfo>>();

                    foreach (var d in pvpInfoResponse.MatchingRivalList)
                    {
                        List<(long characterId, Func<Task<BattleParameter>> battleParameter)> characterDetailInfos = new();
                        for (var i1 = 0; i1 < d.UserCharacterInfoList.Count; i1++)
                        {
                            var i2 = i1;
                            characterDetailInfos.Add((d.UserCharacterInfoList[i1].CharacterId, async () =>
                                    {
                                        if (!characterDetailInfoDict.TryGetValue(d.PlayerInfo.PlayerId, out var details))
                                        {
                                            details = (await GetResponse<GetDetailsInfoRequest, GetDetailsInfoResponse>(new GetDetailsInfoRequest()
                                            {
                                                DeckType = DeckUseContentType.BattleLeagueDefense, TargetPlayerId = d.PlayerInfo.PlayerId,
                                                TargetUserCharacterGuids = d.UserCharacterInfoList.Select(x => x.Guid).ToList()
                                            })).CharacterDetailInfos;
                                            characterDetailInfoDict.Add(d.PlayerInfo.PlayerId, details);
                                        }

                                        return details[i2].BattleParameter;
                                    }
                                ));
                        }

                        list.Add((d.PlayerInfo.PlayerId, d.DefenseBattlePower, characterDetailInfos));
                    }

                    var targetPlayerId = await SelectLeagueTarget(log, PlayerOption.BattleLeague, list);
                    if (targetPlayerId == 0)
                    {
                        continue;
                    }

                    var playerInfo = pvpInfoResponse.MatchingRivalList.FirstOrDefault(d => d.PlayerInfo.PlayerId == targetPlayerId);
                    if (playerInfo == null)
                    {
                        continue;
                    }

                    var pvpStartResponse = await GetResponse<PvpStartRequest, PvpStartResponse>(new PvpStartRequest()
                    {
                        RivalPlayerRank = playerInfo.CurrentRank,
                        RivalPlayerId = playerInfo.PlayerInfo.PlayerId
                    });

                    pvpStartResponse.BattleRewardResult.FixedItemList.PrintUserItems(log);
                    pvpStartResponse.BattleRewardResult.DropItemList.PrintUserItems(log);
                }
                catch (ApiErrorException e)
                {
                    log(e.Message);
                    break;
                }
            }
        });
    }

    private async Task<long> SelectLeagueTarget(Action<string> log, PvpOption pvpOption,
        List<(long playerId, long defenseBattlePower, List<(long characterId, Func<Task<BattleParameter>> battleParameter)> chareacters)> playerInfoList)
    {
        // playerInfoList: dict<playerId, dict<characterId, CharacterDetailInfo>>
        playerInfoList = playerInfoList.ToList();
        foreach (var characterFilter in pvpOption.CharacterFilters)
        {
            if (playerInfoList.Count == 0)
            {
                break;
            }

            switch (characterFilter.FilterStrategy)
            {
                case CharacterFilterStrategy.Character:
                    foreach (var (playerId, _, _) in playerInfoList.Where(d => d.chareacters.Any(x => x.characterId == characterFilter.CharacterId)))
                    {
                        playerInfoList.RemoveAll(x => x.playerId == playerId);
                    }

                    break;
                case CharacterFilterStrategy.PropertyMoreThanSelf:
                    foreach (var (playerId, _, characterDetailInfos) in playerInfoList)
                    {
                        var (characterId, battleParameter) = characterDetailInfos.FirstOrDefault(d => d.characterId == characterFilter.CharacterId);
                        if (characterId == 0) continue;
                        var targetParameterValue = (await battleParameter()).GetParameter(characterFilter.BattleParameterType);
                        var selfCharacterGuid = UserSyncData.UserCharacterDtoInfos.FirstOrDefault(d => d.CharacterId == characterId)?.Guid;
                        if (selfCharacterGuid == null) continue;
                        var lockType = UserSyncData.ReleaseLockEquipmentCooldownTimeStampMap.ContainsKey(LockEquipmentDeckType.League) ? LockEquipmentDeckType.League : LockEquipmentDeckType.None;
                        var selfParameterValue = BattlePowerCalculatorUtil.CalcCharacterBattleParameter(UserId, selfCharacterGuid, lockType).Item2.GetParameter(characterFilter.BattleParameterType);
                        if (targetParameterValue > selfParameterValue) playerInfoList.RemoveAll(x => x.playerId == playerId);
                    }

                    break;
            }
        }

        if (playerInfoList.Count == 0)
        {
            return 0;
        }

        switch (pvpOption.SelectStrategy)
        {
            case TargetSelectStrategy.Random:
                return Enumerable.MinBy(playerInfoList, d => Guid.NewGuid()).playerId;
            case TargetSelectStrategy.LowestBattlePower:
                return Enumerable.MinBy(playerInfoList, d => playerInfoList.Min(x => x.defenseBattlePower)).playerId;
            case TargetSelectStrategy.HighestBattlePower:
                return Enumerable.MaxBy(playerInfoList, d => playerInfoList.Max(x => x.defenseBattlePower)).playerId;
            default:
                return Enumerable.MinBy(playerInfoList, d => Guid.NewGuid()).playerId;
        }
    }

    public async Task LegendLeagueAuto()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            log($"{TextResourceTable.Get("[CommonHeaderGlobalPvpLabel]")}");
            if (UserSyncData.CanJoinTodayLegendLeague != true)
            {
                log(TextResourceTable.Get("[GlobalPvpIsNotParticipateToastMessage]"));
                return;
            }

            var count = 100;
            while (!token.IsCancellationRequested && count-- > 0)
            {
                try
                {
                    var leagueInfoResponse = await GetResponse<GetLegendLeagueInfoRequest, GetLegendLeagueInfoResponse>(new GetLegendLeagueInfoRequest());

                    if (UserSyncData.UserBattleLegendLeagueDtoInfo != null && UserSyncData.UserBattleLegendLeagueDtoInfo.LegendLeagueTodayCount >= OrtegaConst.BattlePvp.MaxLegendLeagueBattleFreeCount)
                    {
                        log(TextResourceTable.GetErrorCodeMessage(ErrorCode.BattlePvpOverLegendLeagueChallengeMaxCount));
                        return;
                    }

                    var list = new List<(long playerId, long defenseBattlePower, List<(long characterId, Func<Task<BattleParameter>> battleParameter)> chareacters)>();
                    foreach (var playerInfo in leagueInfoResponse.MatchingRivalList)
                    {
                        var characterDetailInfos = new List<(long characterId, Func<Task<BattleParameter>> battleParameter)>();
                        foreach (var dtoInfo in playerInfo.UserCharacterDtoInfoList)
                        {
                            characterDetailInfos.Add((dtoInfo.CharacterId, () => Task.FromResult(playerInfo.DefenseCharacterBattleParameterMap[dtoInfo.Guid])));
                        }

                        list.Add((playerInfo.PlayerInfo.PlayerId, playerInfo.DefenseBattlePower, characterDetailInfos));
                    }

                    var targetPlayerId = await SelectLeagueTarget(log, PlayerOption.LegendLeague, list);
                    if (targetPlayerId == 0)
                    {
                        continue;
                    }

                    var leagueStartResponse = await GetResponse<LegendLeagueStartRequest, LegendLeagueStartResponse>(new LegendLeagueStartRequest()
                    {
                        RivalPlayerId = targetPlayerId
                    });

                    log(TextResourceTable.Get("[GlobalPvpChangePointFormat]", leagueStartResponse.GetPoint));
                }
                catch (ApiErrorException e)
                {
                    log(e.Message);
                    break;
                }
            }
        });
    }

    public async Task BountyQuestRewardAuto()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            if (!IsBountyQuestAvailable)
            {
                return;
            }

            log($"{TextResourceTable.Get("[CommonHeaderBountyQuestLabel]")}:\n");
            var getListResponse = await GetResponse<BountyQuestGetListRequest, BountyQuestGetListResponse>(
                new BountyQuestGetListRequest());

            var questIds = getListResponse.UserBountyQuestDtoInfos
                .Where(d => d.BountyQuestEndTime > 0 && !d.IsReward && DateTimeOffset.Now.Add(TimeManager.DiffFromUtc).ToUnixTimeMilliseconds() > d.BountyQuestEndTime)
                .Select(d => d.BountyQuestId).ToList();

            if (questIds.Count > 0)
            {
                var rewardResponse = await GetResponse<RewardRequest, RewardResponse>(new RewardRequest() {BountyQuestIds = questIds, ConsumeCurrency = 0, IsQuick = false});
                rewardResponse.RewardItems.PrintUserItems(log);
                await GetResponse<BountyQuestGetListRequest, BountyQuestGetListResponse>(new BountyQuestGetListRequest());
            }
            else
            {
                log(ResourceStrings.NothingToReceive);
            }

            await GetBountyRequestInfo();
        });
    }

    private bool IsBountyQuestAvailable
    {
        get { return UserSyncData?.UserBattleBossDtoInfo?.BossClearMaxQuestId >= OpenContentTable.GetByOpenCommandType(OpenCommandType.BountyQuest).OpenContentValue; }
    }


    public async Task BountyQuestStartAuto()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            if (!IsBountyQuestAvailable)
            {
                return;
            }

            var response1 = await GetResponse<BountyQuestGetListRequest, BountyQuestGetListResponse>(new BountyQuestGetListRequest());
            if (GameConfig.BountyQuestAuto.TargetItems.Count > 0 && !BountyRequestForceAll)
            {
                var itemNames = string.Join(",", GameConfig.BountyQuestAuto.TargetItems.Select(ItemUtil.GetItemName));
                log($"{TextResourceTable.Get("[CommonHeaderBountyQuestLabel]")}: {ResourceStrings.DesignatedTargetProp} {itemNames}");
                log($"{TextResourceTable.Get("[CommonHeaderBountyQuestLabel]")}: {ResourceStrings.DispatchingTargetPropMission}");
                var bountyQuestStartInfos = BountyQuestAutoFormationUtil.CalcAutoFormation(response1, UserSyncData, GameConfig.BountyQuestAuto);
                foreach (var bountyQuestStartInfo in bountyQuestStartInfos)
                {
                    var startResponse = await GetResponse<BountyQuestStartRequest, BountyQuestStartResponse>(
                        new BountyQuestStartRequest {BountyQuestStartInfos = new List<BountyQuestStartInfo>() {bountyQuestStartInfo}});
                    log($"{ResourceStrings.Dispatched} {bountyQuestStartInfo.BountyQuestId}");
                }
            }
            else
            {
                log($"{TextResourceTable.Get("[CommonHeaderBountyQuestLabel]")}: {ResourceStrings.DispatchingAll}");
                var bountyQuestStartInfos = BountyQuestAutoFormationUtil.CalcAutoFormation(response1, UserSyncData, GameConfig.BountyQuestAuto, true);
                foreach (var bountyQuestStartInfo in bountyQuestStartInfos)
                {
                    var startResponse = await GetResponse<BountyQuestStartRequest, BountyQuestStartResponse>(
                        new BountyQuestStartRequest {BountyQuestStartInfos = new List<BountyQuestStartInfo>() {bountyQuestStartInfo}});
                    log($"{ResourceStrings.Dispatched} {bountyQuestStartInfo.BountyQuestId}");
                }
            }

            await GetBountyRequestInfo();
        });
    }

    /// <summary>
    /// 魔装继承
    /// </summary>
    public async Task AutoEquipmentMatchlessInheritance()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            while (!token.IsCancellationRequested)
            {
                // 批量精炼

                var usersyncData = await UserGetUserData();
                // 找到所有 等级为S、魔装、未装备 的装备
                var equipments = usersyncData.UserSyncData.UserEquipmentDtoInfos.Select(d => new
                {
                    Equipment = d, EquipmentMB = EquipmentTable.GetById(d.EquipmentId)
                });

                var sEquipments = equipments.Where(d =>
                        d.Equipment.CharacterGuid == "" && // 未装备
                        d.Equipment.MatchlessSacredTreasureLv == 1 && // 魔装等级为 1
                        (d.EquipmentMB.RarityFlags & EquipmentRarityFlags.S) != 0 // 稀有度为 S
                ).ToList();

                if (sEquipments.Count == 0)
                {
                    log(TextResourceTable.Get("[ItemBoxButtonBulkCasting]"));

                    try
                    {
                        var castManyResponse = await GetResponse<CastManyRequest, CastManyResponse>(new CastManyRequest()
                        {
                            RarityFlags = EquipmentRarityFlags.S | EquipmentRarityFlags.A | EquipmentRarityFlags.B |
                                          EquipmentRarityFlags.C
                        });
                    }
                    catch (ApiErrorException e) when (e.ErrorCode == ErrorCode.EquipmentMissingEquipment)
                    {
                        // 没有可以精炼的装备了
                        log(ResourceStrings.NothingToSmelt);
                        break;
                    }

                    sEquipments = equipments.Where(d =>
                            d.Equipment.CharacterGuid == "" && // 未装备
                            d.Equipment.MatchlessSacredTreasureLv == 1 && // 魔装等级为 1
                            (d.EquipmentMB.RarityFlags & EquipmentRarityFlags.S) != 0 // 稀有度为 S
                    ).ToList();
                }

                // 按照装备位置进行分组
                foreach (var grouping in sEquipments.GroupBy(d => d.EquipmentMB.SlotType))
                {
                    // 当前能够接受继承的 D 级别装备
                    var currentTypeEquips = equipments.Where(d =>
                    {
                        return (d.EquipmentMB.RarityFlags & EquipmentRarityFlags.D) != 0 &&
                               d.EquipmentMB.EquipmentLv != 1 &&
                               d.EquipmentMB.SlotType == grouping.Key &&
                               d.Equipment.LegendSacredTreasureLv == 0 &&
                               d.Equipment.MatchlessSacredTreasureLv == 0;
                    });
                    var processedDEquips = new List<UserItemDtoInfo>();

                    // 还缺多少装备
                    var needMoreCount = grouping.Count() - currentTypeEquips.Count();
                    var slotType = grouping.Key;
                    if (needMoreCount > 0) await GetInheritatableEquipments(usersyncData, slotType, needMoreCount, log, processedDEquips);

                    // 继承            
                    foreach (var x1 in grouping)
                    {
                        var mb = x1.EquipmentMB;
                        var info = x1.Equipment;
                        usersyncData = await InheritantEquipment(mb, info, log);
                    }
                }
            }
        });
    }

    private async Task GetInheritatableEquipments(GetUserDataResponse usersyncData, EquipmentSlotType slotType, int needMoreCount, Action<string> log, List<UserItemDtoInfo> processedDEquips)
    {
        // 找到未解封的装备物品
        var equipItems = usersyncData.UserSyncData.UserItemDtoInfo.Where(d =>
        {
            if (d.ItemType != ItemType.Equipment) return false;
            var equipmentMb = EquipmentTable.GetById(d.ItemId);
            if (equipmentMb.EquipmentLv == 1) return false;
            if (equipmentMb.SlotType != slotType) return false;
            if ((equipmentMb.RarityFlags & EquipmentRarityFlags.D) == 0) return false;
            return true;
        }).ToList();
        foreach (var equipItem in equipItems)
        {
            if (needMoreCount <= 0) break;

            for (var i = 0; i < equipItem.ItemCount; i++)
            {
                if (needMoreCount <= 0) break;
                var equipmentMb = EquipmentTable.GetById(equipItem.ItemId);
                log($"{ResourceStrings.FindEquipmentCharacter} {equipmentMb.Memo}");
                // 找到可以装备的一个角色
                var userCharacterDtoInfo = usersyncData.UserSyncData.UserCharacterDtoInfos.Where(d =>
                {
                    var characterMb = CharacterTable.GetById(d.CharacterId);
                    if ((characterMb.JobFlags & equipmentMb.EquippedJobFlags) == 0) return false; // 装备职业

                    if (d.Level >= equipmentMb.EquipmentLv) return true; // 装备等级

                    if (usersyncData.UserSyncData.UserLevelLinkMemberDtoInfos.Exists(x =>
                            d.Guid == x.UserCharacterGuid)
                        && usersyncData.UserSyncData.UserLevelLinkDtoInfo.PartyLevel >=
                        equipmentMb.EquipmentLv) // 角色在等级链接里面并且等级链接大于装备等级
                        return true;

                    return false;
                }).First();


                // 获取角色某个位置的装备, 可能没有装备
                var replacedEquip = usersyncData.UserSyncData.UserEquipmentDtoInfos.Where(d =>
                {
                    var byId = EquipmentTable.GetById(d.EquipmentId);
                    return d.CharacterGuid == userCharacterDtoInfo.Guid &&
                           byId.SlotType == equipmentMb.SlotType;
                }).FirstOrDefault();

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
                if (replacedEquip == null)
                {
                    await GetResponse<RemoveEquipmentRequest, RemoveEquipmentResponse>(new RemoveEquipmentRequest()
                    {
                        EquipmentSlotTypes = new List<EquipmentSlotType>() {equipmentMb.SlotType},
                        UserCharacterGuid = userCharacterDtoInfo.Guid
                    });
                }
                else
                {
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
                }

                needMoreCount--;
                processedDEquips.Add(equipItem);
            }
        }
    }

    /// <summary>
    /// 圣装继承
    /// </summary>
    public async Task AutoEquipmentLegendInheritance()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            while (!token.IsCancellationRequested)
            {
                var usersyncData = await UserGetUserData();
                // 找到所有 等级为S、魔装、未装备 的装备
                var equipments = usersyncData.UserSyncData.UserEquipmentDtoInfos.Select(d => new
                {
                    Equipment = d, EquipmentMB = EquipmentTable.GetById(d.EquipmentId)
                });
                var sEquipments = equipments.Where(d =>
                        d.Equipment.CharacterGuid == "" && // 未装备
                        d.Equipment.LegendSacredTreasureLv == 1 && // 圣装等级为 1
                        (d.EquipmentMB.RarityFlags & EquipmentRarityFlags.S) != 0 // 稀有度为 S
                ).ToList();

                if (sEquipments.Count == 0)
                {
                    log(ResourceStrings.NoLegendEquip);
                    break;
                }

                // 按照装备位置进行分组
                foreach (var grouping in sEquipments.GroupBy(d => d.EquipmentMB.SlotType))
                {
                    // 当前能够接受继承的 D 级别装备
                    var currentTypeEquips = equipments.Where(d =>
                    {
                        return (d.EquipmentMB.RarityFlags & EquipmentRarityFlags.D) != 0 &&
                               d.EquipmentMB.EquipmentLv != 1 &&
                               d.EquipmentMB.SlotType == grouping.Key &&
                               d.Equipment.LegendSacredTreasureLv == 0 &&
                               d.Equipment.MatchlessSacredTreasureLv == 0;
                    });
                    var processedDEquips = new List<UserItemDtoInfo>();

                    // 还缺多少装备
                    var needMoreCount = grouping.Count() - currentTypeEquips.Count();
                    if (needMoreCount > 0) await GetInheritatableEquipments(usersyncData, grouping.Key, needMoreCount, log, processedDEquips);

                    // 继承            
                    foreach (var x1 in grouping)
                    {
                        var mb = x1.EquipmentMB;
                        var info = x1.Equipment;
                        usersyncData = await InheritantEquipment(mb, info, log);
                    }
                }
            }
        });
    }

    private async Task<GetUserDataResponse> InheritantEquipment(EquipmentMB mb, UserEquipmentDtoInfo info, Action<string> log)
    {
        GetUserDataResponse usersyncData;
        // 同步数据
        usersyncData = await UserGetUserData();

        var userEquipmentDtoInfo = usersyncData.UserSyncData.UserEquipmentDtoInfos.Where(d =>
        {
            var equipmentMb = EquipmentTable.GetById(d.EquipmentId);
            if (d.LegendSacredTreasureLv == 0 // 未被继承的装备
                && d.MatchlessSacredTreasureLv == 0
                && equipmentMb.EquipmentLv != 1
                && equipmentMb.SlotType == mb.SlotType // 同一个位置 
                && (equipmentMb.RarityFlags & EquipmentRarityFlags.D) != 0 // 稀有度为 D
               )
                return true;

            return false;
        }).FirstOrDefault();

        if (userEquipmentDtoInfo != null)
        {
            var inheritanceEquipmentResponse = await GetResponse<InheritanceEquipmentRequest, InheritanceEquipmentResponse>(
                new InheritanceEquipmentRequest()
                {
                    InheritanceEquipmentGuid = userEquipmentDtoInfo.Guid,
                    SourceEquipmentGuid = info.Guid
                });
            log($"{TextResourceTable.Get("[EquipmentInheritanceButton]")}{ResourceStrings.Finished} {mb.Memo}=>{EquipmentTable.GetById(userEquipmentDtoInfo.EquipmentId).Memo}");
        }
        else
        {
            log(ResourceStrings.NoInheritableDRarityEquip);
        }

        return usersyncData;
    }

    public async Task BossHishSpeedBattle()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var autoResponse = await GetResponse<AutoRequest, AutoResponse>(new AutoRequest());
            if (await IsValidMonthlyBoost())
            {
                var availableCount = OrtegaConst.Shop.MonthlyBoostBattleQuickBonus - autoResponse.UserBattleAuto.QuickTodayUsePrivilegeCount;
                if (availableCount > 0)
                {
                    log(TextResourceTable.Get("[MonthlyBoostPrivilegeDescription3]"));
                    await BattleQuick(log, QuestQuickExecuteType.Privilege, (int) availableCount);
                }
            }

            // 每天有一次免费
            if (autoResponse.UserBattleAuto.QuickTodayUseCurrencyCount >= 1)
            {
                log(TextResourceTable.GetErrorCodeMessage(ErrorCode.BattleAutoNotEnoughPrivilegeCount));
            }
            else
            {
                log($"{TextResourceTable.Get("[AutoBattleButtonQuickForward]")}{TextResourceTable.Get("[CommonRewardLabel]")}:\n");
                await BattleQuick(log, QuestQuickExecuteType.Currency, 1);
            }
        });

        async Task BattleQuick(Action<string> log, QuestQuickExecuteType type, int count)
        {
            var req = new QuickRequest() {QuestQuickExecuteType = type, QuickCount = count};
            var quickResponse = await GetResponse<QuickRequest, QuickResponse>(req);

            log($"{TextResourceTable.Get("[AutoBattleRewardInfoPopulationLabel]")} {TextResourceTable.Get("[ItemName5]")} {quickResponse.AutoBattleRewardResult.GoldByPopulation}");
            log($"{TextResourceTable.Get("[AutoBattleRewardInfoPopulationLabel]")} {TextResourceTable.Get("[ItemName11]")} {quickResponse.AutoBattleRewardResult.PotentialJewelByPopulation}");
            log($"{TextResourceTable.Get("[ItemName6]")} {quickResponse.AutoBattleRewardResult.BattleRewardResult.CharacterExp}");
            log($"{TextResourceTable.Get("[ItemName5]")} {quickResponse.AutoBattleRewardResult.BattleRewardResult.ExtraGold}");
            log($"{TextResourceTable.Get("[ItemName11]")} {quickResponse.AutoBattleRewardResult.BattleRewardResult.PlayerExp}");
            log($"{TextResourceTable.Get("[CharacterLevelUpLabel]")} {quickResponse.AutoBattleRewardResult.BattleRewardResult.RankUp}");

            quickResponse.AutoBattleRewardResult.BattleRewardResult.FixedItemList.PrintUserItems(log);
            quickResponse.AutoBattleRewardResult.BattleRewardResult.DropItemList.PrintUserItems(log);
        }
    }

    public async Task AutoDungeonBattle()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            try
            {
                await AutoDungeonBattle(log, token);
                log($"{TextResourceTable.Get("[CommonHeaderDungeonBattleLabel]")}{ResourceStrings.Finished}");
            }
            catch (Exception e)
            {
                log(e.ToString());
            }
        });
    }

    public async Task GetMyPage()
    {
        await ExecuteQuickAction(async (log, token) => { Mypage = await GetResponse<GetMypageRequest, GetMypageResponse>(new GetMypageRequest()); });
    }

    public async Task Debug()
    {
        await ExecuteQuickAction(async (log, token) => { });
    }

    public async Task LogDebug()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var counter = 0;
            while (!token.IsCancellationRequested)
            {
                log($"Message {counter++}");
                await Task.Delay(TimeSpan.FromMilliseconds(500));
            }

            log("DDDDD");
        });
    }

    public async Task UseFriendCode()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var useFriendCodeResponse = await GetResponse<UseFriendCodeRequest, UseFriendCodeResponse>(new UseFriendCodeRequest() {FriendCode = ""});
        });
    }

    public async Task FreeGacha()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            while (await DoFreeGacha())
            {
            }

            log(ResourceStrings.NoAvailableGacha);

            async Task<bool> DoFreeGacha()
            {
                var gachaListResponse = await GetResponse<GachaGetListRequest, GachaGetListResponse>(new GachaGetListRequest());
                foreach (var gachaCaseInfo in gachaListResponse.GachaCaseInfoList)
                {
                    var userItems = UserSyncData.UserItemDtoInfo.ToList();
                    var buttonInfo = gachaCaseInfo.GachaButtonInfoList.OrderByDescending(d => d.LotteryCount)
                        .FirstOrDefault(d => GameConfig.GachaConfig.AutoGachaConsumeUserItems.Exists(consumeUserItem => CheckCount(d, userItems, consumeUserItem.ItemType, consumeUserItem.ItemId)));
                    if (buttonInfo == null) continue;

                    var gachaCaseMb = GachaCaseTable.GetById(gachaCaseInfo.GachaCaseId);
                    var itemMb = ItemTable.GetByItemTypeAndItemId(buttonInfo.ConsumeUserItem.ItemType, buttonInfo.ConsumeUserItem.ItemId);
                    var name = TextResourceTable.Get(itemMb.NameKey);
                    log(string.Format(ResourceStrings.GachaExecInfo, gachaCaseMb.Memo, buttonInfo.LotteryCount, name, buttonInfo.ConsumeUserItem.ItemCount));
                    var response = await GetResponse<DrawRequest, DrawResponse>(new DrawRequest() {GachaButtonId = buttonInfo.GachaButtonId});
                    response.GachaRewardItemList.PrintUserItems(log);
                    response.BonusRewardItemList.PrintUserItems(log);
                    return true;
                }

                return false;
            }

            bool CheckCount(GachaButtonInfo buttonInfo, List<UserItemDtoInfo> userItems, ItemType itemType, long itemId = 1)
            {
                if (itemId == 0) itemId = 1;
                if (buttonInfo.ConsumeUserItem == null ||
                    buttonInfo.ConsumeUserItem.ItemCount == 0)
                    return true;

                if (buttonInfo.ConsumeUserItem.ItemType != itemType ||
                    buttonInfo.ConsumeUserItem.ItemId != itemId)
                    return false;

                var count = userItems.GetCount(itemType, itemId);
                return buttonInfo.ConsumeUserItem.ItemCount <= count;
            }
        });
    }


    public async Task AutoBuyShopItem()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var autoBuyItems = GameConfig.Shop.AutoBuyItems;
            if (autoBuyItems.Count == 0) return;

            var listResponse = await GetResponse<TradeShopGetListRequest, TradeShopGetListResponse>(new TradeShopGetListRequest());

            log(ResourceStrings.ShopAutoBuyItems);
            foreach (var tabInfo in listResponse.TradeShopTabInfoList)
            {
                if (tabInfo.TradeShopItems == null) continue;

                foreach (var shopItem in tabInfo.TradeShopItems)
                {
                    if (shopItem.IsSoldOut()) continue;

                    var shopAutoBuyItem = autoBuyItems.Find(d =>
                    {
                        if (d.ShopTabId != tabInfo.TradeShopTabId) return false;
                        if (d.BuyItem == null && d.ConsumeItem == null) return false;
                        if (d.BuyItem == null && (
                                (d.ConsumeItem.ItemType == shopItem.ConsumeItem1.ItemType && d.ConsumeItem.ItemId == shopItem.ConsumeItem1.ItemId)
                                || (shopItem.ConsumeItem2 != null && d.ConsumeItem.ItemType == shopItem.ConsumeItem2.ItemType && d.ConsumeItem.ItemId == shopItem.ConsumeItem2.ItemId)))
                            return true;
                        if (d.ConsumeItem == null && d.BuyItem.ItemType == shopItem.GiveItem.ItemType && d.BuyItem.ItemId == shopItem.GiveItem.ItemId)
                            return true;
                        if (d.BuyItem != null && d.ConsumeItem != null && d.BuyItem.IsEqual(shopItem.GiveItem.ItemType, shopItem.GiveItem.ItemId) && (
                                d.ConsumeItem.IsEqual(shopItem.ConsumeItem1.ItemType, shopItem.ConsumeItem1.ItemId)
                                || (shopItem.ConsumeItem2 != null && d.ConsumeItem.IsEqual(shopItem.ConsumeItem2.ItemType, shopItem.ConsumeItem2.ItemId))))
                            return true;

                        return false;
                    });

                    if (shopAutoBuyItem == null) continue;

                    if (shopItem.ConsumeItem1.ItemCount > UserSyncData.UserItemDtoInfo.GetCount(shopItem.ConsumeItem1.ItemType, shopItem.ConsumeItem1.ItemId))
                        continue;
                    if (shopItem.ConsumeItem2 != null && shopItem.ConsumeItem2.ItemCount > UserSyncData.UserItemDtoInfo.GetCount(shopItem.ConsumeItem2.ItemType, shopItem.ConsumeItem2.ItemId))
                        continue;

                    if (shopItem.SalePercent < shopAutoBuyItem.MinDiscountPercent) continue;

                    try
                    {
                        var response = await GetResponse<BuyItemRequest, BuyItemResponse>(
                            new BuyItemRequest
                            {
                                TradeShopTabId = tabInfo.TradeShopTabId, TradeShopItemInfos = new List<TradeShopItemInfo>() {new() {TradeShopItemId = shopItem.TradeShopItemId, TradeCount = 1}}
                            });
                        response.TradeShopItems.Select(d => d.GiveItem).PrintUserItems(log);
                    }
                    catch (ApiErrorException e)
                    {
                        log(e.Message);
                    }
                }
            }

            log($"{ResourceStrings.ShopAutoBuyItems} {ResourceStrings.Finished}");
        });
    }

    public async Task GuildCheckin()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var response1 = await GetResponse<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
            log($"{TextResourceTable.Get("[GuildId]")} {response1.GuildId}");
            var response2 = await GetResponse<GetGuildBaseInfoRequest, GetGuildBaseInfoResponse>(new GetGuildBaseInfoRequest() {BelongGuildId = response1.GuildId});
            log($"{TextResourceTable.Get("[MissionName533]")} {ResourceStrings.Finished}");
            response2.UserSyncData.GivenItemCountInfoList.PrintUserItems(log);
        });
    }

    public async Task GuildRaid()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var response1 = await GetResponse<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
            log($"{TextResourceTable.Get("[GuildId]")} {response1.GuildId}");
            if (response1.GuildId == 0) return;
            bool hasRaid;
            do
            {
                hasRaid = false;
                var response2 = await GetResponse<GetGuildRaidInfoRequest, GetGuildRaidInfoResponse>(new GetGuildRaidInfoRequest() {BelongGuildId = response1.GuildId});
                foreach (var info in response2.GuildRaidInfos)
                {
                    if (info.IsOpen && (info.UserGuildRaidDtoInfo == null || info.UserGuildRaidDtoInfo is {ChallengeCount: < 2}))
                    {
                        if (IsGuildRaidQuickAvailable && info.UserGuildRaidPreviousDtoInfo != null)
                        {
                            var response3 = await GetResponse<QuickStartGuildRaidRequest, QuickStartGuildRaidResponse>(new QuickStartGuildRaidRequest()
                                {BelongGuildId = response1.GuildId, GuildRaidBossType = info.GuildRaidDtoInfo.BossType});
                            log($"{ResourceStrings.BattleResult}: {response3.BattleSimulationResult.BattleEndInfo.IsWinAttacker()}");
                            response3.BattleRewardResult.FixedItemList.PrintUserItems(log);
                            response3.BattleRewardResult.DropItemList.PrintUserItems(log);
                        }
                        else
                        {
                            var response3 = await GetResponse<StartGuildRaidRequest, StartGuildRaidResponse>(new StartGuildRaidRequest()
                                {BelongGuildId = response1.GuildId, GuildRaidBossType = info.GuildRaidDtoInfo.BossType});
                            log($"{ResourceStrings.BattleResult}: {response3.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker()}");
                            response3.BattleRewardResult.FixedItemList.PrintUserItems(log);
                            response3.BattleRewardResult.DropItemList.PrintUserItems(log);
                        }

                        hasRaid = true;
                    }

                    if (info.IsExistWorldDamageReward)
                    {
                        var bossMb = GuildRaidBossTable.GetByGuildRaidBossType(info.GuildRaidDtoInfo.BossType);
                        if (bossMb != null)
                        {
                            var worldRewardInfoResponse =
                                await GetResponse<GetGuildRaidWorldRewardInfoRequest, GetGuildRaidWorldRewardInfoResponse>(new GetGuildRaidWorldRewardInfoRequest() {GuildRaidBossId = bossMb.Id});
                            var guildRaidRewardMb = GuildRaidRewardTable.GetByBossId(bossMb.Id);
                            foreach (var worldDamageBar in guildRaidRewardMb.WorldDamageBarRewards)
                            {
                                var worldRewardInfo = worldRewardInfoResponse.WorldRewardInfos.FirstOrDefault(d => d.GoalDamage == worldDamageBar.GoalDamage);
                                if (worldRewardInfoResponse.TotalWorldDamage >= worldDamageBar.GoalDamage && (worldRewardInfo == null || !worldRewardInfo.IsReceived))
                                {
                                    var resp = await GetResponse<GiveGuildRaidWorldRewardItemRequest, GiveGuildRaidWorldRewardItemResponse>(
                                        new GiveGuildRaidWorldRewardItemRequest {GoalDamage = worldDamageBar.GoalDamage, GuildRaidBossId = bossMb.Id});
                                    log(TextResourceTable.Get("[GuildRaidCurrentWorldDamageFormat]", worldDamageBar.GoalDamage));
                                    resp.RewardItems.PrintUserItems(log);
                                }
                            }
                        }
                    }
                }
            } while (hasRaid);

            log($"{TextResourceTable.Get("[QuickBattleTitle]")} {TextResourceTable.Get("[CommonHeaderGuildRaidLabel]")} {ResourceStrings.Finished}");
        });
    }

    public async Task OpenGuildRaid()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var response1 = await GetResponse<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
            if (response1.GuildId == 0)
            {
                log(TextResourceTable.Get("[RankingNotGuild]"));
                return;
            }

            var guildMemberInfoResponse = await GetResponse<GetGuildMemberInfoRequest, GetGuildMemberInfoResponse>(new GetGuildMemberInfoRequest {GuildId = response1.GuildId});
            var playerInfo = guildMemberInfoResponse.PlayerInfoList.Find(d => d.PlayerId == UserSyncData.UserStatusDtoInfo.PlayerId);
            if (playerInfo.PlayerGuildPositionType == PlayerGuildPositionType.Member || playerInfo.PlayerGuildPositionType == PlayerGuildPositionType.None)
            {
                log(TextResourceTable.GetErrorCodeMessage(ErrorCode.GuildRaidNotHavePermission));
                return;
            }

            var response2 = await GetResponse<GetGuildRaidInfoRequest, GetGuildRaidInfoResponse>(new GetGuildRaidInfoRequest() {BelongGuildId = response1.GuildId});
            var guildRaidInfo = response2.GuildRaidInfos.Find(d => d.GuildRaidDtoInfo.BossType == GuildRaidBossType.Releasable);
            if (guildRaidInfo.IsOpen)
            {
                log(TextResourceTable.GetErrorCodeMessage(ErrorCode.GuildRaidAlreadyOpenGuildRaid));
                return;
            }

            try
            {
                await GetResponse<OpenGuildRaidRequest, OpenGuildRaidResponse>(new OpenGuildRaidRequest {BelongGuildId = response1.GuildId, GuildRaidBossType = GuildRaidBossType.Releasable});
                log($"{TextResourceTable.Get("[GuildRaidReleaseConfirmTitle]")} {ResourceStrings.Success}");
            }
            catch (ApiErrorException e)
            {
                log(e.Message);
            }
        });
    }

    public async Task SetupLocalGvgDefense()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var response1 = await GetResponse<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
            if (response1.GuildId == 0)
            {
                log(TextResourceTable.Get("[RankingNotGuild]"));
                return;
            }

            var response2 = await GetResponse<GetGuildBaseInfoRequest, GetGuildBaseInfoResponse>(new GetGuildBaseInfoRequest() {BelongGuildId = response1.GuildId});
            if (!response2.LocalGuildGvgInfo.IsOpen)
            {
                log(TextResourceTable.GetErrorCodeMessage(ErrorCode.MagicOnionNotOpenGuildBattle));
                return;
            }

            var client = NetworkManager.GetOnionClient();
            var localGvgReceiver = new MagicOnionGvgReceiver(client, log);
            client.SetupGvg(localGvgReceiver, localGvgReceiver, BattleType.GuildBattle);
            await client.Connect();
            while (client.GetState() != HubClientState.Ready)
            {
                if (token.IsCancellationRequested) return;
                log("waiting for connection...");
                await Task.Delay(1000);
            }

            CancellationTokenSource keepaliveCts = new CancellationTokenSource();
            _ = Task.Run(async () =>
            {
                while (!keepaliveCts.IsCancellationRequested)
                {
                    client.SendKeepAliveAsync();
                    await Task.Delay(5000);
                }
            });

            try
            {
                client.SendGvgOpenMap(BattleType.GuildBattle, 0);
                while (!localGvgReceiver.IsCastleInfoUpdated)
                {
                    if (token.IsCancellationRequested) return;
                    log("waiting for castle info...");
                    await Task.Delay(1000);
                }

                var castleInfos = localGvgReceiver.CastleInfos
                    .Where(d => d.GvgCastleState is GvgCastleState.None or GvgCastleState.InBattle && d.GuildId == response1.GuildId)
                    .OrderByDescending(d => LocalGvgCastleTable.GetById(d.CastleId).CastleType)
                    .ToList();

                if (castleInfos.Count == 0)
                {
                    log(ResourceStrings.No_deployable_castles_available);
                    return;
                }

                var noMoreDeploy = false;
                var queue = new Queue<int>(new[] {5, 3, 3, 3});
                while (!noMoreDeploy && !token.IsCancellationRequested)
                {
                    foreach (var castleInfo in castleInfos)
                    {
                        if (token.IsCancellationRequested) break;

                        // open deploy dialog to update character list
                        localGvgReceiver.IsDeployCharacterUpdated = false;
                        client.SendGvgOpenPartyDeployDialog(BattleType.GuildBattle, castleInfo.CastleId);
                        while (!localGvgReceiver.IsDeployCharacterUpdated)
                        {
                            if (token.IsCancellationRequested) return;
                            log("waiting for deploy dialog to open...");
                            await Task.Delay(1000);
                        }

                        // calculate character count
                        var characterCount = queue.TryDequeue(out var i) ? i : 1;
                        var characterInfos = localGvgReceiver.OnUpdateDeployCharacterResponse.PartyCharacterInfos
                            .Where(d => !d.IsDeployed)
                            .OrderByDescending(d => d.UserGvgCharacterInfo.BattlePower)
                            .Take(characterCount).ToList();
                        if (characterInfos.Count == 0)
                        {
                            noMoreDeploy = true;
                            break;
                        }

                        var characterIds = characterInfos.Select(d => d.UserGvgCharacterInfo.UserCharacterInfo.CharacterId).ToList();
                        localGvgReceiver.IsDeployCharacterUpdated = false;
                        // deploy
                        client.SendGvgAddCastleParty(BattleType.GuildBattle, castleInfo.CastleId, characterIds, characterInfos.Count);
                        while (!localGvgReceiver.IsDeployCharacterUpdated)
                        {
                            if (token.IsCancellationRequested) return;
                            log("waiting for deploy...");
                            await Task.Delay(1000);
                        }

                        // log
                        var name = TextResourceTable.Get(LocalGvgCastleTable.GetById(castleInfo.CastleId).NameKey);
                        var characters = string.Join(", ", characterIds.Select(d => CharacterTable.GetById(d).GetCombinedName()));
                        log(string.Format(ResourceStrings.Successfully_deployed, name, characters));
                        client.SendGvgCloseCastleDialog(BattleType.GuildBattle, GvgDialogType.Deploy);
                        await Task.Delay(1000);
                    }
                }
            }
            finally
            {
                keepaliveCts.Cancel();
                client.ClearGvgReceiver();
                await client.DisposeAsync();
            }

            log($"{ResourceStrings.Deploy_defense} {ResourceStrings.Finished}");
        });
    }

    public async Task ReceiveGvgReward()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            log($"{TextResourceTable.Get("[CommonHeaderGvgLabel]")} {TextResourceTable.Get("[CommonHeaderGlobalGvgLabel]")} {TextResourceTable.Get("[GuildRewardTitle]")}");
            var guildIdResponse = await GetResponse<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
            if (guildIdResponse.GuildId <= 0) return;

            var guildBaseInfoResponse = await GetResponse<GetGuildBaseInfoRequest, GetGuildBaseInfoResponse>(new GetGuildBaseInfoRequest
            {
                BelongGuildId = guildIdResponse.GuildId
            });

            if (guildBaseInfoResponse.LocalGuildGvgInfo.CanGetCastleRewardInfoList.IsNotNullOrEmpty())
            {
                log($"{TextResourceTable.Get("[CommonHeaderGvgLabel]")} {TextResourceTable.Get("[GuildRewardTitle]")}");
                var localGvgRewardResponse = await GetResponse<ReceiveLocalGvgRewardRequest, ReceiveLocalGvgRewardResponse>(new ReceiveLocalGvgRewardRequest
                {
                    CastleIdList = guildBaseInfoResponse.LocalGuildGvgInfo.CanGetCastleRewardInfoList.Select(d => d.CastleId).ToList()
                });
                localGvgRewardResponse.RewardItems.PrintUserItems(log);
            }

            if (guildBaseInfoResponse.GlobalGuildGvgInfo.CanGetCastleRewardInfoList.IsNotNullOrEmpty())
            {
                log($"{TextResourceTable.Get("[CommonHeaderGlobalGvgLabel]")} {{TextResourceTable.Get(\"[GuildRewardTitle]\")}}");
                var receiveGlobalGvgRewardResponse = await GetResponse<ReceiveGlobalGvgRewardRequest, ReceiveGlobalGvgRewardResponse>(new ReceiveGlobalGvgRewardRequest
                {
                    CastleIdList = guildBaseInfoResponse.GlobalGuildGvgInfo.CanGetCastleRewardInfoList.Select(d => d.CastleId).ToList()
                });
                receiveGlobalGvgRewardResponse.RewardItems.PrintUserItems(log);
            }
        });
    }

    public async Task AutoBossRequest(long selectedTargetQuerstId = 0)
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var totalCount = 0;
            var winCount = 0;
            var errCount = 0;

            try
            {
                await GetResponse<NextQuestRequest, NextQuestResponse>(new NextQuestRequest());
            }
            catch (ApiErrorException e) when (e.ErrorCode == ErrorCode.BattleAutoNextQuestNotFound)
            {
            }

            while (!token.IsCancellationRequested)
                try
                {
                    var targetQuestId = UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId + 1;
                    var bossResponse = await GetResponse<BossRequest, BossResponse>(new BossRequest() {QuestId = targetQuestId});
                    var win = bossResponse.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker();
                    totalCount++;
                    if (win) winCount++;
                    var info = QuestTable.GetById(targetQuestId).Memo;
                    var result = win ? TextResourceTable.Get("[LocalRaidBattleWinMessage]") : TextResourceTable.Get("[LocalRaidBattleLoseMessage]");
                    log(string.Format(ResourceStrings.AutoBossExecMessage, info, result, totalCount, winCount, errCount));

                    if (GameConfig.RecordBattleLog)
                    {
                        Directory.CreateDirectory(GameConfig.BattleLogDir);
                        var res = win ? "win" : "lose";
                        var filename =
                            $"main-{bossResponse.BattleResult.QuestId}-{res}-{bossResponse.BattleResult.BattleTime.StartBattle}-{bossResponse.BattleResult.SimulationResult.BattleToken}.json";
                        var path = Path.Combine(GameConfig.BattleLogDir, filename);
                        await File.WriteAllTextAsync(path, bossResponse.BattleResult.ToJson(true));
                        if (!win)
                        {
                            // keep only 100 lose logs
                            var files = Directory.GetFiles(GameConfig.BattleLogDir, $"main-*lose*.json").OrderDescending();
                            foreach (var file in files.Skip(20)) File.Delete(file);
                        }
                    }

                    if (win)
                    {
                        if (selectedTargetQuerstId > 0 && selectedTargetQuerstId == targetQuestId) break;
                        var nextQuestResponse = await GetResponse<NextQuestRequest, NextQuestResponse>(new NextQuestRequest());
                    }

                    if (GameConfig.AutoRequestDelay > 0) await Task.Delay(GameConfig.AutoRequestDelay, token);
                }
                catch (Exception e)
                {
                    log(e.Message);
                    errCount++;
                    if (errCount > Max_Err_Count)
                    {
                        log(string.Format(ResourceStrings.AutoBossErrorMessage, Max_Err_Count));
                        return;
                    }

                    if (e is ApiErrorException) await AuthLogin(_lastPlayerDataInfo);
                }
        });
    }

    public async Task AutoInfiniteTowerRequest(long targetStopLayer)
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            if (SelectedAutoTowerType == TowerType.None) return;

            var totalCount = 0;
            var winCount = 0;
            var errCount = 0;
            while (!token.IsCancellationRequested)
                try
                {
                    var towerBattleDtoInfo = UserSyncData.UserTowerBattleDtoInfos.First(d => d.TowerType == SelectedAutoTowerType);
                    if (SelectedAutoTowerType != TowerType.Infinite && towerBattleDtoInfo.TodayClearNewFloorCount >= 10)
                    {
                        log($"{SelectedAutoTowerType} {TextResourceTable.Get("[ClientErrorMessage1700007]")}");
                        break;
                    }

                    var tower = UserSyncData.UserTowerBattleDtoInfos.First(d => d.TowerType == SelectedAutoTowerType);
                    var targetQuestId = tower.MaxTowerBattleId + 1;
                    var bossQuickResponse = await GetResponse<TowerBattleStartRequest, TowerBattleStartResponse>(new TowerBattleStartRequest()
                    {
                        TargetTowerType = SelectedAutoTowerType, TowerBattleQuestId = targetQuestId
                    });
                    var win = bossQuickResponse.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker();
                    totalCount++;
                    if (win) winCount++;

                    if (GameConfig.RecordBattleLog)
                    {
                        Directory.CreateDirectory(GameConfig.BattleLogDir);
                        var res = win ? "win" : "lose";
                        var filename =
                            $"tower-{SelectedAutoTowerType}-{bossQuickResponse.BattleResult.QuestId}-{res}-{bossQuickResponse.BattleResult.BattleTime.StartBattle}-{bossQuickResponse.BattleResult.SimulationResult.BattleToken}.json";
                        var path = Path.Combine(GameConfig.BattleLogDir, filename);
                        await File.WriteAllTextAsync(path, bossQuickResponse.BattleResult.ToJson(true));
                        if (!win)
                        {
                            // keep only 100 lose logs
                            var files = Directory.GetFiles(GameConfig.BattleLogDir, $"tower-{SelectedAutoTowerType}-*lose*.json").OrderDescending();
                            foreach (var file in files.Skip(20)) File.Delete(file);
                        }
                    }

                    var name = TextResourceTable.Get(SelectedAutoTowerType);
                    var result = win ? TextResourceTable.Get("[LocalRaidBattleWinMessage]") : TextResourceTable.Get("[LocalRaidBattleLoseMessage]");

                    if (SelectedAutoTowerType == TowerType.Infinite)
                        log(string.Format(ResourceStrings.AutoTowerInfiniteExecMsg, name, targetQuestId, result, totalCount, winCount, errCount));
                    else
                        log(string.Format(ResourceStrings.AutoTowerElementExecMsg, name, targetQuestId, result, totalCount, winCount, errCount, towerBattleDtoInfo.TodayClearNewFloorCount));

                    if (win && SelectedAutoTowerType == TowerType.Infinite && targetStopLayer > 0 && targetStopLayer == targetQuestId) break;

                    if (GameConfig.AutoRequestDelay > 0) await Task.Delay(GameConfig.AutoRequestDelay, token);
                }
                catch (Exception e)
                {
                    log(e.Message);
                    errCount++;
                    if (errCount > Max_Err_Count)
                    {
                        log(string.Format(ResourceStrings.AutoBossErrorMessage, Max_Err_Count));
                        return;
                    }

                    if (e is ApiErrorException) await AuthLogin(_lastPlayerDataInfo);
                }
        });
    }

    public async Task AutoEquipmentTraning()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var totalCount = 0;
            while (!token.IsCancellationRequested)
            {
                // await UserGetUserData();
                var equipment = UserSyncData.UserEquipmentDtoInfos.First(d => d.Guid == EquipmentId);
                var m =
                    $"{TextResourceTable.Get("[CommonForgedLabel]")} {totalCount}, {TextResourceTable.Get(BaseParameterType.Health)} {equipment.AdditionalParameterHealth},{TextResourceTable.Get(BaseParameterType.Intelligence)} {equipment.AdditionalParameterIntelligence},{TextResourceTable.Get(BaseParameterType.Muscle)} {equipment.AdditionalParameterMuscle},{TextResourceTable.Get(BaseParameterType.Energy)} {equipment.AdditionalParameterEnergy}";
                log(m);
                switch (EquipmentTrainingTargetType)
                {
                    case "Health" when equipment.AdditionalParameterHealth >= EquipmentTrainingTargetValue:
                        return;
                    case "Energy" when equipment.AdditionalParameterEnergy >= EquipmentTrainingTargetValue:
                        return;
                    case "Intelligence" when equipment.AdditionalParameterIntelligence >= EquipmentTrainingTargetValue:
                        return;
                    case "Muscle" when equipment.AdditionalParameterMuscle >= EquipmentTrainingTargetValue:
                        return;
                }

                var response = await GetResponse<TrainingRequest, TrainingResponse>(new TrainingRequest() {EquipmentGuid = EquipmentId, ParameterLockedList = new List<BaseParameterType>()});
                totalCount++;
                var t = 1;
                await Task.Delay(t);
            }
        });
    }

    public async Task GetMissionInfo()
    {
        var missionGroupTypes = new List<MissionGroupType>() {MissionGroupType.Daily, MissionGroupType.Weekly, MissionGroupType.Main};
        var panelMission = Mypage.MypageInfo.MypageIconInfos.FirstOrDefault(d => d.TransferDetailInfo.TransferSpotType == TransferSpotType.PanelMission);
        if (panelMission != null) missionGroupTypes.Add(MissionGroupType.Panel);
        var response = await GetResponse<GetMissionInfoRequest, GetMissionInfoResponse>(new GetMissionInfoRequest()
            {TargetMissionGroupList = missionGroupTypes});
        MissionInfoDict = response.MissionInfoDict;
    }

    public async Task GetBountyRequestInfo()
    {
        if (!IsBountyQuestAvailable)
        {
            return;
        }

        var response = await GetResponse<BountyQuestGetListRequest, BountyQuestGetListResponse>(new BountyQuestGetListRequest());
        BountyQuestResponseInfo = response;
    }

    public async Task RemakeBountyRequest()
    {
        if (BountyQuestResponseInfo.UserBountyQuestDtoInfos.Any(d => d.BountyQuestEndTime == 0))
        {
            var response = await GetResponse<RemakeRequest, RemakeResponse>(new RemakeRequest());
            await GetBountyRequestInfo();
        }
        else
        {
            AddLog(string.Format(ResourceStrings.NoAvailable, TextResourceTable.Get("[BountyQuestTypeSolo]")));
        }
    }

    public async Task GetMonthlyLoginBonusInfo()
    {
        var response = await GetResponse<GetMonthlyLoginBonusInfoRequest, GetMonthlyLoginBonusInfoResponse>(new GetMonthlyLoginBonusInfoRequest());
        MonthlyLoginBonusInfo = response;
    }

    public async Task GetNoticeInfoList()
    {
        var countryCode = OrtegaConst.Addressable.LanguageNameDictionary[NetworkManager.LanguageType];
        var response = await GetResponse<GetNoticeInfoListRequest, GetNoticeInfoListResponse>(new GetNoticeInfoListRequest()
        {
            AccessType = NoticeAccessType.Title,
            CategoryType = NoticeCategoryType.NoticeTab,
            CountryCode = countryCode,
            LanguageType = NetworkManager.LanguageType,
            UserId = _authOption.UserId
        });
        NoticeInfoList = response.NoticeInfoList;
        var response2 = await GetResponse<GetNoticeInfoListRequest, GetNoticeInfoListResponse>(new GetNoticeInfoListRequest()
        {
            AccessType = NoticeAccessType.MyPage,
            CategoryType = NoticeCategoryType.EventTab,
            CountryCode = countryCode,
            LanguageType = NetworkManager.LanguageType,
            UserId = _authOption.UserId
        });
        EventInfoList = response2.NoticeInfoList;
    }

    public TowerType[] GetAvailableTower()
    {
        if (!LoginOk)
        {
            return Array.Empty<TowerType>();
        }

        var now = DateTimeOffset.UtcNow.ToOffset(TimeManager.DiffFromUtc) - TimeSpan.FromHours(4);
        var dayOfWeek = now.DayOfWeek;
        // SelectedAutoTowerType = TowerType.Infinite;
        return dayOfWeek switch
        {
            DayOfWeek.Sunday => new[] {TowerType.Infinite, TowerType.Blue, TowerType.Green, TowerType.Red, TowerType.Yellow},
            DayOfWeek.Monday => new[] {TowerType.Infinite, TowerType.Blue},
            DayOfWeek.Tuesday => new[] {TowerType.Infinite, TowerType.Red},
            DayOfWeek.Wednesday => new[] {TowerType.Infinite, TowerType.Green},
            DayOfWeek.Thursday => new[] {TowerType.Infinite, TowerType.Yellow},
            DayOfWeek.Friday => new[] {TowerType.Infinite, TowerType.Blue, TowerType.Red},
            DayOfWeek.Saturday => new[] {TowerType.Infinite, TowerType.Yellow, TowerType.Green},
            _ => new[] {TowerType.Infinite}
        };
    }

    public async Task ReinforcementEquipmentOneTime()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var equipmentDtoInfo = UserSyncData.UserEquipmentDtoInfos.OrderBy(d => d.ReinforcementLv)
                .FirstOrDefault(d => !string.IsNullOrEmpty(d.CharacterGuid) && EquipmentTable.GetById(d.EquipmentId).EquipmentLv > d.ReinforcementLv);
            if (equipmentDtoInfo != null)
            {
                var response = await GetResponse<ReinforcementRequest, ReinforcementResponse>(new ReinforcementRequest() {EquipmentGuid = equipmentDtoInfo.Guid, NumberOfTimes = 1});
                log($"{TextResourceTable.Get("[CommonReinforceLabel]")} {ResourceStrings.Finished}");
            }
        });
    }

    public async Task ReadAllMemories()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            foreach (var userCharacterBook in UserSyncData.UserCharacterBookDtoInfos)
            {
                var stories = CharacterStoryTable.GetListByCharacterId(userCharacterBook.CharacterId);
                // var episodeIds = new List<long>();
                foreach (var storyMB in stories)
                    if (storyMB.Level <= userCharacterBook.MaxCharacterLevel && storyMB.EpisodeId > userCharacterBook.MaxEpisodeId)
                    {
                        // episodeIds.Add(storyMB.Id);
                        var resp = await GetResponse<GetCharacterStoryRewardRequest, GetCharacterStoryRewardResponse>(new GetCharacterStoryRewardRequest()
                        {
                            IsSkip = true,
                            CharacterStoryIdList = new List<long>() {storyMB.Id}
                        });
                        resp.RewardItemList.PrintUserItems(log);
                    }

                // if (episodeIds.Count > 0)
                // {
                //     var resp = await GetResponse<GetCharacterStoryRewardRequest, GetCharacterStoryRewardResponse>(new GetCharacterStoryRewardRequest()
                //     {
                //         IsSkip = true,
                //         CharacterStoryIdList = episodeIds
                //     });
                //     resp.RewardItemList.PrintUserItems(log);
                // }
            }
        });
    }

    public async Task CompleteMissions()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            await GetMissionInfo();
            var missionIds = new List<long>();
            foreach (var (missionGroupType, missionInfo) in MissionInfoDict)
            {
                if (missionGroupType == MissionGroupType.Panel && missionInfo.UserMissionDtoInfoDict.TryGetValue(MissionType.PanelSheet1, out var info1))
                {
                    var notReceived1 = info1.SelectMany(x => x.MissionStatusHistory[MissionStatusType.NotReceived]).ToList();
                    missionIds.AddRange(notReceived1);

                    var unfinishedIds1 = info1
                        .SelectMany(d => d.MissionStatusHistory)
                        .Where(d => d.Key == MissionStatusType.Locked || d.Key == MissionStatusType.Progress)
                        .SelectMany(d => d.Value).ToList();
                    // 检查是否所有任务都已经完成
                    if (unfinishedIds1.Count == 0 && missionInfo.UserMissionDtoInfoDict.TryGetValue(MissionType.PanelSheet2, out var info2))
                    {
                        var notReceived2 = info2.SelectMany(x => x.MissionStatusHistory[MissionStatusType.NotReceived]).ToList();
                        missionIds.AddRange(notReceived2);

                        var unfinishedIds2 = info2
                            .SelectMany(d => d.MissionStatusHistory)
                            .Where(d => d.Key == MissionStatusType.Locked || d.Key == MissionStatusType.Progress)
                            .SelectMany(d => d.Value).ToList();

                        if (unfinishedIds2.Count == 0 && missionInfo.UserMissionDtoInfoDict.TryGetValue(MissionType.PanelSheet3, out var info3))
                        {
                            var notReceived3 = info3.SelectMany(x => x.MissionStatusHistory[MissionStatusType.NotReceived]).ToList();
                            missionIds.AddRange(notReceived3);
                        }
                    }
                }
                else
                {
                    var notReceived = missionInfo.UserMissionDtoInfoDict.Values.SelectMany(d => d.SelectMany(x => x.GetNotReceivedIdList()));
                    missionIds.AddRange(notReceived);
                }
            }

            var rewardMissionResponse = await GetResponse<RewardMissionRequest, RewardMissionResponse>(new RewardMissionRequest() {TargetMissionIdList = missionIds});
            rewardMissionResponse.RewardInfo.ItemList.PrintUserItems(log);
            rewardMissionResponse.RewardInfo.CharacterList.PrintCharacterDtos(log);
        });
    }

    public async Task RewardMissonActivity()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            await GetMissionInfo();
            foreach (var pair in MissionInfoDict)
            {
                if (pair.Value.UserMissionActivityDtoInfo == null) continue;

                foreach (var (rewardId, statusType) in pair.Value.UserMissionActivityDtoInfo.RewardStatusDict)
                    if (statusType == MissionActivityRewardStatusType.NotReceived)
                    {
                        var rewardMb = TotalActivityMedalRewardTable.GetById(rewardId);
                        log(string.Format(ResourceStrings.RewardMissionMsg, pair.Key, rewardMb.RequiredActivityMedalCount));
                        var response = await GetResponse<RewardMissionActivityRequest, RewardMissionActivityResponse>(new RewardMissionActivityRequest()
                            {MissionGroupType = pair.Key, RequiredCount = rewardMb.RequiredActivityMedalCount});
                        response.RewardInfo.ItemList.PrintUserItems(log);
                        response.RewardInfo.CharacterList.PrintCharacterDtos(log);
                    }
            }
        });
    }


    public async Task AutoUseItems()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            bool successOpen;
            do
            {
                successOpen = false;
                await UserGetUserData();
                foreach (var userItemDtoInfo in UserSyncData.UserItemDtoInfo.Where(d => d.ItemCount > 0 && d.ItemType == ItemType.TreasureChest).ToList())
                {
                    var treasureChestMb = TreasureChestTable.GetById(userItemDtoInfo.ItemId);
                    if (treasureChestMb.TreasureChestLotteryType == TreasureChestLotteryType.Fix ||
                        treasureChestMb.TreasureChestLotteryType == TreasureChestLotteryType.Random)
                        if (userItemDtoInfo.ItemCount >= treasureChestMb.MinOpenCount)
                        {
                            if (treasureChestMb.ChestKeyItemId > 0)
                            {
                                var keyCount = UserSyncData.UserItemDtoInfo.FirstOrDefault(d => d.ItemType == ItemType.TreasureChestKey && d.ItemId == treasureChestMb.ChestKeyItemId)?.ItemCount ?? 0;
                                var available = Math.Min(userItemDtoInfo.ItemCount, keyCount);
                                if (available > 0)
                                {
                                    var openCount = available / treasureChestMb.MinOpenCount * treasureChestMb.MinOpenCount;
                                    await OpenTreasure(openCount, treasureChestMb, log);
                                    successOpen = true;
                                }
                            }
                            else
                            {
                                var openCount = userItemDtoInfo.ItemCount / treasureChestMb.MinOpenCount * treasureChestMb.MinOpenCount;
                                await OpenTreasure(openCount, treasureChestMb, log);
                                successOpen = true;
                            }
                        }
                }
            } while (successOpen);

            log(TextResourceTable.Get("[CharacterResetErrorMessageNotEnoughDiamond]"));
        });

        async Task OpenTreasure(long openCount, TreasureChestMB treasureChestMb, Action<string> log)
        {
            var response = await GetResponse<OpenTreasureChestRequest, OpenTreasureChestResponse>(new OpenTreasureChestRequest()
            {
                OpenCount = (int) openCount,
                TreasureChestId = treasureChestMb.Id
            });
            log($"{TextResourceTable.Get("[CommonOpenLabel]")} {TextResourceTable.Get(treasureChestMb.NameKey)} x {openCount}");
            response.RewardItems.PrintUserItems(log);
        }
    }

    public async Task AutoRankUpCharacter()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            await RankUp(CharacterRarityFlags.R, 3, log);
            await RankUp(CharacterRarityFlags.SR, 2, log);
            log($"{TextResourceTable.Get("[CharacterMenuTabCharacterRankUp]")} {ResourceStrings.Finished}");
        });

        async Task RankUp(CharacterRarityFlags rarityFlags, int count, Action<string> log)
        {
            var rGroup = UserSyncData.UserCharacterDtoInfos
                .Where(d => (d.RarityFlags & rarityFlags) != 0)
                .GroupBy(d => d.CharacterId)
                .ToList();
            foreach (var grouping in rGroup.Where(d => d.Count() >= count))
            {
                var infos = grouping.OrderByDescending(d => d.Level).ToList();
                var main = infos.First();
                var materials = new List<UserCharacterDtoInfo>();
                infos.Remove(main);
                var needCound = count - 1;
                while (needCound > 0)
                {
                    var last = infos.Last();
                    infos.Remove(last);
                    materials.Add(last);
                    needCound--;
                }

                var response = await GetResponse<RankUpRequest, RankUpResponse>(new RankUpRequest()
                {
                    RankUpList = new List<CharacterRankUpMaterialInfo>()
                    {
                        new() {TargetGuid = main.Guid, MaterialGuid1 = materials[0].Guid, MaterialGuid2 = materials.Count == 1 ? null : materials[1].Guid}
                    }
                });
                CharacterTable.GetCharacterName(main.CharacterId, out var name1, out var name2);
                if (!name2.IsNullOrEmpty()) name1 = $"[{name2}] {name1}";

                log($"{TextResourceTable.Get("[ItemBoxButtonUse]")} {TextResourceTable.Get("[CommonFooterCharacterButtonLabel]")} {name1} X {count}, {TextResourceTable.Get(main.RarityFlags)}");
            }
        }
    }

    public async Task AutoLocalRaid()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            // 奖励: 经验珠 强化水 強化秘薬 潜在宝珠 符石兑换券
            var client = NetworkManager.GetOnionClient();
            var localRaidReceiver = new MagicOnionLocalRaidReceiver(client, log);
            client.SetupLocalRaid(localRaidReceiver, localRaidReceiver);
            await client.Connect();
            while (client.GetState() != HubClientState.Ready)
            {
                if (token.IsCancellationRequested) return;
                log("Waiting for connection...");
                await Task.Delay(1000);
            }

            CancellationTokenSource keepaliveCts = new CancellationTokenSource();
            _ = Task.Run(async () =>
            {
                while (!keepaliveCts.IsCancellationRequested)
                {
                    client.SendKeepAliveAsync();
                    await Task.Delay(5000);
                }
            });

            try
            {
                while (!token.IsCancellationRequested)
                {
                    var localRaidInfoResponse = await GetResponse<GetLocalRaidInfoRequest, GetLocalRaidInfoResponse>(new GetLocalRaidInfoRequest());
                    var questId = GetQuestId(localRaidInfoResponse);
                    localRaidReceiver.QuestId = questId;

                    log(TextResourceTable.Get("[LocalRaidRoomSearchButtonJoinRandomRoom]"));
                    client.SendLocalRaidJoinRandomRoom(questId);
                    while (!token.IsCancellationRequested)
                    {
                        await Task.Delay(1000);
                        if (localRaidReceiver.IsNoRemainingChallenges || localRaidReceiver.IsMaxTimeExceeded)
                        {
                            return;
                        }

                        if (localRaidReceiver.IsBattleStarted)
                        {
                            await Task.Delay(2000);
                            try
                            {
                                var battleResultResponse = await GetResponse<GetLocalRaidBattleResultRequest, GetLocalRaidBattleResultResponse>(new GetLocalRaidBattleResultRequest());
                                var isWinAttacker = battleResultResponse.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker();
                                log(isWinAttacker ? TextResourceTable.Get("[LocalRaidBattleWinMessage]") : TextResourceTable.Get("[LocalRaidBattleLoseMessage]"));
                                battleResultResponse.BattleRewardResult?.FixedItemList?.PrintUserItems(log);
                                battleResultResponse.BattleRewardResult?.DropItemList?.PrintUserItems(log);
                            }
                            catch (Exception e)
                            {
                                log(e.Message);
                            }

                            break;
                        }
                    }
                }
            }
            finally
            {
                keepaliveCts.Cancel();
                client.ClearLocalRaidReceiver();
                await client.DisposeAsync();
            }

            long GetQuestId(GetLocalRaidInfoResponse response)
            {
                if (response.OpenEventLocalRaidQuestIds.Count > 0)
                {
                    var localRaidQuestMbs = response.OpenEventLocalRaidQuestIds.Select(LocalRaidQuestTable.GetById).ToList();
                    var localRaidQuestMb = localRaidQuestMbs.OrderByDescending(d =>
                    {
                        if (response.ClearCountDict.TryGetValue(d.Id, out var c) && c > 0)
                        {
                            return d.FixedBattleRewards[0].ItemCount;
                        }

                        return d.FixedBattleRewards[0].ItemCount + d.FirstBattleRewards[0].ItemCount;
                    }).First();
                    return localRaidQuestMb.Id;
                }
                else
                {
                    var localRaidQuestMbs = response.OpenLocalRaidQuestIds.Select(LocalRaidQuestTable.GetById).ToList();
                    var rewardItems = _writableGameConfig.Value.LocalRaid.RewardItems;
                    if (rewardItems.Count == 0)
                    {
                        return localRaidQuestMbs.OrderByDescending(d => d.Level).First().Id;
                    }

                    return localRaidQuestMbs.Select(d =>
                    {
                        var count = 0D;
                        foreach (var rewardItem in rewardItems)
                        {
                            foreach (var reward in d.FixedBattleRewards)
                            {
                                if (reward.IsEqual(rewardItem.ItemType, rewardItem.ItemId))
                                {
                                    count += reward.ItemCount * rewardItem.Weight;
                                }
                            }
                        }

                        return new {quest = d, count};
                    }).OrderByDescending(d => d.count).First().quest.Id;
                }
            }
        });
    }


    public async Task ExecuteAllQuickAction()
    {
        await GetLoginBonus();
        await GetVipGift();
        await ReceiveMonthlyBoost();
        await GetAutoBattleReward();
        await BulkTransferFriendPoint();
        await PresentReceiveItem();
        if (GameConfig.AutoJob.AutoReinforcementEquipmentOneTime) await ReinforcementEquipmentOneTime();
        await BattleBossQuick();
        await InfiniteTowerQuick();
        await BossHishSpeedBattle();
        await GuildCheckin();
        await GuildRaid();
        await BountyQuestRewardAuto();
        await BountyQuestStartAuto();
        if (GameConfig.AutoJob.AutoDungeonBattle) await AutoDungeonBattle();
        await CompleteMissions();
        await RewardMissonActivity();
        if (GameConfig.AutoJob.AutoUseItems) await AutoUseItems();
        if (GameConfig.AutoJob.AutoFreeGacha) await FreeGacha();
        if (GameConfig.AutoJob.AutoUseItems) await AutoUseItems();
        if (GameConfig.AutoJob.AutoRankUpCharacter) await AutoRankUpCharacter();
    }
}