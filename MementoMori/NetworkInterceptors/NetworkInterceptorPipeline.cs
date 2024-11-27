using MementoMori.Ortega.Share.Data.ApiInterface;

namespace MementoMori.NetworkInterceptors;

public class NetworkInterceptorPipeline
{
    private readonly List<INetworkInterceptor> _interceptors = new();

    public NetworkInterceptorPipeline Use(INetworkInterceptor interceptor)
    {
        _interceptors.Add(interceptor);
        return this;
    }

    public async Task<TResp> ExecuteAsync<TReq, TResp>(TReq req, Func<TReq, Task<TResp>> finalHandler)
        where TReq : ApiRequestBase
        where TResp : ApiResponseBase
    {
        var handler = finalHandler;

        foreach (var interceptor in _interceptors.AsReadOnly().Reverse())
        {
            var next = handler; // 这里保存当前的 Next
            handler = async r => await interceptor.InterceptAsync(r, req1 => next(req1));
        }

        return await handler(req);
    }
}