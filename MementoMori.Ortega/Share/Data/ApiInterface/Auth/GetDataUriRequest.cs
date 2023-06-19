using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Auth
{
    [OrtegaAuth("auth/getDataUri", false, true)]
    [MessagePackObject(true)]
    public class GetDataUriRequest : ApiRequestBase
    {
        public string CountryCode { get; set; }

        public long UserId { get; set; }

        public GetDataUriRequest()
        {
        }
    }
}