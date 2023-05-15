namespace Domain.Service.IRepository;

public interface IProductRepository
{
    Task<Product> getProductById(int id);
    Task updateProduct(Product product);
    Task<Product> getProductByName(string name);
}