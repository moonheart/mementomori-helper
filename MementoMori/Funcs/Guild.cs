using MementoMori.MagicOnion;
using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Custom;
using MementoMori.Ortega.Share.Data.ApiInterface.GlobalGvg;
using MementoMori.Ortega.Share.Data.ApiInterface.Guild;
using MementoMori.Ortega.Share.Data.ApiInterface.LocalGvg;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    public async Task GuildCheckin()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var response1 = await GetResponse<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
            log($"{TextResourceTable.Get("[GuildId]")} {response1.GuildId}");
            var response2 = await GetResponse<GetGuildBaseInfoRequest, GetGuildBaseInfoResponse>(new GetGuildBaseInfoRequest {BelongGuildId = response1.GuildId});
            log($"{TextResourceTable.Get("[MissionName533]")} {ResourceStrings.Finished}");
            response2.UserSyncData.GivenItemCountInfoList.PrintUserItems(log);
        });
    }

    public async Task SetupLocalGvgDefense()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var response1 = await GetResponse<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
            if (response1.GuildId == 0)
            {
                log(TextResourceTable.Get("[RankingNotGuild]"));
                return;
            }

            var response2 = await GetResponse<GetGuildBaseInfoRequest, GetGuildBaseInfoResponse>(new GetGuildBaseInfoRequest {BelongGuildId = response1.GuildId});
            if (!response2.LocalGuildGvgInfo.IsOpen)
            {
                log(TextResourceTable.GetErrorCodeMessage(ErrorCode.MagicOnionNotOpenGuildBattle));
                return;
            }

            var client = NetworkManager.GetOnionClient();
            var localGvgReceiver = new MagicOnionGvgReceiver(client, log);
            client.SetupGvg(localGvgReceiver, localGvgReceiver, BattleType.GuildBattle);
            await client.Connect();
            while (client.GetState() != HubClientState.Ready)
            {
                if (token.IsCancellationRequested) return;
                log("waiting for connection...");
                await Task.Delay(1000);
            }

            var keepaliveCts = new CancellationTokenSource();
            _ = Task.Run(async () =>
            {
                while (!keepaliveCts.IsCancellationRequested)
                {
                    client.SendKeepAliveAsync();
                    await Task.Delay(5000);
                }
            });

            try
            {
                client.SendGvgOpenMap(BattleType.GuildBattle, 0);
                while (!localGvgReceiver.IsCastleInfoUpdated)
                {
                    if (token.IsCancellationRequested) return;
                    log("waiting for castle info...");
                    await Task.Delay(1000);
                }

                var castleInfos = localGvgReceiver.CastleInfos
                    .Where(d => d.GvgCastleState is GvgCastleState.None or GvgCastleState.InBattle && d.GuildId == response1.GuildId)
                    .OrderByDescending(d => LocalGvgCastleTable.GetById(d.CastleId).CastleType)
                    .ToList();

                if (castleInfos.Count == 0)
                {
                    log(ResourceStrings.No_deployable_castles_available);
                    return;
                }

                var noMoreDeploy = false;
                var queue = new Queue<int>(new[] {5, 3, 3, 3});
                while (!noMoreDeploy && !token.IsCancellationRequested)
                {
                    foreach (var castleInfo in castleInfos)
                    {
                        if (token.IsCancellationRequested) break;

                        // open deploy dialog to update character list
                        localGvgReceiver.IsDeployCharacterUpdated = false;
                        client.SendGvgOpenPartyDeployDialog(BattleType.GuildBattle, castleInfo.CastleId);
                        while (!localGvgReceiver.IsDeployCharacterUpdated)
                        {
                            if (token.IsCancellationRequested) return;
                            log("waiting for deploy dialog to open...");
                            await Task.Delay(1000);
                        }

                        // calculate character count
                        var characterCount = queue.TryDequeue(out var i) ? i : 1;
                        var characterInfos = localGvgReceiver.OnUpdateDeployCharacterResponse.PartyCharacterInfos
                            .Where(d => !d.IsDeployed)
                            .OrderByDescending(d => d.UserGvgCharacterInfo.BattlePower)
                            .Take(characterCount).ToList();
                        if (characterInfos.Count == 0)
                        {
                            noMoreDeploy = true;
                            break;
                        }

                        var characterIds = characterInfos.Select(d => d.UserGvgCharacterInfo.UserCharacterInfo.CharacterId).ToList();
                        localGvgReceiver.IsDeployCharacterUpdated = false;
                        // deploy
                        client.SendGvgAddCastleParty(BattleType.GuildBattle, castleInfo.CastleId, characterIds, characterInfos.Count);
                        while (!localGvgReceiver.IsDeployCharacterUpdated)
                        {
                            if (token.IsCancellationRequested) return;
                            log("waiting for deploy...");
                            await Task.Delay(1000);
                        }

                        // log
                        var name = TextResourceTable.Get(LocalGvgCastleTable.GetById(castleInfo.CastleId).NameKey);
                        var characters = string.Join(", ", characterIds.Select(d => CharacterTable.GetById(d).GetCombinedName()));
                        log(string.Format(ResourceStrings.Successfully_deployed, name, characters));
                        client.SendGvgCloseCastleDialog(BattleType.GuildBattle, GvgDialogType.Deploy);
                        await Task.Delay(1000);
                    }
                }
            }
            finally
            {
                keepaliveCts.Cancel();
                client.ClearGvgReceiver();
                await client.DisposeAsync();
            }

            log($"{ResourceStrings.Deploy_defense} {ResourceStrings.Finished}");
        });
    }

    public async Task ReceiveGvgReward()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            log($"{TextResourceTable.Get("[CommonHeaderGvgLabel]")} {TextResourceTable.Get("[CommonHeaderGlobalGvgLabel]")} {TextResourceTable.Get("[GuildRewardTitle]")}");
            var guildIdResponse = await GetResponse<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
            if (guildIdResponse.GuildId <= 0) return;

            var guildBaseInfoResponse = await GetResponse<GetGuildBaseInfoRequest, GetGuildBaseInfoResponse>(new GetGuildBaseInfoRequest
            {
                BelongGuildId = guildIdResponse.GuildId
            });

            if (guildBaseInfoResponse.LocalGuildGvgInfo.CanGetCastleRewardInfoList.IsNotNullOrEmpty())
            {
                log($"{TextResourceTable.Get("[CommonHeaderGvgLabel]")} {TextResourceTable.Get("[GuildRewardTitle]")}");
                var localGvgRewardResponse = await GetResponse<ReceiveLocalGvgRewardRequest, ReceiveLocalGvgRewardResponse>(new ReceiveLocalGvgRewardRequest
                {
                    CastleIdList = guildBaseInfoResponse.LocalGuildGvgInfo.CanGetCastleRewardInfoList.Select(d => d.CastleId).ToList()
                });
                localGvgRewardResponse.RewardItems.PrintUserItems(log);
            }

            if (guildBaseInfoResponse.GlobalGuildGvgInfo.CanGetCastleRewardInfoList.IsNotNullOrEmpty())
            {
                log($"{TextResourceTable.Get("[CommonHeaderGlobalGvgLabel]")} {{TextResourceTable.Get(\"[GuildRewardTitle]\")}}");
                var receiveGlobalGvgRewardResponse = await GetResponse<ReceiveGlobalGvgRewardRequest, ReceiveGlobalGvgRewardResponse>(new ReceiveGlobalGvgRewardRequest
                {
                    CastleIdList = guildBaseInfoResponse.GlobalGuildGvgInfo.CanGetCastleRewardInfoList.Select(d => d.CastleId).ToList()
                });
                receiveGlobalGvgRewardResponse.RewardItems.PrintUserItems(log);
            }
        });
    }
}