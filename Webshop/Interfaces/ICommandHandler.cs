namespace Webshop.Interfaces;

public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{ 
    Task<TResult> Handle(TCommand command, CancellationToken cancellationToken);
}