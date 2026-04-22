using Biblioteca.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Biblioteca.Application.Beheaviours;

public class UnhandledExceptionBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger _logger;

    public UnhandledExceptionBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try { 
            return await next();
        } catch(Exception ex) {

            var requestName = typeof(TRequest).Name;
            _logger.LogError(ex, "Unhandled Exception for Request {Name} {@Request}", requestName, request);

            // Permitir que excepciones de negocio conocidas sean manejadas por el controlador/middleware
            if (ex is ConflictException)
                throw;
            if (ex is BadRequestException)
                throw;


            // Re-lanzar la excepción original para preservar stack trace y tipo
            throw;
        }
    }
}
