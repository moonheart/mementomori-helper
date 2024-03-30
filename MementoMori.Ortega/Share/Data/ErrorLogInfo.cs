using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data
{
	[MessagePackObject(true)]
	public class ErrorLogInfo
	{
		public string ApiName { get; set; }

		public long ErrorCode { get; set; }

		public ErrorLogType ErrorLogType { get; set; }

		public long LocalTimeStamp { get; set; }

		public string Message { get; set; }
	}
}
