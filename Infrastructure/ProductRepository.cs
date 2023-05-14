using Domain;
using Domain.Service.IRepository;
using Infrastructure.MySQL;
using Microsoft.EntityFrameworkCore;

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
}