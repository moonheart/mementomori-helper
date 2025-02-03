using Grpc.Net.Client;
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
    public class OrtegaMagicOnionClient : MagicOnionClient<IOrtegaSender, IOrtegaReceiver>, IOrtegaReceiver, IDisconnectReceiver
    {
        public ChatInfo GetLatestChatInfo(ChatType chatType)
        {
            // if (chatType <= ChatType.Guild)
            // {
            // 	Dictionary<ChatType, List<ChatInfo>> chatInfoList = this._chatInfoList;
            // 	List<ChatInfo> list;
            // 	List<ChatInfo> removedBlockPlayerChat = ChatUtil.GetRemovedBlockPlayerChat(list);
            // 	if (!removedBlockPlayerChat.IsNullOrEmpty<ChatInfo>())
            // 	{
            // 		int size = removedBlockPlayerChat._size;
            // 		return removedBlockPlayerChat[size];
            // 	}
            // }
            // throw new NullReferenceException();
            throw new NotImplementedException();
        }

        public List<ChatInfo> GetChatInfos(ChatType chatType)
        {
            // if (chatType > ChatType.Guild)
            // {
            // }
            // Dictionary<ChatType, List<ChatInfo>> chatInfoList = this._chatInfoList;
            // List<ChatInfo> list;
            // return ChatUtil.GetRemovedBlockPlayerChat(list);
            throw new NotImplementedException();
        }

        public List<long> GetAllWorldPlayerIds()
        {
            // List<long> list = new List();
            // List<ChatInfo> removedBlockPlayerChat = ChatUtil.GetRemovedBlockPlayerChat(this._chatInfoList[(uint)1]);
            // int num = 0;
            // if (removedBlockPlayerChat[num].<SystemChatType>k__BackingField == SystemChatType.None)
            // {
            // 	long <PlayerId>k__BackingField = removedBlockPlayerChat[num].<PlayerId>k__BackingField;
            // 	if (!list.Contains(<PlayerId>k__BackingField))
            // 	{
            // 	}
            // }
            // num++;
            // return list;
            throw new NotImplementedException();
        }

        public void ClearGuildChat()
        {
            // throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
        }

        public void ClearSvsChat()
        {
            // throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
        }

        public void ClearWorldChat()
        {
            // throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
        }

        public void SendChatMessageAsync(ChatType chatType, string message)
        {
            // base.TryReconnect();
            // int num = 0;
            // string text = string.Format("SendChatMessageAsync : {0} : {1}", "SendChatMessageAsync : {0} : {1}", message);
            // SendMessageRequest sendMessageRequest = new SendMessageRequest();
            // sendMessageRequest.<ChatType>k__BackingField = chatType;
            // sendMessageRequest.<Message>k__BackingField = message;
            // int num2 = 0;
            // if (num2 < num)
            // {
            // 	num2 += num2;
            // 	num2++;
            // }
        }

        public void SendChatJoinGuildAsync()
        {
            // base.TryReconnect();
        }

        void IOrtegaReceiver.OnNoticePrivateMessage(OnNoticePrivateMessageResponse response)
        {
            // UserDataManager instance = SingletonMonoBehaviour.Instance;
            // long <PlayerId>k__BackingField = response.<PlayerId>k__BackingField;
            // bool flag = instance.IsBlockedPlayer(<PlayerId>k__BackingField);
            // long <PlayerId>k__BackingField2 = response.<PlayerId>k__BackingField;
            // if (!flag)
            // {
            // 	string text = string.Format("OnNoticePrivateMessage : PlayerId -> {0}", flag);
            // 	if (this._chatReceiver != 0)
            // 	{
            // 		int num = 0;
            // 		uint num2;
            // 		if (num < (int)num2)
            // 		{
            // 			num += num;
            // 			num++;
            // 		}
            // 	}
            // 	return;
            // }
            // string text2 = string.Format("OnNoticePrivateMessage Blocked : PlayerId -> {0}", flag);
        }

        void IOrtegaReceiver.OnRemovedFromGuild()
        {
            // if (this._chatReceiver != 0)
            // {
            // }
        }

        void IOrtegaReceiver.OnReceiveGuildChatLog(OnReceiveGuildChatLogResponse response)
        {
            // throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
        }

        void IOrtegaReceiver.OnReceiveSvSChatLog(OnReceiveSvSChatLogResponse response)
        {
            // throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
        }

        void IOrtegaReceiver.OnReceiveWorldChatLog(OnReceiveWorldChatLogResponse response)
        {
            // throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
        }

        void IOrtegaReceiver.OnReceiveMessage(OnReceiveMessageResponse response)
        {
            // ChatInfo <ChatInfo>k__BackingField = response.<ChatInfo>k__BackingField;
            // ChatType <ChatType>k__BackingField = <ChatInfo>k__BackingField.<ChatType>k__BackingField;
            // long <PlayerId>k__BackingField = <ChatInfo>k__BackingField.<PlayerId>k__BackingField;
            // string <Message>k__BackingField = <ChatInfo>k__BackingField.<Message>k__BackingField;
            // int num = 0;
            // string text = string.Format("OnReceiveMessage -> {0} : {1} : {2}", <ChatType>k__BackingField, <ChatType>k__BackingField, <Message>k__BackingField);
            // if (<ChatInfo>k__BackingField.<ChatType>k__BackingField <= ChatType.Guild)
            // {
            // 	Dictionary<ChatType, List<ChatInfo>> chatInfoList = this._chatInfoList;
            // 	ChatType <ChatType>k__BackingField2 = <ChatInfo>k__BackingField.<ChatType>k__BackingField;
            // 	chatInfoList[<ChatType>k__BackingField2].Add(<ChatInfo>k__BackingField);
            // }
            // uint num2;
            // if (this._chatReceiver != 0 && num < (int)num2)
            // {
            // 	num += num;
            // 	num++;
            // }
        }

        public OrtegaMagicOnionClient(GrpcChannel channel, long playerId, string authToken, IMagicOnionLocalRaidNotificaiton localRaidNotificaiton)
            : base(channel, playerId, authToken)
        {
            _localRaidNotificaiton = localRaidNotificaiton;
            _currentLocalRaidPartyInfo = new LocalRaidPartyInfo();
            _chatInfoList = new Dictionary<ChatType, List<ChatInfo>>()
            {
                [ChatType.SvS] = new List<ChatInfo>(),
                [ChatType.World] = new List<ChatInfo>(),
                [ChatType.Guild] = new List<ChatInfo>()
            };
            AttachInternalReceiver(this, this);
        }

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

        public void ClearErrorReceiver()
        {
            _errorReceiver = null;
        }

        public void ClearLocalRaidPartyInfo()
        {
            _currentLocalRaidPartyInfo = null;
        }

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
                AuthenticateRequest authenticateRequest = new AuthenticateRequest()
                {
                    PlayerId = _playerId,
                    AuthToken = _authToken,
                    DeviceType = DeviceType.Android
                };
                await _sender.AuthenticateAsync(authenticateRequest);
            }
        }

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
            if (errorCode > ErrorCode.MagicOnionLocalRaidLeaveRoomNotExistRoom)
            {
                if (errorCode == ErrorCode.MagicOnionLocalRaidExpiredLocalRaidQuest)
                {
                    _currentLocalRaidPartyInfo = null;
                }
            }

            if (errorCode == ErrorCode.MagicOnionLocalRaidDisbandRoomFailed)
            {
                _currentLocalRaidPartyInfo = null;
            }

            _errorReceiver?.OnError(errorCode);

            _localRaidNotificaiton?.OnError(errorCode);
        }

        public void SendGvgAddCastleParty(BattleType battleType, long castleId, List<long> characterIds, int memberCount)
        {
            TryReconnect();
            var addCastlePartyRequest = new AddCastlePartyRequest()
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
            var orderCastlePartyRequest = new OrderCastlePartyRequest()
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
            var openBattleDialogRequest = new OpenBattleDialogRequest() {CastleId = castleId};
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
            var openPartyDeployDialogRequest = new OpenPartyDeployDialogRequest() {CastleId = castleId};
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
            var closeDialogRequest = new CloseDialogRequest() {DialogType = gvgDialogType};
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
            var castleDeclarationRequest = new CastleDeclarationRequest() {CastleId = castleId};
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
            var castleDeclarationCounterRequest = new CastleDeclarationCounterRequest() {CastleId = castleId};
            if (battleType == BattleType.GuildBattle)
            {
                _sender.LocalGvgCastleDeclarationCounter(castleDeclarationCounterRequest);
            }
            else
            {
                _sender.GlobalGvgCastleDeclarationCounter(castleDeclarationCounterRequest);
            }
        }

        public void SendGvgOpenMap(BattleType battleType, int matchingNumber)
        {
            TryReconnect();
            if (battleType == BattleType.GuildBattle)
            {
                _sender.LocalGvgOpenMap();
            }
            else
            {
                _sender.GlobalGvgOpenMap(new OpenMapRequest() {MatchingNumber = matchingNumber});
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

        void IOrtegaReceiver.OnGlobalGvgEndCastleBattle(OnEndCastleBattleResponse response)
        {
            // if (this._gvgGlobalReceiver != 0)
            // {
            // }
        }

        void IOrtegaReceiver.OnGlobalGvgOpenBattleDialog(OnOpenBattleDialogResponse response)
        {
            // if (this._gvgGlobalReceiver != 0)
            // {
            // }
        }

        void IOrtegaReceiver.OnGlobalGvgUpdateCastleParty(OnUpdateCastlePartyResponse response)
        {
            // if (this._gvgGlobalReceiver != 0)
            // {
            // }
        }

        void IOrtegaReceiver.OnGlobalGvgUpdateMap(OnUpdateMapResponse response)
        {
            // if (this._gvgGlobalReceiver != 0)
            // {
            // }
        }

        void IOrtegaReceiver.OnGlobalGvgUpdateDeployCharacter(OnUpdateDeployCharacterResponse response)
        {
            // if (this._gvgGlobalReceiver != 0)
            // {
            // }
        }

        void IOrtegaReceiver.OnGlobalGvgAddOnlyReceiverParty(OnAddOnlyReceiverPartyResponse response)
        {
            // if (this._gvgGlobalReceiver != 0)
            // {
            // }
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
                return info.IsInvite && info.PlayerInfo.PlayerId == _playerId;
            }

            return false;
        }

        public bool IsLocalRaidReadyInRoom()
        {
            if (_currentLocalRaidPartyInfo == null) return false;
            if (_currentLocalRaidPartyInfo.LeaderPlayerId == _playerId) return true;
            foreach (var info in _currentLocalRaidPartyInfo.LocalRaidBattleLogPlayerInfoList)
            {
                return info.IsReady && info.PlayerInfo.PlayerId == _playerId;
            }

            return false;
        }

        public void SendLocalRaidGetRoomList(long questId)
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.GetRoomListAsync(new GetRoomListRequest()
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
                _sender.InviteAsync(new InviteRequest()
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
                _sender.JoinRandomRoomAsync(new JoinRandomRoomRequest()
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
                _sender.JoinRoomAsync(new JoinRoomRequest()
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
                _sender.JoinFriendRoomAsync(new JoinFriendRoomRequest()
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

        public void SendLocalRaidOpenRoom(LocalRaidRoomConditionsType conditionType, long questId, long requiredBattlePower, int password)
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.OpenRoomAsync(new OpenRoomRequest()
                {
                    ConditionsType = conditionType,
                    QuestId = questId,
                    RequiredBattlePower = requiredBattlePower,
                    Password = password,
                    IsAutoStart = true
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
                _sender.RefuseAsync(new RefuseRequest()
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
                _sender.ReadyAsync(new ReadyRequest()
                {
                    IsReady = isReady
                });
            }
        }

        public void SendLocalRaidUpdateRoomCondition(LocalRaidRoomConditionsType conditionType, long requiredBattlePower, int password)
        {
            TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.UpdateRoomConditionAsync(new UpdateRoomConditionRequest()
                {
                    ConditionsType = conditionType,
                    RequiredBattlePower = requiredBattlePower,
                    Password = password
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
            _localRaidReceiver?.OnInvite(response);
        }

        void IOrtegaReceiver.OnJoinRoom(OnJoinRoomResponse response)
        {
            _currentLocalRaidPartyInfo = response.LocalRaidPartyInfo;
            _localRaidReceiver?.OnJoinRoom(response);
            _localRaidNotificaiton?.OnJoinRoom();
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
            _localRaidNotificaiton?.OnRefused();
            _currentLocalRaidPartyInfo = null;
        }

        void IOrtegaReceiver.OnStartBattle()
        {
            _localRaidReceiver?.OnStartBattle();
            _localRaidNotificaiton?.OnStartBattle();
            _currentLocalRaidPartyInfo = null;
        }

        void IOrtegaReceiver.OnUpdatePartyCount(OnUpdatePartyCountResponse response)
        {
            _localRaidReceiver?.OnUpdatePartyCount(response);
        }

        void IOrtegaReceiver.OnUpdateRoom(OnUpdateRoomResponse response)
        {
            _localRaidReceiver?.OnUpdateRoom(response);
        }

        private IMagicOnionChatReceiver _chatReceiver;

        private IMagicOnionGvgReceiver _gvgLocalReceiver;

        private IMagicOnionGvgReceiver _gvgGlobalReceiver;

        private IMagicOnionAuthenticateReceiver _authenticateReceiver;

        private IMagicOnionErrorReceiver _errorReceiver;

        private IMagicOnionLocalRaidNotificaiton _localRaidNotificaiton;

        private IMagicOnionLocalRaidReceiver _localRaidReceiver;

        private Dictionary<ChatType, List<ChatInfo>> _chatInfoList;

        private LocalRaidPartyInfo _currentLocalRaidPartyInfo;
    }
}