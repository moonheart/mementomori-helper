using Grpc.Net.Client;
using MementoMori.Ortega.Common.Data;
using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Network.MagicOnion.Interface;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.Chat;
using MementoMori.Ortega.Share.Data.LocalRaid;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share.MagicOnionShare.Interfaces.Receiver;
using MementoMori.Ortega.Share.MagicOnionShare.Interfaces.Sender;
using MementoMori.Ortega.Share.MagicOnionShare.Request;
using MementoMori.Ortega.Share.MagicOnionShare.Response;

namespace MementoMori.Ortega.Network.MagicOnion.Client
{
    /// <summary>
    /// 游戏的 MagicOnion 客户端实现，处理所有实时通信功能。
    /// 包括本地突袭、公会战、聊天等功能。
    /// </summary>
    public class OrtegaMagicOnionClient : MagicOnionClient<IOrtegaSender, IOrtegaReceiver>, IOrtegaReceiver, IDisconnectReceiver
    {
        #region 聊天相关方法

        /// <summary>
        /// 初始化聊天信息列表
        /// </summary>
        public void InitChat()
        {
            _chatInfoList = new Dictionary<ChatType, List<ChatInfo>>
            {
                [ChatType.SvS] = new List<ChatInfo>(),
                [ChatType.World] = new List<ChatInfo>(),
                [ChatType.Guild] = new List<ChatInfo>(),
                [ChatType.Block] = new List<ChatInfo>()
            };
            _chatOptionDatas = new List<ChatOptionData>();
        }

