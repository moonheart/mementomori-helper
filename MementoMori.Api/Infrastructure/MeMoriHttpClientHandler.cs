namespace MementoMori.Api.Infrastructure;

/// <summary>
/// Memento Mori HTTP 客户端处理器
/// 管理 Ortega 协议的特殊 Header
/// </summary>
public class MeMoriHttpClientHandler : HttpClientHandler
{
    private readonly Dictionary<string, string> _managedHeaders = new();
    private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);

    public string OrtegaAccessToken { get; private set; } = string.Empty;
    public string OrtegaMasterVersion { get; private set; } = string.Empty;
    public string OrtegaAssetVersion { get; private set; } = string.Empty;

    public string AppVersion
    {
        get => _managedHeaders.GetValueOrDefault("ortegaappversion", string.Empty);
        set => _managedHeaders["ortegaappversion"] = value;
    }

    public MeMoriHttpClientHandler()
    {
        _managedHeaders["ortegaaccesstoken"] = "";
        _managedHeaders["ortegadevicetype"] = "2"; // Android
        _managedHeaders["ortegauuid"] = Guid.NewGuid().ToString("N");
        _managedHeaders["accept-encoding"] = "gzip";
        _managedHeaders["user-agent"] = "BestHTTP/2 v2.3.0";
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        await _semaphoreSlim.WaitAsync(cancellationToken);
        try
        {
            // 添加自定义 Headers
            foreach (var pair in _managedHeaders)
            {
                request.Headers.TryAddWithoutValidation(pair.Key, pair.Value);
            }

            var response = await base.SendAsync(request, cancellationToken);

            // 更新 Access Token
            if (response.Headers.TryGetValues("orteganextaccesstoken", out var accessTokenHeaders))
            {
                var token = accessTokenHeaders.FirstOrDefault();
                if (!string.IsNullOrEmpty(token))
                {
                    _managedHeaders["ortegaaccesstoken"] = token;
                    OrtegaAccessToken = token;
                }
            }

            // 更新 Master Version
            if (response.Headers.TryGetValues("ortegamasterversion", out var masterHeaders))
            {
                var version = masterHeaders.FirstOrDefault();
                if (!string.IsNullOrEmpty(version))
                {
                    OrtegaMasterVersion = version;
                }
            }

            // 更新 Asset Version
            if (response.Headers.TryGetValues("ortegaassetversion", out var assetHeaders))
            {
                var version = assetHeaders.FirstOrDefault();
                if (!string.IsNullOrEmpty(version))
                {
                    OrtegaAssetVersion = version;
                }
            }

            return response;
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _semaphoreSlim?.Dispose();
        }
        base.Dispose(disposing);
    }
}
