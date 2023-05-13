namespace Domain.Service.IRepository;

using Domain;

public interface IOrderRepository
{
    Task CreateOrder(Order order);
    Order GetOrder(Guid orderUuid);
    IEnumerable<Order> GetAllOrders();
    Task DeleteOrder(Order order);
    Task CreateOrderDocument(OrderDocument orderDocument);
    Task<OrderDocument?> GetOrderDocument(Guid orderUuid);
}