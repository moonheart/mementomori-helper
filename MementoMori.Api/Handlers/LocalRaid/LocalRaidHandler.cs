using System.Diagnostics;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;
using MementoMori.Api.Services;
using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Network.MagicOnion.Client;
using MementoMori.Ortega.Network.MagicOnion.Interface;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.LocalRaid;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share.MagicOnionShare.Response;
using MementoMori.Ortega.Share.Master;
using MementoMori.Ortega.Share.Master.Interfaces;

namespace MementoMori.Api.Handlers.LocalRaid;

/// <summary>
/// 自动时空洞窟（LocalRaid）处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class LocalRaidHandler : IGameActionHandler, IMagicOnionLocalRaidNotificaiton
{
    private readonly ILogger<LocalRaidHandler> _logger;
    private readonly JobLogger _jobLogger;
    private readonly IServiceProvider _serviceProvider;

    public string ActionName => Text.T("ActionLocalRaid");

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        // 获取配置
        using var scope = _serviceProvider.CreateScope();
        var settingService = scope.ServiceProvider.GetRequiredService<PlayerSettingService>();
        var localRaid = await settingService.GetLocalRaidSettingsAsync(userId);

        var rewardItems = localRaid.RewardItems;
        if (rewardItems.Count == 0)
        {
            // 默认奖励权重
            rewardItems.AddRange(new[]
            {
                new GameConfig.WeightedItem { ItemType = ItemType.ExchangePlaceItem, ItemId = 4, Weight = 4 }, // 符石兑换券
                new GameConfig.WeightedItem { ItemType = ItemType.CharacterTrainingMaterial, ItemId = 2, Weight = 3 }, // 潜能宝珠
                new GameConfig.WeightedItem { ItemType = ItemType.EquipmentReinforcementItem, ItemId = 2, Weight = 2.5 }, // 强化秘药
                new GameConfig.WeightedItem { ItemType = ItemType.CharacterTrainingMaterial, ItemId = 1, Weight = 2 }, // 经验珠
                new GameConfig.WeightedItem { ItemType = ItemType.EquipmentReinforcementItem, ItemId = 1, Weight = 1 } // 强化水
            });
        }

        var createRoom = localRaid.SelfCreateRoom;
        var questStep = 0; // 0 = 最佳关卡; DropToLowerQuest 时每一步降一级

        // 1. 获取活动信息
        var localRaidInfo = await nm.SendRequest<GetLocalRaidInfoRequest, GetLocalRaidInfoResponse>(new GetLocalRaidInfoRequest());
        var questId = GetQuestId(localRaidInfo, rewardItems, localRaid.QuestLevel, questStep);

        await _jobLogger.LogAsync(userId, Text.T("LocalRaidStarting", questId, createRoom));

        // 2. 连接 MagicOnion
        var joinWait = TimeSpan.FromMinutes(Math.Max(1, localRaid.JoinWaitMinutes));
        var lowerAfter = TimeSpan.FromMinutes(Math.Max(1, localRaid.LowerQuestAfterMinutes));
        var roomWait = TimeSpan.FromMinutes(Math.Max(1, localRaid.RoomWaitMinutes));
        var cts = new CancellationTokenSource(TimeSpan.FromMinutes(Math.Max(1, localRaid.MaxTaskMinutes))); // 整个任务的时长上限, 防止卡住
        var joinSw = Stopwatch.StartNew();
        var logAction = new Action<string>(async msg => await _jobLogger.LogAsync(userId, msg));

        // 加入模式等不到房间: 可选先降一级关卡再找 (DropToLowerQuest), 再可选自己开房单刷 (HostWhenNoRoom), 否则放弃.
        // 自己开房连输 MaxSoloLosses 次: 可选降一级关卡继续, 否则停止. 全部受 MaxTaskMinutes 限制.
        while (!cts.IsCancellationRequested)
        {
        var soloLosses = 0;
        OrtegaMagicOnionClient? client = null;
        try
        {
            client = await ConnectOnionAsync(nm, logAction, cts.Token);
            if (client == null) return;

            // 这一轮加入等待: 剩余的加入时间, 或者 (可降级时) 先等 LowerQuestAfterMinutes
            var remaining = joinWait - joinSw.Elapsed;
            if (remaining < TimeSpan.FromMinutes(1)) remaining = TimeSpan.FromMinutes(1);
            var passWait = localRaid.DropToLowerQuest && HasLowerQuest(localRaidInfo, rewardItems, localRaid.QuestLevel, questStep) && lowerAfter < remaining
                ? lowerAfter
                : remaining;

            LocalRaidBaseReceiver receiver = createRoom 
                ? new LocalRaidCreateRoomReceiver(client, logAction, localRaid.WaitSeconds, cts.Token)
                : new LocalRaidJoinRoomReceiver(client, logAction, passWait, cts.Token);

            receiver.QuestId = questId;
            client.SetupLocalRaid(receiver, receiver);

            // 3. 进入循环
            while (!cts.Token.IsCancellationRequested)
            {
                // 获取最新信息
                localRaidInfo = await nm.SendRequest<GetLocalRaidInfoRequest, GetLocalRaidInfoResponse>(new GetLocalRaidInfoRequest());
                questId = GetQuestId(localRaidInfo, rewardItems, localRaid.QuestLevel, questStep);
                receiver.QuestId = questId;
                receiver.IsBattleStarted = false;

                if (createRoom)
                {
                    await _jobLogger.LogAsync(userId, Text.T("LocalRaidCreatingRoom"));
                    client.SendLocalRaidOpenRoom(LocalRaidRoomConditionsType.None, questId, 0, 0);
                }
                else
                {
                    await _jobLogger.LogAsync(userId, Text.T("LocalRaidFindingRoom"));
                    client.SendLocalRaidJoinRandomRoom(questId);
                }

                // 等待战斗开始
                while (!cts.Token.IsCancellationRequested)
                {
                    await Task.Delay(2000, cts.Token);
                    if (receiver.IsNoRemainingChallenges)
                    {
                        await _jobLogger.LogAsync(userId, Text.T("LocalRaidNoAttempts"));
                        return;
                    }
                    if (receiver.IsMaxTimeExceeded)
                    {
                        await _jobLogger.LogAsync(userId, Text.T("LocalRaidWaitTimeout"));
                        goto PassDone;
                    }

                    // 进了房间但一直不开战 (房主挂机等), 退出再找别的房间
                    if (receiver is LocalRaidJoinRoomReceiver joined && !receiver.IsBattleStarted && joined.InRoomSw?.Elapsed > roomWait)
                    {
                        joined.InRoomSw = null;
                        await _jobLogger.LogAsync(userId, Text.T("LocalRaidRoomStuck", localRaid.RoomWaitMinutes));
                        client.SendLocalRaidLeaveRoom();
                    }

                    if (receiver.IsBattleStarted)
                    {
                        if (receiver is LocalRaidJoinRoomReceiver started) started.InRoomSw = null;
                        await Task.Delay(3000, cts.Token); // 等待服务器同步结果
                        try
                        {
                            var battleResult = await nm.SendRequest<GetLocalRaidBattleResultRequest, GetLocalRaidBattleResultResponse>(new GetLocalRaidBattleResultRequest());
                            bool isWin = battleResult.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker();
                            await _jobLogger.LogAsync(userId, isWin ? Text.T("LocalRaidVictory") : Text.T("LocalRaidDefeat"));
                            soloLosses = isWin ? 0 : soloLosses + 1;
                            if (createRoom && localRaid.MaxSoloLosses > 0 && soloLosses >= localRaid.MaxSoloLosses)
                            {
                                if (!localRaid.DropToLowerQuest || !HasLowerQuest(localRaidInfo, rewardItems, localRaid.QuestLevel, questStep))
                                {
                                    await _jobLogger.LogAsync(userId, Text.T("LocalRaidSoloStop"));
                                    return;
                                }
                                questStep++;
                                soloLosses = 0;
                                await _jobLogger.LogAsync(userId, Text.T("LocalRaidLowerQuest", GetQuestId(localRaidInfo, rewardItems, localRaid.QuestLevel, questStep)));
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "Failed to fetch battle result for user {UserId}", userId);
                        }
                        break; // 进入下一场
                    }
                }
            }
        }
        catch (OperationCanceledException)
        {
            await _jobLogger.LogAsync(userId, Text.T("LocalRaidTimeoutOrCancelled"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "LocalRaidHandler error for user {UserId}", userId);
            await _jobLogger.LogAsync(userId, Text.T("LocalRaidError", ex.Message), "Error");
        }
        finally
        {
            if (client != null)
            {
                client.ClearLocalRaidReceiver();
                await client.DisposeAsync();
            }
        }

        PassDone:
        if (createRoom || cts.IsCancellationRequested) return;
        // 加入等超时: 还在加入时间内且有更低的关卡 -> 降一级再找
        if (localRaid.DropToLowerQuest && joinSw.Elapsed < joinWait && HasLowerQuest(localRaidInfo, rewardItems, localRaid.QuestLevel, questStep))
        {
            questStep++;
            await _jobLogger.LogAsync(userId, Text.T("LocalRaidLowerQuest", GetQuestId(localRaidInfo, rewardItems, localRaid.QuestLevel, questStep)));
            continue;
        }
        // 否则: 开启 HostWhenNoRoom 才自己开房间, 不然就放弃 (旧模式)
        if (!localRaid.HostWhenNoRoom) return;
        await _jobLogger.LogAsync(userId, Text.T("LocalRaidHostingOwnRoom"));
        createRoom = true;
        }
    }

    private async Task<OrtegaMagicOnionClient?> ConnectOnionAsync(NetworkManager nm, Action<string> log, CancellationToken ct)
    {
        // 模拟 NetworkManager.GetOnionClient()
        // 由于新项目中没有直接提供这个方法，我们需要手动创建
        // 根据之前对 NetworkManager.cs 的分析：
        // _grpcChannel = GrpcChannel.ForAddress(new Uri($"https://{response.MagicOnionHost}:{response.MagicOnionPort}"));
        
        // 这里需要反射获取私有字段，或者修改 NetworkManager 暴露。
        // 为了最小化侵入，我们尝试通过反射获取 _grpcChannel 和 _authTokenOfMagicOnion
        var type = typeof(NetworkManager);
        var channelField = type.GetField("_grpcChannel", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var tokenField = type.GetField("_authTokenOfMagicOnion", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        var channel = channelField?.GetValue(nm) as Grpc.Net.Client.GrpcChannel;
        var token = tokenField?.GetValue(nm) as string;

        if (channel == null || string.IsNullOrEmpty(token))
        {
            log(Text.T("LocalRaidOnionNotReady"));
            return null;
        }

        var client = new OrtegaMagicOnionClient(channel, nm.PlayerId, token, this);
        await client.Connect();

        int retry = 10;
        while (client.GetState() != HubClientState.Ready && retry-- > 0 && !ct.IsCancellationRequested)
        {
            await Task.Delay(1000, ct);
        }

        if (client.GetState() != HubClientState.Ready)
        {
            log(Text.T("LocalRaidOnionConnectFailed"));
            await client.DisposeAsync();
            return null;
        }

        // 启动 KeepAlive
        _ = Task.Run(async () =>
        {
            while (!ct.IsCancellationRequested)
            {
                client.SendKeepAliveAsync();
                await Task.Delay(5000, ct);
            }
        });

        return client;
    }

    // step 0: 起始关卡 (手动等级, 或按奖励自动选); step N: 比它低的第 N 个关卡 (同一类: 活动本或常规本)
    private long GetQuestId(GetLocalRaidInfoResponse response, List<GameConfig.WeightedItem> rewardItems, int questLevel, int step)
    {
        var best = GetStartQuestId(response, rewardItems, questLevel);
        if (step <= 0) return best;
        var lower = LowerQuests(response, best);
        return lower.Count == 0 ? best : lower[Math.Min(step, lower.Count) - 1];
    }

    private bool HasLowerQuest(GetLocalRaidInfoResponse response, List<GameConfig.WeightedItem> rewardItems, int questLevel, int step) =>
        LowerQuests(response, GetStartQuestId(response, rewardItems, questLevel)).Count > step;

    // 手动指定等级 (给低等级小号用): 该等级的关卡, 没有就取低于它的最高一级, 再没有就最低的; 0 = 按奖励自动选
    private long GetStartQuestId(GetLocalRaidInfoResponse response, List<GameConfig.WeightedItem> rewardItems, int questLevel)
    {
        if (questLevel <= 0) return GetBestQuestId(response, rewardItems);
        var events = response.OpenEventLocalRaidQuestIds;
        var pool = response.LocalRaidQuestInfos.Where(q => events.Contains(q.Id)).ToList();
        if (pool.Count == 0) pool = response.LocalRaidQuestInfos.ToList();
        var pick = pool.Where(q => q.Level <= questLevel).OrderByDescending(q => q.Level).FirstOrDefault()
                   ?? pool.OrderBy(q => q.Level).FirstOrDefault();
        return pick?.Id ?? GetBestQuestId(response, rewardItems);
    }

    private static List<long> LowerQuests(GetLocalRaidInfoResponse response, long best)
    {
        var quests = response.LocalRaidQuestInfos;
        var bestQuest = quests.FirstOrDefault(q => q.Id == best);
        if (bestQuest == null) return new List<long>();
        var isEvent = response.OpenEventLocalRaidQuestIds.Contains(best);
        return quests
            .Where(q => q.Level < bestQuest.Level && response.OpenEventLocalRaidQuestIds.Contains(q.Id) == isEvent)
            .OrderByDescending(q => q.Level)
            .Select(q => q.Id)
            .ToList();
    }

    private long GetBestQuestId(GetLocalRaidInfoResponse response, List<GameConfig.WeightedItem> rewardItems)
    {
        if (response.OpenEventLocalRaidQuestIds.Count > 0)
        {
            // 优先打活动本
            var sorted = response.LocalRaidQuestInfos.OrderByDescending(d =>
            {
                if (response.ClearCountDict.TryGetValue(d.Id, out var c) && c > 0) 
                    return d.FixedBattleRewards[0].ItemCount;
                return d.FixedBattleRewards[0].ItemCount + d.FirstBattleRewards[0].ItemCount;
            }).ToList();
            return sorted.First().Id;
        }

        // 打常规本，根据奖励权重计算
        return response.LocalRaidQuestInfos.Select(d =>
        {
            double score = 0;
            bool isFirst = !response.ClearCountDict.ContainsKey(d.Id);
            foreach (var weightConfig in rewardItems)
            {
                foreach (var reward in d.FixedBattleRewards)
                {
                    if (reward.IsEqual(weightConfig.ItemType, weightConfig.ItemId))
                        score += (double)reward.ItemCount / GetMaxItemCount(weightConfig) * weightConfig.Weight;
                }
                if (isFirst)
                {
                    foreach (var reward in d.FirstBattleRewards)
                    {
                        if (reward.IsEqual(weightConfig.ItemType, weightConfig.ItemId))
                            score += (double)reward.ItemCount / GetMaxItemCount(weightConfig) * weightConfig.Weight;
                    }
                }
            }
            return new { Id = d.Id, Score = score };
        }).OrderByDescending(x => x.Score).First().Id;
    }

    private double GetMaxItemCount(IUserItem userItem)
    {
        return (userItem.ItemType, userItem.ItemId) switch
        {
            (ItemType.CharacterTrainingMaterial, 1) => 241032D,
            (ItemType.EquipmentReinforcementItem, 1) => 296D,
            (ItemType.EquipmentReinforcementItem, 2) => 34D,
            (ItemType.CharacterTrainingMaterial, 2) => 378D,
            (ItemType.ExchangePlaceItem, 4) => 27D,
            _ => 99999999D
        };
    }

    // IMagicOnionLocalRaidNotificaiton 实现 (通常用于 UI 更新，此处留空)
    public void OnError(ErrorCode errorCode) { }
    public void OnReadyBattle() { }
    public void OnStartBattle() { }
    public void OnDisbandRoom() { }
    public void OnRefused() { }
    public void OnInvited(OnInviteResponse response) { }
    public void OnJoinRoom() { }
}
