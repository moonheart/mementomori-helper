using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;
using MementoMori.Api.Services;
using MementoMori.Api.Utils;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle;
using MementoMori.Ortega.Share.Data.DungeonBattle;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 地下城战斗处理器（简化版）
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class DungeonBattleHandler : IGameActionHandler
{
    private readonly ILogger<DungeonBattleHandler> _logger;
    private readonly JobLogger _jobLogger;
    private readonly PlayerSettingService _playerSettingService;

    public string ActionName => "地下城战斗";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, "正在检查地下城战斗...");

        try
        {
            var dungeonSettings = await _playerSettingService.GetDungeonBattleSettingsAsync(userId);
            // DungeonBattleConfig 没有 AutoDungeonBattle 属性，使用 AutoJob 设置替代
            var autoJobSettings = await _playerSettingService.GetAutoJobSettingsAsync(userId);
            if (!autoJobSettings.AutoDungeonBattle)
            {
                await _jobLogger.LogAsync(userId, "地下城自动战斗未开启。");
                return;
            }

            var battleInfoResponse = await nm.SendRequest<GetDungeonBattleInfoRequest, GetDungeonBattleInfoResponse>(
                new GetDungeonBattleInfoRequest(), false);

            if (battleInfoResponse.UserDungeonDtoInfo.IsDoneRewardClearLayer(3))
            {
                await _jobLogger.LogAsync(userId, "地下城已通关完毕。");
                return;
            }

            await _jobLogger.LogAsync(userId, $"开始地下城战斗，当前层: {battleInfoResponse.CurrentDungeonBattleLayer?.LayerCount ?? 0}");

            var maxIterations = 100; // 防止无限循环
            var iterations = 0;

            while (iterations < maxIterations)
            {
                iterations++;

                battleInfoResponse = await nm.SendRequest<GetDungeonBattleInfoRequest, GetDungeonBattleInfoResponse>(
                    new GetDungeonBattleInfoRequest(), false);

                if (battleInfoResponse.UserDungeonDtoInfo.IsDoneRewardClearLayer(3))
                {
                    await _jobLogger.LogAsync(userId, "地下城已完成！");
                    return;
                }

                var currentGrid = battleInfoResponse.CurrentDungeonBattleLayer?.DungeonGrids
                    .FirstOrDefault(d => d.DungeonGridGuid == battleInfoResponse.UserDungeonDtoInfo.CurrentGridGuid);

                if (currentGrid == null) break;

                var gridMb = Masters.DungeonBattleGridTable.GetById(currentGrid.DungeonGridId);
                var state = battleInfoResponse.UserDungeonDtoInfo.CurrentGridState;

                // 处理当前节点
                switch (state)
                {
                    case DungeonBattleGridState.Done:
                        // 选择下一个节点或领取层奖励
                        var nextGrid = SelectNextGrid(battleInfoResponse, currentGrid);
                        if (nextGrid == null)
                        {
                            // 领取层奖励并进入下一层
                            if (battleInfoResponse.CurrentDungeonBattleLayer != null &&
                                !battleInfoResponse.UserDungeonDtoInfo.IsDoneRewardClearLayer(battleInfoResponse.CurrentDungeonBattleLayer.LayerCount))
                            {
                                await nm.SendRequest<RewardClearLayerRequest, RewardClearLayerResponse>(
                                    new RewardClearLayerRequest
                                    {
                                        ClearedLayer = battleInfoResponse.CurrentDungeonBattleLayer.LayerCount,
                                        CurrentTermId = battleInfoResponse.CurrentTermId,
                                        DungeonBattleDifficultyType = battleInfoResponse.CurrentDungeonBattleLayer.DungeonDifficultyType
                                    }, false);
                            }

                            if (battleInfoResponse.CurrentDungeonBattleLayer != null &&
                                battleInfoResponse.CurrentDungeonBattleLayer.LayerCount < 3)
                            {
                                await nm.SendRequest<ProceedLayerRequest, ProceedLayerResponse>(
                                    new ProceedLayerRequest
                                    {
                                        CurrentTermId = battleInfoResponse.CurrentTermId,
                                        DungeonDifficultyType = DungeonBattleDifficultyType.Normal
                                    }, false);
                            }
                        }
                        else
                        {
                            await nm.SendRequest<SelectGridRequest, SelectGridResponse>(
                                new SelectGridRequest
                                {
                                    CurrentTermId = battleInfoResponse.CurrentTermId,
                                    DungeonGridGuid = nextGrid.DungeonGridGuid
                                }, false);
                        }
                        break;

                    case DungeonBattleGridState.Selected:
                        // 执行当前节点的操作
                        await ExecuteGridAction(nm, battleInfoResponse, currentGrid, gridMb, userId, dungeonSettings);
                        break;

                    case DungeonBattleGridState.Reward:
                        // 选择奖励
                        await SelectReward(nm, battleInfoResponse, currentGrid);
                        break;

                    default:
                        iterations = maxIterations; // 退出
                        break;
                }
            }

            await _jobLogger.LogAsync(userId, "地下城战斗处理完毕。");
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "地下城战斗失败 for user {UserId}", userId);
        }
    }

    private DungeonBattleGrid? SelectNextGrid(GetDungeonBattleInfoResponse battleInfo, DungeonBattleGrid currentGrid)
    {
        if (battleInfo.CurrentDungeonBattleLayer == null) return null;

        var allGrids = battleInfo.CurrentDungeonBattleLayer.DungeonGrids
            .GroupBy(d => d.Y)
            .OrderBy(d => d.Key)
            .Select(d => d.OrderBy(x => x.X).ToArray())
            .ToArray();

        if (currentGrid.Y >= allGrids.Length - 1) return null;

        var nextLayer = allGrids[currentGrid.Y + 1];
        var currentLayerSize = allGrids[currentGrid.Y].Length;
        var nextLayerSize = nextLayer.Length;

        // 简单的相邻判断
        foreach (var nextGrid in nextLayer)
        {
            if (currentLayerSize > nextLayerSize)
            {
                if (nextGrid.X == currentGrid.X || nextGrid.X == currentGrid.X - 1)
                    return nextGrid;
            }
            else
            {
                if (nextGrid.X == currentGrid.X || nextGrid.X == currentGrid.X + 1)
                    return nextGrid;
            }
        }

        return nextLayer.FirstOrDefault();
    }

    private async Task ExecuteGridAction(
        NetworkManager nm,
        GetDungeonBattleInfoResponse battleInfo,
        DungeonBattleGrid currentGrid,
        DungeonBattleGridMB gridMb,
        long userId,
        GameConfig.DungeonBattleConfig dungeonSettings)
    {
        var type = gridMb.DungeonGridType;

        switch (type)
        {
            case DungeonBattleGridType.BattleNormal:
            case DungeonBattleGridType.BattleElite:
            case DungeonBattleGridType.BattleBoss:
            case DungeonBattleGridType.EventBattleNormal:
            case DungeonBattleGridType.EventBattleElite:
            case DungeonBattleGridType.TreasureChest:
                await DoBattle(nm, battleInfo, currentGrid, userId, dungeonSettings);
                break;

            case DungeonBattleGridType.Recovery:
                await nm.SendRequest<ExecRecoveryRequest, ExecRecoveryResponse>(
                    new ExecRecoveryRequest
                    {
                        CurrentTermId = battleInfo.CurrentTermId,
                        DungeonGridGuid = currentGrid.DungeonGridGuid,
                        IsHealed = true
                    }, false);
                break;

            case DungeonBattleGridType.Shop:
                await nm.SendRequest<LeaveShopRequest, LeaveShopResponse>(
                    new LeaveShopRequest
                    {
                        CurrentTermId = battleInfo.CurrentTermId,
                        DungeonGridGuid = currentGrid.DungeonGridGuid
                    }, false);
                break;

            case DungeonBattleGridType.Revival:
                await nm.SendRequest<ExecReviveRequest, ExecReviveResponse>(
                    new ExecReviveRequest
                    {
                        CurrentTermId = battleInfo.CurrentTermId,
                        DungeonGridGuid = currentGrid.DungeonGridGuid,
                        IsRevived = true
                    }, false);
                break;

            case DungeonBattleGridType.RelicReinforce:
                await nm.SendRequest<ExecReinforceRelicRequest, ExecReinforceRelicResponse>(
                    new ExecReinforceRelicRequest
                    {
                        CurrentTermId = battleInfo.CurrentTermId,
                        DungeonGridGuid = currentGrid.DungeonGridGuid
                    }, false);
                break;
        }
    }

    private async Task DoBattle(
        NetworkManager nm,
        GetDungeonBattleInfoResponse battleInfo,
        DungeonBattleGrid currentGrid,
        long userId,
        GameConfig.DungeonBattleConfig dungeonSettings)
    {
        // 构建战斗角色列表
        var battleCharacterGuids = new List<string>();
        var userDeckDtoInfo = nm.UserSyncData.UserDeckDtoInfos.FirstOrDefault(d => d.DeckUseContentType == DeckUseContentType.DungeonBattle);

        if (userDeckDtoInfo != null)
        {
            foreach (var characterGuid in userDeckDtoInfo.GetUserCharacterGuids())
            {
                var characterDtoInfo = battleInfo.UserDungeonBattleCharacterDtoInfos.FirstOrDefault(d => d.Guid == characterGuid);
                if (characterDtoInfo == null || characterDtoInfo.CurrentHpPerMill == 0)
                    continue;
                battleCharacterGuids.Add(characterGuid);
            }
        }

        // 补充角色 - 使用战斗力排序
        var emptyPosition = 5 - battleCharacterGuids.Count;
        if (emptyPosition > 0)
        {
            var availableCharacters = battleInfo.UserDungeonBattleCharacterDtoInfos
                .Where(d => !battleCharacterGuids.Contains(d.Guid) && d.CurrentHpPerMill != 0)
                .Select(d => new
                {
                    Dto = d,
                    // 使用基础角色战斗力作为排序依据
                    Power = BattlePowerCalculatorUtil.GetCharacterBattlePower(d.CharacterId)
                })
                .OrderByDescending(d => d.Power)
                .Take(emptyPosition)
                .Select(d => d.Dto.Guid);

            battleCharacterGuids.AddRange(availableCharacters);
        }

        if (battleCharacterGuids.Count == 0)
        {
            // 尝试使用恢复道具
            if (battleInfo.UserDungeonDtoInfo.UseDungeonRecoveryItemCount < dungeonSettings.MaxUseRecoveryItem)
            {
                await nm.SendRequest<UseRecoveryItemRequest, UseRecoveryItemResponse>(
                    new UseRecoveryItemRequest { CurrentTermId = battleInfo.CurrentTermId }, false);
                return;
            }
            return;
        }

        await nm.SendRequest<ExecBattleRequest, ExecBattleResponse>(
            new ExecBattleRequest
            {
                CurrentTermId = battleInfo.CurrentTermId,
                DungeonGridGuid = currentGrid.DungeonGridGuid,
                CharacterGuids = battleCharacterGuids
            }, false);

        await nm.SendRequest<FinishBattleRequest, FinishBattleResponse>(
            new FinishBattleRequest
            {
                DungeonGridGuid = currentGrid.DungeonGridGuid,
                CurrentTermId = battleInfo.CurrentTermId,
                VisitDungeonCount = 0
            }, false);
    }

    private async Task SelectReward(
        NetworkManager nm,
        GetDungeonBattleInfoResponse battleInfo,
        DungeonBattleGrid currentGrid)
    {
        var relicId = battleInfo.RewardRelicIds?.FirstOrDefault() ?? 0;

        await nm.SendRequest<RewardBattleReceiveRelicRequest, RewardBattleReceiveRelicResponse>(
            new RewardBattleReceiveRelicRequest
            {
                CurrentTermId = battleInfo.CurrentTermId,
                DungeonGridGuid = currentGrid.DungeonGridGuid,
                SelectedRelicId = relicId
            }, false);
    }
}
