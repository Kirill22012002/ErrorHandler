using ErrorHandler.Exceptions;
using Microsoft.AspNetCore.Http;
using NLog;
using System.Net;

namespace ErrorHandler;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly NLog.ILogger _logger = LogManager.GetCurrentClassLogger();

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        string result;
        switch (exception)
        {
            case InternalServerErrorException e:
                response.StatusCode = e.StatusCode;
                result = System.Text.Json.JsonSerializer.Serialize((IInternalServerErrorException)e);
                _logger.Error(result);
                break;
            case NotFoundException e:
                response.StatusCode = e.StatusCode;
                result = System.Text.Json.JsonSerializer.Serialize((INotFoundException)e);
                _logger.Error(result);
                break;
            case BadRequestException e:
                response.StatusCode = e.StatusCode;
                result = System.Text.Json.JsonSerializer.Serialize((IBadRequestException)e);
                _logger.Error(result);
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result = System.Text.Json.JsonSerializer.Serialize(exception?.Message);
                _logger.Error(result);
                break;
        }

        await response.WriteAsync(result);
    }
}
