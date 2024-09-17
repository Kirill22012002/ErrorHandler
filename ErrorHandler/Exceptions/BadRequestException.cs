using System.Text.Json.Serialization;

namespace ErrorHandler.Exceptions;

public interface IBadRequestException
{
    [JsonPropertyName("field")] public string Field { get; }
    [JsonPropertyName("message")] public string Message { get; }
}

public class BadRequestException : CustomException, IBadRequestException
{
    public override int StatusCode => 400;

    [JsonPropertyName("field")] public string Field { get; }
    [JsonPropertyName("message")] public override string Message { get; }

    public BadRequestException() : base() { }

    public BadRequestException(string field, string message)
    {
        Field = field;
        Message = message;
    }
}
