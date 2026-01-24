using System.Reflection;
using System.Text.RegularExpressions;
using MementoMori.Api.Infrastructure;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface;
using MementoMori.Ortega.Share.Data.ApiInterface.Auth;
using MementoMori.Ortega.Share.Data.Auth;
using MementoMori.Ortega.Share.Enums;
using MessagePack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MementoMori.Api.Services;

/// <summary>
/// 版本服务 - 负责探测和维护游戏版本信息
/// </summary>
public class VersionService
{
    private readonly ILogger<VersionService> _logger;
    private readonly IConfiguration _configuration;
    private readonly SemaphoreSlim _updateLock = new(1, 1);

    public string AppVersion { get; private set; } = "2.14.0";
    public string? MasterVersion { get; private set; }
    public string? MasterUriFormat { get; private set; }
    public string? AssetCatalogUriFormat { get; private set; }

    public VersionService(ILogger<VersionService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        
        // 从配置初始化
        AppVersion = _configuration["Auth:AppVersion"] ?? "2.14.0";
    }

    /// <summary>
    /// 刷新并探测最新版本
    /// </summary>
    public async Task RefreshVersionAsync()
    {
        await _updateLock.WaitAsync();
        try
        {
            _logger.LogInformation("Refreshing game version info...");

            // 1. 尝试从官网获取最新版本
            string? webVersion = await TryGetVersionFromWebAsync();
            if (webVersion != null)
            {
                AppVersion = webVersion;
                _logger.LogInformation("Found latest version from web: {Version}", AppVersion);
            }

            // 2. 验证版本并获取 MasterUriFormat
            await ProbeAndVerifyVersionAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to refresh version info");
        }
        finally
        {
            _updateLock.Release();
        }
    }

    private async Task<string?> TryGetVersionFromWebAsync()
    {
        try
        {
            using var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(10);
            var html = await httpClient.GetStringAsync("https://mememori-game.com/apps/vars.js");
            var match = Regex.Match(html, @"/apps/mementomori_(?<version>\d+\.\d+\.\d+)\.apk");
            
            if (match.Success && Version.TryParse(match.Groups["version"].Value, out var version))
            {
                return version.ToString();
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning("Failed to get version from official website: {Message}", ex.Message);
        }
        return null;
    }

    private async Task ProbeAndVerifyVersionAsync()
    {
        var authUrl = _configuration["Auth:AuthUrl"] ?? "https://prd1-auth.mememori-boi.com/api/";
        var baseUri = new Uri(authUrl);
        
        // 获取 GetDataUriRequest 的 API 路径
        var path = typeof(GetDataUriRequest).GetCustomAttribute<OrtegaAuthAttribute>()?.Uri ?? "auth/get-data-uri";
        var uri = new Uri(baseUri, path);

        var buildAddCount = 5;
        var minorAddCount = 5;
        var majorAddCount = 5;

        string currentTryVersion = AppVersion;

        while (true)
        {
            var handler = new MeMoriHttpClientHandler { AppVersion = currentTryVersion };
            using var client = new HttpClient(handler);
            client.Timeout = TimeSpan.FromSeconds(10);

            var request = new GetDataUriRequest { CountryCode = "CN" };
            var bytes = MessagePackSerializer.Serialize(request);

            try
            {
                using var response = await client.PostAsync(uri, new ByteArrayContent(bytes)
                {
                    Headers = { { "content-type", "application/json; charset=UTF-8" } }
                });

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"HTTP Error: {response.StatusCode}");
                }

                if (response.Headers.TryGetValues("ortegastatuscode", out var statusCodes))
                {
                    var statusCode = statusCodes.FirstOrDefault();
                    if (statusCode == "0")
                    {
                        // 成功！更新版本信息
                        AppVersion = currentTryVersion;
                        
                        if (response.Headers.TryGetValues("ortegamasterversion", out var masterVersions))
                        {
                            MasterVersion = masterVersions.FirstOrDefault();
                        }

                        await using var stream = await response.Content.ReadAsStreamAsync();
                        var dataUriResponse = MessagePackSerializer.Deserialize<GetDataUriResponse>(stream);
                        
                        MasterUriFormat = dataUriResponse.MasterUriFormat;
                        AssetCatalogUriFormat = dataUriResponse.AssetCatalogUriFormat;

                        _logger.LogInformation("Version verified. AppVersion: {AppVersion}, MasterVersion: {MasterVersion}", 
                            AppVersion, MasterVersion);
                        return;
                    }

                    await using var errorStream = await response.Content.ReadAsStreamAsync();
                    var error = MessagePackSerializer.Deserialize<ApiErrorResponse>(errorStream);

                    if (error.ErrorCode == ErrorCode.CommonRequireClientUpdate)
                    {
                        // 需要更新版本，尝试递增
                        var version = new Version(currentTryVersion);
                        if (buildAddCount > 0)
                        {
                            currentTryVersion = new Version(version.Major, version.Minor, version.Build + 1).ToString(3);
                            buildAddCount--;
                        }
                        else if (minorAddCount > 0)
                        {
                            currentTryVersion = new Version(version.Major, version.Minor + 1, 0).ToString(3);
                            minorAddCount--;
                            buildAddCount = 5;
                        }
                        else if (majorAddCount > 0)
                        {
                            currentTryVersion = new Version(version.Major + 1, 0, 0).ToString(3);
                            majorAddCount--;
                            buildAddCount = 5;
                            minorAddCount = 5;
                        }
                        else
                        {
                            throw new Exception("Reached max try count for version probing");
                        }
                        
                        _logger.LogInformation("Retrying with version {Version}...", currentTryVersion);
                        continue;
                    }

                    throw new Exception($"API Error: {error.ErrorCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during version probing for {Version}", currentTryVersion);
                throw;
            }
        }
    }
}
