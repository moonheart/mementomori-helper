// using Grpc.Net.Client;
// using MementoMori.Ortega.Network.MagicOnion.Client;
//
// namespace MementoMori.MagicOnion;
//
// public class MagicOnionManager
// {
//     // private MagicOnionLocalRaidListener _localRaidListener;
//
//     private GrpcChannel _channel;
//
//     private OrtegaMagicOnionClient _client;
//
//     private string _host;
//
//     private long _playerId;
//
//     private bool _isDisconnecting;
//
//     private Timer _reconnectTimer;
//
//     private Timer _keepAliveTimer;
//     
//     
//     public string AuthTokenOfMagicOnion { get; set; }
//     public OrtegaMagicOnionClient Client
//     {
//         get
//         {
//             return this._client;
//         }
//     }
//
//     public void Init()
//     {
//         
//     }
//
//     public void Setup(string host, int port)
//     {
//         if (_channel == null || !_host.Equals(host))
//         {
//             _host = host;
//             _channel = GrpcChannel.ForAddress(new Uri($"https://{host}:{port}"));
//         }
//
//         if (_client != null)
//         {
//             _client.SetupLocalRaid();
//         }
//     }
// }