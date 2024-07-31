using MementoMori.Exceptions;
using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle;
using MementoMori.Ortega.Share.Data.ApiInterface.Equipment;
using MementoMori.Ortega.Share.Data.DungeonBattle;
using MementoMori.Ortega.Share.Data.Equipment;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    private bool IsDungeonBattleHardModeAvailable =>
        UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId >= OpenContentTable.GetByOpenCommandType(OpenCommandType.DungeonBattleHardMode).OpenContentValue;

    public async Task AutoDungeonBattle(Action<string> log, CancellationToken cancellationToken)
    {
        // 脱装备进副本，然后穿装备
        var competitionInfoResponse = await GetResponse<GetCompetitionInfoRequest, GetCompetitionInfoResponse>(new GetCompetitionInfoRequest());
        if (competitionInfoResponse.IsDungeonBattleEventOpen && _writableGameConfig.Value.DungeonBattle.AutoRemoveEquipment)
        {
            var equips = UserSyncData.UserEquipmentDtoInfos.Where(d => !string.IsNullOrEmpty(d.CharacterGuid)).GroupBy(d => d.CharacterGuid).ToList();
            foreach (var g in equips)
            {
                var characterDto = UserSyncData.UserCharacterDtoInfos.Find(d => d.Guid == g.Key);
                var name = TextResourceTable.Get(CharacterTable.GetById(characterDto.CharacterId).NameKey);
                log(string.Format(ResourceStrings.RemoveEquipmentOfCharacter, name, characterDto.Level));

                // 脱装备
                var removeEquipmentResponse = await GetResponse<RemoveEquipmentRequest, RemoveEquipmentResponse>(new RemoveEquipmentRequest
                {
                    UserCharacterGuid = g.Key,
                    EquipmentSlotTypes = g.Select(d => EquipmentTable.GetById(d.EquipmentId).SlotType).ToList()
                });
            }

            // 进副本
            await GetResponse<GetDungeonBattleInfoRequest, GetDungeonBattleInfoResponse>(new GetDungeonBattleInfoRequest());
            if (competitionInfoResponse.IsDungeonBattleEventOpen)
            {
                foreach (var g in equips)
                {
                    var characterDto = UserSyncData.UserCharacterDtoInfos.Find(d => d.Guid == g.Key);
                    var name = TextResourceTable.Get(CharacterTable.GetById(characterDto.CharacterId).NameKey);
                    log(string.Format(ResourceStrings.PutOnEquipmentOfCharacter, name, characterDto.Level));
                    // 穿装备
                    var changeInfos = g.Select(d =>
                    {
                        var equipmentMb = EquipmentTable.GetById(d.EquipmentId);
                        return new EquipmentChangeInfo
                        {
                            EquipmentGuid = d.Guid,
                            EquipmentId = d.EquipmentId,
                            EquipmentSlotType = equipmentMb.SlotType,
                            IsInherit = false
                        };
                    });
                    var changeEquipmentResponse = await GetResponse<ChangeEquipmentRequest, ChangeEquipmentResponse>(new ChangeEquipmentRequest
                    {
                        UserCharacterGuid = g.Key, EquipmentChangeInfos = changeInfos.ToList()
                    });
                }
            }
        }

        var battleInfoResponse = await GetResponse<GetDungeonBattleInfoRequest, GetDungeonBattleInfoResponse>(new GetDungeonBattleInfoRequest());

        log($"{ResourceStrings.Enter} {TextResourceTable.Get("[CommonHeaderDungeonBattleLabel]")}");

        if (battleInfoResponse.UserDungeonDtoInfo.IsDoneRewardClearLayer(3))
        {
            log($"{TextResourceTable.Get("[CommonHeaderDungeonBattleLabel]")} {ResourceStrings.Finished}");
            return;
        }

        var layerPaths = new Dictionary<int, List<List<DungeonBattleGrid>>>();
        var layerBestPaths = new Dictionary<int, List<DungeonBattleGrid>>();
        var gridData = new Dictionary<string, GetBattleGridDataResponse>();

        var shouldStop = false;
        while (!cancellationToken.IsCancellationRequested && !shouldStop)
        {
            // 获取副本信息
            battleInfoResponse = await GetResponse<GetDungeonBattleInfoRequest, GetDungeonBattleInfoResponse>(new GetDungeonBattleInfoRequest());
            var grids = battleInfoResponse.CurrentDungeonBattleLayer.DungeonGrids.Select(d =>
            {
                var dungeonBattleGridMb = DungeonBattleGridTable.GetById(d.DungeonGridId);
                var power = battleInfoResponse.GridBattlePowerDict.TryGetValue(d.DungeonGridGuid, out var n) ? n : 0;
                return new
                {
                    Grid = d, GridMb = dungeonBattleGridMb, Power = power
                };
            }).ToList();
            var gridDict = grids.ToDictionary(d => d.Grid.DungeonGridGuid, d => d.GridMb);
            foreach (var dungeonBattleGrid in battleInfoResponse.CurrentDungeonBattleLayer.DungeonGrids)
            {
                if (gridData.ContainsKey(dungeonBattleGrid.DungeonGridGuid)) continue;
                if (!gridDict[dungeonBattleGrid.DungeonGridGuid].IsBattleType()) continue;
                var data = await GetResponse<GetBattleGridDataRequest, GetBattleGridDataResponse>(new GetBattleGridDataRequest
                    {CurrentTermId = battleInfoResponse.CurrentTermId, DungeonGridGuid = dungeonBattleGrid.DungeonGridGuid});
                gridData[dungeonBattleGrid.DungeonGridGuid] = data;
            }

            // 当前节点状态
            var currentGrid = grids.First(d =>
                d.Grid.DungeonGridGuid == battleInfoResponse.UserDungeonDtoInfo.CurrentGridGuid);
            // grids 是石台列表, 共11层, 每层的石台数量分别为 1,2,3,2,3,2,3,2,3,2,1
            var allGrids = battleInfoResponse.CurrentDungeonBattleLayer.DungeonGrids
                .GroupBy(d => d.Y)
                .OrderBy(d => d.Key)
                .Select(d => d.OrderBy(d => d.X).ToArray())
                .ToArray();
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
                    .Where(d => !battleCharacterGuids.Contains(d.Guid) && d.CurrentHpPerMill != 0)
                    .OrderByDescending(d => BattlePowerCalculatorUtil.GetUserCharacterBattlePower(UserId, d.Guid))
                    .Select(d => d.Guid).Take(emptyPosition));

                // 全灭, 如果已使用的苹果没有超过限制
                if (battleCharacterGuids.Count == 0 && battleInfoResponse.UserDungeonDtoInfo.UseDungeonRecoveryItemCount < GameConfig.DungeonBattle.MaxUseRecoveryItem)
                {
                    var useRecoveryItemResponse = await GetResponse<UseRecoveryItemRequest, UseRecoveryItemResponse>(new UseRecoveryItemRequest {CurrentTermId = battleInfoResponse.CurrentTermId});
                    return;
                }

                if (battleCharacterGuids.Count == 0)
                {
                    log("All character died");
                    shouldStop = true;
                    return;
                }

                var execBattleResponse = await GetResponse<ExecBattleRequest, ExecBattleResponse>(
                    new ExecBattleRequest
                    {
                        CurrentTermId = battleInfoResponse.CurrentTermId,
                        DungeonGridGuid = currentGrid.Grid.DungeonGridGuid,
                        CharacterGuids = battleCharacterGuids
                    });
                var finishBattleResponse = await GetResponse<FinishBattleRequest, FinishBattleResponse>(
                    new FinishBattleRequest
                    {
                        DungeonGridGuid = currentGrid.Grid.DungeonGridGuid,
                        CurrentTermId = battleInfoResponse.CurrentTermId,
                        VisitDungeonCount = 0
                    });
            }

            DungeonBattleGrid SelectNextGrid()
            {
                // 如果当前层还没有计算过最佳路径，或者最佳路径不包含当前节点，重新计算
                if (!layerBestPaths.TryGetValue(layer, out var bestPath) || bestPath.All(d => d.DungeonGridGuid != currentGrid.Grid.DungeonGridGuid))
                {
                    if (!layerPaths.TryGetValue(layer, out var allPaths))
                    {
                        allPaths = CalcAllPathsToEndFromGrid(currentGrid.Grid);
                        layerPaths[layer] = allPaths;
                    }

                    allPaths.Sort(Comparison);

                    int Comparison(List<DungeonBattleGrid> x, List<DungeonBattleGrid> y)
                    {
                        // 活动节点优先
                        var countX = CountEventGridCount(x);
                        var countY = CountEventGridCount(y);
                        if (countX > countY) return -1;
                        if (countX < countY) return 1;

                        // 然后商店节点，并且有目标物品
                        countX = CountShopGridCount(x);
                        countY = CountShopGridCount(y);
                        if (countX > countY) return -1;
                        if (countX < countY) return 1;

                        // 然后宝箱节点
                        countX = CountTreasureChestGridCount(x);
                        countY = CountTreasureChestGridCount(y);
                        if (countX > countY) return -1;
                        if (countX < countY) return 1;

                        // 然后战斗节点
                        countX = CountBattleGridCount(x);
                        countY = CountBattleGridCount(y);
                        if (countX > countY) return -1;
                        if (countX < countY) return 1;

                        // 然后回复节点
                        countX = CountRecoveryGridCount(x);
                        countY = CountRecoveryGridCount(y);
                        if (countX > countY) return -1;
                        if (countX < countY) return 1;

                        // 然后复活节点
                        countX = CountRevivalGridCount(x);
                        countY = CountRevivalGridCount(y);
                        if (countX > countY) return -1;
                        if (countX < countY) return 1;

                        return 0;
                    }

                    bestPath = allPaths[0];
                    layerBestPaths[layer] = bestPath;
                }

                var currentIndex = bestPath.FindIndex(d => d.DungeonGridGuid == currentGrid.Grid.DungeonGridGuid);
                if (currentIndex == bestPath.Count - 1)
                {
                    // 已经到达最后一个节点
                    return null;
                }

                return bestPath[currentIndex + 1];
            }

            List<List<DungeonBattleGrid>> CalcAllPathsToEndFromGrid(DungeonBattleGrid currentGrid)
            {
                var allPaths = new List<List<DungeonBattleGrid>>();
                CalcPaths(currentGrid, new List<DungeonBattleGrid>(), allPaths);
                return allPaths;
            }

            void CalcPaths(DungeonBattleGrid currentGrid, List<DungeonBattleGrid> currentPath, List<List<DungeonBattleGrid>> allPaths)
            {
                currentPath.Add(currentGrid);

                if (currentGrid.Y == 10) // 到达最后一层
                    allPaths.Add(new List<DungeonBattleGrid>(currentPath));
                else
                {
                    // 获取下一层相邻的石台
                    var nextLayer = allGrids[currentGrid.Y + 1];
                    foreach (var nextGrid in nextLayer)
                    {
                        if (IsAdjacent(currentGrid, nextGrid)) CalcPaths(nextGrid, currentPath, allPaths);
                    }
                }

                currentPath.RemoveAt(currentPath.Count - 1); // 回溯
            }

            bool IsAdjacent(DungeonBattleGrid currentGrid, DungeonBattleGrid nextGrid)
            {
                // 判断两个石台是否相邻
                if (allGrids[currentGrid.Y].Length > allGrids[nextGrid.Y].Length)
                    return nextGrid.X == currentGrid.X || nextGrid.X == currentGrid.X - 1;
                return nextGrid.X == currentGrid.X || nextGrid.X == currentGrid.X + 1;
            }

            long CountEventGridCount(List<DungeonBattleGrid> pathGrids)
            {
                var eventGrids = pathGrids.Where(d => gridDict[d.DungeonGridGuid].DungeonGridType == DungeonBattleGridType.EventBattleElite ||
                                                      gridDict[d.DungeonGridGuid].DungeonGridType == DungeonBattleGridType.EventBattleNormal ||
                                                      gridDict[d.DungeonGridGuid].DungeonGridType == DungeonBattleGridType.EventBattleSpecial).ToList();

                // 计算全路径能获得的活动道具数量
                var totalEventItemCount = 0L;
                foreach (var d in eventGrids)
                {
                    if (gridData.TryGetValue(d.DungeonGridGuid, out var data))
                    {
                        totalEventItemCount +=
                            data.NormalRewardItemList.FirstOrDefault(x => x.ItemType == battleInfoResponse.EventItemType && x.ItemId == battleInfoResponse.EventItemId)?.ItemCount ?? 0L;
                    }
                }

                return totalEventItemCount;
            }

            int CountShopGridCount(ICollection<DungeonBattleGrid> grids)
            {
                return grids.Count(d => gridDict[d.DungeonGridGuid].DungeonGridType == DungeonBattleGridType.Shop &&
                                        GameConfig.DungeonBattle.ShopTargetItems.Any(x =>
                                            battleInfoResponse.UserDungeonBattleShopDtoInfos.Find(y => y.GridGuid == d.DungeonGridGuid).TradeShopItemList.Any(
                                                y => // 商店有目标物品
                                                    y.SalePercent >= x.MinDiscountPercent && y.GiveItem.ItemType == x.ItemType && y.GiveItem.ItemId == x.ItemId))); // 商店的
            }

            int CountTreasureChestGridCount(ICollection<DungeonBattleGrid> grids)
            {
                return grids.Count(d => gridDict[d.DungeonGridGuid].DungeonGridType == DungeonBattleGridType.TreasureChest);
            }

            int CountBattleGridCount(ICollection<DungeonBattleGrid> grids)
            {
                return grids.Count(d => gridDict[d.DungeonGridGuid].DungeonGridType == DungeonBattleGridType.BattleNormal ||
                                        gridDict[d.DungeonGridGuid].DungeonGridType == DungeonBattleGridType.BattleElite ||
                                        gridDict[d.DungeonGridGuid].DungeonGridType == DungeonBattleGridType.BattleBoss ||
                                        gridDict[d.DungeonGridGuid].DungeonGridType == DungeonBattleGridType.BattleBossNoRelic ||
                                        gridDict[d.DungeonGridGuid].DungeonGridType == DungeonBattleGridType.EventBattleElite ||
                                        gridDict[d.DungeonGridGuid].DungeonGridType == DungeonBattleGridType.EventBattleNormal ||
                                        gridDict[d.DungeonGridGuid].DungeonGridType == DungeonBattleGridType.EventBattleSpecial ||
                                        gridDict[d.DungeonGridGuid].DungeonGridType == DungeonBattleGridType.BattleAndRelicReinforce);
            }

            int CountRecoveryGridCount(ICollection<DungeonBattleGrid> grids)
            {
                return grids.Count(d => gridDict[d.DungeonGridGuid].DungeonGridType == DungeonBattleGridType.Recovery);
            }

            int CountRevivalGridCount(ICollection<DungeonBattleGrid> grids)
            {
                return grids.Count(d => gridDict[d.DungeonGridGuid].DungeonGridType == DungeonBattleGridType.Revival);
            }

            switch (state)
            {
                case DungeonBattleGridState.Done:
                    var nextGrid = SelectNextGrid();
                    if (nextGrid == null)
                    {
                        if (!battleInfoResponse.UserDungeonDtoInfo.IsDoneRewardClearLayer(battleInfoResponse.CurrentDungeonBattleLayer.LayerCount))
                        {
                            // 获取当前层奖励
                            var rewardClearLayerResponse = await GetResponse<RewardClearLayerRequest, RewardClearLayerResponse>(new RewardClearLayerRequest
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

                        var diff = battleInfoResponse.CurrentDungeonBattleLayer.LayerCount == 2
                            ? IsDungeonBattleHardModeAvailable ? DungeonBattleDifficultyType.Hard : DungeonBattleDifficultyType.Normal
                            : DungeonBattleDifficultyType.Normal;
                        var proceedLayerResponse = await GetResponse<ProceedLayerRequest, ProceedLayerResponse>(
                            new ProceedLayerRequest
                            {
                                CurrentTermId = battleInfoResponse.CurrentTermId,
                                DungeonDifficultyType = diff
                            });
                    }
                    else
                    {
                        switch (gridDict[nextGrid.DungeonGridGuid].DungeonGridType)
                        {
                            case DungeonBattleGridType.JoinCharacter:
                            {
                                var infos = battleInfoResponse.UserDungeonBattleGuestCharacterDtoInfos.OrderByDescending(d => d.BattlePower).ToList();
                                foreach (var info in infos)
                                {
                                    try
                                    {
                                        var execGuestResponse = await GetResponse<ExecGuestRequest, ExecGuestResponse>(new ExecGuestRequest
                                        {
                                            DungeonGridGuid = nextGrid.DungeonGridGuid,
                                            GuestMBId = info.CharacterId,
                                            CurrentTermId = battleInfoResponse.CurrentTermId
                                        });
                                        break;
                                    }
                                    catch (ApiErrorException e)
                                    {
                                        log(string.Format(ResourceStrings.CaveErrorSelectSupport, e.Message));
                                    }
                                }

                                break;
                            }
                            default:
                            {
                                var selectGridResponse = await GetResponse<SelectGridRequest, SelectGridResponse>(new SelectGridRequest
                                {
                                    CurrentTermId = battleInfoResponse.CurrentTermId,
                                    DungeonGridGuid = nextGrid.DungeonGridGuid
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
                                new ExecRecoveryRequest
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
                            {
                                foreach (var tradeShopItem in tradeShopItemList)
                                {
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
                                }
                            }

                            var leaveShopResponse = await GetResponse<LeaveShopRequest, LeaveShopResponse>(
                                new LeaveShopRequest
                                {
                                    CurrentTermId = battleInfoResponse.CurrentTermId,
                                    DungeonGridGuid = currentGrid.Grid.DungeonGridGuid
                                });
                            break;
                        case DungeonBattleGridType.RelicReinforce:
                            var execReinforceRelicResponse =
                                await GetResponse<ExecReinforceRelicRequest, ExecReinforceRelicResponse>(
                                    new ExecReinforceRelicRequest
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
                                new ExecReviveRequest
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
                        await RewardBattleReinforceRelic(battleInfoResponse.UserDungeonDtoInfo.RelicIds, battleInfoResponse.UserDungeonDtoInfo.RelicIds);
                    else
                    {
                        // 选择加成奖励
                        var relicId = 0L;

                        foreach (var info in GameConfig.DungeonBattleRelicSort)
                        {
                            if (battleInfoResponse.RewardRelicIds.Contains(info.Id))
                            {
                                relicId = info.Id;
                                break;
                            }
                        }

                        var rewardBattleReceiveRelicResponse =
                            await GetResponse<RewardBattleReceiveRelicRequest, RewardBattleReceiveRelicResponse>(
                                new RewardBattleReceiveRelicRequest
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

            async Task RewardBattleReinforceRelic(List<long> newRelicIds, List<long> currentRelicIds)
            {
                // 选择加成奖励
                var relicId = 0L;
                var upgradableRelics = newRelicIds.Where(d =>
                {
                    var mb = DungeonBattleRelicTable.GetByReinforceFrom(d);
                    if (mb == null)
                        // 不可升级
                        return false;

                    if (currentRelicIds.Contains(mb.Id))
                        // 升级后的已经存在了
                        return false;

                    return true;
                }).ToList();
                foreach (var info in GameConfig.DungeonBattleRelicSort)
                {
                    if (upgradableRelics.Contains(info.Id))
                    {
                        relicId = info.Id;
                        try
                        {
                            var rewardBattleReceiveRelicResponse = await GetResponse<RewardBattleReinforceRelicRequest, RewardBattleReinforceRelicResponse>(
                                new RewardBattleReinforceRelicRequest
                                {
                                    CurrentTermId = battleInfoResponse.CurrentTermId,
                                    DungeonGridGuid = currentGrid.Grid.DungeonGridGuid,
                                    SelectedRelicId = relicId
                                });
                            break;
                        }
                        catch (ApiErrorException e) when (e.ErrorCode == ErrorCode.DungeonBattleAlreadyHaveRelic)
                        {
                            log($"{TextResourceTable.GetErrorCodeMessage(e.ErrorCode)}, {ResourceStrings.CaveErrorRelicExist}");
                        }
                    }
                }
            }
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
}