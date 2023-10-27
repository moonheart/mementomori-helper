// using MementoMori.Ortega.Network.MagicOnion.Client;
// using MementoMori.Ortega.Network.MagicOnion.Interface;
// using Ortega.Common.Manager;
//
// namespace MementoMori.Ortega.Network.MagicOnion.Listener
// {
// 	public class MagicOnionLocalRaidListener : MonoBehaviour, IMagicOnionLocalRaidNotificaiton
// 	{
// 		public void Init()
// 		{
// 			this._state = (MagicOnionLocalRaidListener.State)((ulong)0L);
// 			LinkedList<NotificationMessageData> linkedList = new LinkedList();
// 			this._notificationMessages = linkedList;
// 			throw new NullReferenceException();
// 		}
//
// 		public void Bind(UINavigationService navigationService)
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Network.MagicOnion.Listener.MagicOnionLocalRaidListener::Bind(Ortega.Core.UINavigationService)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stfld:UINavigationService(MagicOnionLocalRaidListener::_navigationService, ldloc:MagicOnionLocalRaidListener(this), ldloc:UINavigationService(navigationService))
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
// 		public void Setup()
// 		{
// 			this._notificationMessages.Clear();
// 		}
//
// 		public void Run()
// 		{
// 			this._state = (MagicOnionLocalRaidListener.State)((ulong)1L);
// 		}
//
// 		public void ClearNotificationMessageDatas()
// 		{
// 			this._currentNotificationMessageData = (ulong)0L;
// 			this._notificationMessages.Clear();
// 		}
//
// 		public void OnUpdate()
// 		{
// 			if (this._state == MagicOnionLocalRaidListener.State.CheckNotification)
// 			{
// 				LinkedList<NotificationMessageData> notificationMessages = this._notificationMessages;
// 				if (SingletonMonoBehaviour.Instance.LocalRaidChallengeCount <= 0)
// 				{
// 					this.ClearNotificationMessageDatas();
// 					throw new NullReferenceException();
// 				}
// 				LinkedListNode head = this._notificationMessages.head;
// 				NotificationMessageData notificationMessageData;
// 				this._currentNotificationMessageData = notificationMessageData;
// 				this._notificationMessages.RemoveFirst();
// 				NotificationMessageData currentNotificationMessageData = this._currentNotificationMessageData;
// 				this._state = (MagicOnionLocalRaidListener.State)((ulong)2L);
// 				if (currentNotificationMessageData.NotificationMessageType <= NotificationMessageType.LocalRaidBattleRefuse)
// 				{
// 					Action<DialogCommonViewController> action;
// 					this._navigationService.Push<DialogCommonViewController>(action);
// 				}
// 			}
// 		}
//
// 		private void ToLocalRaidBattleInvitation()
// 		{
// 			Action<DialogCommonInvitationLocalRaidViewController> action;
// 			this._navigationService.Push<DialogCommonInvitationLocalRaidViewController>(action);
// 		}
//
// 		private void ToLocalRaid()
// 		{
// 			OrtegaMagicOnionClient client = SingletonMonoBehaviour.Instance._client;
// 			OrtegaMagicOnionClient magicOnionClient = client;
// 			OrtegaMagicOnionClient magicOnionClient2 = magicOnionClient;
// 			if (magicOnionClient2._currentLocalRaidPartyInfo > (ulong)0L)
// 			{
// 				LocalRaidPartyInfo currentLocalRaidPartyInfo = magicOnionClient2._currentLocalRaidPartyInfo;
// 				LocalRaidPartyInfo localRaidPartyInfo = currentLocalRaidPartyInfo;
// 				this._navigationService.Append<CompetitionViewController>();
// 				Action<LocalRaidRoomManagementViewController> action;
// 				this._navigationService.PushAndRemoveUntil<LocalRaidRoomManagementViewController>(action);
// 				this._currentNotificationMessageData = (ulong)0L;
// 				this._notificationMessages.Clear();
// 			}
// 			this._state = (MagicOnionLocalRaidListener.State)((ulong)1L);
// 		}
//
// 		private void ToReady()
// 		{
// 			Action<DialogCommonViewController> action;
// 			this._navigationService.Push<DialogCommonViewController>(action);
// 		}
//
// 		private void ToBattleStart()
// 		{
// 			Action<DialogCommonViewController> action;
// 			this._navigationService.Push<DialogCommonViewController>(action);
// 		}
//
// 		private void ToDismiss()
// 		{
// 			Action<DialogCommonViewController> action;
// 			this._navigationService.Push<DialogCommonViewController>(action);
// 		}
//
// 		private void ToRefuse()
// 		{
// 			Action<DialogCommonViewController> action;
// 			this._navigationService.Push<DialogCommonViewController>(action);
// 		}
//
// 		private void ToBattleSimulation()
// 		{
// 			Action<GetLocalRaidBattleResultResponse> <>9__17_ = MagicOnionLocalRaidListener.<>c.<>9__17_0;
// 			if (<>9__17_ == 0)
// 			{
// 				Action<GetLocalRaidBattleResultResponse> action;
// 				MagicOnionLocalRaidListener.<>c.<>9__17_0 = action;
// 			}
// 			Action<ApiErrorResponse> action2;
// 			LocalRaidApiProxy.RequestGetBattleResult(<>9__17_, action2);
// 		}
//
// 		private void ChangeState(MagicOnionLocalRaidListener.State state)
// 		{
// 			this._state = state;
// 		}
//
// 		private bool IsIgnoreNotification()
// 		{
// 			LocalRaidMainViewController localRaidMainViewController = this._navigationService.Get<LocalRaidMainViewController>();
// 			int num = 0;
// 			if (!(localRaidMainViewController != num))
// 			{
// 				LocalRaidRoomManagementViewController localRaidRoomManagementViewController = this._navigationService.Get<LocalRaidRoomManagementViewController>();
// 				int num2 = 0;
// 				return localRaidRoomManagementViewController != num2;
// 			}
// 			return true;
// 		}
//
// 		void IMagicOnionLocalRaidNotificaiton.OnError(ErrorCode errorCode)
// 		{
// 			NetworkManager instance = SingletonMonoBehaviour.Instance;
// 		}
//
// 		void IMagicOnionLocalRaidNotificaiton.OnReadyBattle()
// 		{
// 			if (!this.IsIgnoreNotification() && (this._currentNotificationMessageData == (ulong)0L || this._currentNotificationMessageData.NotificationMessageType != NotificationMessageType.LocalRaidBattleReady))
// 			{
// 				LinkedList<NotificationMessageData> notificationMessages = this._notificationMessages;
// 				LinkedListNode<NotificationMessageData> linkedListNode = notificationMessages.AddLast(new NotificationMessageData
// 				{
// 					NotificationMessageType = (NotificationMessageType)((ulong)0L)
// 				});
// 			}
// 		}
//
// 		void IMagicOnionLocalRaidNotificaiton.OnStartBattle()
// 		{
// 			if (!this.IsIgnoreNotification())
// 			{
// 				LinkedList<NotificationMessageData> notificationMessages = this._notificationMessages;
// 				LinkedListNode<NotificationMessageData> linkedListNode = notificationMessages.AddLast(new NotificationMessageData
// 				{
// 					NotificationMessageType = (NotificationMessageType)((ulong)1L)
// 				});
// 			}
// 		}
//
// 		void IMagicOnionLocalRaidNotificaiton.OnDisbandRoom()
// 		{
// 			if (!this.IsIgnoreNotification())
// 			{
// 				LinkedList<NotificationMessageData> notificationMessages = this._notificationMessages;
// 				LinkedListNode<NotificationMessageData> linkedListNode = notificationMessages.AddLast(new NotificationMessageData
// 				{
// 					NotificationMessageType = (NotificationMessageType)((ulong)4L)
// 				});
// 			}
// 		}
//
// 		void IMagicOnionLocalRaidNotificaiton.OnRefused()
// 		{
// 			if (!this.IsIgnoreNotification())
// 			{
// 				LinkedList<NotificationMessageData> notificationMessages = this._notificationMessages;
// 				LinkedListNode<NotificationMessageData> linkedListNode = notificationMessages.AddLast(new NotificationMessageData
// 				{
// 					NotificationMessageType = (NotificationMessageType)((ulong)5L)
// 				});
// 			}
// 		}
//
// 		void IMagicOnionLocalRaidNotificaiton.OnInvited(OnInviteResponse response)
// 		{
// 			while (this._currentNotificationMessageData == (ulong)0L || this._currentNotificationMessageData.PlayerId != response.<PlayerId>k__BackingField)
// 			{
// 				int num = 0;
// 				LinkedList<NotificationMessageData> notificationMessages = this._notificationMessages;
// 				bool flag;
// 				if (flag)
// 				{
// 				}
// 				if (num == 0)
// 				{
// 					NotificationMessageData notificationMessageData = new NotificationMessageData();
// 					int num2 = 0;
// 					notificationMessageData.NotificationMessageType = (NotificationMessageType)((ulong)2L);
// 					notificationMessageData.RoomId = num2;
// 					notificationMessageData.GuildName = num2;
// 					notificationMessageData.PlayerId = notificationMessageData;
// 					notificationMessageData.PlayerName = num2;
// 					notificationMessageData.PlayerLevel = notificationMessageData;
// 					notificationMessageData.BattlePower = notificationMessageData;
// 					notificationMessageData.CharacterIconId = notificationMessageData;
// 					notificationMessageData.QuestId = notificationMessageData;
// 					notificationMessageData.ClearCount = notificationMessageData;
// 					break;
// 				}
// 			}
// 		}
//
// 		void IMagicOnionLocalRaidNotificaiton.OnJoinRoom()
// 		{
// 			LinkedList<NotificationMessageData> notificationMessages = this._notificationMessages;
// 			LinkedListNode<NotificationMessageData> linkedListNode = notificationMessages.AddFirst(new NotificationMessageData
// 			{
// 				NotificationMessageType = (NotificationMessageType)((ulong)3L)
// 			});
// 		}
//
// 		private LinkedList<NotificationMessageData> _notificationMessages;
//
// 		private NotificationMessageData _currentNotificationMessageData;
//
// 		private UINavigationService _navigationService;
//
// 		private MagicOnionLocalRaidListener.State _state;
//
// 		private enum State
// 		{
// 			None,
// 			CheckNotification,
// 			Wait
// 		}
// 	}
// }
