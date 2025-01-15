using MementoMori.MagicOnion;
using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Network.MagicOnion.Client;
using MementoMori.Ortega.Share.Data.ApiInterface.LocalRaid;
using MementoMori.Ortega.Share.Data.Item;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    public async Task AutoLocalRaid()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var rewardItems = PlayerOption.LocalRaid.RewardItems;
            if (rewardItems.Count == 0)
            {
                rewardItems = GameConfig.LocalRaid.RewardItems;
                if (rewardItems.Count == 0)
                {
                    rewardItems.AddRange(new[]
                    {
                        new GameConfig.WeightedItem(ItemType.ExchangePlaceItem, 4, 4), // 符石兑换券
                        new GameConfig.WeightedItem(ItemType.CharacterTrainingMaterial, 2, 3), // 潜能宝珠
                        new GameConfig.WeightedItem(ItemType.EquipmentReinforcementItem, 2, 2.5), // 强化秘药
                        new GameConfig.WeightedItem(ItemType.CharacterTrainingMaterial, 1, 2), // 经验珠
                        new GameConfig.WeightedItem(ItemType.EquipmentReinforcementItem, 1, 1) // 强化水
                    });
                }
            }

            var createRoom = _playersOption.Value.TryGetValue(NetworkManager.PlayerId, out var c) && c.LocalRaid.SelfCreateRoom;

            OrtegaMagicOnionClient client = null;
            LocalRaidBaseReceiver localRaidReceiver = null;
            var maxRetry = 10;
            while (!token.IsCancellationRequested)
            {
                client = NetworkManager.GetOnionClient();
                localRaidReceiver = createRoom ? new LocalRaidCreateRoomReceiver(client, log, PlayerOption.LocalRaid.WaitSeconds, token) : new LocalRaidJoinRoomReceiver(client, log, token);
                client.SetupLocalRaid(localRaidReceiver, localRaidReceiver);
                var connectCts = new CancellationTokenSource();
                await client.Connect();
                connectCts.CancelAfter(TimeSpan.FromMinutes(1));
                while (client.GetState() != HubClientState.Ready && !connectCts.IsCancellationRequested)
                {
                    if (token.IsCancellationRequested) return;
                    log("Waiting for connection...");
                    await Task.Delay(1000);
                }

                if (client.GetState() == HubClientState.Ready) break;

                await client.DisposeAsync();
                if (maxRetry-- < 0)
                {
                    log("Failed to connect to the server.");
                    return;
                }
            }

            var keepaliveCts = new CancellationTokenSource();
            ;
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
                    localRaidReceiver.IsBattleStarted = false;

                    if (createRoom)
                    {
                        log(TextResourceTable.Get("[LocalRaidRoomSearchButtonCreateRoom]"));
                        client.SendLocalRaidOpenRoom(LocalRaidRoomConditionsType.None, questId, 0, 0);
                    }
                    else
                    {
                        log(TextResourceTable.Get("[LocalRaidRoomSearchButtonJoinRandomRoom]"));
                        client.SendLocalRaidJoinRandomRoom(questId);
                    }

                    while (!token.IsCancellationRequested)
                    {
                        await Task.Delay(1000);
                        if (localRaidReceiver.IsNoRemainingChallenges || localRaidReceiver.IsMaxTimeExceeded) return;

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
                    var localRaidQuestMbs = response.LocalRaidQuestInfos;
                    var localRaidQuestMb = localRaidQuestMbs.OrderByDescending(d =>
                    {
                        if (response.ClearCountDict.TryGetValue(d.Id, out var c) && c > 0) return d.FixedBattleRewards[0].ItemCount;

                        return d.FixedBattleRewards[0].ItemCount + d.FirstBattleRewards[0].ItemCount;
                    }).First();
                    return localRaidQuestMb.Id;
                }
                else
                {
                    var localRaidQuestMbs = response.LocalRaidQuestInfos;
                    if (rewardItems.Count == 0) return localRaidQuestMbs.OrderByDescending(d => d.Level).First().Id;

                    return localRaidQuestMbs.Select(d =>
                    {
                        var count = 0D;
                        // if cleared dict contains the quest id, it means the quest has been cleared before
                        var isFirst = !response.ClearCountDict.ContainsKey(d.Id);
                        foreach (var rewardItem in rewardItems)
                        {
                            foreach (var reward in d.FixedBattleRewards)
                            {
                                if (reward.IsEqual(rewardItem.ItemType, rewardItem.ItemId)) count += reward.ItemCount / GetMaxItemCount(rewardItem) * rewardItem.Weight;
                            }

                            if (!isFirst) continue;
                            foreach (var reward in d.FirstBattleRewards)
                            {
                                if (reward.IsEqual(rewardItem.ItemType, rewardItem.ItemId)) count += reward.ItemCount / GetMaxItemCount(rewardItem) * rewardItem.Weight;
                            }
                        }

                        return new {quest = d, count};
                    }).OrderByDescending(d => d.count).First().quest.Id;
                }
            }

            double GetMaxItemCount(IUserItem userItem)
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
        });
    }
}