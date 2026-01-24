using System.Security.Cryptography;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Master;
using MessagePack;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MementoMori.Api.Services;

/// <summary>
/// Master 数据同步服务 - 负责定时下载和同步游戏 Master 数据文件 (二进制)
/// </summary>
public class MasterDataService : BackgroundService
{
    private readonly ILogger<MasterDataService> _logger;
    private readonly VersionService _versionService;
    private readonly string _masterPath = "Master";
    private readonly HttpClient _unityHttpClient;

    public MasterDataService(ILogger<MasterDataService> logger, VersionService versionService)
    {
        _logger = logger;
        _versionService = versionService;
        
        _unityHttpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };
        _unityHttpClient.DefaultRequestHeaders.Add("User-Agent", "UnityPlayer/2021.3.10f1 (UnityWebRequest/1.0, libcurl/7.80.0-DEV)");
        _unityHttpClient.DefaultRequestHeaders.Add("X-Unity-Version", "2021.3.10f1");

        if (!Directory.Exists(_masterPath))
        {
            Directory.CreateDirectory(_masterPath);
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("MasterDataService is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // 同步 Master 数据 (内部会自动刷新版本)
                await SyncMasterDataAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while syncing master data");
            }

            // 3. 等待 1 小时
            _logger.LogInformation("Next sync in 1 hour...");
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }

    /// <summary>
    /// 同步 Master 数据文件
    /// </summary>
    public async Task SyncMasterDataAsync(CancellationToken ct = default)
    {
        try
        {
            // 确保版本信息是最新的
            await _versionService.RefreshVersionAsync();

            if (string.IsNullOrEmpty(_versionService.MasterVersion) || string.IsNullOrEmpty(_versionService.MasterUriFormat))
            {
                _logger.LogWarning("Cannot sync master data: MasterVersion or MasterUriFormat is missing.");
                return;
            }

            _logger.LogInformation("Starting Master data sync for version {Version}...", _versionService.MasterVersion);

            // 下载 master-catalog
            var catalogUrl = string.Format(_versionService.MasterUriFormat!, _versionService.MasterVersion, "master-catalog");
            var catalogBytes = await _unityHttpClient.GetByteArrayAsync(catalogUrl, ct);
            var catalog = MessagePackSerializer.Deserialize<MasterBookCatalog>(catalogBytes);

            int updatedCount = 0;
            int skippedCount = 0;

            // 允许下载的语言包后缀（可选，如果想精简可以加过滤）
            HashSet<string> allowedLangMb = new()
            {
                "TextResourceJaJpMB",
                "TextResourceZhTwMB",
                "TextResourceZhCnMB",
                "TextResourceEnUsMB",
                "TextResourceKoKrMB"
            };

            foreach (var (name, info) in catalog.MasterBookInfoMap)
            {
                // 过滤不需要的语言包
                if (name.StartsWith("TextResource") && !allowedLangMb.Contains(name))
                    continue;

                var localFile = Path.Combine(_masterPath, name);
                
                // 校验 MD5
                if (File.Exists(localFile))
                {
                    var localHash = await CalculateMd5Async(localFile);
                    if (localHash.Equals(info.Hash, StringComparison.OrdinalIgnoreCase))
                    {
                        skippedCount++;
                        continue;
                    }
                }

                // 下载更新
                _logger.LogDebug("Downloading {Name}...", name);
                var mbUrl = string.Format(_versionService.MasterUriFormat!, _versionService.MasterVersion, name);
                var mbBytes = await _unityHttpClient.GetByteArrayAsync(mbUrl, ct);
                
                await File.WriteAllBytesAsync(localFile, mbBytes, ct);
                updatedCount++;
            }

            _logger.LogInformation("Master sync completed. Updated: {Updated}, Skipped: {Skipped}", updatedCount, skippedCount);

            Masters.LoadAllMasters();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to sync master data from {Url}", _versionService.MasterUriFormat);
        }
    }

    private async Task<string> CalculateMd5Async(string path)
    {
        using var md5 = MD5.Create();
        using var stream = File.OpenRead(path);
        var hash = await md5.ComputeHashAsync(stream);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }
}
