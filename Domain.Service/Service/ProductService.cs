using Domain.Service.IService;

namespace Domain.Service.Service;

public class ProductService : IProductService
{
    public int updateStock(Product product)
    {
        return product.Stock -= 1;
    }

    public bool hasEnoughStock(Product product)
    {
        return product.Stock >= 1;
    }

    public bool hasToOrderMoreStock(Product product)
    {
        return product.Stock < 5;
    }
}