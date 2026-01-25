using MementoMori.Api.Extensions;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;
using MementoMori.Api.Services;
using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.Battle;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 竞技场 (Arena) 自动战斗处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class ArenaPvpHandler : IGameActionHandler
{
    private readonly ILogger<ArenaPvpHandler> _logger;
    private readonly JobLogger _jobLogger;
    private readonly IServiceProvider _serviceProvider;

    public string ActionName => "竞技场自动战斗";

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

        await _jobLogger.LogAsync(userId, "正在检查竞技场挑战次数...");

        int count = 10; // 限制单次任务最大对战次数
        while (count-- > 0)
        {
            try
            {
                // 1. 获取竞技场信息
                var pvpInfoResp = await nm.SendRequest<GetPvpInfoRequest, GetPvpInfoResponse>(new GetPvpInfoRequest(), false);

                // 2. 检查次数
                if (nm.UserSyncData.UserBattlePvpDtoInfo.PvpTodayCount >= OrtegaConst.BattlePvp.MaxPvpBattleFreeCount)
                {
                    _logger.LogInformation("User {UserId} has no free arena challenge counts left.", userId);
                    await _jobLogger.LogAsync(userId, "今日竞技场免费次数已用完。");
                    return;
                }

                // 3. 构建对手列表
                var rivals = new List<(long playerId, long defenseBattlePower, List<(long characterId, string? guid)> characters, long rank)>();
                foreach (var rival in pvpInfoResp.MatchingRivalList)
                {
                    rivals.Add((
                        rival.PlayerInfo.PlayerId, 
                        rival.DefenseBattlePower, 
                        rival.UserCharacterInfoList.Select(x => (x.CharacterId, x.Guid)).ToList(),
                        rival.CurrentRank
                    ));
                }

                // 4. 选择目标 (复用 LegendLeague 的简单选择逻辑)
                var target = SelectTarget(playerOption.BattleLeague, rivals);
                if (target.playerId == 0)
                {
                    _logger.LogInformation("No suitable arena target found for user {UserId}.", userId);
                    await _jobLogger.LogAsync(userId, "未找到符合筛选条件的竞技场对手。");
                    break;
                }

                // 5. 开始战斗
                var startResp = await nm.SendRequest<PvpStartRequest, PvpStartResponse>(new PvpStartRequest
                {
                    RivalPlayerId = target.playerId,
                    RivalPlayerRank = target.rank
                }, false);

                bool isWin = startResp.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker();
                string rivalName = startResp.RivalPlayerInfo?.PlayerName ?? "Unknown";
                
                await _jobLogger.LogAsync(userId, $"对战 {rivalName} (排名 {target.rank}): {(isWin ? "胜利" : "失败")}");
                
                // 延迟
                await Task.Delay(Random.Shared.Next(1000, 3000));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ArenaPvpHandler for user {UserId}", userId);
                await _jobLogger.LogAsync(userId, $"竞技场对战异常: {ex.Message}", "Error");
                break;
            }
        }
    }

    private (long playerId, long rank) SelectTarget(
        PvpOption option, 
        List<(long playerId, long defenseBattlePower, List<(long characterId, string? guid)> characters, long rank)> rivals)
    {
        var filtered = rivals.Where(r => !option.ExcludePlayerIds.Contains(r.playerId)).ToList();

        // 角色过滤
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
            }
            filtered.RemoveAll(r => toRemove.Contains(r.playerId));
        }

        if (filtered.Count == 0) return (0, 0);

        // 排序策略
        var selected = option.SelectStrategy switch
        {
            TargetSelectStrategy.LowestBattlePower => filtered.OrderBy(r => r.defenseBattlePower).First(),
            TargetSelectStrategy.HighestBattlePower => filtered.OrderByDescending(r => r.defenseBattlePower).First(),
            _ => filtered.OrderBy(_ => Guid.NewGuid()).First() // Random
        };

        return (selected.playerId, selected.rank);
    }
}
