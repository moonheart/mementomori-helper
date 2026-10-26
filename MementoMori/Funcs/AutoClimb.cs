using MementoMori.Exceptions;
using MementoMori.Ortega.Custom;
using MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using MementoMori.Ortega.Share.Data.ApiInterface.Quest;
using MementoMori.Ortega.Share.Data.ApiInterface.TowerBattle;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    private const int ProgressMaxLosses = 3; // 连输3次就停, 等下次成长后再打
    private const int ProgressMaxFights = 60; // 每轮上限, 避免短时间请求太多被 403

    // 定时爬塔: 今天开放的塔都爬一遍, 属性塔没解放就跳过
    public async Task AutoClimbTowers()
    {
        _elementTowersLocked = false;
        foreach (var t in GetAvailableTower())
            if (t == TowerType.Infinite || !_elementTowersLocked)
                await AutoProgressTower(t);
    }

    public async Task AutoProgressQuest()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            int fights = 0, losses = 0, wins = 0;
            var startQuest = UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId;
            await GetResponse<MapInfoRequest, MapInfoResponse>(new MapInfoRequest {IsUpdateOtherPlayerInfo = true});
            while (!token.IsCancellationRequested && fights < ProgressMaxFights && losses < ProgressMaxLosses)
            {
                var questId = UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId + 1;
                if (QuestTable.GetById(questId) == null) break; // 通关了
                try
                {
                    await GetResponse<GetQuestInfoRequest, GetQuestInfoResponse>(new GetQuestInfoRequest {TargetQuestId = questId});
                    var resp = await GetResponse<BossRequest, BossResponse>(new BossRequest {QuestId = questId});
                    fights++;
                    var win = resp.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker();
                    log($"Quest {QuestTable.GetById(questId).Memo}: {(win ? "WIN" : "LOSE")}");
                    await _battleLogManager.SaveBattleLog(resp.BattleResult, "main", QuestTable.GetById(questId).Memo, autoDeletePreserveCount: 50);
                    if (win)
                    {
                        losses = 0;
                        wins++;
                        await GetResponse<MapInfoRequest, MapInfoResponse>(new MapInfoRequest {IsUpdateOtherPlayerInfo = true});
                        try { await GetResponse<NextQuestRequest, NextQuestResponse>(new NextQuestRequest()); }
                        catch (Exception) { /* 没有下一关可移动时会报错 */ }
                    }
                    else losses++;
                }
                catch (Exception e)
                {
                    log($"Quest {questId}: {e.Message}");
                    break;
                }

                await Task.Delay(Math.Max(GameConfig.AutoRequestDelay, 1500), token);
            }

            var endQuest = UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId;
            string Q(long id) => QuestTable.GetById(id)?.Memo ?? id.ToString();
            if (endQuest > startQuest) Track(GrowthKind.Quest, $"Main quest {Q(startQuest)} → {Q(endQuest)} ({wins} wins, {fights - wins} losses)");
            else if (losses >= ProgressMaxLosses) Track(GrowthKind.Info, $"Stuck at quest {Q(endQuest + 1)} — lost {losses}x, will retry after next growth");
        });
    }

    private bool _elementTowersLocked;

    public async Task AutoProgressTower(TowerType type)
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            int fights = 0, losses = 0;
            var startFloor = UserSyncData.UserTowerBattleDtoInfos?.FirstOrDefault(d => d.TowerType == type)?.MaxTowerBattleId ?? 0;
            while (!token.IsCancellationRequested && fights < ProgressMaxFights && losses < ProgressMaxLosses)
            {
                var tower = UserSyncData.UserTowerBattleDtoInfos?.FirstOrDefault(d => d.TowerType == type);
                if (tower == null) break;
                if (type != TowerType.Infinite && tower.TodayClearNewFloorCount >= 10) break;
                var questId = tower.MaxTowerBattleId + 1;
                try
                {
                    var resp = await GetResponse<StartRequest, StartResponse>(new StartRequest {TargetTowerType = type, TowerBattleQuestId = questId});
                    fights++;
                    var win = resp.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker();
                    log($"{TextResourceTable.Get(type)} {questId}: {(win ? "WIN" : "LOSE")}");
                    await _battleLogManager.SaveBattleLog(resp.BattleResult, $"tower-{type}", questId.ToString(), autoDeletePreserveCount: 50);
                    losses = win ? 0 : losses + 1;
                }
                catch (Exception e)
                {
                    // 属性塔未解放时其余属性塔也不用试了
                    if (e is ApiErrorException {ErrorCode: ErrorCode.TowerBattleElementTowerNotOpen}) _elementTowersLocked = true;
                    log($"{TextResourceTable.Get(type)} {questId}: {e.Message}");
                    break;
                }

                await Task.Delay(Math.Max(GameConfig.AutoRequestDelay, 1500), token);
            }

            var endFloor = UserSyncData.UserTowerBattleDtoInfos?.FirstOrDefault(d => d.TowerType == type)?.MaxTowerBattleId ?? 0;
            if (endFloor > startFloor) Track(GrowthKind.Tower, $"{TextResourceTable.Get(type)} floor {startFloor} → {endFloor}");
        });
    }
}
