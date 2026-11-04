using MementoMori.Api.Handlers;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using MementoMori.Ortega.Share.Data.ApiInterface.Quest;
using MementoMori.Ortega.Share.Data.ApiInterface.TowerBattle;

namespace MementoMori.Api.Autopilot;

internal static class Climb
{
    public const int MaxLosses = 3; // 连输3次就停, 等下次成长后再打
    public const int MaxFights = 60; // ponytail: 每轮上限, 防止再被 403
    public static readonly TimeSpan FightDelay = TimeSpan.FromMilliseconds(1500);
}

/// <summary>
/// 主线推图, 连输3次就停
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class AutoClimbQuestHandler : IGameActionHandler
{
    private readonly GrowthTracker _tracker;
    private readonly BattleLogService _battleLog;
    private readonly TamamoService _tamamo;
    private readonly PlayerSettingService _settings;
    private readonly JobLogger _log;
    public string ActionName => "Climb main quest";

    public async Task ExecuteAsync(AccountContext context)
    {
        var nm = context.NetworkManager;
        var userId = context.AccountInfo.UserId;
        int fights = 0, losses = 0, wins = 0;
        string Q(long id) => QuestTable.GetById(id)?.Memo ?? id.ToString();
        var startQuest = nm.UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId;
        // 默认上传到 tamamo (不需要 key)
        var upload = (await _settings.GetSettingAsync<AutopilotConfig>(userId, AutopilotJob.SettingKey) ?? new()).UploadClears;
        var uploadKey = upload ? await _tamamo.GetKeyAsync(userId) : null;
        await nm.SendRequest<MapInfoRequest, MapInfoResponse>(new MapInfoRequest {IsUpdateOtherPlayerInfo = true});
        while (fights < Climb.MaxFights && losses < Climb.MaxLosses)
        {
            var questId = nm.UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId + 1;
            if (QuestTable.GetById(questId) == null) break; // 通关了
            try
            {
                await nm.SendRequest<GetQuestInfoRequest, GetQuestInfoResponse>(new GetQuestInfoRequest {TargetQuestId = questId});
                var resp = await nm.SendRequest<BossRequest, BossResponse>(new BossRequest {QuestId = questId});
                fights++;
                var win = resp.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker();
                await _log.LogAsync(userId, $"Quest {Q(questId)}: {(win ? "WIN" : "LOSE")}");
                await _battleLog.TrySaveBattleLogAsync(userId, "battle/boss", resp);
                if (win)
                {
                    losses = 0;
                    wins++;
                    if (upload)
                    {
                        try { await _tamamo.UploadQuestClearAsync(uploadKey, nm.UserSyncData, context.AccountInfo.CurrentWorldId ?? 0, questId, resp.BattleResult.SimulationResult); }
                        catch (Exception e) { await _log.LogAsync(userId, $"Tamamo upload {Q(questId)}: {e.Message}", "Warning"); }
                    }
                    await nm.SendRequest<MapInfoRequest, MapInfoResponse>(new MapInfoRequest {IsUpdateOtherPlayerInfo = true});
                    try { await nm.SendRequest<NextQuestRequest, NextQuestResponse>(new NextQuestRequest()); }
                    catch (NetworkManager.ApiErrorException) { /* 没有下一关可移动时会报错 */ }
                }
                else losses++;
            }
            catch (Exception e)
            {
                await _log.LogAsync(userId, $"Quest {Q(questId)}: {e.Message}", "Warning");
                break;
            }

            await Task.Delay(Climb.FightDelay);
        }

        var endQuest = nm.UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId;
        if (endQuest > startQuest)
            await _tracker.TrackAsync(userId, GrowthKind.Quest, $"Main quest {Q(startQuest)} → {Q(endQuest)} ({wins} wins, {fights - wins} losses)");
        else if (losses >= Climb.MaxLosses)
            await _tracker.TrackAsync(userId, GrowthKind.Info, $"Stuck at quest {Q(endQuest + 1)} — lost {losses}x, will retry after next growth");
    }
}

/// <summary>
/// 爬所有能爬的塔 (无穷塔 + 属性塔), 每座塔连输3次就停
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class AutoClimbTowerHandler : IGameActionHandler
{
    private readonly GrowthTracker _tracker;
    private readonly BattleLogService _battleLog;
    private readonly JobLogger _log;
    public string ActionName => "Climb towers";

    public async Task ExecuteAsync(AccountContext context)
    {
        var nm = context.NetworkManager;
        var userId = context.AccountInfo.UserId;
        var types = (nm.UserSyncData.UserTowerBattleDtoInfos ?? new()).Select(t => t.TowerType).OrderBy(t => t != TowerType.Infinite).ToList();
        var elementLocked = false;
        foreach (var type in types)
        {
            if (type != TowerType.Infinite && elementLocked) continue;
            int fights = 0, losses = 0;
            var name = TextResourceTable.Get(type);
            var startFloor = nm.UserSyncData.UserTowerBattleDtoInfos.First(d => d.TowerType == type).MaxTowerBattleId;
            while (fights < Climb.MaxFights && losses < Climb.MaxLosses)
            {
                var tower = nm.UserSyncData.UserTowerBattleDtoInfos.First(d => d.TowerType == type);
                if (type != TowerType.Infinite && tower.TodayClearNewFloorCount >= 10) break;
                var questId = tower.MaxTowerBattleId + 1;
                try
                {
                    var resp = await nm.SendRequest<StartRequest, StartResponse>(new StartRequest {TargetTowerType = type, TowerBattleQuestId = questId});
                    fights++;
                    var win = resp.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker();
                    await _log.LogAsync(userId, $"{name} {questId}: {(win ? "WIN" : "LOSE")}");
                    await _battleLog.TrySaveBattleLogAsync(userId, "towerBattle/start", resp);
                    losses = win ? 0 : losses + 1;
                }
                catch (Exception e)
                {
                    // 属性塔未解放 (或今天不开) 是正常的, 其余属性塔也不用试了, 不刷警告
                    if (e is NetworkManager.ApiErrorException {ErrorCode: ErrorCode.TowerBattleElementTowerNotOpen})
                    {
                        elementLocked = true;
                        break;
                    }
                    await _log.LogAsync(userId, $"{name} {questId}: {e.Message}", "Warning");
                    break;
                }

                await Task.Delay(Climb.FightDelay);
            }

            var endFloor = nm.UserSyncData.UserTowerBattleDtoInfos.First(d => d.TowerType == type).MaxTowerBattleId;
            if (endFloor > startFloor) await _tracker.TrackAsync(userId, GrowthKind.Tower, $"{name} floor {startFloor} → {endFloor}");
        }
    }
}
