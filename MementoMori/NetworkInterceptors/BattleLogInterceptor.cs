using AutoCtor;
using Injectio.Attributes;
using MementoMori.Apis;
using MementoMori.Ortega.Share.Data.ApiInterface;
using MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using MementoMori.Ortega.Share.Data.ApiInterface.TowerBattle;
using Microsoft.Extensions.Options;

namespace MementoMori.NetworkInterceptors;

[RegisterSingleton]
[AutoConstruct]
public partial class BattleLogInterceptor : INetworkInterceptor
{
    private readonly BattleLogManager _battleLogManager;
    private readonly IMemeMoriServerApi _memeMoriServerApi;
    private readonly IOptionsMonitor<GameConfig> _gameConfig;

    public async Task<TResp> InterceptAsync<TReq, TResp>(TReq req, Func<TReq, Task<TResp>> next)
        where TReq : ApiRequestBase
        where TResp : ApiResponseBase
    {
        var resp = await next(req);
        await WriteLogToDisk(req, resp);
        await ReportLogToServer(req, resp);
        return resp;
    }

    private async Task WriteLogToDisk<TReq, TResp>(TReq request, TResp response)
    {
        if (!_gameConfig.CurrentValue.RecordBattleLog)
            return;
        switch (request, response)
        {
            case (BossRequest req, BossResponse resp):
                await _battleLogManager.SaveBattleLog(resp.BattleResult, "main", resp.BattleResult.QuestId.ToString(), "main-*lose");
                break;
            case (StartRequest req, StartResponse resp):
                await _battleLogManager.SaveBattleLog(resp.BattleResult, $@"tower-{req.TargetTowerType}", resp.BattleResult.QuestId.ToString(), $"tower-{req.TargetTowerType}-*lose");
                break;
            case (PvpStartRequest req, PvpStartResponse resp):
                await _battleLogManager.SaveBattleLog(resp.BattleResult, "battleleague", resp.RivalPlayerInfo.PlayerName, autoDeletePreserveCount: 100);
                break;
            case (LegendLeagueStartRequest req, LegendLeagueStartResponse resp):
                await _battleLogManager.SaveBattleLog(resp.BattleResult, "legendleague", resp.RivalPlayerInfo.PlayerName);
                break;
        }
    }

    private async Task ReportLogToServer<TReq, TResp>(TReq request, TResp response)
    {
        if (!_gameConfig.CurrentValue.ReportBattleLog)
            return;

        // switch (request, response)
        // {
        //     case (BossRequest req, BossResponse resp):
        //         await _battleLogManager.SaveBattleLog(resp.BattleResult, "main", resp.BattleResult.QuestId.ToString(), "main-*lose");
        //         break;
        //     case (StartRequest req, StartResponse resp):
        //         await _battleLogManager.SaveBattleLog(resp.BattleResult, $@"tower-{req.TargetTowerType}", resp.BattleResult.QuestId.ToString(), $"tower-{req.TargetTowerType}-*lose");
        //         break;
        //     case (PvpStartRequest req, PvpStartResponse resp):
        //         await _battleLogManager.SaveBattleLog(resp.BattleResult, "battleleague", resp.RivalPlayerInfo.PlayerName, autoDeletePreserveCount: 100);
        //         break;
        //     case (LegendLeagueStartRequest req, LegendLeagueStartResponse resp):
        //         await _battleLogManager.SaveBattleLog(resp.BattleResult, "legendleague", resp.RivalPlayerInfo.PlayerName);
        //         break;
        // }
    }
}