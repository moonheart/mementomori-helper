using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
    [OrtegaApi("friend/useFriendCode", true, false)]
    [MessagePackObject(true)]
    public class UseFriendCodeRequest : ApiRequestBase
    {
        public string FriendCode { get; set; }
    }
}