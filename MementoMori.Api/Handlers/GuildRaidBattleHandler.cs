using MementoMori.Api.Extensions;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Guild;
using MementoMori.Ortega.Share.Data.ApiInterface.GuildRaid;
using MementoMori.Ortega.Share.Data.GuildRaid;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 公会副本自动战斗处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class GuildRaidBattleHandler : IGameActionHandler
{
    private readonly ILogger<GuildRaidBattleHandler> _logger;
    private readonly JobLogger _jobLogger;

    public string ActionName => "公会副本自动战斗";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        try
        {
            // 1. 获取公会ID
            var guildIdResp = await nm.SendRequest<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest(), false);

            if (guildIdResp.GuildId == 0)
            {
                _logger.LogDebug("User {UserId} is not in a guild, skipping guild raid.", userId);
                return;
            }

            await _jobLogger.LogAsync(userId, $"开始公会副本自动战斗，公会ID: {guildIdResp.GuildId}");

            // 2. 检查是否有快速战斗权限
            var isQuickAvailable = IsGuildRaidQuickAvailable(nm);

            // 3. 循环战斗直到没有可挑战的副本
            bool hasRaid;
            int totalBattles = 0;
            do
            {
                hasRaid = false;
                var raidInfoResp = await nm.SendRequest<GetGuildRaidInfoRequest, GetGuildRaidInfoResponse>(
                    new GetGuildRaidInfoRequest { BelongGuildId = guildIdResp.GuildId }, false);

                foreach (var info in raidInfoResp.GuildRaidInfos)
                {
                    // 检查副本是否开放且挑战次数 < 2
                    if (info.IsOpen && (info.UserGuildRaidDtoInfo == null || info.UserGuildRaidDtoInfo.ChallengeCount < 2))
                    {
                        totalBattles++;

                        // 根据权限选择快速战斗或普通战斗
                        if (isQuickAvailable && info.UserGuildRaidPreviousDtoInfo != null)
                        {
                            // 快速战斗
                            var quickResp = await nm.SendRequest<QuickStartGuildRaidRequest, QuickStartGuildRaidResponse>(
                                new QuickStartGuildRaidRequest
                                {
                                    BelongGuildId = guildIdResp.GuildId,
                                    GuildRaidBossType = info.GuildRaidDtoInfo.BossType
                                }, false);

                            var isWin = quickResp.BattleSimulationResult.BattleEndInfo.IsWinAttacker();
                            await _jobLogger.LogAsync(userId, $"公会副本快速战斗完成，结果: {(isWin ? "胜利" : "失败")}");

                            LogRewards(quickResp.BattleRewardResult.FixedItemList, quickResp.BattleRewardResult.DropItemList);
                        }
                        else
                        {
                            // 普通战斗
                            var startResp = await nm.SendRequest<StartGuildRaidRequest, StartGuildRaidResponse>(
                                new StartGuildRaidRequest
                                {
                                    BelongGuildId = guildIdResp.GuildId,
                                    GuildRaidBossType = info.GuildRaidDtoInfo.BossType
                                }, false);

                            var isWin = startResp.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker();
                            await _jobLogger.LogAsync(userId, $"公会副本战斗完成，结果: {(isWin ? "胜利" : "失败")}");

                            LogRewards(startResp.BattleRewardResult.FixedItemList, startResp.BattleRewardResult.DropItemList);
                        }

                        hasRaid = true;
                    }

                    // 领取全球伤害奖励
                    if (info.IsExistWorldDamageReward)
                    {
                        await ClaimWorldDamageRewards(userId, nm, guildIdResp.GuildId, info);
                    }
                }
            } while (hasRaid);

            if (totalBattles > 0)
            {
                await _jobLogger.LogAsync(userId, $"公会副本自动战斗完成，共进行 {totalBattles} 场战斗。");
            }
            else
            {
                _logger.LogDebug("No available guild raids for user {UserId}.", userId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to execute guild raid battle for user {UserId}", userId);
            await _jobLogger.LogAsync(userId, $"公会副本战斗失败: {ex.Message}", "Error");
        }
    }

    /// <summary>
    /// 检查是否有快速战斗权限
    /// </summary>
    private bool IsGuildRaidQuickAvailable(NetworkManager nm)
    {
        var userSyncData = nm.UserSyncData;
        var vipLevel = userSyncData.UserStatusDtoInfo.Vip;
        var vip = MementoMori.Ortega.Share.Masters.VipTable.GetByLevel(vipLevel);

        if (vip != null && vip.IsQuickStartGuildRaidAvailable)
        {
            return true;
        }

        // 检查是否通过主线进度解锁
        var openContent = MementoMori.Ortega.Share.Masters.OpenContentTable.GetByOpenCommandType(OpenCommandType.GuildRaidQuick);
        if (openContent != null)
        {
            var bossClearMaxQuestId = userSyncData.UserBattleBossDtoInfo?.BossClearMaxQuestId ?? 0;
            return bossClearMaxQuestId >= openContent.OpenContentValue;
        }

        return false;
    }

    /// <summary>
    /// 领取全球伤害奖励
    /// </summary>
    private async Task ClaimWorldDamageRewards(long userId, NetworkManager nm, long guildId, GuildRaidInfo raidInfo)
    {
        try
        {
            var bossMb = MementoMori.Ortega.Share.Masters.GuildRaidBossTable.GetByGuildRaidBossType(raidInfo.GuildRaidDtoInfo.BossType);
            if (bossMb == null)
            {
                _logger.LogWarning("Guild raid boss MB not found for type {BossType}", raidInfo.GuildRaidDtoInfo.BossType);
                return;
            }

            var worldRewardInfoResp = await nm.SendRequest<GetGuildRaidWorldRewardInfoRequest, GetGuildRaidWorldRewardInfoResponse>(
                new GetGuildRaidWorldRewardInfoRequest { GuildRaidBossId = bossMb.Id }, false);

            var guildRaidRewardMb = MementoMori.Ortega.Share.Masters.GuildRaidRewardTable.GetByBossId(bossMb.Id);
            if (guildRaidRewardMb == null || guildRaidRewardMb.WorldDamageBarRewards == null)
            {
                return;
            }

            int rewardCount = 0;
            foreach (var worldDamageBar in guildRaidRewardMb.WorldDamageBarRewards)
            {
                var worldRewardInfo = worldRewardInfoResp.WorldRewardInfos?.FirstOrDefault(d => d.GoalDamage == worldDamageBar.GoalDamage);

                // 检查是否达成目标且未领取
                if (worldRewardInfoResp.TotalWorldDamage >= worldDamageBar.GoalDamage &&
                    (worldRewardInfo == null || !worldRewardInfo.IsReceived))
                {
                    var claimResp = await nm.SendRequest<GiveGuildRaidWorldRewardItemRequest, GiveGuildRaidWorldRewardItemResponse>(
                        new GiveGuildRaidWorldRewardItemRequest
                        {
                            GoalDamage = worldDamageBar.GoalDamage,
                            GuildRaidBossId = bossMb.Id
                        }, false);

                    rewardCount++;
                    _logger.LogInformation("Claimed guild raid world reward for user {UserId}, GoalDamage: {GoalDamage}",
                        userId, worldDamageBar.GoalDamage);
                }
            }

            if (rewardCount > 0)
            {
                await _jobLogger.LogAsync(userId, $"领取了 {rewardCount} 个公会副本全球伤害奖励。");
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to claim world damage rewards for user {UserId}", userId);
        }
    }

    /// <summary>
    /// 记录战斗奖励
    /// </summary>
    private void LogRewards(List<UserItem> fixedItems, List<UserItem> dropItems)
    {
        // 简单记录，不打印详细信息以避免日志过多
        var totalFixed = fixedItems?.Count ?? 0;
        var totalDrop = dropItems?.Count ?? 0;

        if (totalFixed > 0 || totalDrop > 0)
        {
            _logger.LogDebug("Battle rewards: {FixedCount} fixed items, {DropCount} drop items", totalFixed, totalDrop);
        }
    }
}
