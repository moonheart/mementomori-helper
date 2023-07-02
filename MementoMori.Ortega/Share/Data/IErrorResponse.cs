using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data
{
	public interface IErrorResponse
	{
		ErrorHandlingType ErrorHandlingType { get; set; }

		long ErrorMessageId { get; set; }

		string[] MessageParams { get; set; }
	}
}
