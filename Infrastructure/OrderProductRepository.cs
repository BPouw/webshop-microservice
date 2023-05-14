using Domain;
using Domain.Service.IRepository;
using Infrastructure.MySQL;

namespace Infrastructure;

public class OrderProductRepository : IOrderProductRepository
{
    private readonly WebshopDbContext _context;

    public OrderProductRepository(WebshopDbContext context)
    {
        _context = context;
    }

    public async Task<int> createOrderProduct(int orderId, int productId)
    {
        OrderProduct op = new OrderProduct()
        {
            OrderId = orderId,
            ProductId = productId
        };

        _context.Order_Product.Add(op);
        await _context.SaveChangesAsync();

        return op.ID;
    }
}