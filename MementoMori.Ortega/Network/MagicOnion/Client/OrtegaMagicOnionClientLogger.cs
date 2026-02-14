using MagicOnion.Client;

namespace MementoMori.Ortega.Network.MagicOnion.Client
{
	/// <summary>
	/// MagicOnion 客户端日志记录器
	/// </summary>
	public class OrtegaMagicOnionClientLogger : IMagicOnionClientLogger
	{
		public void Error(Exception ex, string message)
		{
			// 日志记录逻辑可在此实现
		}

		public void Information(string message)
		{
			// 日志记录逻辑可在此实现
		}

		public void Debug(string message)
		{
			// 日志记录逻辑可在此实现
		}

		public void Trace(string message)
		{
			// 日志记录逻辑可在此实现
		}
	}
}
