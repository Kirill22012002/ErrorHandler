namespace ErrorHandler.Exceptions;

public interface INotFoundException
{
}

public class NotFoundException : CustomException, INotFoundException
{
    public override int StatusCode => 404;

    public NotFoundException() : base() { }
}
