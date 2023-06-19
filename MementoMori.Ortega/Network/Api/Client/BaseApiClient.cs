// using System;
// using System.Runtime.InteropServices;
// using System.Text;
// using Cpp2IlInjected;
// using MementoMori.Ortega.Share.Data.ApiInterface;
// using MementoMori.Ortega.Share.Data.DtoInfo;
// using MementoMori.Ortega.Share.Enums;
// using Ortega.Common;
// using Ortega.Common.Manager;
// using Ortega.Network.Request;
// using Ortega.Share;
// using Ortega.Share.Data;
// using Ortega.Share.Utils;
//
// namespace Ortega.Network.Api.Client
// {
// 	public abstract class BaseApiClient : IDisposable
// 	{
// 		public void Dispose()
// 		{
// 			if (this._networkRequest != 0)
// 			{
// 				return;
// 			}
// 			this._networkRequest = 0;
// 			this._callbackSuccess = null;
// 			this._callbackError = null;
// 			this._requestBytes = null;
// 		}
//
// 		public abstract ApiResponseBase GetResponse();
//
// 		public abstract ApiRequestBase GetRequest();
//
// 		public void SetAppVersion(string appVersion)
// 		{
// 			_appVersion = appVersion;
// 		}
//
// 		public void SetUri(string uri, bool isAuth)
// 		{
// 			GameManager instance = SingletonMonoBehaviour.Instance;
// 			if (isAuth)
// 			{
// 			}
// 			string text = instance.<ApiHost>k__BackingField + uri;
// 			this._url = text;
// 			throw new NullReferenceException();
// 		}
//
// 		public void SetAccessToken(string accessToken)
// 		{
// 			/*
// An exception occurred when decompiling this method (06002FA8)
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Network.Api.Client.BaseApiClient::SetAccessToken(System.String)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stfld:string(BaseApiClient::_accessToken, ldloc:BaseApiClient(this), ldloc:string(accessToken))
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
// 		public void SetDeviceType(DeviceType deviceType)
// 		{
// 			this._deviceType = deviceType;
// 		}
//
// 		public void SetCheckVersion(bool isCheckVersion)
// 		{
// 			this._isCheckVersion = isCheckVersion;
// 		}
//
// 		public void SetCallbackSuccess(Action<ApiResponseBase> callback)
// 		{
// 			/*
// An exception occurred when decompiling this method (06002FAB)
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Network.Api.Client.BaseApiClient::SetCallbackSuccess(System.Action`1<Ortega.Share.Data.ApiInterface.ApiResponseBase>)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stfld:class [mscorlib]System.Action`1<class Ortega.Share.Data.ApiInterface.ApiResponseBase>(BaseApiClient::_callbackSuccess, ldloc:BaseApiClient(this), ldloc:class [mscorlib]System.Action`1<class Ortega.Share.Data.ApiInterface.ApiResponseBase>(callback))
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
// 		public void SetCallbackFailed(Action<ApiErrorResponse> callback)
// 		{
// 			/*
// An exception occurred when decompiling this method (06002FAC)
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Network.Api.Client.BaseApiClient::SetCallbackFailed(System.Action`1<Ortega.Share.Data.ApiInterface.ApiErrorResponse>)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stfld:class [mscorlib]System.Action`1<class Ortega.Share.Data.ApiInterface.ApiErrorResponse>(BaseApiClient::_callbackError, ldloc:BaseApiClient(this), ldloc:class [mscorlib]System.Action`1<class Ortega.Share.Data.ApiInterface.ApiErrorResponse>(callback))
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
// 		public void Post()
// 		{
// 			if (this._retryCount == 1)
// 			{
// 			}
// 			UnityWebRequestWrapper unityWebRequestWrapper = new UnityWebRequestWrapper();
// 			this._networkRequest = unityWebRequestWrapper;
// 			INetworkRequest networkRequest = this._networkRequest;
// 			int num = 0;
// 			uint num2;
// 			if (num < (int)num2)
// 			{
// 				num += num;
// 				num++;
// 			}
// 			INetworkRequest networkRequest2 = this._networkRequest;
// 			INetworkRequest networkRequest3 = this._networkRequest;
// 			string accessToken = this._accessToken;
// 			INetworkRequest networkRequest4 = this._networkRequest;
// 			string appVersion = this._appVersion;
// 			INetworkRequest networkRequest5 = this._networkRequest;
// 			DeviceType deviceType = this._deviceType;
// 			INetworkRequest networkRequest6 = this._networkRequest;
// 			string <UUID>k__BackingField = SingletonMonoBehaviour.Instance.<UUID>k__BackingField;
// 			ApiRequestBase request = this.GetRequest();
// 			if (request == 0)
// 			{
// 				num += 312;
// 			}
// 			bool flag = request != 0;
// 			INetworkRequest networkRequest7 = this._networkRequest;
// 			string text = SingletonMonoBehaviour.Instance.IsNeedToUpdateOneTimeToken(flag);
// 			INetworkRequest networkRequest8 = this._networkRequest;
// 			string oneTimeToken = SingletonMonoBehaviour.Instance._oneTimeToken;
// 			INetworkRequest networkRequest9 = this._networkRequest;
// 			string viewerId = SingletonMonoBehaviour.Instance._viewerId;
// 			INetworkRequest networkRequest10 = this._networkRequest;
// 			uint num3;
// 			if (num < (int)num3)
// 			{
// 				num += num;
// 				num++;
// 			}
// 			INetworkRequest networkRequest11 = this._networkRequest;
// 		}
//
// 		public void Retry()
// 		{
// 			if (this._networkRequest != 0)
// 			{
// 			}
// 			this._networkRequest = (ulong)0L;
// 			this.Post();
// 		}
//
// 		public void OnSuccess(ApiResponseBase apiResponseBase)
// 		{
// 			if (this._callbackSuccess != 0)
// 			{
// 				ApiResponseBase response = this.GetResponse();
// 				return;
// 			}
// 		}
//
// 		public bool OnFailed(ApiErrorResponse apiResponse)
// 		{
// 			if (this._callbackError != (ulong)0L)
// 			{
// 				Action<ApiErrorResponse> callbackError = this._callbackError;
// 				return true;
// 			}
// 		}
//
// 		public long GetTimeStamp()
// 		{
// 			string <HeaderUtcNowTimeStamp>k__BackingField = OrtegaConst.HttpHeaderResponse.<HeaderUtcNowTimeStamp>k__BackingField;
// 			return this.GetHeader(<HeaderUtcNowTimeStamp>k__BackingField).ToLong();
// 		}
//
// 		public string GetAccessToken()
// 		{
// 			string <HeaderNextAccessTokenKey>k__BackingField = OrtegaConst.HttpHeaderResponse.<HeaderNextAccessTokenKey>k__BackingField;
// 			return this.GetHeader(<HeaderNextAccessTokenKey>k__BackingField);
// 		}
//
// 		public string GetMasterVersion()
// 		{
// 			string <HeaderMasterVersion>k__BackingField = OrtegaConst.HttpHeaderResponse.<HeaderMasterVersion>k__BackingField;
// 			return this.GetHeader(<HeaderMasterVersion>k__BackingField);
// 		}
//
// 		public string GetAssetVersion()
// 		{
// 			string <HeaderAssetVersion>k__BackingField = OrtegaConst.HttpHeaderResponse.<HeaderAssetVersion>k__BackingField;
// 			return this.GetHeader(<HeaderAssetVersion>k__BackingField);
// 		}
//
// 		public string GetDmmOneTimeToken()
// 		{
// 			return this.GetHeader("OrtegaDmmOneTimeToken");
// 		}
//
// 		public int GetRetryCount()
// 		{
// 			return this._retryCount;
// 		}
//
// 		public ApiErrorResponse GetErrorResponse()
// 		{
// 			INetworkRequest networkRequest = this._networkRequest;
// 			ApiErrorResponse apiErrorResponse;
// 			return apiErrorResponse;
// 		}
//
// 		public bool IsDone()
// 		{
// 			if (this._networkRequest != 0)
// 			{
// 				return;
// 			}
// 		}
//
// 		public bool IsMaintenanceError()
// 		{
// 			INetworkRequest networkRequest = this._networkRequest;
// 			if ("maintenance-mode" != 0 && this.GetHttpStatusCode() == 503)
// 			{
// 				INetworkRequest networkRequest2 = this._networkRequest;
// 				bool flag;
// 				return flag;
// 			}
// 			throw new NullReferenceException();
// 		}
//
// 		public bool IsBlockedDueToExcessiveAccessError()
// 		{
// 			INetworkRequest networkRequest = this._networkRequest;
// 			if ("Blocked due to excessive access" != 0 && this.GetHttpStatusCode() == 403)
// 			{
// 				INetworkRequest networkRequest2 = this._networkRequest;
// 				bool flag;
// 				return flag;
// 			}
// 			throw new NullReferenceException();
// 		}
//
// 		public bool IsHttpError()
// 		{
// 			INetworkRequest networkRequest = this._networkRequest;
// 			if (typeof(INetworkRequest).TypeHandle == 0)
// 			{
// 				return typeof(INetworkRequest).TypeHandle != null;
// 			}
// 			return this.GetHttpStatusCode() != 200;
// 		}
//
// 		public int GetHttpStatusCode()
// 		{
// 			INetworkRequest networkRequest = this._networkRequest;
// 			int num = 0;
// 			uint num2;
// 			if (num < (int)num2)
// 			{
// 				num += num;
// 				num++;
// 			}
// 			networkRequest += networkRequest;
// 			throw new NullReferenceException();
// 		}
//
// 		public string GetHttpDataText()
// 		{
// 			INetworkRequest networkRequest = this._networkRequest;
// 			throw new NullReferenceException();
// 		}
//
// 		public bool IsResponseDataError()
// 		{
// 			INetworkRequest networkRequest = this._networkRequest;
// 			if (typeof(INetworkRequest).TypeHandle != 0)
// 			{
// 			}
// 			return true;
// 		}
//
// 		public bool IsTimeStampError()
// 		{
// 			string <HeaderUtcNowTimeStamp>k__BackingField = OrtegaConst.HttpHeaderResponse.<HeaderUtcNowTimeStamp>k__BackingField;
// 			ulong num;
// 			return !long.TryParse(this.GetHeader(<HeaderUtcNowTimeStamp>k__BackingField), (long)num);
// 		}
//
// 		public bool TryApiStatusCode([Out] ApiStatusCode apiStatusCode)
// 		{
// 			string <HeaderStatusCodeKey>k__BackingField = OrtegaConst.HttpHeaderResponse.<HeaderStatusCodeKey>k__BackingField;
// 			ulong num;
// 			bool flag = byte.TryParse(this.GetHeader(<HeaderStatusCodeKey>k__BackingField), (byte)num);
// 			if (!flag)
// 			{
// 				return flag;
// 			}
// 			apiStatusCode.value__ = (byte)num;
// 			return true;
// 		}
//
// 		public bool IsUpdatedMasterDataVersion(string oldVersion)
// 		{
// 			if (!this._isCheckVersion)
// 			{
// 				string <HeaderMasterVersion>k__BackingField = OrtegaConst.HttpHeaderResponse.<HeaderMasterVersion>k__BackingField;
// 				return !this.GetHeader(<HeaderMasterVersion>k__BackingField).Equals(oldVersion);
// 			}
// 			throw new NullReferenceException();
// 		}
//
// 		public bool IsUpdatedAssetVersion(string oldVersion)
// 		{
// 			if (!this._isCheckVersion)
// 			{
// 				string <HeaderAssetVersion>k__BackingField = OrtegaConst.HttpHeaderResponse.<HeaderAssetVersion>k__BackingField;
// 				return !this.GetHeader(<HeaderAssetVersion>k__BackingField).Equals(oldVersion);
// 			}
// 			throw new NullReferenceException();
// 		}
//
// 		public void LogRequest()
// 		{
// 			throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
// 		}
//
// 		public void LogResponse()
// 		{
// 			throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
// 		}
//
// 		public string GetBacktraceMessage()
// 		{
// 			if (this._networkRequest != (ulong)0L)
// 			{
// 				StringBuilder stringBuilder = new StringBuilder();
// 				INetworkRequest networkRequest = this._networkRequest;
// 				Type type;
// 				string text = type.ToString();
// 				StringBuilder stringBuilder2 = stringBuilder.AppendLine(text);
// 				string <UUID>k__BackingField = SingletonMonoBehaviour.Instance.<UUID>k__BackingField;
// 				string text2 = "【UUID】" + <UUID>k__BackingField;
// 				StringBuilder stringBuilder3 = stringBuilder.AppendLine(text2);
// 				int httpStatusCode = this.GetHttpStatusCode();
// 				string text4;
// 				string text3 = "【StatusCode】" + text4;
// 				StringBuilder stringBuilder4 = stringBuilder.AppendLine(text3);
// 				INetworkRequest networkRequest2 = this._networkRequest;
// 				if (stringBuilder4 != 0)
// 				{
// 				}
// 				int num;
// 				string text5 = string.Format("【DataLength】{0}", num);
// 				StringBuilder stringBuilder5 = stringBuilder.AppendLine(text5);
// 				INetworkRequest networkRequest3 = this._networkRequest;
// 				string text6;
// 				StringBuilder stringBuilder6 = stringBuilder.AppendLine(text6);
// 				string requestLog = this.GetRequestLog();
// 				StringBuilder stringBuilder7 = stringBuilder.AppendLine(requestLog);
// 				return stringBuilder.ToString();
// 			}
// 			return "_networkRequest:null";
// 		}
//
// 		protected byte[] GetBody()
// 		{
// 			INetworkRequest networkRequest = this._networkRequest;
// 			throw new NullReferenceException();
// 		}
//
// 		private string GetRequestLog()
// 		{
// 			string[] array;
// 			string text;
// 			for (;;)
// 			{
// 				array = new string[6];
// 				if ("【API Request】" == 0 || array != 0)
// 				{
// 					array[0] = "【API Request】";
// 					string url = this._url;
// 					if (url == 0 || array != 0)
// 					{
// 						array[1] = url;
// 						if ("\n" == 0 || "\n" != 0)
// 						{
// 							array[2] = "\n";
// 							string debugLogInfo = this.GetDebugLogInfo();
// 							if (debugLogInfo == 0 || debugLogInfo != 0)
// 							{
// 								array[3] = debugLogInfo;
// 								if ("\n" == 0 || "\n" != 0)
// 								{
// 									array[4] = "\n";
// 									if (text == 0 || "\n" != 0)
// 									{
// 										break;
// 									}
// 								}
// 							}
// 						}
// 					}
// 				}
// 			}
// 			array[5] = text;
// 			return string.Concat(array);
// 		}
//
// 		private string GetHeader(string name)
// 		{
// 			INetworkRequest networkRequest = this._networkRequest;
// 			int num = 0;
// 			uint num2;
// 			if (num < (int)num2)
// 			{
// 				num += num;
// 				num++;
// 			}
// 			networkRequest += networkRequest;
// 			throw new NullReferenceException();
// 		}
//
// 		private void DisposeRequest()
// 		{
// 			/*
// An exception occurred when decompiling this method (06002FCA)
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Network.Api.Client.BaseApiClient::DisposeRequest()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	IL_09:
// 	stfld:INetworkRequest(BaseApiClient::_networkRequest, ldloc:BaseApiClient(this), conv.u8:uint64[exp:INetworkRequest](ldc.i8:int64[exp:uint64](0)))
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
// 		protected BaseApiClient()
// 		{
// 			this.LogRequest();
// 		}
//
// 		private const string HeaderContentTypeKey = "Content-Type";
//
// 		private const string HeaderContentTypeValue = "application/json; charset=UTF-8";
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x10")]
// 		private int _retryCount;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x18")]
// 		protected INetworkRequest _networkRequest;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x20")]
// 		protected string _url;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x28")]
// 		protected string _accessToken;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x30")]
// 		protected string _appVersion;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x38")]
// 		protected DeviceType _deviceType;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x40")]
// 		protected byte[] _requestBytes;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x48")]
// 		protected Action<ApiResponseBase> _callbackSuccess;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x50")]
// 		protected Action<ApiErrorResponse> _callbackError;
//
// 		[Cpp2IlInjected.FieldOffset(Offset = "0x58")]
// 		protected bool _isCheckVersion;
// 	}
// }
