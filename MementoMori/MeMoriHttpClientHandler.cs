using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace MementoMori;

public class MeMoriHttpClientHandler : HttpClientHandler
{
    private readonly Subject<string> _ortegaaccessToken = new();
    public IObservable<string> OrtegaAccessToken => _ortegaaccessToken;

    public MeMoriHttpClientHandler(Dictionary<string, string> headers)
    {
        foreach (var (key, value) in headers)
        {
            _managedHeaders[key] = value;
        }
    }

    private readonly Dictionary<string, string> _managedHeaders = new();

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
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

        return response;
    }
}