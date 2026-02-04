using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Friend;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 好友管理处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class FriendManageHandler : IGameActionHandler
{
    private readonly ILogger<FriendManageHandler> _logger;
    private readonly JobLogger _jobLogger;
    private readonly PlayerSettingService _playerSettingService;

    public string ActionName => "好友管理";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, "正在执行好友管理...");

        try
        {
            var friendManageSettings = await _playerSettingService.GetFriendManageSettingsAsync(userId);

            // 自动移除不活跃好友
            if (friendManageSettings.AutoRemoveInactiveFriend)
            {
                var info = await nm.SendRequest<GetPlayerInfoListRequest, GetPlayerInfoListResponse>(
                    new GetPlayerInfoListRequest { FriendInfoType = FriendInfoType.Friend }, false);

                var removedCount = 0;
                foreach (var playerInfo in info.PlayerInfoList)
                {
                    // 跳过白名单
                    if (friendManageSettings.AutoRemoveWhitelist?.Contains(playerInfo.PlayerId) == true)
                        continue;

                    // 7天未登录
                    if (playerInfo.LastLoginTime < TimeSpan.FromDays(7))
                        continue;

                    try
                    {
                        await nm.SendRequest<RemoveFriendRequest, RemoveFriendResponse>(
                            new RemoveFriendRequest { TargetPlayerId = playerInfo.PlayerId }, false);
                        removedCount++;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "移除好友失败 {PlayerId} for user {UserId}", playerInfo.PlayerId, userId);
                    }
                }

                if (removedCount > 0)
                {
                    await _jobLogger.LogAsync(userId, $"已移除 {removedCount} 个不活跃好友。");
                }
            }

            // 自动接受好友请求
            if (friendManageSettings.AutoAcceptFriendRequest)
            {
                var info = await nm.SendRequest<GetPlayerInfoListRequest, GetPlayerInfoListResponse>(
                    new GetPlayerInfoListRequest { FriendInfoType = FriendInfoType.ApprovalPending }, false);

                if (info.FriendNum < 40 && info.PlayerInfoList.Count > 0)
                {
                    var response = await nm.SendRequest<ReplyAllFriendRequest, ReplyAllFriendResponse>(
                        new ReplyAllFriendRequest { IsApproval = true }, false);
                    await _jobLogger.LogAsync(userId, $"已接受 {response.ProcessedNum} 个好友请求。");
                }
            }

            // 自动发送好友请求
            if (friendManageSettings.AutoSendFriendRequest)
            {
                var info = await nm.SendRequest<GetPlayerInfoListRequest, GetPlayerInfoListResponse>(
                    new GetPlayerInfoListRequest { FriendInfoType = FriendInfoType.Recommend }, false);

                if (info.FriendNum < 40 && info.PlayerInfoList.Count > 0)
                {
                    var response = await nm.SendRequest<BulkApplyFriendsRequest, BulkApplyFriendsResponse>(
                        new BulkApplyFriendsRequest
                        {
                            TargetPlayerIdList = info.PlayerInfoList.Select(d => d.PlayerId).ToList()
                        }, false);
                    await _jobLogger.LogAsync(userId, $"已发送 {response.AppliedPlayerIdList?.Count ?? 0} 个好友请求。");
                }
            }

            await _jobLogger.LogAsync(userId, "好友管理执行完毕。");
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "好友管理失败 for user {UserId}", userId);
        }
    }
}