        /// <summary>
        /// 获取指定类型的最新聊天信息
        /// </summary>
        /// <param name="chatType">聊天类型</param>
        /// <param name="chatViewType">聊天视图类型</param>
        /// <returns>最新的有效聊天信息，如果没有则返回 null</returns>
        public ChatInfo GetLatestChatInfo(ChatType chatType, ChatViewType chatViewType)
        {
            if (_chatInfoList == null) return null;
            
            if (_chatInfoList.TryGetValue(chatType, out var list))
            {
                // 从后往前遍历，找到第一个不被过滤的聊天信息
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    var chatInfo = list[i];
                    if (!IsRemoveBlockPlayerAndSharedInfoChat(chatInfo, chatViewType))
                    {
                        return chatInfo;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 检查聊天信息是否应该被过滤（被屏蔽的玩家或共享信息）
        /// </summary>
        /// <param name="chatInfo">聊天信息</param>
        /// <param name="chatViewType">聊天视图类型</param>
        /// <returns>true 表示应该被过滤，false 表示保留</returns>
        private bool IsRemoveBlockPlayerAndSharedInfoChat(ChatInfo chatInfo, ChatViewType chatViewType)
        {
            // 检查玩家是否被屏蔽
            // if (_blockedPlayerIds != null && _blockedPlayerIds.Contains(chatInfo.PlayerId))
            // {
            //     return true;
            // }
            
            // 如果 chatViewType 不是 Chat（即 GuildChat 或 PlayVideoComment），检查是否是共享信息
            if (chatViewType != ChatViewType.Chat && chatInfo.IsSharedInfo())
            {
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// 获取指定类型的聊天信息列表
        /// </summary>
        public List<ChatInfo> GetChatInfos(ChatType chatType)
        {
            if (chatType > ChatType.Guild)
            {
                return new List<ChatInfo>();
            }

            if (_chatInfoList.TryGetValue(chatType, out var list))
            {
                return GetRemovedBlockPlayerChat(list);
            }
            return new List<ChatInfo>();
        }

        /// <summary>
        /// 获取所有世界频道的玩家ID
        /// </summary>
        public List<long> GetAllWorldPlayerIds()
        {
            var result = new List<long>();
            if (_chatInfoList.TryGetValue(ChatType.World, out var list))
            {
                var filteredList = GetRemovedBlockPlayerChat(list);
                foreach (var chatInfo in filteredList)
                {
                    if (chatInfo.SystemChatType == SystemChatType.None)
                    {
                        if (!result.Contains(chatInfo.PlayerId))
                        {
                            result.Add(chatInfo.PlayerId);
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取聊天选项数据
        /// </summary>
        public ChatOptionData GetChatOptionData(ChatType chatType, ChatIdentityInfo chatIdentityInfo)
        {
            if (chatType == ChatType.Guild)
            {
                foreach (var chatOptionData in _chatOptionDatas)
                {
                    if (chatOptionData.ChatIdentityInfo.Equals(chatIdentityInfo))
                    {
                        return chatOptionData;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 添加聊天选项数据
        /// </summary>
        public void AddChatOptionData(ChatOptionData chatOptionData)
        {
            _chatOptionDatas.Add(chatOptionData);
        }

        /// <summary>
        /// 清除公会聊天记录
        /// </summary>
        public void ClearGuildChat()
        {
            if (_chatInfoList.TryGetValue(ChatType.Guild, out var list))
            {
                list.Clear();
            }
        }

        /// <summary>
        /// 清除SvS聊天记录
        /// </summary>
        public void ClearSvsChat()
        {
            if (_chatInfoList.TryGetValue(ChatType.SvS, out var list))
            {
                list.Clear();
            }
        }

        /// <summary>
        /// 清除世界聊天记录
        /// </summary>
        public void ClearWorldChat()
        {
            if (_chatInfoList.TryGetValue(ChatType.World, out var list))
            {
                list.Clear();
            }
        }

        /// <summary>
        /// 清除屏蔽聊天记录
        /// </summary>
        public void ClearBlockChat()
        {
            if (_chatInfoList.TryGetValue(ChatType.Block, out var list))
            {
                list.Clear();
            }
        }

        /// <summary>
        /// 发送聊天消息
        /// </summary>
        public void SendChatMessageAsync(ChatType chatType, string message)
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.SendMessageAsync(new SendMessageRequest
                {
                    ChatType = chatType,
                    Message = message
                });
            }
        }

        /// <summary>
        /// 发送加入公会聊天
        /// </summary>
        public void SendChatJoinGuildAsync()
        {
            TryReconnect();
            // 加入公会聊天频道，无需发送特定请求
        }

        /// <summary>
        /// 过滤被屏蔽玩家的聊天
        /// </summary>
        private List<ChatInfo> GetRemovedBlockPlayerChat(List<ChatInfo> chatList)
        {
            // TODO: 实现屏蔽玩家过滤逻辑
            return chatList ?? new List<ChatInfo>();
        }

        #endregion

        #region 构造函数和初始化

        public OrtegaMagicOnionClient(GrpcChannel channel, long playerId, string authToken, IMagicOnionLocalRaidNotificaiton localRaidNotificaiton)
            : base(channel, playerId, authToken)
        {
            _localRaidNotificaiton = localRaidNotificaiton;
            _currentLocalRaidPartyInfo = new LocalRaidPartyInfo();
            _chatInfoList = new Dictionary<ChatType, List<ChatInfo>>
            {
                [ChatType.SvS] = new List<ChatInfo>(),
                [ChatType.World] = new List<ChatInfo>(),
                [ChatType.Guild] = new List<ChatInfo>(),
                [ChatType.Block] = new List<ChatInfo>()
            };
            _chatOptionDatas = new List<ChatOptionData>();
            AttachInternalReceiver(this, this);
        }

        #endregion

        #region Setup 方法

        public void SetupLocalRaid(IMagicOnionLocalRaidReceiver localRaidReceiver, IMagicOnionErrorReceiver errorReceiver)
        {
            _localRaidReceiver = localRaidReceiver;
            _errorReceiver = errorReceiver;
        }

        public void SetupChat(IMagicOnionChatReceiver receiver, IMagicOnionAuthenticateReceiver authenticateReceiver, IMagicOnionErrorReceiver errorReceiver)
        {
            _chatReceiver = receiver;
            _authenticateReceiver = authenticateReceiver;
            _errorReceiver = errorReceiver;
        }

        public void SetupGvg(IMagicOnionGvgReceiver receiver, IMagicOnionErrorReceiver errorReceiver, BattleType battleType)
        {
            if (battleType == BattleType.GuildBattle)
            {
                _gvgLocalReceiver = receiver;
            }
            else
            {
                _gvgGlobalReceiver = receiver;
            }

            _errorReceiver = errorReceiver;
        }

        public void ClearGvgReceiver()
        {
            _gvgLocalReceiver = null;
            _gvgGlobalReceiver = null;
        }

        public void ClearLocalRaidReceiver()
        {
            _localRaidReceiver = null;
        }

        public void ClearGuildTowerReceiver(IMagicOnionGuildTowerReceiver receiver)
        {
            if (_guildTowerReceiver == receiver)
            {
                _guildTowerReceiver = null;
            }
        }

        public void ClearAchieveRewardReceiver()
        {
            _achieveRewardReceiver = null;
        }

        public void ClearErrorReceiver()
        {
            _errorReceiver = null;
        }

        public void ClearLocalRaidPartyInfo()
        {
            _currentLocalRaidPartyInfo = null;
        }

        public void SetupGuildTower(IMagicOnionGuildTowerReceiver receiver)
        {
            _guildTowerReceiver = receiver;
        }

        public void SetupAchieveReward(IMagicOnionAchieveRewardReceiver receiver)
        {
            _achieveRewardReceiver = receiver;
        }

        #endregion

        #region 基础方法

        public long GetPlayerId()
        {
            return _playerId;
        }

        public void SendKeepAliveAsync()
        {
            TryReconnect();
            if (_state == HubClientState.Ready)
            {
                _sender.KeepAliveAsync();
            }
        }

        protected override async Task Authenticate()
        {
            if (_sender != null)
            {
                await base.Authenticate();
                var authenticateRequest = new AuthenticateRequest
                {
                    PlayerId = _playerId,
                    AuthToken = _authToken,
                    DeviceType = DeviceType.Android
                };
                await _sender.AuthenticateAsync(authenticateRequest);
            }
        }

        #endregion

        #region IOrtegaReceiver 实现

        void IDisconnectReceiver.OnDisconnect()
        {
            _currentLocalRaidPartyInfo = null;
        }

        void IOrtegaReceiver.OnAuthenticateSuccess()
        {
            SucceededAuthentication();
            _authenticateReceiver?.OnAuthenticateSuccess();
            _gvgLocalReceiver?.OnAuthenticateSuccess();
            _gvgGlobalReceiver?.OnAuthenticateSuccess();
        }

        void IOrtegaReceiver.OnError(ErrorCode errorCode)
        {
            // 处理 LocalRaid 相关错误
            if (errorCode > ErrorCode.MagicOnionLocalRaidLeaveRoomNotExistRoom)
            {
                if (errorCode == ErrorCode.MagicOnionLocalRaidExpiredLocalRaidQuest ||
                    errorCode == ErrorCode.MagicOnionLocalRaidRefuseNotExistRoom)
                {
                    _currentLocalRaidPartyInfo = null;
                }
            }

            if (errorCode == ErrorCode.MagicOnionLocalRaidDisbandRoomFailed ||
                errorCode == ErrorCode.MagicOnionLocalRaidStartBattleExpiredLocalRaidQuest)
            {
                _currentLocalRaidPartyInfo = null;
            }

            // 认证失败
            if (errorCode == ErrorCode.MagicOnionAuthenticationFail)
            {
                FailedAuthentication();
            }

            _errorReceiver?.OnError(errorCode);
            _localRaidNotificaiton?.OnError(errorCode);
        }

        #endregion

        #region GvG 公会战方法

        public void SendGvgAddCastleParty(BattleType battleType, long castleId, List<long> characterIds, int memberCount)
        {
            TryReconnect();
            var addCastlePartyRequest = new AddCastlePartyRequest
            {
                CastleId = castleId,
                CharacterIds = characterIds,
                MemberCount = memberCount
            };
            if (battleType == BattleType.GuildBattle)
            {
                _sender.LocalGvgAddCastleParty(addCastlePartyRequest);
            }
            else
            {
                _sender.GlobalGvgAddCastleParty(addCastlePartyRequest);
            }
        }

        public void SendGvgOrderCastleParty(BattleType battleType, int index, long firstCharacterId, long ownerPlayerId, bool isUp)
        {
            TryReconnect();
            var orderCastlePartyRequest = new OrderCastlePartyRequest
            {
                Index = index,
                FirstCharacterId = firstCharacterId,
                OwnerPlayerId = ownerPlayerId,
                IsUp = isUp
            };
            if (battleType == BattleType.GuildBattle)
            {
                _sender.LocalGvgOrderCastleParty(orderCastlePartyRequest);
            }
            else
            {
                _sender.GlobalGvgOrderCastleParty(orderCastlePartyRequest);
            }
        }

        public void SendGvgOpenBattleDialog(BattleType battleType, long castleId)
        {
            TryReconnect();
            var openBattleDialogRequest = new OpenBattleDialogRequest { CastleId = castleId };
            if (battleType == BattleType.GuildBattle)
            {
                _sender.LocalGvgOpenBattleDialog(openBattleDialogRequest);
            }
            else
            {
                _sender.GlobalGvgOpenBattleDialog(openBattleDialogRequest);
            }
        }

        public void SendGvgOpenPartyDeployDialog(BattleType battleType, long castleId)
        {
            TryReconnect();
            var openPartyDeployDialogRequest = new OpenPartyDeployDialogRequest { CastleId = castleId };
            if (battleType == BattleType.GuildBattle)
            {
                _sender.LocalGvgOpenPartyDeployDialog(openPartyDeployDialogRequest);
            }
            else
            {
                _sender.GlobalGvgOpenPartyDeployDialog(openPartyDeployDialogRequest);
            }
        }

        public void SendGvgCloseCastleDialog(BattleType battleType, GvgDialogType gvgDialogType)
        {
            TryReconnect();
            var closeDialogRequest = new CloseDialogRequest { DialogType = gvgDialogType };
            if (battleType == BattleType.GuildBattle)
            {
                _sender.LocalGvgCloseCastleDialog(closeDialogRequest);
            }
            else
            {
                _sender.GlobalGvgCloseCastleDialog(closeDialogRequest);
            }
        }

        public void SendGvgCastleDeclaration(BattleType battleType, long castleId)
        {
            TryReconnect();
            var castleDeclarationRequest = new CastleDeclarationRequest { CastleId = castleId };
            if (battleType == BattleType.GuildBattle)
            {
                _sender.LocalGvgCastleDeclaration(castleDeclarationRequest);
            }
            else
            {
                _sender.GlobalGvgCastleDeclaration(castleDeclarationRequest);
            }
        }

        public void SendGvgCastleDeclarationCounter(BattleType battleType, long castleId)
        {
            TryReconnect();
            var castleDeclarationCounterRequest = new CastleDeclarationCounterRequest { CastleId = castleId };
            if (battleType == BattleType.GuildBattle)
            {
                _sender.LocalGvgCastleDeclarationCounter(castleDeclarationCounterRequest);
            }
            else
            {
                _sender.GlobalGvgCastleDeclarationCounter(castleDeclarationCounterRequest);
            }
        }

        public void SendGvgOpenMap(BattleType battleType, int matchingNumber = 0)
        {
            TryReconnect();
            if (battleType == BattleType.GuildBattle)
            {
                _sender.LocalGvgOpenMap();
            }
            else
            {
                _sender.GlobalGvgOpenMap(new OpenMapRequest { MatchingNumber = matchingNumber });
            }
        }

        public void SendGvgCloseMap(BattleType battleType)
        {
            TryReconnect();
            if (battleType == BattleType.GuildBattle)
            {
                _sender.LocalGvgCloseMap();
            }
            else
            {
                _sender.GlobalGvgCloseMap();
            }
        }

        /// <summary>
        /// 设置城池备注
        /// </summary>
        public void SetGvgMemo(BattleType battleType, long castleId, GvgCastleMemoMarkType memoMarkType, string memoMessage)
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                var setCastleMemoRequest = new SetCastleMemoRequest
                {
                    CastleId = castleId,
                    GvgCastleMemoMarkType = memoMarkType,
                    Message = memoMessage
                };
                if (battleType == BattleType.GrandBattle)
                {
                    _sender.GlobalGvgSetCastleMemo(setCastleMemoRequest);
                }
                else
                {
                    _sender.LocalGvgSetCastleMemo(setCastleMemoRequest);
                }
            }
        }

        /// <summary>
        /// 重置城池备注
        /// </summary>
        public void ResetGvgMemo(BattleType battleType, long castleId)
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                var resetCastleMemoRequest = new ResetCastleMemoRequest { CastleId = castleId };
                if (battleType == BattleType.GrandBattle)
                {
                    _sender.GlobalGvgResetCastleMemo(resetCastleMemoRequest);
                }
                else
                {
                    _sender.LocalGvgResetCastleMemo(resetCastleMemoRequest);
                }
            }
        }

        #endregion

        #region GvG 接收器实现

        void IOrtegaReceiver.OnGlobalGvgEndCastleBattle(OnEndCastleBattleResponse response)
        {
            _gvgGlobalReceiver?.OnEndCastleBattle(response);
        }

        void IOrtegaReceiver.OnGlobalGvgOpenBattleDialog(OnOpenBattleDialogResponse response)
        {
            _gvgGlobalReceiver?.OnOpenBattleDialog(response);
        }

        void IOrtegaReceiver.OnGlobalGvgUpdateCastleParty(OnUpdateCastlePartyResponse response)
        {
            _gvgGlobalReceiver?.OnUpdateCastleParty(response);
        }

        void IOrtegaReceiver.OnGlobalGvgUpdateMap(OnUpdateMapResponse response)
        {
            _gvgGlobalReceiver?.OnUpdateMap(response);
        }

        void IOrtegaReceiver.OnGlobalGvgUpdateDeployCharacter(OnUpdateDeployCharacterResponse response)
        {
            _gvgGlobalReceiver?.OnUpdateDeployCharacter(response);
        }

        void IOrtegaReceiver.OnGlobalGvgAddOnlyReceiverParty(OnAddOnlyReceiverPartyResponse response)
        {
            _gvgGlobalReceiver?.OnAddOnlyReceiverParty(response);
        }

        void IOrtegaReceiver.OnLocalGvgEndCastleBattle(OnEndCastleBattleResponse response)
        {
            _gvgLocalReceiver?.OnEndCastleBattle(response);
        }

        void IOrtegaReceiver.OnLocalGvgOpenBattleDialog(OnOpenBattleDialogResponse response)
        {
            _gvgLocalReceiver?.OnOpenBattleDialog(response);
        }

        void IOrtegaReceiver.OnLocalGvgUpdateCastleParty(OnUpdateCastlePartyResponse response)
        {
            _gvgLocalReceiver?.OnUpdateCastleParty(response);
        }

        void IOrtegaReceiver.OnLocalGvgUpdateMap(OnUpdateMapResponse response)
        {
            _gvgLocalReceiver?.OnUpdateMap(response);
        }

        void IOrtegaReceiver.OnLocalGvgUpdateDeployCharacter(OnUpdateDeployCharacterResponse response)
        {
            _gvgLocalReceiver?.OnUpdateDeployCharacter(response);
        }

        void IOrtegaReceiver.OnLocalGvgAddOnlyReceiverParty(OnAddOnlyReceiverPartyResponse response)
        {
            _gvgLocalReceiver?.OnAddOnlyReceiverParty(response);
        }

        void IOrtegaReceiver.OnLocalGvgUpdateCastleMemo(OnUpdateCastleMemoResponse response)
        {
            _gvgLocalReceiver?.OnUpdateCastleMemo(response);
        }

        void IOrtegaReceiver.OnGlobalGvgUpdateCastleMemo(OnUpdateCastleMemoResponse response)
        {
            _gvgGlobalReceiver?.OnUpdateCastleMemo(response);
        }

        #endregion

        #region 其他接收器实现

        void IOrtegaReceiver.OnNoticeGuildTowerInfo(GuildTowerInfoResponse response)
        {
            _guildTowerReceiver?.OnNoticeGuildTowerInfo(response);
        }

        void IOrtegaReceiver.OnReceiveAchieveReward(OnReceiveAchieveRewardResponse response)
        {
            _achieveRewardReceiver?.OnReceiveAchieveReward(response);
        }

        void IOrtegaReceiver.OnInviteRefuse(OnInviteRefuseResponse response)
        {
            _localRaidReceiver?.OnInviteRefuse(response);
        }

        void IOrtegaReceiver.OnReceiveBlockChatLog(OnReceiveBlockChatLogResponse response)
        {
            _chatReceiver?.OnReceiveBlockChatLog(response);
        }

        void IOrtegaReceiver.OnReactChat(OnReactChatResponse response)
        {
            if (response.ReactChatInfoList.IsNullOrEmpty()) return;

            // 按是否是自己发送的排序（自己的在前面）
            var orderedList = response.ReactChatInfoList
                .OrderByDescending(x => x.ReactPlayerId == _playerId)
                .ToList();

            foreach (var reactChatInfo in orderedList)
            {
                var chatOptionData = GetChatOptionData(ChatType.Guild, reactChatInfo.ChatIdentityInfo);
                if (chatOptionData == null)
                {
                    chatOptionData = new ChatOptionData(reactChatInfo.ChatIdentityInfo);
                    AddChatOptionData(chatOptionData);
                }

                if (chatOptionData.ChatReactionCountMap == null)
                {
                    chatOptionData.ChatReactionCountMap = new Dictionary<ChatReactionType, int>();
                }

                // 更新反应计数
                if (chatOptionData.ChatReactionCountMap.ContainsKey(reactChatInfo.ChatReactionType))
                {
                    chatOptionData.ChatReactionCountMap[reactChatInfo.ChatReactionType]++;
                }
                else
                {
                    chatOptionData.ChatReactionCountMap[reactChatInfo.ChatReactionType] = 1;
                }

                // 更新自己的反应类型
                if (reactChatInfo.ReactPlayerId == _playerId)
                {
                    chatOptionData.MyChatReactionType = reactChatInfo.ChatReactionType;
                }
            }

            _chatReceiver?.OnReactChat(response);
        }

        void IOrtegaReceiver.OnChangeChatOption(OnChangeChatOptionResponse response)
        {
            if (response.ChangeChatOptionInfoList.IsNullOrEmpty()) return;

            foreach (var changeChatOptionInfo in response.ChangeChatOptionInfoList)
            {
                var chatOptionData = GetChatOptionData(ChatType.Guild, changeChatOptionInfo.ChatIdentityInfo);
                if (chatOptionData == null)
                {
                    chatOptionData = new ChatOptionData(changeChatOptionInfo.ChatIdentityInfo);
                    AddChatOptionData(chatOptionData);
                }
                chatOptionData.IsAnnounced = changeChatOptionInfo.IsAnnounced;
            }

            _chatReceiver?.OnChangeChatOption(response);
        }

        void IOrtegaReceiver.OnNoticePrivateMessage(OnNoticePrivateMessageResponse response)
        {
            // TODO: 检查是否被屏蔽
            // var isBlocked = UserDataManager.Instance.IsBlockedPlayer(response.PlayerId);
            // if (isBlocked) return;

            _chatReceiver?.OnNoticePrivateMessage(response);
        }

        void IOrtegaReceiver.OnRemovedFromGuild()
        {
            _chatReceiver?.OnRemovedFromGuild();
        }

        void IOrtegaReceiver.OnReceiveGuildChatLog(OnReceiveGuildChatLogResponse response)
        {
            if (_chatInfoList.TryGetValue(ChatType.Guild, out var list))
            {
                list.Clear();
                if (response.ChatInfoList != null)
                {
                    list.AddRange(response.ChatInfoList);
                }
            }
            _chatReceiver?.OnReceiveGuildChatLog(response);
        }

        void IOrtegaReceiver.OnReceiveSvSChatLog(OnReceiveSvSChatLogResponse response)
        {
            if (_chatInfoList.TryGetValue(ChatType.SvS, out var list))
            {
                list.Clear();
                if (response.ChatInfoList != null)
                {
                    list.AddRange(response.ChatInfoList);
                }
            }
            _chatReceiver?.OnReceiveSvSChatLog(response);
        }

        void IOrtegaReceiver.OnReceiveWorldChatLog(OnReceiveWorldChatLogResponse response)
        {
            if (_chatInfoList.TryGetValue(ChatType.World, out var list))
            {
                list.Clear();
                if (response.ChatInfoList != null)
                {
                    list.AddRange(response.ChatInfoList);
                }
            }
            _chatReceiver?.OnReceiveWorldChatLog(response);
        }

        void IOrtegaReceiver.OnReceiveMessage(OnReceiveMessageResponse response)
        {
            var chatInfo = response.ChatInfo;
            if (chatInfo.ChatType <= ChatType.Guild)
            {
                if (_chatInfoList.TryGetValue(chatInfo.ChatType, out var list))
                {
                    list.Add(chatInfo);
                }
            }
            _chatReceiver?.OnReceiveMessage(response);
        }

        #endregion

        #region LocalRaid 本地突袭方法

        public LocalRaidPartyInfo GetCurrentLocalRaidPartyInfo()
        {
            return _currentLocalRaidPartyInfo;
        }

        public bool IsLocalRaidMasterInRoom()
        {
            return _currentLocalRaidPartyInfo != null && _currentLocalRaidPartyInfo.LeaderPlayerId == _playerId;
        }

        public bool IsLocalRaidInRoom()
        {
            return _currentLocalRaidPartyInfo != null;
        }

        public bool IsLocalRaidInvitedInRoom()
        {
            if (_currentLocalRaidPartyInfo == null) return false;
            foreach (var info in _currentLocalRaidPartyInfo.LocalRaidBattleLogPlayerInfoList)
            {
                if (info.IsInvite && info.PlayerInfo.PlayerId == _playerId)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsLocalRaidReadyInRoom()
        {
            if (_currentLocalRaidPartyInfo == null) return false;
            if (_currentLocalRaidPartyInfo.LeaderPlayerId == _playerId) return true;
            foreach (var info in _currentLocalRaidPartyInfo.LocalRaidBattleLogPlayerInfoList)
            {
                if (info.IsReady && info.PlayerInfo.PlayerId == _playerId)
                {
                    return true;
                }
            }
            return false;
        }

        public void SendLocalRaidGetRoomList(long questId)
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.GetRoomListAsync(new GetRoomListRequest
                {
                    QuestId = questId
                });
            }
        }

        public void SendLocalRaidDisbandRoom()
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.DisbandRoomAsync();
            }
        }

        public void SendLocalRaidInvite(long playerId)
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.InviteAsync(new InviteRequest
                {
                    FriendPlayerId = playerId
                });
            }
        }

        public void SendLocalRaidJoinRandomRoom(long questId)
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.JoinRandomRoomAsync(new JoinRandomRoomRequest
                {
                    QuestId = questId
                });
            }
        }

        public void SendLocalRaidJoinRoom(string roomId, int password)
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.JoinRoomAsync(new JoinRoomRequest
                {
                    RoomId = roomId,
                    Password = password
                });
            }
        }

