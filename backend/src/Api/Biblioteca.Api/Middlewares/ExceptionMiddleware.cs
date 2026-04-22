using Biblioteca.Api.Errors;
using Biblioteca.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;

namespace Biblioteca.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred while processing request {Method} {Path}", context.Request.Method, context.Request.Path);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch(ex)
            {
                case NotFoundException notFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
                case FluentValidation.ValidationException validationException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    var errors = validationException.Errors.Select(e =>  e.ErrorMessage ).ToArray();
                    var validationJson = JsonConvert.SerializeObject(errors);
                    result=JsonConvert.SerializeObject(new CodeErrorException(statusCode, errors, validationJson));
                    break;

                case BadRequestException badRequestException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            if (string.IsNullOrEmpty(result))
            {
                result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, new string[] {ex.Message },ex.StackTrace));
            }

            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(result);
        }
    }


}
