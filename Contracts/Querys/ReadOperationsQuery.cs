namespace Contracts.Querys
{
    public record ReadOperationsQuery() : IQuery<IEnumerable<OperationQueryResponse>>;
}