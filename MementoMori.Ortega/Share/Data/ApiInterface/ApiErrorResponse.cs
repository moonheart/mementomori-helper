using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface
{
	[MessagePackObject(true)]
	public class ApiErrorResponse : ApiResponseBase, IErrorResponse
	{
		public ErrorCode ErrorCode { get; set; }

		public string Message { get; set; }

		[Obsolete("ErrorCodeに移行します")]
		public ErrorHandlingType ErrorHandlingType { get; set; }

		[Obsolete("ErrorCodeに移行します")]
		public long ErrorMessageId { get; set; }

		[Obsolete("ErrorCodeに移行します")]
		public string[] MessageParams{ get; set; }
	}
}
