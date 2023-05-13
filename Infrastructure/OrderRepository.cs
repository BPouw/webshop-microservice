using Domain;
using Domain.Service.IRepository;

namespace Infrastructure;

public class OrderRepository : IOrderRepository
{

    private readonly WebshopDbContext _context;
    
    public OrderRepository(WebshopDbContext dbContext)
    {
        this._context = dbContext;
    }
    
    public async Task CreateOrder(Order order)
    {
        _context.Order.Add(order);
        await _context.SaveChangesAsync();
    }

    public Order GetOrder(Guid orderUuid)
    {
        return _context.Order.Where(x => x.OrderId == orderUuid).FirstOrDefault();
    }

    public IEnumerable<Order> GetAllOrders()
    {
        return _context.Order;
    }
    
    public async Task DeleteOrder(Order order)
    {
        _context.Order.Remove(order);
        await _context.SaveChangesAsync();
    }
}