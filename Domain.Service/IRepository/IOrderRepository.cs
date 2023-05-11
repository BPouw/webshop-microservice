namespace Domain.Service.IRepository;

using Domain;

public interface IOrderRepository
{
    Task CreateOrder(Order order);
    Order GetOrder(string OrderUuid);
    IEnumerable<Order> GetAllOrders();
    Task DeleteOrder(Order order);
}