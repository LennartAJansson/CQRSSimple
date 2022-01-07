namespace Contracts.Querys
{
    using MediatR;

    public interface IQuery<T> : IRequest<T> { }
}