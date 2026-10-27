using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.GlobalGvg;
using MementoMori.Ortega.Share.Data.ApiInterface.Guild;
using MementoMori.Ortega.Share.Data.ApiInterface.LocalGvg;

namespace MementoMori.Api.Handlers;

/// <summary>
/// GVG奖励领取处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class GvgRewardHandler : IGameActionHandler
{
    private readonly ILogger<GvgRewardHandler> _logger;
    private readonly JobLogger _jobLogger;

    public string ActionName => Text.T("ActionGvgReward");

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, Text.T("GvgChecking"));

        try
        {
            var guildIdResponse = await nm.SendRequest<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
            if (guildIdResponse.GuildId <= 0)
            {
                await _jobLogger.LogAsync(userId, Text.T("GvgNotJoined"));
                return;
            }

            var guildBaseInfoResponse = await nm.SendRequest<GetGuildBaseInfoRequest, GetGuildBaseInfoResponse>(
                new GetGuildBaseInfoRequest { BelongGuildId = guildIdResponse.GuildId });

            // 领取本地GVG奖励
            if (guildBaseInfoResponse.LocalGuildGvgInfo?.CanGetCastleRewardInfoList?.Count > 0)
            {
                var localGvgRewardResponse = await nm.SendRequest<ReceiveLocalGvgRewardRequest, ReceiveLocalGvgRewardResponse>(
                    new ReceiveLocalGvgRewardRequest
                    {
                        CastleIdList = guildBaseInfoResponse.LocalGuildGvgInfo.CanGetCastleRewardInfoList.Select(d => d.CastleId).ToList()
                    });
                await _jobLogger.LogAsync(userId, Text.T("GvgLocalClaimed"));
            }

            // 领取全球GVG奖励
            if (guildBaseInfoResponse.GlobalGuildGvgInfo?.CanGetCastleRewardInfoList?.Count > 0)
            {
                var receiveGlobalGvgRewardResponse = await nm.SendRequest<ReceiveGlobalGvgRewardRequest, ReceiveGlobalGvgRewardResponse>(
                    new ReceiveGlobalGvgRewardRequest
                    {
                        CastleIdList = guildBaseInfoResponse.GlobalGuildGvgInfo.CanGetCastleRewardInfoList.Select(d => d.CastleId).ToList()
                    });
                await _jobLogger.LogAsync(userId, Text.T("GvgGlobalClaimed"));
            }

            await _jobLogger.LogAsync(userId, Text.T("GvgDone"));
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "领取GVG奖励失败 for user {UserId}", userId);
        }
    }
}
