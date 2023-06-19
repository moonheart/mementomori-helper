using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Auth
{
    [MessagePackObject(true)]
    [OrtegaAuth("auth/getServerHost", true, false)]
    public class GetServerHostRequest : ApiRequestBase
    {
        public long WorldId { get; set; }

        public GetServerHostRequest()
        {
        }
    }
}