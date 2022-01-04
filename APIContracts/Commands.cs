namespace APIContracts
{
    using MediatR;

    public class Commands
    {

    }
    //Should contain all CQRS contracts
    public interface ICommand<T> : IRequest<T> { }

    //Cannot create a bill for a table that already have one
    public record OpenBill(int TableId) : ICommand<Bill>;
    public record AddItemToBill(int TableId);
    public record RemoveItemFromBill(int TableId);
    public record DeliverItemOnBill(int TableId, int ProductId);
    public record CloseBill(int TableId);


    public interface IQuery<T> : IRequest<T> { }

    public record ListBills() : IQuery<IEnumerable<Bill>>;
    public record GetBill(int TableId) : IQuery<Bill>;
    public record ListItemsOnBill(int TableId) : IQuery<IEnumerable<Product>>;

    public record Bill();
    public record Product(int ProductId, ItemType Type);
    public enum ItemType { Other, Beverage, Food, Snacks }
}