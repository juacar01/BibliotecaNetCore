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
            throw new Exception("Unhandled exception occurred", ex);
        }

        return await next();
    }
}
