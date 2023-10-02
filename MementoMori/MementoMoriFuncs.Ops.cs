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
using System.Xml.Linq;
using MementoMori.Ortega.Share.Data.Auth;

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

    private CancellationTokenSource _cancellationTokenSource;

    public ObservableCollection<string> MesssageList { get; } = new();

    private const int Max_Err_Count = 20;

    private PlayerDataInfo _lastPlayerDataInfo;

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

    public async Task SyncUserData()
    {
        await UserGetUserData();
    }

    private void AddLog(string message)
    {
        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
        MesssageList.Insert(0, message);
        if (MesssageList.Count > 1000) MesssageList.RemoveAt(MesssageList.Count - 1);
    }

    private ConcurrentQueue<Func<Action<string>, CancellationToken, Task>> _funcs = new();

    public async Task ExecuteQuickAction(Func<Action<string>, CancellationToken, Task> func)
    {
        if (!IsQuickActionExecuting)
        {
            IsQuickActionExecuting = true;
            _cancellationTokenSource = new CancellationTokenSource();
            MesssageList.Clear();
            _funcs.Clear();
            _funcs.Enqueue(func);
        }
        else
        {
            _funcs.Enqueue(func);
            return;
        }

        _ = Task.Run(async () =>
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
                if (_funcs.TryDequeue(out var func1))
                    try
                    {
                        await func1(AddLog, _cancellationTokenSource.Token);
                    }
                    catch (Exception e)
                    {
                        AddLog(e.ToString());
                        break;
                    }
                else
                    break;

            IsQuickActionExecuting = false;
        });
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
                var bonus = await GetResponse<ReceiveDailyLoginBonusRequest, ReceiveDailyLoginBonusResponse>(new ReceiveDailyLoginBonusRequest() {ReceiveDay = DateTime.Now.Day});
                log("每日登录奖励：\n");
                bonus.RewardItemList.PrintUserItems(log);
                await GetMonthlyLoginBonusInfo();
            }
            else
            {
                log("每日登录奖励已领取过了");
            }

            var monthlyLoginBonusMb = Masters.MonthlyLoginBonusTable.GetById(MonthlyLoginBonusInfo.MonthlyLoginBonusId);
            var monthlyLoginBonusRewardListMb = Masters.MonthlyLoginBonusRewardListTable.GetById(monthlyLoginBonusMb.RewardListId);
            foreach (var loginCountRewardInfo in monthlyLoginBonusRewardListMb.LoginCountRewardList)
                // 登录次数达到 && 没有领取过
                if (loginCountRewardInfo.DayCount <= MonthlyLoginBonusInfo.ReceivedDailyRewardDayList.Count &&
                    !MonthlyLoginBonusInfo.ReceivedLoginCountRewardDayList.Contains(loginCountRewardInfo.DayCount))
                {
                    var resp = await GetResponse<ReceiveLoginCountBonusRequest, ReceiveLoginCountBonusResponse>(new ReceiveLoginCountBonusRequest
                    {
                        ReceiveDayCount = loginCountRewardInfo.DayCount
                    });
                    log($"领取的登录次数 {loginCountRewardInfo.DayCount} 奖励：\n");
                    resp.RewardItemList.PrintUserItems(log);
                }

            await GetMonthlyLoginBonusInfo();
        });
    }

    public async Task GetVipGift()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            if (UserSyncData.ExistVipDailyGift == true)
            {
                var bonus = await GetResponse<GetDailyGiftRequest, GetDailyGiftResponse>(new GetDailyGiftRequest());
                log("领取每日VIP奖励：");
                bonus.ItemList.PrintUserItems(log);
            }
            else
            {
                log("VIP 奖励领取过了");
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
            log("领取自动战斗奖励奖励：");
            log($"战斗次数 {bonus.AutoBattleRewardResult.BattleCountAll}");
            log($"胜利次数 {bonus.AutoBattleRewardResult.BattleCountWin}");
            log($"总时间 {TimeSpan.FromMilliseconds(bonus.AutoBattleRewardResult.BattleTotalTime)}");
            log($"领民金币 {bonus.AutoBattleRewardResult.GoldByPopulation}");
            log($"领民潜能珠宝 {bonus.AutoBattleRewardResult.PotentialJewelByPopulation}");
            log("固定掉落");
            bonus.AutoBattleRewardResult.BattleRewardResult.FixedItemList.PrintUserItems(log);
            log("随机掉落");
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
                log("友情点发送接收成功");
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
                        log("领取礼物箱：");
                        resp.ResultItems.Select(d => d.Item).PrintUserItems(log);
                    }
                    catch (ApiErrorException e) when (e.ErrorCode == ErrorCode.PresentReceiveOverLimitCountPresent)
                    {
                        log(e.Message);
                        foreach (var presentItem in getListResponse.userPresentDtoInfos.SelectMany(d => d.ItemList))
                        {
                            if (presentItem.Item.ItemType == ItemType.QuestQuickTicket)
                            {
                                var count = UserSyncData.UserItemDtoInfo.FirstOrDefault(d => d.ItemType == presentItem.Item.ItemType && d.ItemId == presentItem.Item.ItemId)?.ItemCount ?? 0;
                                var itemMb = Masters.ItemTable.GetByItemTypeAndItemId(presentItem.Item.ItemType, presentItem.Item.ItemId);
                                var maxItemCount = itemMb.MaxItemCount;
                                if (count >= maxItemCount) continue;

                                var name = Masters.TextResourceTable.Get(itemMb.NameKey);
                                var useCount = (int) Math.Floor(maxItemCount * 0.1);
                                log($"使用达到上限的物品: {name}×{useCount}, {count}/{maxItemCount}");
                                var response = await GetResponse<UseAutoBattleRewardItemRequest, UseAutoBattleRewardItemResponse>(new UseAutoBattleRewardItemRequest()
                                {
                                    ItemType = (QuestQuickTicketType) itemMb.ItemType,
                                    UseCount = useCount
                                });
                                response.RewardItemList.PrintUserItems(log);
                                usedItem = true;
                                break;
                            }

                            if (presentItem.Item.ItemType == ItemType.Equipment)
                            {
                                var count = UserSyncData.UserItemDtoInfo.FirstOrDefault(d => d.ItemType == presentItem.Item.ItemType && d.ItemId == presentItem.Item.ItemId)?.ItemCount ?? 0;
                                var maxItemCount = 999;
                                if (maxItemCount != count) continue;

                                var name = Masters.TextResourceTable.Get(Masters.EquipmentTable.GetById(presentItem.Item.ItemId).NameKey);
                                var useCount = (int) Math.Floor(maxItemCount * 0.1);
                                log($"使用达到上限的物品: {name}×{useCount}, {count}/{maxItemCount}");
                                var response = await GetResponse<CastRequest, CastResponse>(new CastRequest {UserEquipment = new UserEquipment(presentItem.Item.ItemId, useCount)});
                                response.ResultItemList.PrintUserItems(log);
                                usedItem = true;
                                break;
                            }
                        }
                    }
                else
                    log("礼物箱没有可领取的");
            } while (usedItem);
        });
    }

    public async Task BattleBossQuick()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            try
            {
                var bossQuickResponse = await GetResponse<BossQuickRequest, BossQuickResponse>(
                    new BossQuickRequest()
                    {
                        QuestId = UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId,
                        QuickCount = 3
                    });
                if (bossQuickResponse.BattleRewardResult == null)
                {
                    log("快速战斗奖励为空");
                    return;
                }

                log("Boss 快速战斗奖励：\n");
                bossQuickResponse.BattleRewardResult.FixedItemList.PrintUserItems(log);
                bossQuickResponse.BattleRewardResult.DropItemList.PrintUserItems(log);
            }
            catch (ApiErrorException e) when (e.ErrorCode == ErrorCode.BattleBossNotEnoughBossChallengeCount)
            {
                log(e.Message);
            }
        });
    }

    public async Task InfiniteTowerQuick()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            try
            {
                var tower = UserSyncData.UserTowerBattleDtoInfos.First(d => d.TowerType == TowerType.Infinite);
                log("无穷之塔战斗奖励：\n");

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
            log("竞技场战斗奖励：\n");
            for (var i = 0; i < 5; i++)
            {
                var pvpInfoResponse = await GetResponse<GetPvpInfoRequest, GetPvpInfoResponse>(
                    new GetPvpInfoRequest());

                if (UserSyncData.UserBattlePvpDtoInfo.PvpTodayCount >= OrtegaConst.BattlePvp.MaxPvpBattleFreeCount)
                {
                    log("剩餘挑戰次數不足");
                    return;
                }

                var pvpRankingPlayerInfo = pvpInfoResponse.MatchingRivalList.OrderBy(d => d.DefenseBattlePower).First();

                var pvpStartResponse = await GetResponse<PvpStartRequest, PvpStartResponse>(new PvpStartRequest()
                {
                    RivalPlayerRank = pvpRankingPlayerInfo.CurrentRank,
                    RivalPlayerId = pvpRankingPlayerInfo.PlayerInfo.PlayerId
                });

                pvpStartResponse.BattleRewardResult.FixedItemList.PrintUserItems(log);
                pvpStartResponse.BattleRewardResult.DropItemList.PrintUserItems(log);
            }
        });
    }

    public async Task BountyQuestRewardAuto()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            log("祈愿之泉完成奖励：\n");
            var getListResponse = await GetResponse<BountyQuestGetListRequest, BountyQuestGetListResponse>(
                new BountyQuestGetListRequest());

            var questIds = getListResponse.UserBountyQuestDtoInfos
                .Where(d => d.BountyQuestEndTime > 0 && !d.IsReward && DateTimeOffset.Now.AddHours(1).ToUnixTimeMilliseconds() > d.BountyQuestEndTime)
                .Select(d => d.BountyQuestId).ToList();

            if (questIds.Count > 0)
            {
                var rewardResponse = await GetResponse<RewardRequest, RewardResponse>(new RewardRequest() {BountyQuestIds = questIds, ConsumeCurrency = 0, IsQuick = false});
                rewardResponse.RewardItems.PrintUserItems(log);
                await GetResponse<BountyQuestGetListRequest, BountyQuestGetListResponse>(new BountyQuestGetListRequest());
            }
            else
            {
                log("没有可以领取的");
            }

            await GetBountyRequestInfo();
        });
    }

    public async Task BountyQuestStartAuto()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var response1 = await GetResponse<BountyQuestGetListRequest, BountyQuestGetListResponse>(new BountyQuestGetListRequest());
            if (_gameConfig.BountyQuestAuto.TargetItems.Count > 0 && !BountyRequestForceAll)
            {
                var itemNames = string.Join(",", _gameConfig.BountyQuestAuto.TargetItems.Select(ItemUtil.GetItemName));
                log($"祈愿之泉: 指定了目标道具 {itemNames}");
                log("祈愿之泉: 正在派遣目标道具任务");
                var bountyQuestStartInfos = BountyQuestAutoFormationUtil.CalcAutoFormation(response1, UserSyncData, _gameConfig.BountyQuestAuto);
                foreach (var bountyQuestStartInfo in bountyQuestStartInfos)
                {
                    var startResponse = await GetResponse<BountyQuestStartRequest, BountyQuestStartResponse>(
                        new BountyQuestStartRequest {BountyQuestStartInfos = new List<BountyQuestStartInfo>() {bountyQuestStartInfo}});
                    log($"已派遣 {bountyQuestStartInfo.BountyQuestId}");
                }
            }
            else
            {
                log($"祈愿之泉: 未指定目标道具 或 强制派遣, 派遣所有任务");
                var bountyQuestStartInfos = BountyQuestAutoFormationUtil.CalcAutoFormation(response1, UserSyncData, _gameConfig.BountyQuestAuto, true);
                foreach (var bountyQuestStartInfo in bountyQuestStartInfos)
                {
                    var startResponse = await GetResponse<BountyQuestStartRequest, BountyQuestStartResponse>(
                        new BountyQuestStartRequest {BountyQuestStartInfos = new List<BountyQuestStartInfo>() {bountyQuestStartInfo}});
                    log($"已派遣 {bountyQuestStartInfo.BountyQuestId}");
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
                    Equipment = d, EquipmentMB = Masters.EquipmentTable.GetById(d.EquipmentId)
                });

                var sEquipments = equipments.Where(d =>
                        d.Equipment.CharacterGuid == "" && // 未装备
                        d.Equipment.MatchlessSacredTreasureLv == 1 && // 魔装等级为 1
                        (d.EquipmentMB.RarityFlags & EquipmentRarityFlags.S) != 0 // 稀有度为 S
                ).ToList();

                if (sEquipments.Count == 0)
                {
                    log("批量精炼");

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
                        log("没有可以精炼的装备了");
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
            var equipmentMb = Masters.EquipmentTable.GetById(d.ItemId);
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
                var equipmentMb = Masters.EquipmentTable.GetById(equipItem.ItemId);
                log($"为装备找可穿戴的角色, 脱穿一次 {equipmentMb.Memo}");
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
                        return true;

                    return false;
                }).First();


                // 获取角色某个位置的装备, 可能没有装备
                var replacedEquip = usersyncData.UserSyncData.UserEquipmentDtoInfos.Where(d =>
                {
                    var byId = Masters.EquipmentTable.GetById(d.EquipmentId);
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
                    Equipment = d, EquipmentMB = Masters.EquipmentTable.GetById(d.EquipmentId)
                });
                var sEquipments = equipments.Where(d =>
                        d.Equipment.CharacterGuid == "" && // 未装备
                        d.Equipment.LegendSacredTreasureLv == 1 && // 圣装等级为 1
                        (d.EquipmentMB.RarityFlags & EquipmentRarityFlags.S) != 0 // 稀有度为 S
                ).ToList();

                if (sEquipments.Count == 0)
                {
                    log("没有圣装了");
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
            var equipmentMb = Masters.EquipmentTable.GetById(d.EquipmentId);
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
            log($"继承完成 {mb.Memo}=>{userEquipmentDtoInfo.Guid}");
        }
        else
        {
            log("没有找到可被继承的D装");
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
                    log($"月卡免费高速战斗 {availableCount} 次");
                    await BattleQuick(log, QuestQuickExecuteType.Privilege, (int) availableCount);
                }
            }

            // 每天有一次免费
            if (autoResponse.UserBattleAuto.QuickTodayUseCurrencyCount >= 1)
            {
                log("今日没有免费高速战斗次数了");
            }
            else
            {
                log("冒险高速战斗奖励：\n");
                await BattleQuick(log, QuestQuickExecuteType.Currency, 1);
            }
        });

        async Task BattleQuick(Action<string> log, QuestQuickExecuteType type, int count)
        {
            var req = new QuickRequest() {QuestQuickExecuteType = type, QuickCount = count};
            var quickResponse = await GetResponse<QuickRequest, QuickResponse>(req);

            log($"金币 {quickResponse.AutoBattleRewardResult.GoldByPopulation}");
            log($"潜能宝珠 {quickResponse.AutoBattleRewardResult.PotentialJewelByPopulation}");
            log($"角色经验 {quickResponse.AutoBattleRewardResult.BattleRewardResult.CharacterExp}");
            log($"额外金币 {quickResponse.AutoBattleRewardResult.BattleRewardResult.ExtraGold}");
            log($"用户经验 {quickResponse.AutoBattleRewardResult.BattleRewardResult.PlayerExp}");
            log($"升级 {quickResponse.AutoBattleRewardResult.BattleRewardResult.RankUp}");

            quickResponse.AutoBattleRewardResult.BattleRewardResult.FixedItemList.PrintUserItems(log);
            quickResponse.AutoBattleRewardResult.BattleRewardResult.DropItemList.PrintUserItems(log);
        }
    }

    public async Task AutoDungeonBattle()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            await AutoDungeonBattle(log, token);
            log("完成");
        });
    }

    public async Task GetMyPage()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            Mypage = await GetResponse<GetMypageRequest, GetMypageResponse>(new GetMypageRequest());
            log("完成");
        });
    }

    public async Task Debug()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var bossResponse = await GetResponse<BossRequest, BossResponse>(new BossRequest() {QuestId = UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId + 1});
            log(bossResponse.ToJson(true));
        });
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

            log("没有可以执行的抽卡了");

            async Task<bool> DoFreeGacha()
            {
                var gachaListResponse = await GetResponse<GachaGetListRequest, GachaGetListResponse>(new GachaGetListRequest());
                foreach (var gachaCaseInfo in gachaListResponse.GachaCaseInfoList)
                {
                    var userItems = UserSyncData.UserItemDtoInfo.ToList();
                    var buttonInfo = gachaCaseInfo.GachaButtonInfoList.OrderByDescending(d => d.LotteryCount)
                        .FirstOrDefault(d => _gameConfig.GachaConfig.AutoGachaConsumeUserItems.Exists(consumeUserItem => CheckCount(d, userItems, consumeUserItem.ItemType, consumeUserItem.ItemId)));
                    if (buttonInfo == null) continue;

                    var gachaCaseMb = Masters.GachaCaseTable.GetById(gachaCaseInfo.GachaCaseId);
                    var itemMb = Masters.ItemTable.GetByItemTypeAndItemId(buttonInfo.ConsumeUserItem.ItemType, buttonInfo.ConsumeUserItem.ItemId);
                    var name = Masters.TextResourceTable.Get(itemMb.NameKey);
                    log($"抽卡 {gachaCaseMb.Memo} {buttonInfo.LotteryCount}次 消耗 {buttonInfo.ConsumeUserItem.ItemCount}个 {name}");
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

    public async Task GuildCheckin()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var response1 = await GetResponse<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
            log($"公会 Id {response1.GuildId}");
            var response2 = await GetResponse<GetGuildBaseInfoRequest, GetGuildBaseInfoResponse>(new GetGuildBaseInfoRequest() {BelongGuildId = response1.GuildId});
            log("签到成功");
            response2.UserSyncData.GivenItemCountInfoList.PrintUserItems(log);
        });
    }

    public async Task GuildRaid()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var response1 = await GetResponse<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
            log($"公会 Id {response1.GuildId}");
            bool hasRaid;
            do
            {
                hasRaid = false;
                var response2 = await GetResponse<GetGuildRaidInfoRequest, GetGuildRaidInfoResponse>(new GetGuildRaidInfoRequest() {BelongGuildId = response1.GuildId});
                foreach (var info in response2.GuildRaidInfos)
                {
                    if (info.IsOpen && (info.UserGuildRaidDtoInfo == null || info.UserGuildRaidDtoInfo is {ChallengeCount: < 2}))
                    {
                        if (info.UserGuildRaidPreviousDtoInfo != null)
                        {
                            var response3 = await GetResponse<QuickStartGuildRaidRequest, QuickStartGuildRaidResponse>(new QuickStartGuildRaidRequest()
                                {BelongGuildId = response1.GuildId, GuildRaidBossType = info.GuildRaidDtoInfo.BossType});
                            log($"战斗结果: {response3.BattleSimulationResult.BattleEndInfo.IsWinAttacker()}");
                            log("固定掉落");
                            response3.BattleRewardResult.FixedItemList.PrintUserItems(log);
                            log("随机掉落");
                            response3.BattleRewardResult.DropItemList.PrintUserItems(log);
                        }
                        else
                        {
                            var response3 = await GetResponse<StartGuildRaidRequest, StartGuildRaidResponse>(new StartGuildRaidRequest()
                                {BelongGuildId = response1.GuildId, GuildRaidBossType = info.GuildRaidDtoInfo.BossType});
                            log($"战斗结果: {response3.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker()}");
                            log("固定掉落");
                            response3.BattleRewardResult.FixedItemList.PrintUserItems(log);
                            log("随机掉落");
                            response3.BattleRewardResult.DropItemList.PrintUserItems(log);
                        }

                        hasRaid = true;
                    }

                    if (info.IsExistWorldDamageReward)
                    {
                        var bossMb = Masters.GuildRaidBossTable.GetByGuildRaidBossType(info.GuildRaidDtoInfo.BossType);
                        if (bossMb != null)
                        {
                            var worldRewardInfoResponse =
                                await GetResponse<GetGuildRaidWorldRewardInfoRequest, GetGuildRaidWorldRewardInfoResponse>(new GetGuildRaidWorldRewardInfoRequest() {GuildRaidBossId = bossMb.Id});
                            var guildRaidRewardMb = Masters.GuildRaidRewardTable.GetByBossId(bossMb.Id);
                            foreach (var worldDamageBar in guildRaidRewardMb.WorldDamageBarRewards)
                            {
                                var worldRewardInfo = worldRewardInfoResponse.WorldRewardInfos.FirstOrDefault(d => d.GoalDamage == worldDamageBar.GoalDamage);
                                if (worldRewardInfoResponse.TotalWorldDamage >= worldDamageBar.GoalDamage && (worldRewardInfo == null || !worldRewardInfo.IsReceived))
                                {
                                    var resp = await GetResponse<GiveGuildRaidWorldRewardItemRequest, GiveGuildRaidWorldRewardItemResponse>(
                                        new GiveGuildRaidWorldRewardItemRequest {GoalDamage = worldDamageBar.GoalDamage, GuildRaidBossId = bossMb.Id});
                                    log($"领取世界伤害奖励 {worldDamageBar.GoalDamage}");
                                    resp.RewardItems.PrintUserItems(log);
                                }
                            }
                        }
                    }
                }
            } while (hasRaid);

            log("扫荡讨伐战完成");
        });
    }

    public async Task AutoBossRequest()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var totalCount = 0;
            var winCount = 0;
            var errCount = 0;
            while (!token.IsCancellationRequested)
                try
                {
                    var targetQuestId = UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId + 1;
                    var bossResponse = await GetResponse<BossRequest, BossResponse>(new BossRequest() {QuestId = targetQuestId});
                    var win = bossResponse.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker();
                    totalCount++;
                    if (win)
                    {
                        var nextQuestResponse = await GetResponse<NextQuestRequest, NextQuestResponse>(new NextQuestRequest());
                        winCount++;
                    }

                    var info = Masters.QuestTable.GetById(targetQuestId).Memo;
                    var result = win ? "胜" : "负";
                    var m = $"挑战 {info} boss 一次：{result} 总次数：{totalCount} 胜利次数：{winCount}, Err: {errCount}";
                    log(m);
                    if (_gameConfig.AutoRequestDelay > 0) await Task.Delay(_gameConfig.AutoRequestDelay, token);
                }
                catch (Exception e)
                {
                    log(e.Message);
                    errCount++;
                    if (errCount > Max_Err_Count)
                    {
                        log($"错误达到了 {Max_Err_Count} 次, 中断");
                        return;
                    }

                    if (e is ApiErrorException) await AuthLogin(_lastPlayerDataInfo);
                }
        });
    }

    public async Task AutoInfiniteTowerRequest()
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
                        log($"{SelectedAutoTowerType} 塔挑战次数达到上限");
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

                    var name = Masters.TextResourceTable.Get(SelectedAutoTowerType);
                    var result = win ? "胜" : "负";

                    if (SelectedAutoTowerType == TowerType.Infinite)
                        log($"挑战 {name} {targetQuestId} 层一次：{result}, 总次数：{totalCount} 胜利次数：{winCount}, Err: {errCount}");
                    else
                        log($"挑战 {name} {targetQuestId}层一次：{result}, {towerBattleDtoInfo.TodayClearNewFloorCount}/10 总次数：{totalCount} 胜利次数：{winCount}, Err: {errCount}");
                    if (_gameConfig.AutoRequestDelay > 0) await Task.Delay(_gameConfig.AutoRequestDelay, token);
                }
                catch (Exception e)
                {
                    log(e.Message);
                    errCount++;
                    if (errCount > Max_Err_Count)
                    {
                        log($"错误达到了 {Max_Err_Count} 次, 中断");
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
                    $"打磨装备 {totalCount} 耐力 {equipment.AdditionalParameterHealth} 魔力 {equipment.AdditionalParameterIntelligence} 力量 {equipment.AdditionalParameterMuscle} 战技 {equipment.AdditionalParameterEnergy}";
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
        var response = await GetResponse<GetMissionInfoRequest, GetMissionInfoResponse>(new GetMissionInfoRequest()
            {TargetMissionGroupList = new List<MissionGroupType>() {MissionGroupType.Daily, MissionGroupType.Weekly, MissionGroupType.Main}});
        MissionInfoDict = response.MissionInfoDict;
    }

    public async Task GetBountyRequestInfo()
    {
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
            AddLog("没有可以重置的赏金任务");
        }
    }

    public async Task GetMonthlyLoginBonusInfo()
    {
        var response = await GetResponse<GetMonthlyLoginBonusInfoRequest, GetMonthlyLoginBonusInfoResponse>(new GetMonthlyLoginBonusInfoRequest());
        MonthlyLoginBonusInfo = response;
    }

    public async Task GetNoticeInfoList()
    {
        var response = await GetResponse<GetNoticeInfoListRequest, GetNoticeInfoListResponse>(new GetNoticeInfoListRequest()
        {
            AccessType = NoticeAccessType.Title,
            CategoryType = NoticeCategoryType.NoticeTab,
            CountryCode = "CN",
            LanguageType = LanguageType.zhTW,
            UserId = _authOption.UserId
        });
        NoticeInfoList = response.NoticeInfoList;
        var response2 = await GetResponse<GetNoticeInfoListRequest, GetNoticeInfoListResponse>(new GetNoticeInfoListRequest()
        {
            AccessType = NoticeAccessType.MyPage,
            CategoryType = NoticeCategoryType.EventTab,
            CountryCode = "CN",
            LanguageType = LanguageType.zhTW,
            UserId = _authOption.UserId
        });
        EventInfoList = response2.NoticeInfoList;
    }

    public TowerType[] GetAvailableTower()
    {
        var now = DateTimeOffset.UtcNow.ToOffset(_timeManager.DiffFromUtc) - TimeSpan.FromHours(4);
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
                .FirstOrDefault(d => !string.IsNullOrEmpty(d.CharacterGuid) && Masters.EquipmentTable.GetById(d.EquipmentId).EquipmentLv > d.ReinforcementLv);
            if (equipmentDtoInfo != null)
            {
                var response = await GetResponse<ReinforcementRequest, ReinforcementResponse>(new ReinforcementRequest() {EquipmentGuid = equipmentDtoInfo.Guid, NumberOfTimes = 1});
                log($"强化一次完成");
            }
        });
    }

    public async Task CompleteMissions()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            await GetMissionInfo();
            var missionIds = MissionInfoDict.Values.SelectMany(d => d.UserMissionDtoInfoDict.Values.SelectMany(x => x.SelectMany(f => f.MissionStatusHistory[MissionStatusType.NotReceived]))).ToList();
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
                        var rewardMb = Masters.TotalActivityMedalRewardTable.GetById(rewardId);
                        log($"领取 {pair.Key} 的 {rewardMb.RequiredActivityMedalCount} 奖励");
                        var response = await GetResponse<RewardMissionActivityRequest, RewardMissionActivityResponse>(
                            new RewardMissionActivityRequest() {MissionGroupType = pair.Key, RequiredCount = rewardMb.RequiredActivityMedalCount});
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
                    var treasureChestMb = Masters.TreasureChestTable.GetById(userItemDtoInfo.ItemId);
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

            log("没有可使用的物品了");
        });

        async Task OpenTreasure(long openCount, TreasureChestMB treasureChestMb, Action<string> log)
        {
            var response = await GetResponse<OpenTreasureChestRequest, OpenTreasureChestResponse>(new OpenTreasureChestRequest()
            {
                OpenCount = (int) openCount,
                TreasureChestId = treasureChestMb.Id
            });
            log($"打开物品 {Masters.TextResourceTable.Get(treasureChestMb.NameKey)} x {openCount}");
            response.RewardItems.PrintUserItems(log);
        }
    }

    public async Task AutoRankUpCharacter()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            await RankUp(CharacterRarityFlags.R, 3, log);
            await RankUp(CharacterRarityFlags.SR, 2, log);
            log("角色合成完成");
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
                Masters.CharacterTable.GetCharacterName(main.CharacterId, out var name1, out var name2);
                if (!name2.IsNullOrEmpty()) name1 = $"[{name2}] {name1}";

                log($"消耗了角色 {name1} X {count}, {Masters.TextResourceTable.Get(main.RarityFlags)}");
            }
        }
    }

    public async Task ExecuteAllQuickAction()
    {
        await GetLoginBonus();
        await GetVipGift();
        await GetAutoBattleReward();
        await BulkTransferFriendPoint();
        await PresentReceiveItem();
        if (_gameConfig.AutoJob.AutoReinforcementEquipmentOneTime) await ReinforcementEquipmentOneTime();
        await BattleBossQuick();
        await InfiniteTowerQuick();
        await BossHishSpeedBattle();
        await GuildCheckin();
        await GuildRaid();
        await BountyQuestRewardAuto();
        await BountyQuestStartAuto();
        if (_gameConfig.AutoJob.AutoDungeonBattle) await AutoDungeonBattle();
        await CompleteMissions();
        await RewardMissonActivity();
        if (_gameConfig.AutoJob.AutoUseItems) await AutoUseItems();
        if (_gameConfig.AutoJob.AutoFreeGacha) await FreeGacha();
        if (_gameConfig.AutoJob.AutoUseItems) await AutoUseItems();
        if (_gameConfig.AutoJob.AutoRankUpCharacter) await AutoRankUpCharacter();
    }
}