using System.Text.Json.Serialization;

namespace ErrorHandler.Exceptions;

public interface IInternalServerErrorException
{
    [JsonPropertyName("field")] public string Field { get; }
    [JsonPropertyName("message")] public string Message { get; }
    [JsonPropertyName("description")] public string Description { get; }
}

public class InternalServerErrorException : CustomException, IInternalServerErrorException
{
    public override int StatusCode => 500;

    [JsonPropertyName("field")] public string Field { get; }
    [JsonPropertyName("message")] public override string Message { get; }
    [JsonPropertyName("description")] public string Description { get; }

    public InternalServerErrorException() : base() { }

    public InternalServerErrorException(string message)
    {
        Message = message;
    }

    public InternalServerErrorException(string message, string description) : this(message)
    {
        Description = description;
    }

    public InternalServerErrorException(string field, string message, string description) : this(message, description)
    {
        Field = field;
    }
}
