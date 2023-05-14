namespace Domain.Service.IRepository;

public interface IProductRepository
{
    Task<Product> getProductById(int id);
}