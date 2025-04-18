namespace MyShoppingList.Application.Abstractions;

public interface IHandler<in TRequest> where TRequest : IRequest
{
    Task HandleAsync(TRequest command, CancellationToken cancellationToken);
}

public interface IHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> HandleAsync(TRequest command, CancellationToken cancellationToken);
}
