using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Auth;

[MessagePackObject(true)]
[OrtegaAuth("auth/createWorldPlayer")]
public class CreateWorldPlayerRequest : ApiRequestBase, IHasSteamTicketApiRequest
{
    public long WorldId { get; set; }

    public string Comment { get; set; }

    public string Name { get; set; }

    public string SteamTicket { get; set; }
}