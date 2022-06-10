namespace MinimalControllers;

public interface IEndpoint
{
    internal static string HandleDefinitionName { get; } = "HandleAsync";
}

public interface IEndpoint<TResponse> : IEndpoint
{
    Task<TResponse> HandleAsync(CancellationToken cancellationToken);
}

public interface IEndpoint<T0, TResponse> : IEndpoint
{
    Task<TResponse> HandleAsync(T0 t0, CancellationToken cancellationToken);
}

public interface IEndpoint<T0, T1, TResponse> : IEndpoint
{
    Task<TResponse> HandleAsync(T0 t0, T1 t1, CancellationToken cancellationToken);
}
