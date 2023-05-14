using Domain;
using Domain.Service.IRepository;
using Infrastructure.MySQL;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;

namespace Infrastructure;

public class CustomerRepository : ICustomerRepository
{
    private readonly WebshopDbContext _context;
    
    public CustomerRepository(WebshopDbContext context)
    {
        _context = context;
    }

    public async Task<Customer> getCustomerById(int id)
    {
        return await _context.Customer.Where(x => x.ID == id).Include(x => x.Address).FirstOrDefaultAsync();
    }
}