namespace Domain.Service.IService;

public interface IOrderService
{
    bool isValidOrder(Order order, List<Product> products);
}