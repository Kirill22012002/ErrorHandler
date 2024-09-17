namespace ErrorHandler.Exceptions;

public abstract class CustomException : Exception
{
    public abstract int StatusCode { get; }

    protected CustomException() : base() { }
}
