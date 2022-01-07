namespace Contracts.Commands
{
    using MediatR;

    public interface ICommand<T> : IRequest<T> { }
}