namespace Domain.Service.IService;

public interface IProductService
{
    int updateStock(Product product);
    bool hasEnoughStock(Product product);
    bool hasToOrderMoreStock(Product product);
}