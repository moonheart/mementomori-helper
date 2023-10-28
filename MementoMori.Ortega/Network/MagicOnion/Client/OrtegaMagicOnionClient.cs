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
            this._localRaidNotificaiton = localRaidNotificaiton;
			this._currentLocalRaidPartyInfo = new LocalRaidPartyInfo();
            this._chatInfoList = new Dictionary<ChatType, List<ChatInfo>>()
            {
                [ChatType.SvS] = new List<ChatInfo>(),
                [ChatType.World] = new List<ChatInfo>(),
                [ChatType.Guild] = new List<ChatInfo>()
            };
			base.AttachInternalReceiver(this, this);
		}

		public void SetupLocalRaid(IMagicOnionLocalRaidReceiver localRaidReceiver, IMagicOnionErrorReceiver errorReceiver)
        {
            _localRaidReceiver = localRaidReceiver;
            _errorReceiver = errorReceiver;
		}

		public void SetupChat(IMagicOnionChatReceiver receiver, IMagicOnionAuthenticateReceiver authenticateReceiver, IMagicOnionErrorReceiver errorReceiver)
		{
            this._chatReceiver = receiver;
            this._authenticateReceiver = authenticateReceiver;
            this._errorReceiver = errorReceiver;
		}

		public void SetupGvg(IMagicOnionGvgReceiver receiver, IMagicOnionErrorReceiver errorReceiver, BattleType battleType)
		{
            if (battleType == BattleType.GuildBattle)
            {
                _gvgGlobalReceiver = receiver;
                
            }
            else
            {
                
            }
            this._gvgLocalReceiver = receiver;
            this._errorReceiver = errorReceiver;
			/*
An exception occurred when decompiling this method

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Network.MagicOnion.Client.OrtegaMagicOnionClient::SetupGvg(Ortega.Network.MagicOnion.Interface.IMagicOnionGvgReceiver,Ortega.Network.MagicOnion.Interface.IMagicOnionErrorReceiver,Ortega.Share.Enums.BattleType)

 ---> System.Exception: Basic block has to end with unconditional control flow. 
{
	IL_1F:
	stfld:IMagicOnionGvgReceiver(OrtegaMagicOnionClient::_gvgLocalReceiver, ldloc:OrtegaMagicOnionClient(this), ldloc:IMagicOnionGvgReceiver(receiver))
	stfld:IMagicOnionErrorReceiver(OrtegaMagicOnionClient::_errorReceiver, ldloc:OrtegaMagicOnionClient(this), ldloc:IMagicOnionErrorReceiver(errorReceiver))
}

   at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
   at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
*/;
		}

		public void ClearGvgReceiver()
		{
			/*
An exception occurred when decompiling this method

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Network.MagicOnion.Client.OrtegaMagicOnionClient::ClearGvgReceiver()

 ---> System.Exception: Basic block has to end with unconditional control flow. 
{
	Block_0:
	stfld:IMagicOnionGvgReceiver(OrtegaMagicOnionClient::_gvgLocalReceiver, ldloc:OrtegaMagicOnionClient(this), conv.u8:uint64[exp:IMagicOnionGvgReceiver](ldc.i8:int64[exp:uint64](0)))
	stfld:IMagicOnionGvgReceiver(OrtegaMagicOnionClient::_gvgGlobalReceiver, ldloc:OrtegaMagicOnionClient(this), conv.u8:uint64[exp:IMagicOnionGvgReceiver](ldc.i8:int64[exp:uint64](0)))
}

   at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
   at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
*/;
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
			return this._playerId;
		}

		public void SendKeepAliveAsync()
		{
			base.TryReconnect();
            if (_state == HubClientState.Ready)
            {
                // _sender.
            }
		}

		protected override void Authenticate()
		{
            if (_sender != null)
            {
                AuthenticateRequest authenticateRequest = new AuthenticateRequest()
                {
                    PlayerId = _playerId,
                    AuthToken = _authToken,
                    DeviceType = DeviceType.Android
                };
                _sender.AuthenticateAsync(authenticateRequest);
            }
			base.Authenticate();
		}

		void IDisconnectReceiver.OnDisconnect()
        {
            _currentLocalRaidPartyInfo = null;
		}

		void IOrtegaReceiver.OnAuthenticateSuccess()
		{
			this.SucceededAuthentication();
			if (this._authenticateReceiver != null)
            {
            }
			if (this._gvgLocalReceiver != null)
			{
			}
			if (this._gvgGlobalReceiver != null)
			{
			}
		}

		void IOrtegaReceiver.OnError(ErrorCode errorCode)
		{
            string text = string.Format("OnError : ErrorCode -> {0}", errorCode);
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
            if (_errorReceiver != null)
            {
            }
            if (_localRaidNotificaiton == null)
            {
            }
		}

		public void SendGvgAddCastleParty(BattleType battleType, long castleId, List<long> characterIds, int memberCount)
		{
			// base.TryReconnect();
			// AddCastlePartyRequest addCastlePartyRequest = new AddCastlePartyRequest();
			// addCastlePartyRequest.<CastleId>k__BackingField = castleId;
			// addCastlePartyRequest.<CharacterIds>k__BackingField = characterIds;
			// addCastlePartyRequest.<MemberCount>k__BackingField = 0;
			// if (battleType != BattleType.GrandBattle)
			// {
			// }
			// string text = string.Format("SendGvgAddCastleParty -> memberCount : {0}", addCastlePartyRequest);
		}

		public void SendGvgOrderCastleParty(BattleType battleType, int index, long firstCharacterId, long ownerPlayerId, bool isUp)
		{
			// object[] array;
			// for (;;)
			// {
			// 	base.TryReconnect();
			// 	OrderCastlePartyRequest orderCastlePartyRequest = new OrderCastlePartyRequest();
			// 	orderCastlePartyRequest.<IsUp>k__BackingField = false;
			// 	orderCastlePartyRequest.<OwnerPlayerId>k__BackingField = 0L;
			// 	orderCastlePartyRequest.<Index>k__BackingField = index;
			// 	orderCastlePartyRequest.<FirstCharacterId>k__BackingField = firstCharacterId;
			// 	if ((battleType != BattleType.GrandBattle && index == 0) || index != 0)
			// 	{
			// 	}
			// 	array = new object[4];
			// 	if (array == 0 || array != 0)
			// 	{
			// 		array[0] = array;
			// 		if (array == 0 || array != 0)
			// 		{
			// 			array[1] = array;
			// 			if (array == 0 || array != 0)
			// 			{
			// 				array[2] = array;
			// 				if (array == 0 || array != 0)
			// 				{
			// 					break;
			// 				}
			// 			}
			// 		}
			// 	}
			// }
			// array[3] = array;
			// string text = string.Format("SendGvgOrderCastleParty -> Index : {0}, FirstCharacterId : {1}, IsUp = {2}, OwnerPlayerId = {3}", array);
		}

		public void SendGvgOpenBattleDialog(BattleType battleType, long castleId)
		{
			// base.TryReconnect();
			// OpenBattleDialogRequest openBattleDialogRequest = new OpenBattleDialogRequest();
			// openBattleDialogRequest.<CastleId>k__BackingField = castleId;
			// if (battleType != BattleType.GrandBattle)
			// {
			// }
			// string text = string.Format("SendGvgOpenBattleDialog -> CastleId : {0}", openBattleDialogRequest);
		}

		public void SendGvgOpenPartyDeployDialog(BattleType battleType, long castleId)
		{
			// base.TryReconnect();
			// OpenPartyDeployDialogRequest openPartyDeployDialogRequest = new OpenPartyDeployDialogRequest();
			// openPartyDeployDialogRequest.<CastleId>k__BackingField = castleId;
			// if (battleType != BattleType.GrandBattle)
			// {
			// }
			// string text = string.Format("SendGvgOpenPartyDeployDialog -> CastleId : {0}", openPartyDeployDialogRequest);
		}

		public void SendGvgCloseCastleDialog(BattleType battleType, GvgDialogType gvgDialogType)
		{
			// base.TryReconnect();
			// if (battleType != BattleType.GrandBattle)
			// {
			// 	if (this != 0)
			// 	{
			// 		new CloseDialogRequest().<DialogType>k__BackingField = gvgDialogType;
			// 	}
			// 	return;
			// }
			// CloseDialogRequest closeDialogRequest = new CloseDialogRequest();
			// throw new NullReferenceException();
		}

		public void SendGvgCastleDeclaration(BattleType battleType, long castleId)
		{
			// base.TryReconnect();
			// CastleDeclarationRequest castleDeclarationRequest = new CastleDeclarationRequest();
			// castleDeclarationRequest.<CastleId>k__BackingField = castleId;
			// if (battleType != BattleType.GrandBattle)
			// {
			// }
			// string text = string.Format("SendGvgCastleDeclaration -> CastleId : {0}", castleDeclarationRequest);
		}

		public void SendGvgCastleDeclarationCounter(BattleType battleType, long castleId)
		{
			// base.TryReconnect();
			// CastleDeclarationCounterRequest castleDeclarationCounterRequest = new CastleDeclarationCounterRequest();
			// castleDeclarationCounterRequest.<CastleId>k__BackingField = castleId;
			// if (battleType != BattleType.GrandBattle)
			// {
			// }
			// string text = string.Format("SendGvgCastleDeclarationCounter -> CastleId : {0}", castleDeclarationCounterRequest);
		}

		public void SendGvgOpenMap(BattleType battleType, int matchingNumber)
		{
			// base.TryReconnect();
			// if (battleType != BattleType.GrandBattle)
			// {
			// 	if (this != 0)
			// 	{
			// 	}
			// 	string text = string.Format("SendGvgOpenMap -> matchingNumber : {0}", "SendGvgOpenMap -> matchingNumber : {0}");
			// 	return;
			// }
			// OpenMapRequest openMapRequest = new OpenMapRequest();
			// int num = 0;
			// openMapRequest.<MatchingNumber>k__BackingField = 0;
			// int num2 = 0;
			// if (num2 < num)
			// {
			// 	num2 += num2;
			// 	num2++;
			// }
			// num2 += 34;
			// num2 += 312;
			// throw new NullReferenceException();
		}

		public void SendGvgCloseMap(BattleType battleType)
		{
			// base.TryReconnect();
			// if (battleType != BattleType.GrandBattle)
			// {
			// }
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
			// if (this._gvgLocalReceiver != 0)
			// {
			// }
		}

		void IOrtegaReceiver.OnLocalGvgOpenBattleDialog(OnOpenBattleDialogResponse response)
		{
			// if (this._gvgLocalReceiver != 0)
			// {
			// }
		}

		void IOrtegaReceiver.OnLocalGvgUpdateCastleParty(OnUpdateCastlePartyResponse response)
		{
			// if (this._gvgLocalReceiver != 0)
			// {
			// }
		}

		void IOrtegaReceiver.OnLocalGvgUpdateMap(OnUpdateMapResponse response)
		{
			// if (this._gvgLocalReceiver != 0)
			// {
			// }
		}

		void IOrtegaReceiver.OnLocalGvgUpdateDeployCharacter(OnUpdateDeployCharacterResponse response)
		{
			// if (this._gvgLocalReceiver != 0)
			// {
			// }
		}

		void IOrtegaReceiver.OnLocalGvgAddOnlyReceiverParty(OnAddOnlyReceiverPartyResponse response)
		{
			// if (this._gvgLocalReceiver != 0)
			// {
			// }
		}

		public LocalRaidPartyInfo GetCurrentLocalRaidPartyInfo()
		{
			return this._currentLocalRaidPartyInfo;
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
			base.TryReconnect();
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
			base.TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.DisbandRoomAsync();
            }
		}

		public void SendLocalRaidInvite(long playerId)
		{
			base.TryReconnect();
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
			base.TryReconnect();
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
			base.TryReconnect();
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
			base.TryReconnect();
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
			base.TryReconnect();
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
                    Password = password
                });
            }
		}

		public void SendLocalRaidStartBattle()
		{
			base.TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.StartBattleAsync();
            }
		}

		public void SendLocalRaidUpdatePartyCount()
		{
			base.TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.UpdatePartyCountAsync();
            }
		}

		public void SendLocalRaidRefuse(long targetPlayerId)
		{
			base.TryReconnect();
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
			base.TryReconnect();
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
			base.TryReconnect();
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
			base.TryReconnect();
            if (_state == HubClientState.Ready && _sender != null)
            {
                _sender.UpdateBattlePowerAsync(new UpdateBattlePowerRequest());
            }
		}

		public void SendRoomClear()
		{
			base.TryReconnect();
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
            this._currentLocalRaidPartyInfo = response.LocalRaidPartyInfo;
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
