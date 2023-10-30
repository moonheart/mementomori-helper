using MementoMori.Ortega.Common.Enums;

namespace MementoMori.Ortega.Network.MagicOnion.Client
{
	public abstract class BaseMagicOnionClient
	{
		public BaseMagicOnionClient(long playerId, string authToken)
		{
			this._playerId = playerId;
			this._authToken = authToken;
			this._state = HubClientState.Idle;
		}

		public abstract Task DisposeAsync();

		public abstract bool IsExistHubClient();

		protected abstract Task ConnectHub();

		protected abstract Task Authenticate();

		protected abstract void SucceededAuthentication();

		protected abstract void FailedAuthentication();

		protected abstract void WatchDisconnect();

		public void RefreshAuthToken(string authToken)
        {
            _authToken = authToken;
		}

		public HubClientState GetState()
		{
			return this._state;
		}

		public bool IsNeedToReconnect()
        {
            return _state == HubClientState.Disconnected;
		}

		public async Task Connect()
		{
			if (this._state != HubClientState.Ready)
            {
                await ConnectHub();
				await Authenticate();
			}
		}

		public int GetRetryCount()
		{
			return this._retryCount;
		}

		public void ResetRetryCount()
		{
			this._retryCount = (int)((ulong)0L);
		}

		public void TryReconnect()
		{
			if (this._state <= HubClientState.Connecting && this._state != HubClientState.Ready)
			{
				HubClientState state = this._state;
				if (state != HubClientState.Idle)
				{
					if (state == HubClientState.FailedAuthentication)
					{
						this.Authenticate();
						return;
					}
					if (state != HubClientState.Disconnected)
					{
						return;
					}
				}
				this.ConnectHub();
			}
		}

		public bool IsReady()
		{
			return this._state == HubClientState.Ready;
		}

		protected void ChangeState(HubClientState state)
		{
			this._state = state;
			string text = string.Format("{0} State", "{0} State");
		}

		protected void Log(string message)
		{
			// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
		}

		protected void LogRequest(string message)
		{
			// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
		}

		protected void LogResponse(string message)
		{
			// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
		}

		private void AddRetryCount()
		{
		}

		protected long _playerId;

		protected string _authToken;

		protected HubClientState _state;

		private int _retryCount;
	}
}
