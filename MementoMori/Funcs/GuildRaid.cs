using MementoMori.Exceptions;
using MementoMori.Ortega.Share.Data.ApiInterface.Guild;
using MementoMori.Ortega.Share.Data.ApiInterface.GuildRaid;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    private bool IsGuildRaidQuickAvailable
    {
        get
        {
            var vip = VipTable.GetByLevel(UserSyncData.UserStatusDtoInfo.Vip);
            var isQuickAvailable = vip.IsQuickStartGuildRaidAvailable;
            if (!isQuickAvailable) isQuickAvailable = UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId >= OpenContentTable.GetByOpenCommandType(OpenCommandType.GuildRaidQuick).OpenContentValue;

            return isQuickAvailable;
        }
    }

    public async Task GuildRaid()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var response1 = await GetResponse<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
            log($"{TextResourceTable.Get("[GuildId]")} {response1.GuildId}");
            if (response1.GuildId == 0) return;
            bool hasRaid;
            do
            {
                hasRaid = false;
                var response2 = await GetResponse<GetGuildRaidInfoRequest, GetGuildRaidInfoResponse>(new GetGuildRaidInfoRequest {BelongGuildId = response1.GuildId});
                foreach (var info in response2.GuildRaidInfos)
                {
                    if (info.IsOpen && (info.UserGuildRaidDtoInfo == null || info.UserGuildRaidDtoInfo is {ChallengeCount: < 2}))
                    {
                        if (IsGuildRaidQuickAvailable && info.UserGuildRaidPreviousDtoInfo != null)
                        {
                            var response3 = await GetResponse<QuickStartGuildRaidRequest, QuickStartGuildRaidResponse>(new QuickStartGuildRaidRequest
                                {BelongGuildId = response1.GuildId, GuildRaidBossType = info.GuildRaidDtoInfo.BossType});
                            log($"{ResourceStrings.BattleResult}: {response3.BattleSimulationResult.BattleEndInfo.IsWinAttacker()}");
                            response3.BattleRewardResult.FixedItemList.PrintUserItems(log);
                            response3.BattleRewardResult.DropItemList.PrintUserItems(log);
                        }
                        else
                        {
                            var response3 = await GetResponse<StartGuildRaidRequest, StartGuildRaidResponse>(new StartGuildRaidRequest
                                {BelongGuildId = response1.GuildId, GuildRaidBossType = info.GuildRaidDtoInfo.BossType});
                            log($"{ResourceStrings.BattleResult}: {response3.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker()}");
                            response3.BattleRewardResult.FixedItemList.PrintUserItems(log);
                            response3.BattleRewardResult.DropItemList.PrintUserItems(log);
                        }

                        hasRaid = true;
                    }

                    if (info.IsExistWorldDamageReward)
                    {
                        var bossMb = GuildRaidBossTable.GetByGuildRaidBossType(info.GuildRaidDtoInfo.BossType);
                        if (bossMb != null)
                        {
                            var worldRewardInfoResponse =
                                await GetResponse<GetGuildRaidWorldRewardInfoRequest, GetGuildRaidWorldRewardInfoResponse>(new GetGuildRaidWorldRewardInfoRequest {GuildRaidBossId = bossMb.Id});
                            var guildRaidRewardMb = GuildRaidRewardTable.GetByBossId(bossMb.Id);
                            foreach (var worldDamageBar in guildRaidRewardMb.WorldDamageBarRewards)
                            {
                                var worldRewardInfo = worldRewardInfoResponse.WorldRewardInfos.FirstOrDefault(d => d.GoalDamage == worldDamageBar.GoalDamage);
                                if (worldRewardInfoResponse.TotalWorldDamage >= worldDamageBar.GoalDamage && (worldRewardInfo == null || !worldRewardInfo.IsReceived))
                                {
                                    var resp = await GetResponse<GiveGuildRaidWorldRewardItemRequest, GiveGuildRaidWorldRewardItemResponse>(
                                        new GiveGuildRaidWorldRewardItemRequest {GoalDamage = worldDamageBar.GoalDamage, GuildRaidBossId = bossMb.Id});
                                    log(TextResourceTable.Get("[GuildRaidCurrentWorldDamageFormat]", worldDamageBar.GoalDamage));
                                    resp.RewardItems.PrintUserItems(log);
                                }
                            }
                        }
                    }
                }
            } while (hasRaid);

            log($"{TextResourceTable.Get("[QuickBattleTitle]")} {TextResourceTable.Get("[CommonHeaderGuildRaidLabel]")} {ResourceStrings.Finished}");
        });
    }

    public async Task OpenGuildRaid()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var response1 = await GetResponse<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
            if (response1.GuildId == 0)
            {
                log(TextResourceTable.Get("[RankingNotGuild]"));
                return;
            }

            var guildMemberInfoResponse = await GetResponse<GetGuildMemberInfoRequest, GetGuildMemberInfoResponse>(new GetGuildMemberInfoRequest {GuildId = response1.GuildId});
            var playerInfo = guildMemberInfoResponse.PlayerInfoList.Find(d => d.PlayerId == UserSyncData.UserStatusDtoInfo.PlayerId);
            if (playerInfo.PlayerGuildPositionType == PlayerGuildPositionType.Member || playerInfo.PlayerGuildPositionType == PlayerGuildPositionType.None)
            {
                log(TextResourceTable.GetErrorCodeMessage(ErrorCode.GuildRaidNotHavePermission));
                return;
            }

            var response2 = await GetResponse<GetGuildRaidInfoRequest, GetGuildRaidInfoResponse>(new GetGuildRaidInfoRequest {BelongGuildId = response1.GuildId});
            var guildRaidInfo = response2.GuildRaidInfos.Find(d => d.GuildRaidDtoInfo.BossType == GuildRaidBossType.Releasable);
            if (guildRaidInfo.IsOpen)
            {
                log(TextResourceTable.GetErrorCodeMessage(ErrorCode.GuildRaidAlreadyOpenGuildRaid));
                return;
            }

            try
            {
                await GetResponse<OpenGuildRaidRequest, OpenGuildRaidResponse>(new OpenGuildRaidRequest {BelongGuildId = response1.GuildId, GuildRaidBossType = GuildRaidBossType.Releasable});
                log($"{TextResourceTable.Get("[GuildRaidReleaseConfirmTitle]")} {ResourceStrings.Success}");
            }
            catch (ApiErrorException e)
            {
                log(e.Message);
            }
        });
    }
}