using Domain.Service.IService;

namespace Domain.Service.Service;

public class OrderService : IOrderService
{
    public bool isValidOrder(Order order, List<Product> products)
    {
        return products.Count <= 20;
    }
}