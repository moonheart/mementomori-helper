// using System.Runtime.CompilerServices;
// using MagicOnion;
// using MementoMori.Ortega.Network.MagicOnion.Interface;
//
// namespace MementoMori.Ortega.Network.MagicOnion.Client
// {
// 	public abstract class MagicOnionClient<TSender, TReceiver> : BaseMagicOnionClient where TSender : IStreamingHub<TSender, TReceiver>
// 	{
// 		public MagicOnionClient(GrpcChannelx channel, long playerId, string authToken)
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Network.MagicOnion.Client.MagicOnionClient`2::.ctor(MagicOnion.GrpcChannelx,System.Int64,System.String)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	call:void(BaseMagicOnionClient::.ctor, ldloc:MagicOnionClient`2[exp:BaseMagicOnionClient](this), ldloc:int64(playerId), ldloc:string(authToken))
// 	stfld:IDisconnectReceiver(MagicOnionClient`2::_internalDisconnectReceiver, ldloc:MagicOnionClient`2(this), ldloc:GrpcChannelx[exp:IDisconnectReceiver](channel))
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
// 		public override bool IsExistHubClient()
// 		{
// 			bool flag;
// 			return flag;
// 		}
//
// 		public override Task DisposeAsync()
// 		{
// 			throw new NullReferenceException();
// 		}
//
// 		protected void AttachInternalReceiver(TReceiver internalReceiver, IDisconnectReceiver internalDisconnectReceiver)
// 		{
// 			throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
// 		}
//
// 		protected override void ConnectHub()
// 		{
// 			Action defaultContextAction = AsyncVoidMethodBuilder.Create().m_coreState.m_defaultContextAction;
// 		}
//
// 		protected override void Authenticate()
// 		{
// 			base.ChangeState(HubClientState.Authenticating);
// 		}
//
// 		protected override void SucceededAuthentication()
// 		{
// 			base.ResetRetryCount();
// 			base.ChangeState(HubClientState.Ready);
// 		}
//
// 		protected override void FailedAuthentication()
// 		{
// 			base.ChangeState(HubClientState.FailedAuthentication);
// 		}
//
// 		protected override void WatchDisconnect()
// 		{
// 			Action defaultContextAction = AsyncVoidMethodBuilder.Create().m_coreState.m_defaultContextAction;
// 		}
//
// 		protected GrpcChannelx _channel;
//
// 		protected TSender _sender;
//
// 		protected TReceiver _internalReceiver;
//
// 		protected IDisconnectReceiver _internalDisconnectReceiver;
// 	}
// }
