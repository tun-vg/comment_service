using comment_service.Common.Interfaces;

namespace comment_service.Dispatcher;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TResult> Send<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IQueryHandler<,>)
            .MakeGenericType(query.GetType(), typeof(TResult));

        dynamic? handler = _serviceProvider.GetService(handlerType)
                           ?? throw new InvalidOperationException($"Handler for {query.GetType().Name} not found!");
        
        return handler.Handle((dynamic)query, cancellationToken);
    }
}