namespace Domain.Service.IService;

public interface IOrderService
{
    bool isValidOrder(Order order);
}