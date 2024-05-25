using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Auth
{
    [MessagePackObject(true)]
    [OrtegaAuth("auth/createUser", false, false)]
    public class CreateUserRequest : ApiRequestBase, IHasSteamTicketApiRequest
    {
        public string AdverisementId { get; set; }

        public string AppVersion { get; set; }

        public string CountryCode { get; set; }

        public string DeviceToken { get; set; }

        public LanguageType DisplayLanguage { get; set; }

        public string ModelName { get; set; }

        public string OSVersion { get; set; }

        public string SteamTicket { get; set; }

        public int AuthToken { get; set; }
    }
}