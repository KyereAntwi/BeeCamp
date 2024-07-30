namespace BeeCamp.Shared.Utilities.CQRS;

public interface ICommand : ICommand<Unit>;

public interface ICommand<out TResult> : IRequest<TResult> where TResult : notnull;

public interface ICommandHandler<in TCommand, TResponse> 
    : IRequestHandler<TCommand, TResponse> 
    where TCommand : ICommand<TResponse>
    where TResponse : notnull;