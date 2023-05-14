namespace Domain.Service.IRepository;

public interface IOrderProductRepository
{
    Task<int> createOrderProduct(int orderId, int productId);
}