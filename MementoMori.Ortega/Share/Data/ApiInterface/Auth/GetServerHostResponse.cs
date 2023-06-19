using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Auth
{
    [MessagePackObject(true)]
    public class GetServerHostResponse : ApiResponseBase
    {
        public string ApiHost { get; set; }

        public string MagicOnionHost { get; set; }

        public int MagicOnionPort { get; set; }

        public GetServerHostResponse()
        {
        }
    }
}