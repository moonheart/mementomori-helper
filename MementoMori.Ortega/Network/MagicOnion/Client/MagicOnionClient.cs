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
            await _sender.WaitForDisconnect();
        }

		protected void AttachInternalReceiver(TReceiver internalReceiver, IDisconnectReceiver internalDisconnectReceiver)
		{
			_internalReceiver = internalReceiver;
            _internalDisconnectReceiver = internalDisconnectReceiver;
		}

		protected override async Task ConnectHub()
        {
            ChangeState(HubClientState.Connecting);
            _sender = await StreamingHubClient.ConnectAsync<TSender, TReceiver>(_channel, _internalReceiver);
        }

		protected override Task Authenticate()
		{
            base.ChangeState(HubClientState.Authenticating);
            return Task.CompletedTask;
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
