using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.User
{
    [OrtegaApi("user/getUserData", true, false)]
    [MessagePackObject(true)]
    public class GetUserDataRequest : ApiRequestBase
    {
        public GetUserDataRequest()
        {
        }
    }
}