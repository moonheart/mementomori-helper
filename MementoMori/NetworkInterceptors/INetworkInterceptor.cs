using MementoMori.Ortega.Share.Data.ApiInterface;

namespace MementoMori.NetworkInterceptors;

public interface INetworkInterceptor
{
    Task<TResp> InterceptAsync<TReq, TResp>(TReq req, Func<TReq, Task<TResp>> next)
        where TReq : ApiRequestBase
        where TResp : ApiResponseBase;
}