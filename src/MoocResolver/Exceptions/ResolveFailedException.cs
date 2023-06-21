namespace MoocResolver.Exceptions;

public class ResolveFailedException : Exception
{
    public string ErrorCode { get; set; }

    public ResolveFailedException(string errorCode)
    {
        ErrorCode = errorCode;
    }

    public ResolveFailedException(string errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }
}