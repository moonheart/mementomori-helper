using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Response
{
	[MessagePackObject(false)]
	public class OnErrorResponse : IErrorResponse
	{
		[Key(0)]
		public ErrorHandlingType ErrorHandlingType { get; set; }

		[Key(1)]
		public long ErrorMessageId { get; set; }

		[Key(2)]
		public string[] MessageParams { get; set; }
	}
}
