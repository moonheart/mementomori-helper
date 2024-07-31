using MementoMori.Exceptions;
using MementoMori.Ortega.Share.Data.ApiInterface.Auth;
using MementoMori.Ortega.Share.Data.ApiInterface.Friend;
using MementoMori.Ortega.Share.Data.ApiInterface.User;
using MementoMori.Ortega.Share.Data.DtoInfo;
using Microsoft.Extensions.DependencyInjection;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    public async Task BulkTransferFriendPoint()
    {
        await ExecuteQuickAction(async (log, _) =>
        {
            try
            {
                await GetResponse<BulkTransferFriendPointRequest, BulkTransferFriendPointResponse>(new BulkTransferFriendPointRequest());
                log($"{TextResourceTable.Get("[ItemName9]")} {TextResourceTable.Get("[BulkReceiveAndSend]")} {ResourceStrings.Finished}");
            }
            catch (ApiErrorException e) when (e.ErrorCode == ErrorCode.FriendAlreadyMaxReceived)
            {
                log(e.Message);
            }
        });
    }

    public async Task UseFriendCode()
    {
        await ExecuteQuickAction(async (log, _) =>
        {
            var iconInfo = Mypage.MypageInfo.GetMypageIconInfoByTransferSpotType(TransferSpotType.FriendCampaign);
            if (iconInfo != null)
            {
                var response = await GetResponse<GetFriendCampaignInfoRequest, GetFriendCampaignInfoResponse>(new GetFriendCampaignInfoRequest());
                var friendCampaignMb = FriendCampaignTable.GetArray().MaxBy(d => d.Id);
                if (friendCampaignMb != null)
                {
                    var userFriendMissionDtoInfo = response.UserFriendMissionDtoInfoList.Find(d => d.AchievementType == MissionAchievementType.UseFriendCode);
                    if (userFriendMissionDtoInfo == null)
                    {
                        userFriendMissionDtoInfo = new UserFriendMissionDtoInfo
                        {
                            MissionStatusHistory = new Dictionary<MissionStatusType, List<long>>
                            {
                                [MissionStatusType.Locked] = friendCampaignMb.FriendMissionIdList.ToList(),
                                [MissionStatusType.Progress] = [],
                                [MissionStatusType.NotReceived] = [],
                                [MissionStatusType.Received] = []
                            }
                        };
                    }

                    var friendCode = response.FriendCode;
                    foreach (var id in userFriendMissionDtoInfo.MissionStatusHistory[MissionStatusType.NotReceived])
                    {
                        await ReceiveReward(id);
                    }

                    foreach (var id in userFriendMissionDtoInfo.MissionStatusHistory[MissionStatusType.Progress])
                    {
                        await UseCode();
                        await ReceiveReward(id);
                    }

                    foreach (var id in userFriendMissionDtoInfo.MissionStatusHistory[MissionStatusType.Locked])
                    {
                        await UseCode();
                        await ReceiveReward(id);
                    }

                    async Task UseCode()
                    {
                        var networkManager = _serviceProvider.GetService<MementoNetworkManager>();
                        var funcs = _serviceProvider.GetService<MementoMoriFuncs>();
                        funcs.NetworkManager = networkManager;

                        var timeserverId = _lastPlayerDataInfo.WorldId / 1000;
                        var timeServerMb = TimeServerTable.GetById(timeserverId);
                        var countryCode = timeServerMb.CountryCodeList.FirstOrDefault() ?? "CN";

                        log("Creating new account...");
                        var createUserResponse = await networkManager.GetResponse<CreateUserRequest, CreateUserResponse>(new CreateUserRequest
                        {
                            AdverisementId = Guid.NewGuid().ToString("D"),
                            AppVersion = AuthOption.AppVersion,
                            CountryCode = countryCode,
                            DeviceToken = "",
                            ModelName = AuthOption.ModelName,
                            DisplayLanguage = NetworkManager.LanguageType,
                            OSVersion = AuthOption.OSVersion,
                            SteamTicket = ""
                        });

                        funcs.UserId = createUserResponse.UserId;
                        networkManager.UserId = createUserResponse.UserId;

                        var createWorldPlayerResponse = await networkManager.GetResponse<CreateWorldPlayerRequest, CreateWorldPlayerResponse>(
                            new CreateWorldPlayerRequest
                            {
                                WorldId = createUserResponse.RecommendWorldId,
                                Comment = "Nice to meet you!",
                                Name = "New Player"
                            });

                        // get server host
                        await networkManager.SetServerHost(createUserResponse.RecommendWorldId);

                        // do login
                        await networkManager.GetResponse<LoginPlayerRequest, LoginPlayerResponse>(new LoginPlayerRequest
                        {
                            PlayerId = createWorldPlayerResponse.PlayerId, Password = createWorldPlayerResponse.Password
                        }, log);

                        log($"Using friend code {friendCode}...");
                        await networkManager.GetResponse<UseFriendCodeRequest, UseFriendCodeResponse>(new UseFriendCodeRequest {FriendCode = friendCode});
                    }

                    async Task ReceiveReward(long missionId)
                    {
                        var res = await GetResponse<RewardFriendMissionRequest, RewardFriendMissionResponse>(new RewardFriendMissionRequest
                        {
                            AchievementType = MissionAchievementType.UseFriendCode,
                            FriendMissionId = missionId,
                            FriendCampaignId = friendCampaignMb.Id
                        });
                        res.RewardItemList.PrintUserItems(log);
                    }
                }
            }
        });
    }

    public async Task AutoFriendManage()
    {
        await ExecuteQuickAction(async (log, _) =>
        {
            var manageOption = PlayerOption.FriendManage;
            if (manageOption.AutoRemoveInactiveFriend)
            {
                var info = await GetResponse<GetPlayerInfoListRequest, GetPlayerInfoListResponse>(new GetPlayerInfoListRequest {FriendInfoType = FriendInfoType.Friend});
                foreach (var playerInfo in info.PlayerInfoList)
                {
                    if (manageOption.AutoRemoveWhitelist.Contains(playerInfo.PlayerId)) continue;

                    if (playerInfo.LastLoginTime < TimeSpan.FromDays(7)) continue;

                    await GetResponse<RemoveFriendRequest, RemoveFriendResponse>(new RemoveFriendRequest {TargetPlayerId = playerInfo.PlayerId});
                    log($"{ResourceStrings.Remove_friends_inactive_for_7_days}: {playerInfo.PlayerName}");
                }
            }

            if (manageOption.AutoAcceptFriendRequest)
            {
                var info = await GetResponse<GetPlayerInfoListRequest, GetPlayerInfoListResponse>(new GetPlayerInfoListRequest {FriendInfoType = FriendInfoType.ApprovalPending});
                if (info.FriendNum < 40 && info.PlayerInfoList.Count > 0)
                {
                    var response = await GetResponse<ReplyAllFriendRequest, ReplyAllFriendResponse>(new ReplyAllFriendRequest {IsApproval = true});
                    log($"{ResourceStrings.Auto_accept_friend_requests}: {response.ProcessedNum}");
                }
            }

            if (manageOption.AutoSendFriendRequest)
            {
                var info = await GetResponse<GetPlayerInfoListRequest, GetPlayerInfoListResponse>(new GetPlayerInfoListRequest {FriendInfoType = FriendInfoType.Recommend});
                if (info.FriendNum < 40 && info.PlayerInfoList.Count > 0)
                {
                    var response = await GetResponse<BulkApplyFriendsRequest, BulkApplyFriendsResponse>(new BulkApplyFriendsRequest
                        {TargetPlayerIdList = info.PlayerInfoList.Select(d => d.PlayerId).ToList()});
                    foreach (var l in response.AppliedPlayerIdList)
                    {
                        var name = info.PlayerInfoList.Find(d => d.PlayerId == l).PlayerName;
                        log($"{ResourceStrings.Auto_send_friend_requests}: {name}");
                    }
                }
            }
        });
    }
}