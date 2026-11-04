namespace MementoMori.Ortega.Common.Enums
{
	public enum HubClientState
	{
		Idle,
		Connecting,
		Authenticating,
		ReAuthenticating,
		Ready,
		FailedAuthentication,
		FailedReAuthenticating,
		Disconnected
	}
}
