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

    public string ActionName => "自动时空洞窟";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        // 获取配置
        using var scope = _serviceProvider.CreateScope();
        var settingService = scope.ServiceProvider.GetRequiredService<PlayerSettingService>();
        var playerOption = await settingService.GetSettingAsync<PlayerOption>(userId, "PlayerOption");

        if (playerOption == null)
        {
            _logger.LogDebug("User {UserId} has no PlayerOption configured, skipping.", userId);
            return;
        }

        var rewardItems = playerOption.LocalRaid.RewardItems;
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

        var createRoom = playerOption.LocalRaid.SelfCreateRoom;
        
        // 1. 获取活动信息
        var localRaidInfo = await nm.SendRequest<GetLocalRaidInfoRequest, GetLocalRaidInfoResponse>(new GetLocalRaidInfoRequest());
        var questId = GetQuestId(localRaidInfo, rewardItems);

        await _jobLogger.LogAsync(userId, $"开始时空洞窟任务 (QuestId: {questId}, 自建房: {createRoom})...");

        // 2. 连接 MagicOnion
        var cts = new CancellationTokenSource(TimeSpan.FromMinutes(15)); // 总时长限制
        var logAction = new Action<string>(async msg => await _jobLogger.LogAsync(userId, msg));

        OrtegaMagicOnionClient? client = null;
        try
        {
            client = await ConnectOnionAsync(nm, logAction, cts.Token);
            if (client == null) return;

            LocalRaidBaseReceiver receiver = createRoom 
                ? new LocalRaidCreateRoomReceiver(client, logAction, playerOption.LocalRaid.WaitSeconds, cts.Token)
                : new LocalRaidJoinRoomReceiver(client, logAction, cts.Token);

            receiver.QuestId = questId;
            client.SetupLocalRaid(receiver, receiver);

            // 3. 进入循环
            while (!cts.Token.IsCancellationRequested)
            {
                // 获取最新信息
                localRaidInfo = await nm.SendRequest<GetLocalRaidInfoRequest, GetLocalRaidInfoResponse>(new GetLocalRaidInfoRequest());
                questId = GetQuestId(localRaidInfo, rewardItems);
                receiver.QuestId = questId;
                receiver.IsBattleStarted = false;

                if (createRoom)
                {
                    await _jobLogger.LogAsync(userId, "正在创建房间...");
                    client.SendLocalRaidOpenRoom(LocalRaidRoomConditionsType.None, questId, 0, 0);
                }
                else
                {
                    await _jobLogger.LogAsync(userId, "正在寻找随机房间...");
                    client.SendLocalRaidJoinRandomRoom(questId);
                }

                // 等待战斗开始
                while (!cts.Token.IsCancellationRequested)
                {
                    await Task.Delay(2000, cts.Token);
                    if (receiver.IsNoRemainingChallenges)
                    {
                        await _jobLogger.LogAsync(userId, "挑战次数已用完。");
                        return;
                    }
                    if (receiver.IsMaxTimeExceeded)
                    {
                        await _jobLogger.LogAsync(userId, "等待超时。");
                        return;
                    }

                    if (receiver.IsBattleStarted)
                    {
                        await Task.Delay(3000, cts.Token); // 等待服务器同步结果
                        try
                        {
                            var battleResult = await nm.SendRequest<GetLocalRaidBattleResultRequest, GetLocalRaidBattleResultResponse>(new GetLocalRaidBattleResultRequest());
                            bool isWin = battleResult.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker();
                            await _jobLogger.LogAsync(userId, isWin ? "战斗胜利！" : "战斗失败。");
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
            await _jobLogger.LogAsync(userId, "任务超时或被取消。");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "LocalRaidHandler error for user {UserId}", userId);
            await _jobLogger.LogAsync(userId, $"发生错误: {ex.Message}", "Error");
        }
        finally
        {
            if (client != null)
            {
                client.ClearLocalRaidReceiver();
                await client.DisposeAsync();
            }
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
            log("MagicOnion 通道未就绪，请确保已登录。");
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
            log("无法连接到 MagicOnion 服务。");
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

    private long GetQuestId(GetLocalRaidInfoResponse response, List<GameConfig.WeightedItem> rewardItems)
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
