// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Runtime.InteropServices;
// using Backtrace.Unity.Model;
// using Cpp2IlInjected;
// using MementoMori.Ortega.Share.Data.ApiInterface;
// using Ortega.Common.Data;
// using Ortega.Common.MasterData;
// using Ortega.Common.ViewController;
// using Ortega.Core;
// using Ortega.Network.Api.Client;
// using Ortega.Share;
// using Ortega.Share.Master.Data;
// using Ortega.Share.Master.Table;
// using Ortega.Share.Utils;
// using UnityEngine;
//
namespace Ortega.Common.Manager
{
	public class NetworkManager
	{
		public long DiffServerTimeMilliSeconds
		{
			get;
			private set;
		}
//
// 		public void Init()
// 		{
// 			Queue<BaseApiClient> queue = new Queue<BaseApiClient>();
// 			this._clients = queue;
// 			Timer timer = new Timer();
// 			this._timerTimeout = timer;
// 			Dictionary<Type, string> dictionary = new Dictionary();
// 			this._gameApiUriMap = dictionary;
// 			Dictionary<Type, string> dictionary2 = new Dictionary();
// 			this._authApiUriMap = dictionary2;
// 			int num = 0;
// 			if (typeof(ApiRequestBase) != 0)
// 			{
// 				int num2 = 0;
// 				bool flag;
// 				bool flag2;
// 				if (flag || flag2)
// 				{
// 					Dictionary<Type, string> gameApiUriMap = this._gameApiUriMap;
// 					OrtegaApiAttribute ortegaApiAttribute;
// 					string <Uri>k__BackingField = ortegaApiAttribute.<Uri>k__BackingField;
// 					gameApiUriMap.Add(num2, <Uri>k__BackingField);
// 				}
// 			}
// 			num++;
// 			GameManager instance = SingletonMonoBehaviour.Instance;
// 			string <BaseApiUri>k__BackingField = SingletonMonoBehaviour.Instance.<BaseApiUri>k__BackingField;
// 			instance.<ApiHost>k__BackingField = <BaseApiUri>k__BackingField;
// 		}
//
// 		public void Reset()
// 		{
// 			this._clients.Clear();
// 			int num = 0;
// 			if (this._currentClient != null)
// 			{
// 				this._currentClient.Dispose();
// 				this._currentClient = null;
// 			}
// 			string empty = string.Empty;
// 			this._gameServerAccessToken = empty;
// 			string empty2 = string.Empty;
// 			this._authServerAccessToken = empty2;
// 			string empty3 = string.Empty;
// 			this._masterVersion = empty3;
// 			string empty4 = string.Empty;
// 			this._assetVersion = empty4;
// 			this._serverUtcTimeStamp = (long)num;
// 			this._lastConnectedLocalYearMonthDay = num;
// 			this._isChangeDay = num != 0;
// 			this._requestTryCount = num;
// 			GameManager instance = SingletonMonoBehaviour.Instance;
// 			string <BaseApiUri>k__BackingField = SingletonMonoBehaviour.Instance.<BaseApiUri>k__BackingField;
// 			instance.<ApiHost>k__BackingField = <BaseApiUri>k__BackingField;
// 			this._state = (NetworkManager.State)((ulong)1L);
// 		}
//
// 		public void SendRequest<TRequest, TResponse>(TRequest request, Action cpp2il__autoParamName__idx_1, Action cpp2il__autoParamName__idx_2) where TRequest : ApiRequestBase where TResponse : ApiResponseBase
// 		{
// 		}
//
// 		public string GetMasterVersion()
// 		{
// 			return this._masterVersion;
// 		}
//
// 		public string GetAssetVersion()
// 		{
// 			return this._assetVersion;
// 		}
//
// 		public bool IsInValidGameServerAccessToken()
// 		{
// 			return string.IsNullOrEmpty(this._gameServerAccessToken);
// 		}
//
// 		public bool IsConnecting()
// 		{
// 			if (this._state == NetworkManager.State.None)
// 			{
// 			}
// 			return this._state != NetworkManager.State.CheckQueue;
// 		}
//
// 		public bool IsBusy()
// 		{
// 			Queue<BaseApiClient> clients = this._clients;
// 			if (this._currentClient == (ulong)0L)
// 			{
// 				if (this._state == NetworkManager.State.None)
// 				{
// 				}
// 				return this._state != NetworkManager.State.CheckQueue;
// 			}
// 			return true;
// 		}
//
// 		public void OnUpdate()
// 		{
// 			if (this._state <= NetworkManager.State.Failed)
// 			{
// 				this.ProcessCheckQueue();
// 			}
// 		}
//
// 		private void EnqueueRequest<TRequest, TResponse>(TRequest request, Action<TResponse> callbackSuccess, [Optional] Action<ApiErrorResponse> callbackError) where TRequest : ApiRequestBase where TResponse : ApiResponseBase
// 		{
// 			bool flag;
// 			while (!flag)
// 			{
// 				if (this._currentClient != (ulong)0L)
// 				{
// 					BaseApiClient currentClient = this._currentClient;
// 					ApiRequestBase request2 = currentClient.GetRequest();
// 					if (currentClient._retryCount != 0 && !this._currentClient.IsDone())
// 					{
// 						int num = 0;
// 						Type type;
// 						string text = string.Format("{0}重複", type);
// 						int num2 = 0;
// 						text.FieldGetter(num2, num, callbackError);
// 						return;
// 					}
// 				}
// 				Queue<BaseApiClient> clients = this._clients;
// 				bool flag2;
// 				if (flag2)
// 				{
// 					BaseApiClient baseApiClient;
// 					if (baseApiClient.GetRequest() == 0)
// 					{
// 						continue;
// 					}
// 					int num3 = 0;
// 					Type type2;
// 					string text2 = string.Format("{0}重複", type2);
// 					int num4 = 0;
// 					text2.FieldGetter(num4, num3, callbackError);
// 				}
// 				ulong num5;
// 				if (num5 == (ulong)0L)
// 				{
// 					break;
// 				}
// 			}
// 			Type type3;
// 			if (this._authApiUriMap.ContainsKey(type3))
// 			{
// 			}
// 			Dictionary<Type, string> gameApiUriMap = this._gameApiUriMap;
// 			string text3 = gameApiUriMap[type3];
// 			int count = gameApiUriMap._count;
// 			Ortega.Share.Enums.DeviceType deviceType = SingletonMonoBehaviour.Instance.GetDeviceType();
// 			Queue<BaseApiClient> clients2 = this._clients;
// 			Type type4;
// 			string text4 = string.Format("NetworkManager -> Added Queue : {0}", type4);
// 		}
//
// 		private void ProcessCheckQueue()
// 		{
// 			BaseApiClient baseApiClient = this._clients.Dequeue();
// 			this._currentClient = baseApiClient;
// 			this._state = (NetworkManager.State)((ulong)3L);
// 			this.ProcessRequest();
// 		}
//
// 		private void ProcessRetry()
// 		{
// 			StaticAccess.ApiConnectingViewController.Show();
// 			this._timerTimeout.Start();
// 			Type type = this._currentClient.GetRequest().GetType();
// 			int num = 0;
// 			if (type.HasGenericAttribute(num != 0))
// 			{
// 			}
// 			string authServerAccessToken = this._authServerAccessToken;
// 			this._currentClient._accessToken = authServerAccessToken;
// 			this._currentClient.Retry();
// 			int num2 = 0;
// 			OrtegaLogger.Log("NetworkManager -> Retry", (ErrorLevel)num2);
// 			this._state = (NetworkManager.State)((ulong)4L);
// 		}
//
// 		private void ProcessRequest()
// 		{
// 			ApiRequestBase request = this._currentClient.GetRequest();
// 			if (request != 0)
// 			{
// 			}
// 			bool flag = this.IsSkipCheckVersion(request);
// 			if (!flag && this._lastConnectedLocalYearMonthDay != 0)
// 			{
// 				if (this._isChangeDay == flag)
// 				{
// 					int num;
// 					if (num <= this._lastConnectedLocalYearMonthDay)
// 					{
// 						goto IL_47;
// 					}
// 					this._lastConnectedLocalYearMonthDay = num;
// 				}
// 				uint num2;
// 				uint num3;
// 				num2.m_value = num3;
// 				return;
// 			}
// 			IL_47:
// 			if (this._currentClient.GetRequest() != 0)
// 			{
// 			}
// 			StaticAccess.ApiConnectingViewController.Show();
// 			this._timerTimeout.Start();
// 			Type type = this._currentClient.GetRequest().GetType();
// 			int num4 = 0;
// 			if (type.HasGenericAttribute(num4 != 0))
// 			{
// 			}
// 			string authServerAccessToken = this._authServerAccessToken;
// 			this._currentClient._accessToken = authServerAccessToken;
// 			this._currentClient.Post();
// 			this._state = (NetworkManager.State)((ulong)4L);
// 		}
//
// 		private void ProcessRequestWait()
// 		{
// 			long elapsedMilliSeconds = this._timerTimeout.GetElapsedMilliSeconds();
// 			if (this._currentClient.IsDone())
// 			{
// 				this._state = (NetworkManager.State)((ulong)5L);
// 			}
// 		}
//
// 		private void ProcessCheckError()
// 		{
// 			/*
// An exception occurred when decompiling this method (06005F70)
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Common.Manager.NetworkManager::ProcessCheckError()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_39:
// 	stfld:string(DmmManager::_oneTimeToken, ldloc:DmmManager(var_102_76D), ldloc:string(var_103_77A))
// 	stloc:int64(var_105_79A, callgetter:int64(TimeUtil::get_UtcNowTimeStamp))
// 	stfld:int64(DmmManager::_oneTimeTokenReceivedTime, ldloc:DmmManager(var_102_76D), ldloc:int64(var_105_79A))
// 	stloc:string(var_106_7B1, call:string(string::Concat, ldstr:string("【DMM】RefreshToken : "), ldloc:string(var_103_77A)))
// 	stloc:int32(var_107_7B4, ldc.i4:int32(0))
// 	stloc:int32(var_108_7B7, ldc.i4:int32(0))
// 	call:void(object::FieldGetter, ldloc:string[exp:object](var_106_7B1), ldloc:int32[exp:string](var_108_7B7), ldloc:int32[exp:string](var_107_7B4), ldloc:object[][exp:object](var_98))
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
// 		private void ProcessSuccess()
// 		{
// 			this.SetParameters();
// 			ApiResponseBase response = this._currentClient.GetResponse();
// 			int num = 0;
// 			if (response != 0)
// 			{
// 				UserDataManager instance = SingletonMonoBehaviour.Instance;
// 				uint num2;
// 				if (num < (int)num2)
// 				{
// 					num += num;
// 					num++;
// 				}
// 			}
// 			if (response != 0)
// 			{
// 				GuildDataManager instance2 = SingletonMonoBehaviour.Instance;
// 				uint num3;
// 				if (num < (int)num3)
// 				{
// 					num += num;
// 					num++;
// 				}
// 				if (instance2 != 0)
// 				{
// 					instance2.<SyncData>k__BackingField = instance2;
// 				}
// 			}
// 			if (this._currentClient.GetRequest() != 0)
// 			{
// 				this._requestTryCount = num;
// 			}
// 			this._currentClient.OnSuccess(response);
// 			this._currentClient.Dispose();
// 			this._currentClient = num;
// 			this._state = (NetworkManager.State)((ulong)1L);
// 			this.ProcessCheckQueue();
// 		}
//
// 		private void SetParameters()
// 		{
// 			string accessToken = this._currentClient.GetAccessToken();
// 			bool flag = string.IsNullOrEmpty(accessToken);
// 			Type type = this._currentClient.GetRequest().GetType();
// 			int num = 0;
// 			if (!type.HasGenericAttribute(num != 0))
// 			{
// 				this._authServerAccessToken = accessToken;
// 				string authServerAccessToken = this._authServerAccessToken;
// 				string text = "_authServerAccessToken:" + authServerAccessToken;
// 			}
// 			this._gameServerAccessToken = accessToken;
// 			string masterVersion = this._currentClient.GetMasterVersion();
// 			this._masterVersion = masterVersion;
// 			string assetVersion = this._currentClient.GetAssetVersion();
// 			this._assetVersion = assetVersion;
// 			long timeStamp = this._currentClient.GetTimeStamp();
// 			this._serverUtcTimeStamp = timeStamp;
// 			DmmManager instance = SingletonMonoBehaviour.Instance;
// 			string dmmOneTimeToken = this._currentClient.GetDmmOneTimeToken();
// 			if (!string.IsNullOrEmpty(dmmOneTimeToken))
// 			{
// 				instance._oneTimeToken = dmmOneTimeToken;
// 				long utcNowTimeStamp = TimeUtil.UtcNowTimeStamp;
// 				instance._oneTimeTokenReceivedTime = utcNowTimeStamp;
// 				string text2 = "【DMM】RefreshToken : " + dmmOneTimeToken;
// 			}
// 			long num2 = this._serverUtcTimeStamp;
// 			long num3 = TimeUtil.UtcTimeCalculation(DateTime.UtcNow);
// 			num2 -= num3;
// 			this.<DiffServerTimeMilliSeconds>k__BackingField = num2;
// 			if (SingletonMonoBehaviour.Instance.<CurrentScene>k__BackingField != SceneTransition.Title && SingletonMonoBehaviour.Instance.<SyncData>k__BackingField != (ulong)0L)
// 			{
// 				int dateIntYearMonthDay = TimeManager.<Instance>k__BackingField.GetDateIntYearMonthDay();
// 				if (this._lastConnectedLocalYearMonthDay == 0)
// 				{
// 					this._lastConnectedLocalYearMonthDay = dateIntYearMonthDay;
// 					return;
// 				}
// 				if (dateIntYearMonthDay > this._lastConnectedLocalYearMonthDay)
// 				{
// 					this._lastConnectedLocalYearMonthDay = dateIntYearMonthDay;
// 					this._isChangeDay = true;
// 				}
// 			}
// 		}
//
// 		private void ChangeState(NetworkManager.State state)
// 		{
// 			this._state = state;
// 		}
//
// 		private bool CheckTryCount(ApiRequestBase request)
// 		{
// 			Type curRequestType = this._curRequestType;
// 			this._prevRequestType = curRequestType;
// 			Type type = request.GetType();
// 			this._curRequestType = type;
// 			Type prevRequestType = this._prevRequestType;
// 			int num = 0;
// 			if (prevRequestType.Equals(num))
// 			{
// 				Type curRequestType2 = this._curRequestType;
// 				this._prevRequestType = curRequestType2;
// 			}
// 			Type curRequestType3 = this._curRequestType;
// 			Type prevRequestType2 = this._prevRequestType;
// 			if (curRequestType3.Equals(prevRequestType2))
// 			{
// 			}
// 			if (this._requestTryCount > 3)
// 			{
// 				return true;
// 			}
// 		}
//
// 		private void OpenTwitter()
// 		{
// 			Application.OpenURL(LocalTextResourceTable.Get("[TwitterUrl]"));
// 		}
//
// 		public NetworkManager()
// 		{
// 		}
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x18")]
// 		private UINavigationService _navigationService;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x20")]
// 		private Queue<BaseApiClient> _clients;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x28")]
// 		private BaseApiClient _currentClient;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x30")]
// 		private Dictionary<Type, string> _gameApiUriMap;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x38")]
// 		private Dictionary<Type, string> _authApiUriMap;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x40")]
// 		private string _gameServerAccessToken;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x48")]
// 		private string _authServerAccessToken;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x50")]
// 		private string _masterVersion;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x58")]
// 		private string _assetVersion;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x60")]
// 		private long _serverUtcTimeStamp;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x68")]
// 		private int _lastConnectedLocalYearMonthDay;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x6C")]
// 		private bool _isChangeDay;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x70")]
// 		private NetworkManager.State _state;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x78")]
// 		private Timer _timerTimeout;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x80")]
// 		private Type _prevRequestType;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x88")]
// 		private Type _curRequestType;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x90")]
// 		private int _requestTryCount;
//
// 		private enum State
// 		{
// 			None,
// 			CheckQueue,
// 			Retry,
// 			Request,
// 			RequestWait,
// 			CheckError,
// 			Error,
// 			Success,
// 			Failed,
// 			ChangeDay
// 		}
	}
}
