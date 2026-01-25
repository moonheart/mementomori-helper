using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using MementoMori.Ortega.Share.Data.ApiInterface.Character;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Share.Data.Battle;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 传奇竞技场自动对战处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class LegendLeagueHandler : IGameActionHandler
{
    private readonly ILogger<LegendLeagueHandler> _logger;
    private readonly JobLogger _jobLogger;
    private readonly IServiceProvider _serviceProvider;

    public string ActionName => "传奇竞技场自动对战";

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

        await _jobLogger.LogAsync(userId, "正在检查传奇竞技场...");

        var characterDetailInfoDict = new Dictionary<long, List<(string? guid, CharacterDetailInfo? details)>>();
        int count = 10; // 限制单次任务最多打10次，防止死循环

        while (count-- > 0)
        {
            try
            {
                // 1. 获取竞技场信息
                var leagueInfoResp = await nm.SendRequest<GetLegendLeagueInfoRequest, GetLegendLeagueInfoResponse>(new GetLegendLeagueInfoRequest(), false);

                if (!leagueInfoResp.IsInTimeCanChallenge)
                {
                    _logger.LogInformation("Legend league is not open for user {UserId}.", userId);
                    await _jobLogger.LogAsync(userId, "传奇竞技场当前未开放。");
                    return;
                }

                // 2. 检查次数
                if (nm.UserSyncData.UserBattleLegendLeagueDtoInfo != null && 
                    nm.UserSyncData.UserBattleLegendLeagueDtoInfo.LegendLeagueTodayCount >= 2) // OrtegaConst.BattlePvp.MaxLegendLeagueBattleFreeCount = 2
                {
                    _logger.LogInformation("User {UserId} has no free challenge counts left.", userId);
                    await _jobLogger.LogAsync(userId, "传奇竞技场今日免费次数已用完。");
                    return;
                }

                // 3. 构建对手列表
                var rivals = new List<(long playerId, long defenseBattlePower, List<(long characterId, string? guid)> characters)>();
                foreach (var rival in leagueInfoResp.MatchingRivalList)
                {
                    rivals.Add((
                        rival.PlayerInfo.PlayerId, 
                        rival.DefenseBattlePower, 
                        rival.UserCharacterDtoInfoList.Select(x => (x.CharacterId, x.Guid)).ToList()
                    ));
                }

                // 4. 选择目标
                var targetPlayerId = await SelectTargetAsync(nm, playerOption.LegendLeague, rivals, characterDetailInfoDict);
                if (targetPlayerId == 0)
                {
                    _logger.LogInformation("No suitable target found for user {UserId} in Legend League.", userId);
                    await _jobLogger.LogAsync(userId, "未找到符合筛选条件的对手，跳过对战。");
                    break;
                }

                // 5. 开始战斗
                var startResp = await nm.SendRequest<LegendLeagueStartRequest, LegendLeagueStartResponse>(
                    new LegendLeagueStartRequest { RivalPlayerId = targetPlayerId }, false);

                bool isWin = startResp.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker();
                string rivalName = startResp.RivalPlayerInfo?.PlayerName ?? "Unknown";
                
                await _jobLogger.LogAsync(userId, $"对战 {rivalName}: {(isWin ? "胜利" : "失败")} (获得积分: {startResp.GetPoint})");
                
                // 稍微延迟
                await Task.Delay(Random.Shared.Next(1000, 3000));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LegendLeagueHandler for user {UserId}", userId);
                await _jobLogger.LogAsync(userId, $"竞技场对战异常: {ex.Message}", "Error");
                break;
            }
        }
    }

    private async Task<long> SelectTargetAsync(
        NetworkManager nm, 
        PvpOption option, 
        List<(long playerId, long defenseBattlePower, List<(long characterId, string? guid)> characters)> rivals,
        Dictionary<long, List<(string? guid, CharacterDetailInfo? details)>> cache)
    {
        var filtered = rivals.Where(r => !option.ExcludePlayerIds.Contains(r.playerId)).ToList();

        // 角色过滤逻辑
        if (option.CharacterFilters.Count > 0)
        {
            var toRemove = new HashSet<long>();
            foreach (var filter in option.CharacterFilters)
            {
                if (filter.FilterStrategy == CharacterFilterStrategy.Character)
                {
                    foreach (var rival in filtered)
                    {
                        if (rival.characters.Any(c => c.characterId == filter.CharacterId))
                        {
                            toRemove.Add(rival.playerId);
                        }
                    }
                }
                // PropertyMoreThanSelf 逻辑较复杂且涉及大量详情请求，暂时不实现，仅做基础过滤
            }
            filtered.RemoveAll(r => toRemove.Contains(r.playerId));
        }

        if (filtered.Count == 0) return 0;

        // 根据策略排序
        return option.SelectStrategy switch
        {
            TargetSelectStrategy.LowestBattlePower => filtered.OrderBy(r => r.defenseBattlePower).First().playerId,
            TargetSelectStrategy.HighestBattlePower => filtered.OrderByDescending(r => r.defenseBattlePower).First().playerId,
            _ => filtered.OrderBy(_ => Guid.NewGuid()).First().playerId // Random
        };
    }
}
