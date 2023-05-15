using Domain;
using Domain.Service.IRepository;
using Infrastructure.MySQL;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Infrastructure;

public class ProductRepository : IProductRepository
{
    private readonly WebshopDbContext _context;
    public ProductRepository(WebshopDbContext context)
    {
        _context = context;
    }

    public async Task<Product> getProductById(int id)
    {
        return await _context.Product.Where(x => x.ID == id).FirstOrDefaultAsync();
    }

    public async Task updateProduct(Product product)
    {
        _context.Product.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task<Product> getProductByName(string name)
    {
        return await _context.Product.Where(x => x.Name == name).FirstOrDefaultAsync();
    }

    public async Task<List<Product>> getProducts()
    {
        return await _context.Product.ToListAsync();
    }
    
}