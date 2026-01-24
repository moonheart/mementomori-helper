using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MementoMori.Ortega.Share.Data.ApiInterface;

namespace MementoMori.Api.Services
{
    /// <summary>
    /// Ortega API 发现服务
    /// 在启动时扫描所有标记了 [OrtegaApi] 特性的 Request 类型，建立路由映射
    /// </summary>
    public class OrtegaApiDiscoveryService
    {
        private readonly Dictionary<string, OrtegaApiInfo> _apiMap = new();
        
        public OrtegaApiDiscoveryService()
        {
            DiscoverApis();
        }

        /// <summary>
        /// 扫描并注册所有 Ortega API
        /// </summary>
        private void DiscoverApis()
        {
            var ortegaAssembly = typeof(ApiRequestBase).Assembly;
            var requestTypes = ortegaAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(ApiRequestBase)));

            foreach (var requestType in requestTypes)
            {
                var ortegaApiAttr = requestType.GetCustomAttribute<OrtegaApiAttribute>();
                var ortegaAuthAttr = requestType.GetCustomAttribute<OrtegaAuthAttribute>();
                
                if (ortegaApiAttr != null)
                {
                    RegisterApi(ortegaApiAttr.Uri, requestType, ortegaApiAttr.IsRequiredLogin, ortegaApiAttr.IsIgnoreMaintenance);
                }
                else if (ortegaAuthAttr != null)
                {
                    RegisterApi(ortegaAuthAttr.Uri, requestType, ortegaAuthAttr.IsRequiredLogin, ortegaAuthAttr.IsIgnoreMaintenance);
                }
            }

            Console.WriteLine($"[OrtegaApiDiscovery] Discovered {_apiMap.Count} API endpoints");
        }

        /// <summary>
        /// 注册单个 API
        /// </summary>
        private void RegisterApi(string uri, Type requestType, bool isRequiredLogin, bool isIgnoreMaintenance)
        {
            // 查找对应的 Response 类型
            var responseTypeName = requestType.Name.Replace("Request", "Response");
            var responseType = requestType.Assembly.GetTypes()
                .FirstOrDefault(t => t.Name == responseTypeName && t.IsSubclassOf(typeof(ApiResponseBase)));

            if (responseType == null)
            {
                Console.WriteLine($"[OrtegaApiDiscovery] Warning: No response type found for {requestType.Name}");
                return;
            }

            var apiInfo = new OrtegaApiInfo
            {
                Uri = uri,
                RequestType = requestType,
                ResponseType = responseType,
                IsRequiredLogin = isRequiredLogin,
                IsIgnoreMaintenance = isIgnoreMaintenance
            };

            _apiMap[uri] = apiInfo;
        }

        /// <summary>
        /// 根据 URI 获取 API 信息
        /// </summary>
        public OrtegaApiInfo? GetApiInfo(string uri)
        {
            return _apiMap.TryGetValue(uri, out var apiInfo) ? apiInfo : null;
        }

        /// <summary>
        /// 获取所有已注册的 API
        /// </summary>
        public IReadOnlyDictionary<string, OrtegaApiInfo> GetAllApis()
        {
            return _apiMap;
        }

        public List<string> DebugDiscovery()
        {
            var logs = new List<string>();
            try 
            {
                var ortegaAssembly = typeof(ApiRequestBase).Assembly;
                logs.Add($"Assembly: {ortegaAssembly.FullName}");
                
                var allTypes = ortegaAssembly.GetTypes();
                logs.Add($"Total types in assembly: {allTypes.Length}");

                var requestTypes = allTypes
                    .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(ApiRequestBase)))
                    .ToList();
                
                logs.Add($"Found {requestTypes.Count} subclasses of ApiRequestBase");

                foreach (var requestType in requestTypes)
                {
                    var ortegaApiAttr = requestType.GetCustomAttribute<OrtegaApiAttribute>();
                    if (ortegaApiAttr == null)
                    {
                        var ortegaAuthAttr = requestType.GetCustomAttribute<OrtegaAuthAttribute>();
                         if (ortegaAuthAttr == null)
                         {
                            logs.Add($"[Skip] {requestType.Name}: No OrtegaApi/OrtegaAuth attribute");
                            continue;
                         }
                    }

                    var uri = ortegaApiAttr?.Uri;
                    logs.Add($"Processing {requestType.Name}, URI: {uri}");

                    var responseTypeName = requestType.Name.Replace("Request", "Response");
                    var responseType = requestType.Assembly.GetTypes()
                        .FirstOrDefault(t => t.Name == responseTypeName && t.IsSubclassOf(typeof(ApiResponseBase)));

                    if (responseType == null)
                    {
                        logs.Add($"[Error] {requestType.Name}: Response type '{responseTypeName}' not found or not subclass of ApiResponseBase");
                        // Check if type exists but not subclass
                        var existingType = requestType.Assembly.GetTypes().FirstOrDefault(t => t.Name == responseTypeName);
                        if (existingType != null)
                        {
                             logs.Add($"[Info] Type '{responseTypeName}' exists but IsSubclassOf(ApiResponseBase) is {existingType.IsSubclassOf(typeof(ApiResponseBase))}");
                             logs.Add($"[Info] BaseType of '{responseTypeName}' is {existingType.BaseType?.Name}");
                        }
                    }
                    else
                    {
                        logs.Add($"[Success] Registered {requestType.Name} -> {responseType.Name} at {uri}");
                    }
                }
            }
            catch(Exception ex)
            {
                logs.Add($"Exception: {ex}");
            }
            return logs;
        }
    }

    /// <summary>
    /// Ortega API 信息
    /// </summary>
    public class OrtegaApiInfo
    {
        public string Uri { get; set; } = string.Empty;
        public Type RequestType { get; set; } = null!;
        public Type ResponseType { get; set; } = null!;
        public bool IsRequiredLogin { get; set; }
        public bool IsIgnoreMaintenance { get; set; }
    }
}
