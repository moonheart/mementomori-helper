using System.Runtime.CompilerServices;
using Grpc.Net.Client;
using MagicOnion;
using MagicOnion.Client;
using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Network.MagicOnion.Interface;

namespace MementoMori.Ortega.Network.MagicOnion.Client
{
	public abstract class MagicOnionClient<TSender, TReceiver> : BaseMagicOnionClient where TSender : IStreamingHub<TSender, TReceiver>
	{
		public MagicOnionClient(GrpcChannel channel, long playerId, string authToken): base(playerId, authToken)
        {
            this._channel = channel;
        }

		public override bool IsExistHubClient()
        {
            return _sender != null;
        }

		public override async Task DisposeAsync()
		{
            if (_sender == null) return;
            await _sender.DisposeAsync();
        }

		protected void AttachInternalReceiver(TReceiver internalReceiver, IDisconnectReceiver internalDisconnectReceiver)
		{
			_internalReceiver = internalReceiver;
            _internalDisconnectReceiver = internalDisconnectReceiver;
		}

		protected override void ConnectHub()
        {
            _sender = StreamingHubClient.ConnectAsync<TSender, TReceiver>(_channel, _internalReceiver).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		protected override void Authenticate()
		{
			base.ChangeState(HubClientState.Authenticating);
		}

		protected override void SucceededAuthentication()
		{
			base.ResetRetryCount();
			base.ChangeState(HubClientState.Ready);
		}

		protected override void FailedAuthentication()
		{
			base.ChangeState(HubClientState.FailedAuthentication);
		}

		protected override void WatchDisconnect()
		{
            _sender.WaitForDisconnect().ConfigureAwait(false).GetAwaiter().GetResult();
		}

		protected GrpcChannel _channel;

		protected TSender _sender;

		protected TReceiver _internalReceiver;

		protected IDisconnectReceiver _internalDisconnectReceiver;
	}
}
