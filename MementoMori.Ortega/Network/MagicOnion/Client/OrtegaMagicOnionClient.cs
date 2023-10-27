// using Grpc.Net.Client;
// using MementoMori.Ortega.Network.MagicOnion.Interface;
// using MementoMori.Ortega.Share;
// using MementoMori.Ortega.Share.Data.Chat;
// using MementoMori.Ortega.Share.Data.LocalRaid;
// using MementoMori.Ortega.Share.Enums;
// using MementoMori.Ortega.Share.Extensions;
// using MementoMori.Ortega.Share.MagicOnionShare.Interfaces.Receiver;
// using MementoMori.Ortega.Share.MagicOnionShare.Interfaces.Sender;
// using MementoMori.Ortega.Share.MagicOnionShare.Request;
// using MementoMori.Ortega.Share.MagicOnionShare.Response;
//
// namespace MementoMori.Ortega.Network.MagicOnion.Client
// {
// 	public class OrtegaMagicOnionClient : MagicOnionClient<IOrtegaSender, IOrtegaReceiver>, IOrtegaReceiver, IDisconnectReceiver
// 	{
// 		public ChatInfo GetLatestChatInfo(ChatType chatType)
// 		{
// 			// if (chatType <= ChatType.Guild)
// 			// {
// 			// 	Dictionary<ChatType, List<ChatInfo>> chatInfoList = this._chatInfoList;
// 			// 	List<ChatInfo> list;
// 			// 	List<ChatInfo> removedBlockPlayerChat = ChatUtil.GetRemovedBlockPlayerChat(list);
// 			// 	if (!removedBlockPlayerChat.IsNullOrEmpty<ChatInfo>())
// 			// 	{
// 			// 		int size = removedBlockPlayerChat._size;
// 			// 		return removedBlockPlayerChat[size];
// 			// 	}
// 			// }
// 			// throw new NullReferenceException();
//             throw new NotImplementedException();
//         }
//
// 		public List<ChatInfo> GetChatInfos(ChatType chatType)
// 		{
// 			// if (chatType > ChatType.Guild)
// 			// {
// 			// }
// 			// Dictionary<ChatType, List<ChatInfo>> chatInfoList = this._chatInfoList;
// 			// List<ChatInfo> list;
// 			// return ChatUtil.GetRemovedBlockPlayerChat(list);
//             throw new NotImplementedException();
//         } 
//
// 		public List<long> GetAllWorldPlayerIds()
// 		{
// 			// List<long> list = new List();
// 			// List<ChatInfo> removedBlockPlayerChat = ChatUtil.GetRemovedBlockPlayerChat(this._chatInfoList[(uint)1]);
// 			// int num = 0;
// 			// if (removedBlockPlayerChat[num].<SystemChatType>k__BackingField == SystemChatType.None)
// 			// {
// 			// 	long <PlayerId>k__BackingField = removedBlockPlayerChat[num].<PlayerId>k__BackingField;
// 			// 	if (!list.Contains(<PlayerId>k__BackingField))
// 			// 	{
// 			// 	}
// 			// }
// 			// num++;
// 			// return list;
//             throw new NotImplementedException();
// 		}
//
// 		public void ClearGuildChat()
// 		{
// 			// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
//             throw new NotImplementedException();
// 		}
//
// 		public void ClearSvsChat()
// 		{
// 			// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
//             throw new NotImplementedException();
// 		}
//
// 		public void ClearWorldChat()
// 		{
// 			// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
//             throw new NotImplementedException();
// 		}
//
// 		public void SendChatMessageAsync(ChatType chatType, string message)
// 		{
// 			// base.TryReconnect();
// 			// int num = 0;
// 			// string text = string.Format("SendChatMessageAsync : {0} : {1}", "SendChatMessageAsync : {0} : {1}", message);
// 			// SendMessageRequest sendMessageRequest = new SendMessageRequest();
// 			// sendMessageRequest.<ChatType>k__BackingField = chatType;
// 			// sendMessageRequest.<Message>k__BackingField = message;
// 			// int num2 = 0;
// 			// if (num2 < num)
// 			// {
// 			// 	num2 += num2;
// 			// 	num2++;
// 			// }
//             throw new NotImplementedException();
// 		}
//
// 		public void SendChatJoinGuildAsync()
// 		{
// 			// base.TryReconnect();
//             throw new NotImplementedException();
// 		}
//
// 		void IOrtegaReceiver.OnNoticePrivateMessage(OnNoticePrivateMessageResponse response)
// 		{
// 			// UserDataManager instance = SingletonMonoBehaviour.Instance;
// 			// long <PlayerId>k__BackingField = response.<PlayerId>k__BackingField;
// 			// bool flag = instance.IsBlockedPlayer(<PlayerId>k__BackingField);
// 			// long <PlayerId>k__BackingField2 = response.<PlayerId>k__BackingField;
// 			// if (!flag)
// 			// {
// 			// 	string text = string.Format("OnNoticePrivateMessage : PlayerId -> {0}", flag);
// 			// 	if (this._chatReceiver != 0)
// 			// 	{
// 			// 		int num = 0;
// 			// 		uint num2;
// 			// 		if (num < (int)num2)
// 			// 		{
// 			// 			num += num;
// 			// 			num++;
// 			// 		}
// 			// 	}
// 			// 	return;
// 			// }
// 			// string text2 = string.Format("OnNoticePrivateMessage Blocked : PlayerId -> {0}", flag);
//             throw new NotImplementedException();
// 		}
//
// 		void IOrtegaReceiver.OnRemovedFromGuild()
// 		{
// 			// if (this._chatReceiver != 0)
// 			// {
// 			// }
//             throw new NotImplementedException();
// 		}
//
// 		void IOrtegaReceiver.OnReceiveGuildChatLog(OnReceiveGuildChatLogResponse response)
// 		{
// 			// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
//             throw new NotImplementedException();
// 		}
//
// 		void IOrtegaReceiver.OnReceiveSvSChatLog(OnReceiveSvSChatLogResponse response)
// 		{
// 			// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
//             throw new NotImplementedException();
// 		}
//
// 		void IOrtegaReceiver.OnReceiveWorldChatLog(OnReceiveWorldChatLogResponse response)
// 		{
// 			// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
//             throw new NotImplementedException();
// 		}
//
// 		void IOrtegaReceiver.OnReceiveMessage(OnReceiveMessageResponse response)
// 		{
// 			// ChatInfo <ChatInfo>k__BackingField = response.<ChatInfo>k__BackingField;
// 			// ChatType <ChatType>k__BackingField = <ChatInfo>k__BackingField.<ChatType>k__BackingField;
// 			// long <PlayerId>k__BackingField = <ChatInfo>k__BackingField.<PlayerId>k__BackingField;
// 			// string <Message>k__BackingField = <ChatInfo>k__BackingField.<Message>k__BackingField;
// 			// int num = 0;
// 			// string text = string.Format("OnReceiveMessage -> {0} : {1} : {2}", <ChatType>k__BackingField, <ChatType>k__BackingField, <Message>k__BackingField);
// 			// if (<ChatInfo>k__BackingField.<ChatType>k__BackingField <= ChatType.Guild)
// 			// {
// 			// 	Dictionary<ChatType, List<ChatInfo>> chatInfoList = this._chatInfoList;
// 			// 	ChatType <ChatType>k__BackingField2 = <ChatInfo>k__BackingField.<ChatType>k__BackingField;
// 			// 	chatInfoList[<ChatType>k__BackingField2].Add(<ChatInfo>k__BackingField);
// 			// }
// 			// uint num2;
// 			// if (this._chatReceiver != 0 && num < (int)num2)
// 			// {
// 			// 	num += num;
// 			// 	num++;
// 			// }
//             throw new NotImplementedException();
// 		}
//
// 		public OrtegaMagicOnionClient(GrpcChannel channel, long playerId, string authToken, IMagicOnionLocalRaidNotificaiton localRaidNotificaiton)
// 			: base(channel, playerId, authToken)
//         {
//             this._localRaidNotificaiton = localRaidNotificaiton;
// 			this._currentLocalRaidPartyInfo = new LocalRaidPartyInfo();
//             this._chatInfoList = new Dictionary<ChatType, List<ChatInfo>>()
//             {
//                 [ChatType.SvS] = new List<ChatInfo>(),
//                 [ChatType.World] = new List<ChatInfo>(),
//                 [ChatType.Guild] = new List<ChatInfo>()
//             };
// 			base.AttachInternalReceiver(this, this);
// 		}
//
// 		public void SetupLocalRaid(IMagicOnionLocalRaidReceiver localRaidReceiver, IMagicOnionErrorReceiver errorReceiver)
//         {
//             _localRaidReceiver = localRaidReceiver;
//             _errorReceiver = errorReceiver;
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Network.MagicOnion.Client.OrtegaMagicOnionClient::SetupLocalRaid(Ortega.Network.MagicOnion.Interface.IMagicOnionLocalRaidReceiver,Ortega.Network.MagicOnion.Interface.IMagicOnionErrorReceiver)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stfld:IMagicOnionLocalRaidReceiver(OrtegaMagicOnionClient::_localRaidReceiver, ldloc:OrtegaMagicOnionClient(this), ldloc:IMagicOnionLocalRaidReceiver(localRaidReceiver))
// 	stfld:IMagicOnionErrorReceiver(OrtegaMagicOnionClient::_errorReceiver, ldloc:OrtegaMagicOnionClient(this), ldloc:IMagicOnionErrorReceiver(errorReceiver))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public void SetupChat(IMagicOnionChatReceiver receiver, IMagicOnionAuthenticateReceiver authenticateReceiver, IMagicOnionErrorReceiver errorReceiver)
// 		{
//             this._chatReceiver = receiver;
//             this._authenticateReceiver = authenticateReceiver;
//             this._errorReceiver = errorReceiver;
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Network.MagicOnion.Client.OrtegaMagicOnionClient::SetupChat(Ortega.Network.MagicOnion.Interface.IMagicOnionChatReceiver,Ortega.Network.MagicOnion.Interface.IMagicOnionAuthenticateReceiver,Ortega.Network.MagicOnion.Interface.IMagicOnionErrorReceiver)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stfld:IMagicOnionChatReceiver(OrtegaMagicOnionClient::_chatReceiver, ldloc:OrtegaMagicOnionClient(this), ldloc:IMagicOnionChatReceiver(receiver))
// 	stfld:IMagicOnionAuthenticateReceiver(OrtegaMagicOnionClient::_authenticateReceiver, ldloc:OrtegaMagicOnionClient(this), ldloc:IMagicOnionAuthenticateReceiver(authenticateReceiver))
// 	stfld:IMagicOnionErrorReceiver(OrtegaMagicOnionClient::_errorReceiver, ldloc:OrtegaMagicOnionClient(this), ldloc:IMagicOnionErrorReceiver(errorReceiver))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public void SetupGvg(IMagicOnionGvgReceiver receiver, IMagicOnionErrorReceiver errorReceiver, BattleType battleType)
// 		{
//             this._gvgLocalReceiver = receiver;
//             this._errorReceiver = errorReceiver;
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Network.MagicOnion.Client.OrtegaMagicOnionClient::SetupGvg(Ortega.Network.MagicOnion.Interface.IMagicOnionGvgReceiver,Ortega.Network.MagicOnion.Interface.IMagicOnionErrorReceiver,Ortega.Share.Enums.BattleType)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	IL_1F:
// 	stfld:IMagicOnionGvgReceiver(OrtegaMagicOnionClient::_gvgLocalReceiver, ldloc:OrtegaMagicOnionClient(this), ldloc:IMagicOnionGvgReceiver(receiver))
// 	stfld:IMagicOnionErrorReceiver(OrtegaMagicOnionClient::_errorReceiver, ldloc:OrtegaMagicOnionClient(this), ldloc:IMagicOnionErrorReceiver(errorReceiver))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public void ClearGvgReceiver()
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Network.MagicOnion.Client.OrtegaMagicOnionClient::ClearGvgReceiver()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stfld:IMagicOnionGvgReceiver(OrtegaMagicOnionClient::_gvgLocalReceiver, ldloc:OrtegaMagicOnionClient(this), conv.u8:uint64[exp:IMagicOnionGvgReceiver](ldc.i8:int64[exp:uint64](0)))
// 	stfld:IMagicOnionGvgReceiver(OrtegaMagicOnionClient::_gvgGlobalReceiver, ldloc:OrtegaMagicOnionClient(this), conv.u8:uint64[exp:IMagicOnionGvgReceiver](ldc.i8:int64[exp:uint64](0)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public void ClearLocalRaidReceiver()
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Network.MagicOnion.Client.OrtegaMagicOnionClient::ClearLocalRaidReceiver()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stfld:IMagicOnionLocalRaidReceiver(OrtegaMagicOnionClient::_localRaidReceiver, ldloc:OrtegaMagicOnionClient(this), conv.u8:uint64[exp:IMagicOnionLocalRaidReceiver](ldc.i8:int64[exp:uint64](0)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public void ClearErrorReceiver()
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Network.MagicOnion.Client.OrtegaMagicOnionClient::ClearErrorReceiver()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stfld:IMagicOnionErrorReceiver(OrtegaMagicOnionClient::_errorReceiver, ldloc:OrtegaMagicOnionClient(this), conv.u8:uint64[exp:IMagicOnionErrorReceiver](ldc.i8:int64[exp:uint64](0)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public void ClearLocalRaidPartyInfo()
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Network.MagicOnion.Client.OrtegaMagicOnionClient::ClearLocalRaidPartyInfo()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stfld:LocalRaidPartyInfo(OrtegaMagicOnionClient::_currentLocalRaidPartyInfo, ldloc:OrtegaMagicOnionClient(this), conv.u8:uint64[exp:LocalRaidPartyInfo](ldc.i8:int64[exp:uint64](0)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public long GetPlayerId()
// 		{
// 			return this._playerId;
// 		}
//
// 		public void SendKeepAliveAsync()
// 		{
// 			base.TryReconnect();
// 		}
//
// 		protected override void Authenticate()
// 		{
// 			AuthenticateRequest authenticateRequest = new AuthenticateRequest()
//             {
//                 PlayerId = _playerId,
//                 AuthToken = _authToken,
//                 DeviceType = DeviceType.Android
//             };
//             
// 			base.Authenticate();
// 		}
//
// 		void IDisconnectReceiver.OnDisconnect()
//         {
//             _currentLocalRaidPartyInfo = null;
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Network.MagicOnion.Client.OrtegaMagicOnionClient::Ortega.Network.MagicOnion.Interface.IDisconnectReceiver.OnDisconnect()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stfld:LocalRaidPartyInfo(OrtegaMagicOnionClient::_currentLocalRaidPartyInfo, ldloc:OrtegaMagicOnionClient(this), conv.u8:uint64[exp:LocalRaidPartyInfo](ldc.i8:int64[exp:uint64](0)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		void IOrtegaReceiver.OnAuthenticateSuccess()
// 		{
// 			this.SucceededAuthentication();
// 			if (this._authenticateReceiver != 0)
// 			{
// 			}
// 			if (this._gvgLocalReceiver != 0)
// 			{
// 			}
// 			if (this._gvgGlobalReceiver != 0)
// 			{
// 			}
// 		}
//
// 		void IOrtegaReceiver.OnError(ErrorCode errorCode)
// 		{
// 			string text = string.Format("OnError : ErrorCode -> {0}", "OnError : ErrorCode -> {0}");
// 			if (errorCode > ErrorCode.MagicOnionLocalRaidLeaveRoomNotExistRoom)
// 			{
// 				if (text == (ulong)4294967291L)
// 				{
// 					goto IL_3D;
// 				}
// 				if (errorCode != ErrorCode.MagicOnionLocalRaidExpiredLocalRaidQuest)
// 				{
// 					goto IL_46;
// 				}
// 			}
// 			if (errorCode != ErrorCode.MagicOnionLocalRaidDisbandRoomFailed && text > (ulong)1L)
// 			{
// 				goto IL_46;
// 			}
// 			IL_3D:
// 			this._currentLocalRaidPartyInfo = (ulong)0L;
// 			IL_46:
// 			if (this._errorReceiver != 0)
// 			{
// 			}
// 			if (this._localRaidNotificaiton != 0)
// 			{
// 			}
// 		}
//
// 		public void SendGvgAddCastleParty(BattleType battleType, long castleId, List<long> characterIds, int memberCount)
// 		{
// 			// base.TryReconnect();
// 			// AddCastlePartyRequest addCastlePartyRequest = new AddCastlePartyRequest();
// 			// addCastlePartyRequest.<CastleId>k__BackingField = castleId;
// 			// addCastlePartyRequest.<CharacterIds>k__BackingField = characterIds;
// 			// addCastlePartyRequest.<MemberCount>k__BackingField = 0;
// 			// if (battleType != BattleType.GrandBattle)
// 			// {
// 			// }
// 			// string text = string.Format("SendGvgAddCastleParty -> memberCount : {0}", addCastlePartyRequest);
//             throw new NotImplementedException();
// 		}
//
// 		public void SendGvgOrderCastleParty(BattleType battleType, int index, long firstCharacterId, long ownerPlayerId, bool isUp)
// 		{
// 			// object[] array;
// 			// for (;;)
// 			// {
// 			// 	base.TryReconnect();
// 			// 	OrderCastlePartyRequest orderCastlePartyRequest = new OrderCastlePartyRequest();
// 			// 	orderCastlePartyRequest.<IsUp>k__BackingField = false;
// 			// 	orderCastlePartyRequest.<OwnerPlayerId>k__BackingField = 0L;
// 			// 	orderCastlePartyRequest.<Index>k__BackingField = index;
// 			// 	orderCastlePartyRequest.<FirstCharacterId>k__BackingField = firstCharacterId;
// 			// 	if ((battleType != BattleType.GrandBattle && index == 0) || index != 0)
// 			// 	{
// 			// 	}
// 			// 	array = new object[4];
// 			// 	if (array == 0 || array != 0)
// 			// 	{
// 			// 		array[0] = array;
// 			// 		if (array == 0 || array != 0)
// 			// 		{
// 			// 			array[1] = array;
// 			// 			if (array == 0 || array != 0)
// 			// 			{
// 			// 				array[2] = array;
// 			// 				if (array == 0 || array != 0)
// 			// 				{
// 			// 					break;
// 			// 				}
// 			// 			}
// 			// 		}
// 			// 	}
// 			// }
// 			// array[3] = array;
// 			// string text = string.Format("SendGvgOrderCastleParty -> Index : {0}, FirstCharacterId : {1}, IsUp = {2}, OwnerPlayerId = {3}", array);
//             throw new NotImplementedException();
// 		}
//
// 		public void SendGvgOpenBattleDialog(BattleType battleType, long castleId)
// 		{
// 			// base.TryReconnect();
// 			// OpenBattleDialogRequest openBattleDialogRequest = new OpenBattleDialogRequest();
// 			// openBattleDialogRequest.<CastleId>k__BackingField = castleId;
// 			// if (battleType != BattleType.GrandBattle)
// 			// {
// 			// }
// 			// string text = string.Format("SendGvgOpenBattleDialog -> CastleId : {0}", openBattleDialogRequest);
//             throw new NotImplementedException();
// 		}
//
// 		public void SendGvgOpenPartyDeployDialog(BattleType battleType, long castleId)
// 		{
// 			// base.TryReconnect();
// 			// OpenPartyDeployDialogRequest openPartyDeployDialogRequest = new OpenPartyDeployDialogRequest();
// 			// openPartyDeployDialogRequest.<CastleId>k__BackingField = castleId;
// 			// if (battleType != BattleType.GrandBattle)
// 			// {
// 			// }
// 			// string text = string.Format("SendGvgOpenPartyDeployDialog -> CastleId : {0}", openPartyDeployDialogRequest);
//             throw new NotImplementedException();
// 		}
//
// 		public void SendGvgCloseCastleDialog(BattleType battleType, GvgDialogType gvgDialogType)
// 		{
// 			// base.TryReconnect();
// 			// if (battleType != BattleType.GrandBattle)
// 			// {
// 			// 	if (this != 0)
// 			// 	{
// 			// 		new CloseDialogRequest().<DialogType>k__BackingField = gvgDialogType;
// 			// 	}
// 			// 	return;
// 			// }
// 			// CloseDialogRequest closeDialogRequest = new CloseDialogRequest();
// 			// throw new NullReferenceException();
//             throw new NotImplementedException();
// 		}
//
// 		public void SendGvgCastleDeclaration(BattleType battleType, long castleId)
// 		{
// 			// base.TryReconnect();
// 			// CastleDeclarationRequest castleDeclarationRequest = new CastleDeclarationRequest();
// 			// castleDeclarationRequest.<CastleId>k__BackingField = castleId;
// 			// if (battleType != BattleType.GrandBattle)
// 			// {
// 			// }
// 			// string text = string.Format("SendGvgCastleDeclaration -> CastleId : {0}", castleDeclarationRequest);
//             throw new NotImplementedException();
// 		}
//
// 		public void SendGvgCastleDeclarationCounter(BattleType battleType, long castleId)
// 		{
// 			// base.TryReconnect();
// 			// CastleDeclarationCounterRequest castleDeclarationCounterRequest = new CastleDeclarationCounterRequest();
// 			// castleDeclarationCounterRequest.<CastleId>k__BackingField = castleId;
// 			// if (battleType != BattleType.GrandBattle)
// 			// {
// 			// }
// 			// string text = string.Format("SendGvgCastleDeclarationCounter -> CastleId : {0}", castleDeclarationCounterRequest);
//             throw new NotImplementedException();
// 		}
//
// 		public void SendGvgOpenMap(BattleType battleType, int matchingNumber)
// 		{
// 			// base.TryReconnect();
// 			// if (battleType != BattleType.GrandBattle)
// 			// {
// 			// 	if (this != 0)
// 			// 	{
// 			// 	}
// 			// 	string text = string.Format("SendGvgOpenMap -> matchingNumber : {0}", "SendGvgOpenMap -> matchingNumber : {0}");
// 			// 	return;
// 			// }
// 			// OpenMapRequest openMapRequest = new OpenMapRequest();
// 			// int num = 0;
// 			// openMapRequest.<MatchingNumber>k__BackingField = 0;
// 			// int num2 = 0;
// 			// if (num2 < num)
// 			// {
// 			// 	num2 += num2;
// 			// 	num2++;
// 			// }
// 			// num2 += 34;
// 			// num2 += 312;
// 			// throw new NullReferenceException();
//             throw new NotImplementedException();
// 		}
//
// 		public void SendGvgCloseMap(BattleType battleType)
// 		{
// 			// base.TryReconnect();
// 			// if (battleType != BattleType.GrandBattle)
// 			// {
// 			// }
//             throw new NotImplementedException();
// 		}
//
// 		void IOrtegaReceiver.OnGlobalGvgEndCastleBattle(OnEndCastleBattleResponse response)
// 		{
// 			// if (this._gvgGlobalReceiver != 0)
// 			// {
// 			// }
//             throw new NotImplementedException();
// 		}
//
// 		void IOrtegaReceiver.OnGlobalGvgOpenBattleDialog(OnOpenBattleDialogResponse response)
// 		{
// 			// if (this._gvgGlobalReceiver != 0)
// 			// {
// 			// }
//             throw new NotImplementedException();
// 		}
//
// 		void IOrtegaReceiver.OnGlobalGvgUpdateCastleParty(OnUpdateCastlePartyResponse response)
// 		{
// 			// if (this._gvgGlobalReceiver != 0)
// 			// {
// 			// }
//             throw new NotImplementedException();
// 		}
//
// 		void IOrtegaReceiver.OnGlobalGvgUpdateMap(OnUpdateMapResponse response)
// 		{
// 			// if (this._gvgGlobalReceiver != 0)
// 			// {
// 			// }
//             throw new NotImplementedException();
// 		}
//
// 		void IOrtegaReceiver.OnGlobalGvgUpdateDeployCharacter(OnUpdateDeployCharacterResponse response)
// 		{
// 			// if (this._gvgGlobalReceiver != 0)
// 			// {
// 			// }
//             throw new NotImplementedException();
// 		}
//
// 		void IOrtegaReceiver.OnGlobalGvgAddOnlyReceiverParty(OnAddOnlyReceiverPartyResponse response)
// 		{
// 			// if (this._gvgGlobalReceiver != 0)
// 			// {
// 			// }
//             throw new NotImplementedException();
// 		}
//
// 		void IOrtegaReceiver.OnLocalGvgEndCastleBattle(OnEndCastleBattleResponse response)
// 		{
// 			// if (this._gvgLocalReceiver != 0)
// 			// {
// 			// }
//             throw new NotImplementedException();
// 		}
//
// 		void IOrtegaReceiver.OnLocalGvgOpenBattleDialog(OnOpenBattleDialogResponse response)
// 		{
// 			// if (this._gvgLocalReceiver != 0)
// 			// {
// 			// }
//             throw new NotImplementedException();
// 		}
//
// 		void IOrtegaReceiver.OnLocalGvgUpdateCastleParty(OnUpdateCastlePartyResponse response)
// 		{
// 			// if (this._gvgLocalReceiver != 0)
// 			// {
// 			// }
//             throw new NotImplementedException();
// 		}
//
// 		void IOrtegaReceiver.OnLocalGvgUpdateMap(OnUpdateMapResponse response)
// 		{
// 			// if (this._gvgLocalReceiver != 0)
// 			// {
// 			// }
//             throw new NotImplementedException();
// 		}
//
// 		void IOrtegaReceiver.OnLocalGvgUpdateDeployCharacter(OnUpdateDeployCharacterResponse response)
// 		{
// 			// if (this._gvgLocalReceiver != 0)
// 			// {
// 			// }
//             throw new NotImplementedException();
// 		}
//
// 		void IOrtegaReceiver.OnLocalGvgAddOnlyReceiverParty(OnAddOnlyReceiverPartyResponse response)
// 		{
// 			// if (this._gvgLocalReceiver != 0)
// 			// {
// 			// }
//             throw new NotImplementedException();
// 		}
//
// 		public LocalRaidPartyInfo GetCurrentLocalRaidPartyInfo()
// 		{
// 			return this._currentLocalRaidPartyInfo;
// 		}
//
// 		public bool IsLocalRaidMasterInRoom()
// 		{
// 			if (this._currentLocalRaidPartyInfo != null)
// 			{
// 				long playerId = this._playerId;
// 				bool flag;
// 				return flag;
// 			}
// 		}
//
// 		public bool IsLocalRaidInRoom()
// 		{
// 			throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
// 		}
//
// 		public bool IsLocalRaidInvitedInRoom()
// 		{
// 			while (this._currentLocalRaidPartyInfo != (ulong)0L)
// 			{
// 				List<LocalRaidBattleLogPlayerInfo> <LocalRaidBattleLogPlayerInfoList>k__BackingField = this._currentLocalRaidPartyInfo.<LocalRaidBattleLogPlayerInfoList>k__BackingField;
// 				bool flag;
// 				if (flag)
// 				{
// 					long playerId = SingletonMonoBehaviour.Instance.PlayerId;
// 				}
// 				ulong num;
// 				if (num == (ulong)0L)
// 				{
// 					break;
// 				}
// 			}
// 			throw new NullReferenceException();
// 		}
//
// 		public bool IsLocalRaidReadyInRoom()
// 		{
// 			while (this._currentLocalRaidPartyInfo != (ulong)0L)
// 			{
// 				LocalRaidPartyInfo currentLocalRaidPartyInfo = this._currentLocalRaidPartyInfo;
// 				long playerId = this._playerId;
// 				if (currentLocalRaidPartyInfo.<LeaderPlayerId>k__BackingField == playerId)
// 				{
// 					return true;
// 				}
// 				if (this._currentLocalRaidPartyInfo == (ulong)0L)
// 				{
// 					break;
// 				}
// 				List<LocalRaidBattleLogPlayerInfo> <LocalRaidBattleLogPlayerInfoList>k__BackingField = this._currentLocalRaidPartyInfo.<LocalRaidBattleLogPlayerInfoList>k__BackingField;
// 				bool flag;
// 				if (flag)
// 				{
// 					long playerId2 = SingletonMonoBehaviour.Instance.PlayerId;
// 				}
// 				ulong num;
// 				if (num == (ulong)0L)
// 				{
// 					break;
// 				}
// 			}
// 			throw new NullReferenceException();
// 		}
//
// 		public void SendLocalRaidGetRoomList(long questId)
// 		{
// 			base.TryReconnect();
// 			int num = 0;
// 			string text = string.Format("SendLocalRaidGetRoomList : questId = {0}", "SendLocalRaidGetRoomList : questId = {0}");
// 			new GetRoomListRequest().<QuestId>k__BackingField = questId;
// 			int num2 = 0;
// 			if (num2 < num)
// 			{
// 				num2 += num2;
// 				num2++;
// 			}
// 		}
//
// 		public void SendLocalRaidDisbandRoom()
// 		{
// 			base.TryReconnect();
// 		}
//
// 		public void SendLocalRaidInvite(long playerId)
// 		{
// 			base.TryReconnect();
// 			if (this != 0)
// 			{
// 				new InviteRequest().<FriendPlayerId>k__BackingField = playerId;
// 				int num = 0;
// 				uint num2;
// 				if (num < (int)num2)
// 				{
// 					num += num;
// 					num++;
// 				}
// 			}
// 		}
//
// 		public void SendLocalRaidJoinRandomRoom(long questId)
// 		{
// 			base.TryReconnect();
// 			int num = 0;
// 			string text = string.Format("SendLocalRaidJoinRandomRoom : questId = {0}", "SendLocalRaidJoinRandomRoom : questId = {0}");
// 			new JoinRandomRoomRequest().<QuestId>k__BackingField = questId;
// 			int num2 = 0;
// 			if (num2 < num)
// 			{
// 				num2 += num2;
// 				num2++;
// 			}
// 		}
//
// 		public void SendLocalRaidJoinRoom(string roomId, int password)
// 		{
// 			base.TryReconnect();
// 			int num = 0;
// 			string text = string.Format("SendLocalRaidJoinRandomRoom : roomId = {0} password = {1}", roomId, "SendLocalRaidJoinRandomRoom : roomId = {0} password = {1}");
// 			JoinRoomRequest joinRoomRequest = new JoinRoomRequest();
// 			joinRoomRequest.<RoomId>k__BackingField = roomId;
// 			joinRoomRequest.<Password>k__BackingField = password;
// 			int num2 = 0;
// 			if (num2 < num)
// 			{
// 				num2 += num2;
// 				num2++;
// 			}
// 		}
//
// 		public void SendLocalRaidJoinFriendRoom(string roomId)
// 		{
// 			base.TryReconnect();
// 			string text = "SendLocalRaidJoinFriendRoom : roomId = " + roomId;
// 			if (this != 0)
// 			{
// 				new JoinFriendRoomRequest().<RoomId>k__BackingField = roomId;
// 				int num = 0;
// 				uint num2;
// 				if (num < (int)num2)
// 				{
// 					num += num;
// 					num++;
// 				}
// 			}
// 		}
//
// 		public void SendLocalRaidLeaveRoom()
// 		{
// 			base.TryReconnect();
// 		}
//
// 		public void SendLocalRaidOpenRoom(LocalRaidRoomConditionsType conditionType, long questId, long requiredBattlePower, int password)
// 		{
// 			object[] array;
// 			for (;;)
// 			{
// 				base.TryReconnect();
// 				array = new object[4];
// 				if (array == 0 || array != 0)
// 				{
// 					array[0] = array;
// 					if (array == 0 || array != 0)
// 					{
// 						array[1] = array;
// 						if (array == 0 || array != 0)
// 						{
// 							array[2] = array;
// 							if (array == 0 || array != 0)
// 							{
// 								break;
// 							}
// 						}
// 					}
// 				}
// 			}
// 			array[3] = array;
// 			string text = string.Format("SendLocalRaidOpenRoom : conditionType = {0} questId : {1} requiredBattlePower = {2} password = {3}", array);
// 			if (array != 0)
// 			{
// 				OpenRoomRequest openRoomRequest = new OpenRoomRequest();
// 				openRoomRequest.<ConditionsType>k__BackingField = conditionType;
// 				int num = 0;
// 				openRoomRequest.<QuestId>k__BackingField = questId;
// 				openRoomRequest.<Password>k__BackingField = 0;
// 				openRoomRequest.<RequiredBattlePower>k__BackingField = requiredBattlePower;
// 				uint num2;
// 				if (num < (int)num2)
// 				{
// 					num += num;
// 					num++;
// 				}
// 			}
// 		}
//
// 		public void SendLocalRaidStartBattle()
// 		{
// 			base.TryReconnect();
// 		}
//
// 		public void SendLocalRaidUpdatePartyCount()
// 		{
// 			base.TryReconnect();
// 		}
//
// 		public void SendLocalRaidRefuse(long targetPlayerId)
// 		{
// 			base.TryReconnect();
// 			int num = 0;
// 			string text = string.Format("SendLocalRaidRefuse : targetPlayerId = {0}", "SendLocalRaidRefuse : targetPlayerId = {0}");
// 			new RefuseRequest().<TargetPlayerId>k__BackingField = targetPlayerId;
// 			int num2 = 0;
// 			if (num2 < num)
// 			{
// 				num2 += num2;
// 				num2++;
// 			}
// 		}
//
// 		public void SendLocalRaidReady(bool isReady)
// 		{
// 			base.TryReconnect();
// 			if (this != 0)
// 			{
// 				new ReadyRequest().<IsReady>k__BackingField = isReady;
// 				int num = 0;
// 				uint num2;
// 				if (num < (int)num2)
// 				{
// 					num += num;
// 					num++;
// 				}
// 			}
// 		}
//
// 		public void SendLocalRaidUpdateRoomCondition(LocalRaidRoomConditionsType conditionType, long requiredBattlePower, int password)
// 		{
// 			base.TryReconnect();
// 			int num = 0;
// 			string text = string.Format("SendLocalRaidUpdateRoomCondition : conditionType = {0} requiredBattlePower = {1} password = {2}", "SendLocalRaidUpdateRoomCondition : conditionType = {0} requiredBattlePower = {1} password = {2}", "SendLocalRaidUpdateRoomCondition : conditionType = {0} requiredBattlePower = {1} password = {2}", "SendLocalRaidUpdateRoomCondition : conditionType = {0} requiredBattlePower = {1} password = {2}");
// 			if ("SendLocalRaidUpdateRoomCondition : conditionType = {0} requiredBattlePower = {1} password = {2}" != 0 && num < new UpdateRoomConditionRequest
// 			{
// 				<ConditionsType>k__BackingField = conditionType,
// 				<RequiredBattlePower>k__BackingField = requiredBattlePower,
// 				<Password>k__BackingField = password
// 			})
// 			{
// 				num += num;
// 				num++;
// 			}
// 		}
//
// 		public void SendLocalRaidUpdateBattlePower()
// 		{
// 			base.TryReconnect();
// 			if (this != 0)
// 			{
// 				UpdateBattlePowerRequest updateBattlePowerRequest = new UpdateBattlePowerRequest();
// 				int num = 0;
// 				uint num2;
// 				if (num < (int)num2)
// 				{
// 					num += num;
// 					num++;
// 				}
// 			}
// 		}
//
// 		public void SendRoomClear()
// 		{
// 			base.TryReconnect();
// 		}
//
// 		void IOrtegaReceiver.OnDisbandRoom()
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Network.MagicOnion.Client.OrtegaMagicOnionClient::Ortega.Share.MagicOnionShare.Interfaces.Receiver.IOrtegaReceiver.OnDisbandRoom()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	IL_34:
// 	stfld:LocalRaidPartyInfo(OrtegaMagicOnionClient::_currentLocalRaidPartyInfo, ldloc:OrtegaMagicOnionClient(this), conv.u8:uint64[exp:LocalRaidPartyInfo](ldc.i8:int64[exp:uint64](0)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		void IOrtegaReceiver.OnGetRoomList(OnGetRoomListResponse response)
// 		{
// 			if (this._localRaidReceiver != 0)
// 			{
// 				int num = 0;
// 				uint num2;
// 				if (num < (int)num2)
// 				{
// 					num += num;
// 					num++;
// 				}
// 			}
// 		}
//
// 		void IOrtegaReceiver.OnInvite(OnInviteResponse response)
// 		{
// 			if (this._localRaidReceiver != 0)
// 			{
// 			}
// 			if (this._currentLocalRaidPartyInfo > (ulong)0L || this._localRaidNotificaiton != 0)
// 			{
// 			}
// 		}
//
// 		void IOrtegaReceiver.OnJoinRoom(OnJoinRoomResponse response)
// 		{
// 			LocalRaidPartyInfo <LocalRaidPartyInfo>k__BackingField = response.<LocalRaidPartyInfo>k__BackingField;
// 			this._currentLocalRaidPartyInfo = <LocalRaidPartyInfo>k__BackingField;
// 			if (this._localRaidReceiver != 0)
// 			{
// 				int num = 0;
// 				uint num2;
// 				if (num < (int)num2)
// 				{
// 					num += num;
// 					num++;
// 				}
// 			}
// 			if (this._currentLocalRaidPartyInfo != (ulong)0L)
// 			{
// 				LocalRaidPartyInfo currentLocalRaidPartyInfo = this._currentLocalRaidPartyInfo;
// 				long playerId = this._playerId;
// 				if (currentLocalRaidPartyInfo.<LeaderPlayerId>k__BackingField == playerId)
// 				{
// 					return;
// 				}
// 			}
// 			if (!this.IsLocalRaidInvitedInRoom() || this._localRaidNotificaiton != 0)
// 			{
// 			}
// 		}
//
// 		void IOrtegaReceiver.OnLeaveRoom()
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Network.MagicOnion.Client.OrtegaMagicOnionClient::Ortega.Share.MagicOnionShare.Interfaces.Receiver.IOrtegaReceiver.OnLeaveRoom()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	IL_09:
// 	stfld:LocalRaidPartyInfo(OrtegaMagicOnionClient::_currentLocalRaidPartyInfo, ldloc:OrtegaMagicOnionClient(this), conv.u8:uint64[exp:LocalRaidPartyInfo](ldc.i8:int64[exp:uint64](0)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		void IOrtegaReceiver.OnLockRoom()
// 		{
// 			if (this._localRaidReceiver != 0)
// 			{
// 			}
// 		}
//
// 		void IOrtegaReceiver.OnRefuse()
// 		{
// 			if (this._localRaidReceiver != 0)
// 			{
// 			}
// 			if (this._currentLocalRaidPartyInfo != (ulong)0L)
// 			{
// 				LocalRaidPartyInfo currentLocalRaidPartyInfo = this._currentLocalRaidPartyInfo;
// 				long playerId = this._playerId;
// 				if (currentLocalRaidPartyInfo.<LeaderPlayerId>k__BackingField == playerId)
// 				{
// 					goto IL_32;
// 				}
// 			}
// 			IMagicOnionLocalRaidNotificaiton localRaidNotificaiton = this._localRaidNotificaiton;
// 			IL_32:
// 			this._currentLocalRaidPartyInfo = (ulong)0L;
// 			throw new NullReferenceException();
// 		}
//
// 		void IOrtegaReceiver.OnStartBattle()
// 		{
// 			if (this._localRaidReceiver != 0)
// 			{
// 			}
// 			if (this._currentLocalRaidPartyInfo != (ulong)0L)
// 			{
// 				LocalRaidPartyInfo currentLocalRaidPartyInfo = this._currentLocalRaidPartyInfo;
// 				long playerId = this._playerId;
// 				if (currentLocalRaidPartyInfo.<LeaderPlayerId>k__BackingField == playerId)
// 				{
// 					goto IL_32;
// 				}
// 			}
// 			IMagicOnionLocalRaidNotificaiton localRaidNotificaiton = this._localRaidNotificaiton;
// 			IL_32:
// 			this._currentLocalRaidPartyInfo = (ulong)0L;
// 			throw new NullReferenceException();
// 		}
//
// 		void IOrtegaReceiver.OnUpdatePartyCount(OnUpdatePartyCountResponse response)
// 		{
// 			if (this._localRaidReceiver != 0)
// 			{
// 				int num = 0;
// 				uint num2;
// 				if (num < (int)num2)
// 				{
// 					num += num;
// 					num++;
// 				}
// 			}
// 		}
//
// 		void IOrtegaReceiver.OnUpdateRoom(OnUpdateRoomResponse response)
// 		{
// 			LocalRaidPartyInfo <LocalRaidPartyInfo>k__BackingField = response.<LocalRaidPartyInfo>k__BackingField;
// 			this._currentLocalRaidPartyInfo = <LocalRaidPartyInfo>k__BackingField;
// 			if (this._localRaidReceiver != 0)
// 			{
// 				int num = 0;
// 				uint num2;
// 				if (num < (int)num2)
// 				{
// 					num += num;
// 					num++;
// 				}
// 			}
// 			if (this._currentLocalRaidPartyInfo != (ulong)0L)
// 			{
// 				LocalRaidPartyInfo currentLocalRaidPartyInfo = this._currentLocalRaidPartyInfo;
// 				long playerId = this._playerId;
// 				if (currentLocalRaidPartyInfo.<LeaderPlayerId>k__BackingField != playerId || !currentLocalRaidPartyInfo.IsReady || this._localRaidNotificaiton != 0)
// 				{
// 				}
// 			}
// 		}
//
// 		private IMagicOnionChatReceiver _chatReceiver;
//
// 		private IMagicOnionGvgReceiver _gvgLocalReceiver;
//
// 		private IMagicOnionGvgReceiver _gvgGlobalReceiver;
//
// 		private IMagicOnionAuthenticateReceiver _authenticateReceiver;
//
// 		private IMagicOnionErrorReceiver _errorReceiver;
//
// 		private IMagicOnionLocalRaidNotificaiton _localRaidNotificaiton;
//
// 		private IMagicOnionLocalRaidReceiver _localRaidReceiver;
//
// 		private Dictionary<ChatType, List<ChatInfo>> _chatInfoList;
//
// 		private LocalRaidPartyInfo _currentLocalRaidPartyInfo;
// 	}
// }
