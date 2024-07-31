using MementoMori.Exceptions;
using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using MementoMori.Ortega.Share.Data.ApiInterface.Character;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Character;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    public async Task PvpAuto()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            log($"{TextResourceTable.Get("[CommonHeaderLocalPvpLabel]")}:\n");
            var count = 100;
            var characterDetailInfoDict = new Dictionary<long, List<(string, CharacterDetailInfo)>>();
            while (!token.IsCancellationRequested && count-- > 0)
            {
                try
                {
                    var pvpInfoResponse = await GetResponse<GetPvpInfoRequest, GetPvpInfoResponse>(new GetPvpInfoRequest());

                    if (UserSyncData.UserBattlePvpDtoInfo.PvpTodayCount >= OrtegaConst.BattlePvp.MaxPvpBattleFreeCount)
                    {
                        log(TextResourceTable.GetErrorCodeMessage(ErrorCode.BattlePvpOverLegendLeagueChallengeMaxCount));
                        return;
                    }

                    List<(long PlayerId, long DefenseBattlePower, List<(long characterId, Func<Task<BattleParameter>> battleParameter)> characterDetailInfos)> list = new();

                    foreach (var d in pvpInfoResponse.MatchingRivalList)
                    {
                        var characterDetailInfos = BuildPvpPlayerInfo(
                            characterDetailInfoDict,
                            DeckUseContentType.LegendLeagueOffense,
                            d.PlayerInfo.PlayerId,
                            d.UserCharacterInfoList.Select(x => (x.CharacterId, x.Guid)).ToList());

                        list.Add((d.PlayerInfo.PlayerId, d.DefenseBattlePower, characterDetailInfos));
                    }

                    var targetPlayerId = await SelectLeagueTarget(log, PlayerOption.BattleLeague, list);
                    if (targetPlayerId == 0) continue;

                    var playerInfo = pvpInfoResponse.MatchingRivalList.FirstOrDefault(d => d.PlayerInfo.PlayerId == targetPlayerId);
                    if (playerInfo == null) continue;

                    var pvpStartResponse = await GetResponse<PvpStartRequest, PvpStartResponse>(new PvpStartRequest
                    {
                        RivalPlayerRank = playerInfo.CurrentRank,
                        RivalPlayerId = playerInfo.PlayerInfo.PlayerId
                    });

                    await _battleLogManager.SaveBattleLog(pvpStartResponse.BattleResult, "battleleague", pvpStartResponse.RivalPlayerInfo.PlayerName, autoDeletePreserveCount: 100);

                    log(pvpStartResponse.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker()
                        ? $"{TextResourceTable.Get("[CommonWinLabel]")}: {pvpStartResponse.RivalPlayerInfo.PlayerName}"
                        : $"{TextResourceTable.Get("[CommonLoseLabel]")}: {pvpStartResponse.RivalPlayerInfo.PlayerName}");

                    pvpStartResponse.BattleRewardResult.FixedItemList.PrintUserItems(log);
                    pvpStartResponse.BattleRewardResult.DropItemList.PrintUserItems(log);
                }
                catch (ApiErrorException e)
                {
                    log(e.Message);
                    break;
                }
                finally
                {
                    await Task.Delay(TimeSpan.FromSeconds(Random.Shared.Next(2, 5)));
                }
            }
        });
    }

    private async Task<long> SelectLeagueTarget(Action<string> log, PvpOption pvpOption,
        List<(long playerId, long defenseBattlePower, List<(long characterId, Func<Task<BattleParameter>> battleParameter)> chareacters)> playerInfoList)
    {
        // playerInfoList: dict<playerId, dict<characterId, CharacterDetailInfo>>
        var localplayerInfoList = playerInfoList.ToList();

        localplayerInfoList.RemoveAll(d => pvpOption.ExcludePlayerIds.Contains(d.playerId));

        foreach (var characterFilter in pvpOption.CharacterFilters)
        {
            if (playerInfoList.Count == 0) break;

            switch (characterFilter.FilterStrategy)
            {
                case CharacterFilterStrategy.Character:
                    foreach (var (playerId, _, _) in playerInfoList.Where(d => d.chareacters.Any(x => x.characterId == characterFilter.CharacterId)))
                    {
                        localplayerInfoList.RemoveAll(x => x.playerId == playerId);
                    }

                    break;
                case CharacterFilterStrategy.PropertyMoreThanSelf:
                    foreach (var (playerId, _, characterDetailInfos) in playerInfoList)
                    {
                        var (characterId, battleParameter) = characterDetailInfos.FirstOrDefault(d => d.characterId == characterFilter.CharacterId);
                        if (characterId == 0) continue;
                        var battleParameters = await battleParameter();
                        var targetParameterValue = battleParameters?.GetParameter(characterFilter.BattleParameterType) ?? 0;
                        var selfCharacterGuid = UserSyncData.UserCharacterDtoInfos.FirstOrDefault(d => d.CharacterId == characterId)?.Guid;
                        if (selfCharacterGuid == null) continue;
                        var lockType = UserSyncData.ReleaseLockEquipmentCooldownTimeStampMap.ContainsKey(LockEquipmentDeckType.League) ? LockEquipmentDeckType.League : LockEquipmentDeckType.None;
                        var selfParameterValue = BattlePowerCalculatorUtil.CalcCharacterBattleParameter(UserId, selfCharacterGuid, lockType).Item2.GetParameter(characterFilter.BattleParameterType);
                        if (targetParameterValue > selfParameterValue) localplayerInfoList.RemoveAll(x => x.playerId == playerId);
                    }

                    break;
            }
        }

        if (localplayerInfoList.Count == 0) return 0;

        switch (pvpOption.SelectStrategy)
        {
            case TargetSelectStrategy.Random:
                return localplayerInfoList.MinBy(d => Guid.NewGuid()).playerId;
            case TargetSelectStrategy.LowestBattlePower:
                return localplayerInfoList.MinBy(d => d.defenseBattlePower).playerId;
            case TargetSelectStrategy.HighestBattlePower:
                return localplayerInfoList.MaxBy(d => d.defenseBattlePower).playerId;
            default:
                return localplayerInfoList.MinBy(d => Guid.NewGuid()).playerId;
        }
    }

    private List<(long characterId, Func<Task<BattleParameter>> battleParameter)> BuildPvpPlayerInfo(
        Dictionary<long, List<(string guid, CharacterDetailInfo)>> characterDetailInfoDict,
        DeckUseContentType deckUseContentType,
        long playerId,
        List<(long characterId, string guid)> characters)
    {
        var characterDetailInfos = new List<(long characterId, Func<Task<BattleParameter>> battleParameter)>();
        foreach (var (characterId, guid) in characters)
        {
            characterDetailInfos.Add((characterId, async () =>
                    {
                        if (!characterDetailInfoDict.TryGetValue(playerId, out var details))
                        {
                            var allGuids = characters.Select(x => x.guid).ToList();
                            var guids = allGuids.Where(d => !string.IsNullOrEmpty(d)).ToList();
                            if (guids.Count == 0)
                            {
                                details = new List<(string, CharacterDetailInfo)> {(null, null), (null, null), (null, null), (null, null), (null, null)};
                                characterDetailInfoDict.Add(playerId, details);
                            }
                            else
                            {
                                var details1 = (await GetResponse<GetDetailsInfoRequest, GetDetailsInfoResponse>(new GetDetailsInfoRequest
                                {
                                    DeckType = deckUseContentType, TargetPlayerId = playerId,
                                    TargetUserCharacterGuids = guids.ToList()
                                })).CharacterDetailInfos;

                                details = new List<(string, CharacterDetailInfo)>();
                                var index = 0;
                                foreach (var guid1 in allGuids)
                                {
                                    if (string.IsNullOrEmpty(guid1))
                                        details.Add((null, null));
                                    else
                                        details.Add((guid1, details1[index++]));
                                }

                                characterDetailInfoDict.Add(playerId, details);
                            }
                        }

                        return details.FirstOrDefault(d => d.guid == guid).Item2?.BattleParameter;
                    }
                ));
        }

        return characterDetailInfos;
    }

    public async Task LegendLeagueAuto()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            log($"{TextResourceTable.Get("[CommonHeaderGlobalPvpLabel]")}");
            var count = 100;
            var characterDetailInfoDict = new Dictionary<long, List<(string, CharacterDetailInfo)>>();
            while (!token.IsCancellationRequested && count-- > 0)
            {
                try
                {
                    var leagueInfoResponse = await GetResponse<GetLegendLeagueInfoRequest, GetLegendLeagueInfoResponse>(new GetLegendLeagueInfoRequest());

                    if (!leagueInfoResponse.IsInTimeCanChallenge)
                    {
                        log(TextResourceTable.GetErrorCodeMessage(ClientErrorCode.PvpGlobalIsNotOpen));
                        return;
                    }

                    if (UserSyncData.UserBattleLegendLeagueDtoInfo != null && UserSyncData.UserBattleLegendLeagueDtoInfo.LegendLeagueTodayCount >= OrtegaConst.BattlePvp.MaxLegendLeagueBattleFreeCount)
                    {
                        log(TextResourceTable.GetErrorCodeMessage(ErrorCode.BattlePvpOverLegendLeagueChallengeMaxCount));
                        return;
                    }

                    var list = new List<(long playerId, long defenseBattlePower, List<(long characterId, Func<Task<BattleParameter>> battleParameter)> chareacters)>();
                    foreach (var d in leagueInfoResponse.MatchingRivalList)
                    {
                        var characterDetailInfos = BuildPvpPlayerInfo(
                            characterDetailInfoDict,
                            DeckUseContentType.LegendLeagueOffense,
                            d.PlayerInfo.PlayerId,
                            d.UserCharacterDtoInfoList.Select(x => (x.CharacterId, x.Guid)).ToList());

                        list.Add((d.PlayerInfo.PlayerId, d.DefenseBattlePower, characterDetailInfos));
                    }

                    var targetPlayerId = await SelectLeagueTarget(log, PlayerOption.LegendLeague, list);
                    if (targetPlayerId == 0) continue;

                    var leagueStartResponse = await GetResponse<LegendLeagueStartRequest, LegendLeagueStartResponse>(new LegendLeagueStartRequest
                    {
                        RivalPlayerId = targetPlayerId
                    });

                    await _battleLogManager.SaveBattleLog(leagueStartResponse.BattleResult, "legendleague", leagueStartResponse.RivalPlayerInfo.PlayerName);

                    log(leagueStartResponse.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker()
                        ? $"{TextResourceTable.Get("[CommonWinLabel]")}: {leagueStartResponse.RivalPlayerInfo.PlayerName}"
                        : $"{TextResourceTable.Get("[CommonLoseLabel]")}: {leagueStartResponse.RivalPlayerInfo.PlayerName}");

                    log(TextResourceTable.Get("[GlobalPvpChangePointFormat]", leagueStartResponse.GetPoint));
                }
                catch (ApiErrorException e)
                {
                    log(e.Message);
                    break;
                }
                finally
                {
                    await Task.Delay(TimeSpan.FromSeconds(Random.Shared.Next(2, 5)));
                }
            }
        });
    }
}