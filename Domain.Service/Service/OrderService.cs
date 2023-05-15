using Domain.Service.IService;

namespace Domain.Service.Service;

public class OrderService : IOrderService
{
    public bool isValidOrder(Order order)
    {
        return order.OrderProducts.Count <= 20;
    }
}