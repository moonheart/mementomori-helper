using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface;

namespace MementoMori.Exceptions;

public class ApiErrorException: Exception
{
    public ErrorCode ErrorCode { get; }
    public ApiErrorException(ErrorCode errorCode): base(Masters.TextResourceTable.GetErrorCodeMessage(errorCode))
    {
        ErrorCode = errorCode;

    }
}