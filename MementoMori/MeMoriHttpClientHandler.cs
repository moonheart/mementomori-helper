using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;

namespace MementoMori;

public class MeMoriHttpClientHandler : HttpClientHandler
{
    private readonly Subject<string> _ortegaaccessToken = new();
    private readonly Subject<string> _ortegaMasterVersion = new();
    private readonly Subject<string> _ortegaAssetVersion = new();
    public IObservable<string> OrtegaAccessToken => _ortegaaccessToken;
    public IObservable<string> OrtegaMasterVersion => _ortegaMasterVersion;
    public IObservable<string> OrtegaAssetVersion => _ortegaAssetVersion;

    public MeMoriHttpClientHandler(Dictionary<string, string> headers)
    {
        foreach (var (key, value) in headers)
        {
            _managedHeaders[key] = value;
        }
    }

    private readonly Dictionary<string, string> _managedHeaders = new();
    private readonly SemaphoreSlim _semaphoreSlim = new(1);
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        await _semaphoreSlim.WaitAsync(cancellationToken);
        try
        {
            foreach (var pair in _managedHeaders)
            {
                request.Headers.Add(pair.Key, pair.Value);
            }

            var response = await base.SendAsync(request, cancellationToken);
            if (response.Headers.TryGetValues("orteganextaccesstoken", out var headers))
            {
                _managedHeaders["ortegaaccesstoken"] = headers.FirstOrDefault() ?? "";
                _ortegaaccessToken.OnNext(_managedHeaders["ortegaaccesstoken"]);
            }
            if (response.Headers.TryGetValues("ortegamasterversion", out var headers1))
            {
                var masterVersion = headers1.FirstOrDefault() ?? "";
                _ortegaMasterVersion.OnNext(masterVersion);
            }
            if (response.Headers.TryGetValues("ortegaassetversion", out var headers2))
            {
                var assetVersion = headers2.FirstOrDefault() ?? "";
                _ortegaAssetVersion.OnNext(assetVersion);
            }

            return response;
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }
}