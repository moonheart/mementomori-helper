using System;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface;

namespace MementoMori.Api.Infrastructure
{
    /// <summary>
    /// Ortega API 调用器
    /// 使用 NetworkManager 动态调用 Ortega API
    /// </summary>
    [RegisterScoped]
    [AutoConstructor]
    public partial class OrtegaInvoker
    {
        private readonly AccountManager _accountManager;

        /// <summary>
        /// 动态调用 Ortega API
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="requestJson">请求 JSON 字符串</param>
        /// <param name="requestType">请求类型</param>
        /// <param name="responseType">响应类型</param>
        /// <returns>响应对象</returns>
        public async Task<object> InvokeAsync(long userId, string requestJson, Type requestType, Type responseType)
        {
            // 获取账户上下文
            var accountContext = await _accountManager.GetOrCreateAsync(userId);
            if (accountContext == null)
            {
                throw new InvalidOperationException($"Account context not found for user {userId}");
            }

            // 反序列化请求对象
            var request = DeserializeRequest(requestJson, requestType);

            // 使用 NetworkManager 发送请求
            var response = await SendRequestDynamic(accountContext.NetworkManager, request, requestType, responseType);
            
            return response;
        }

        /// <summary>
        /// 反序列化请求
        /// </summary>
        private object DeserializeRequest(string json, Type requestType)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(json) || json == "{}")
                {
                    // 如果请求体为空，创建默认实例
                    return Activator.CreateInstance(requestType)!;
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                
                return JsonSerializer.Deserialize(json, requestType, options)!;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to deserialize request of type {requestType.Name}", ex);
            }
        }

        /// <summary>
        /// 动态调用 NetworkManager.SendRequest<TReq, TResp>
        /// </summary>
        private async Task<object> SendRequestDynamic(NetworkManager networkManager, object request, Type requestType, Type responseType)
        {
            try
            {
                // 获取 SendRequest 泛型方法
                var sendRequestMethod = typeof(NetworkManager)
                    .GetMethod("SendRequest", BindingFlags.Public | BindingFlags.Instance);

                if (sendRequestMethod == null)
                {
                    throw new InvalidOperationException("SendRequest method not found in NetworkManager");
                }

                // 创建泛型方法：SendRequest<TReq, TResp>
                var genericMethod = sendRequestMethod.MakeGenericMethod(requestType, responseType);

                // 调用方法，参数: (request, userDataCallback = null)
                var task = (Task)genericMethod.Invoke(networkManager, new[] { request, null })!;
                await task;

                // 获取 Task<T> 的结果
                var resultProperty = task.GetType().GetProperty("Result");
                if (resultProperty == null)
                {
                    throw new InvalidOperationException("Failed to get result from Task");
                }

                return resultProperty.GetValue(task)!;
            }
            catch (TargetInvocationException ex)
            {
                // 展开 TargetInvocationException，抛出真实的异常
                throw ex.InnerException ?? ex;
            }
            catch (Exception ex) when (ex is not NetworkManager.ApiErrorException)
            {
                throw new InvalidOperationException($"Failed to invoke SendRequest for {requestType.Name}", ex);
            }
        }
    }
}