        public void SendLocalRaidJoinFriendRoom(string roomId)
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.JoinFriendRoomAsync(new JoinFriendRoomRequest
                {
                    RoomId = roomId
                });
            }
        }

        public void SendLocalRaidLeaveRoom()
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.LeaveRoomAsync();
            }
        }

        public void SendLocalRaidOpenRoom(LocalRaidRoomConditionsType conditionType, long questId, long requiredBattlePower, int password, bool isAutoStart = true)
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.OpenRoomAsync(new OpenRoomRequest
                {
                    ConditionsType = conditionType,
                    QuestId = questId,
                    RequiredBattlePower = requiredBattlePower,
                    Password = password,
                    IsAutoStart = isAutoStart
                });
            }
        }

        public void SendLocalRaidStartBattle()
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.StartBattleAsync();
            }
        }

        public void SendLocalRaidUpdatePartyCount()
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.UpdatePartyCountAsync();
            }
        }

        public void SendLocalRaidRefuse(long targetPlayerId)
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.RefuseAsync(new RefuseRequest
                {
                    TargetPlayerId = targetPlayerId
                });
            }
        }

        public void SendLocalRaidReady(bool isReady)
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.ReadyAsync(new ReadyRequest
                {
                    IsReady = isReady
                });
            }
        }

        public void SendLocalRaidUpdateRoomCondition(LocalRaidRoomConditionsType conditionType, long requiredBattlePower, int password, bool isAutoStart = true)
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.UpdateRoomConditionAsync(new UpdateRoomConditionRequest
                {
                    ConditionsType = conditionType,
                    RequiredBattlePower = requiredBattlePower,
                    Password = password,
                    IsAutoStart = isAutoStart
                });
            }
        }

        public void SendLocalRaidUpdateBattlePower()
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.UpdateBattlePowerAsync(new UpdateBattlePowerRequest());
            }
        }

        public void SendRoomClear()
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.RoomClearAsync();
            }
        }

        #endregion

        #region LocalRaid 接收器实现

        void IOrtegaReceiver.OnDisbandRoom()
        {
            _localRaidReceiver?.OnDisbandRoom();
            _currentLocalRaidPartyInfo = null;
        }

        void IOrtegaReceiver.OnGetRoomList(OnGetRoomListResponse response)
        {
            _localRaidReceiver?.OnGetRoomList(response);
        }

        void IOrtegaReceiver.OnInvite(OnInviteResponse response)
        {
            // 如果已经在房间中，自动拒绝其他邀请
            if (_currentLocalRaidPartyInfo != null)
            {
                if (_currentLocalRaidPartyInfo.RoomId != response.RoomId && _sender != null)
                {
                    _sender.InviteRefuseAsync(new InviteRefuseRequest
                    {
                        InvitedFriendPlayerId = response.PlayerId
                    });
                }
                return;
            }

            _localRaidReceiver?.OnInvite(response);
        }

        void IOrtegaReceiver.OnJoinRoom(OnJoinRoomResponse response)
        {
            _currentLocalRaidPartyInfo = response.LocalRaidPartyInfo;
            _localRaidReceiver?.OnJoinRoom(response);

            // 如果不是房主且是被邀请的，通知
            if (_currentLocalRaidPartyInfo != null && _currentLocalRaidPartyInfo.LeaderPlayerId != _playerId)
            {
                if (IsLocalRaidInvitedInRoom())
                {
                    _localRaidNotificaiton?.OnJoinRoom();
                }
            }
        }

        void IOrtegaReceiver.OnLeaveRoom()
        {
            _localRaidReceiver?.OnLeaveRoom();
            _currentLocalRaidPartyInfo = null;
        }

        void IOrtegaReceiver.OnLockRoom()
        {
            _localRaidReceiver?.OnLockRoom();
        }

        void IOrtegaReceiver.OnRefuse()
        {
            _localRaidReceiver?.OnRefuse();

            // 如果是房主，通知被拒绝
            if (_currentLocalRaidPartyInfo != null && _currentLocalRaidPartyInfo.LeaderPlayerId == _playerId)
            {
                _localRaidNotificaiton?.OnRefused();
            }
            _currentLocalRaidPartyInfo = null;
        }

        void IOrtegaReceiver.OnStartBattle()
        {
            _localRaidReceiver?.OnStartBattle();

            // 如果不是房主且不是自动开始，通知
            if (_currentLocalRaidPartyInfo != null)
            {
                if (_currentLocalRaidPartyInfo.LeaderPlayerId != _playerId && !_currentLocalRaidPartyInfo.IsAutoStart)
                {
                    _localRaidNotificaiton?.OnStartBattle();
                }
            }
            _currentLocalRaidPartyInfo = null;
        }

        void IOrtegaReceiver.OnUpdatePartyCount(OnUpdatePartyCountResponse response)
        {
            _localRaidReceiver?.OnUpdatePartyCount(response);
        }

        void IOrtegaReceiver.OnUpdateRoom(OnUpdateRoomResponse response)
        {
            _currentLocalRaidPartyInfo = response.LocalRaidPartyInfo;
            _localRaidReceiver?.OnUpdateRoom(response);

            // 如果是房主，检查是否所有人都准备好了且是自动开始
            if (_currentLocalRaidPartyInfo != null && _currentLocalRaidPartyInfo.LeaderPlayerId == _playerId)
            {
                if (_currentLocalRaidPartyInfo.IsAutoStart && IsLocalRaidReadyInRoom())
                {
                    _localRaidNotificaiton?.OnStartBattle();
                }
            }
        }

        #endregion

        #region 私有字段

        private IMagicOnionChatReceiver _chatReceiver;
        private IMagicOnionGvgReceiver _gvgLocalReceiver;
        private IMagicOnionGvgReceiver _gvgGlobalReceiver;
        private IMagicOnionAuthenticateReceiver _authenticateReceiver;
        private IMagicOnionErrorReceiver _errorReceiver;
        private IMagicOnionLocalRaidNotificaiton _localRaidNotificaiton;
        private IMagicOnionLocalRaidReceiver _localRaidReceiver;
        private IMagicOnionGuildTowerReceiver _guildTowerReceiver;
        private IMagicOnionAchieveRewardReceiver _achieveRewardReceiver;
        private Dictionary<ChatType, List<ChatInfo>> _chatInfoList;
        private List<ChatOptionData> _chatOptionDatas;
        private LocalRaidPartyInfo _currentLocalRaidPartyInfo;

        #endregion
    }
}
