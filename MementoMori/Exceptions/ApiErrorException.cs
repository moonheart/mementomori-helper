namespace MementoMori.Exceptions;

public class ApiErrorException : Exception
{
    public ApiErrorException(ErrorCode errorCode) : base(TextResourceTable.GetErrorCodeMessage(errorCode))
    {
        ErrorCode = errorCode;
    }

    public ErrorCode ErrorCode { get; }
}