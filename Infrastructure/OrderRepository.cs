using Domain;
using Domain.Service.IRepository;
using Infrastructure.Mongo;
using MongoDB.Driver;
using Infrastructure.MySQL;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public class OrderRepository : IOrderRepository
{

    private readonly WebshopDbContext _context;
    private readonly IMongoCollection<OrderDocument> _orderCollection;

    public OrderRepository(WebshopDbContext dbContext, IOptions<WebshopDatabaseSettings> webshopDBSettings)
    {
        this._context = dbContext;

        MongoClient client = new MongoClient(webshopDBSettings.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(webshopDBSettings.Value.DatabaseName);

        _orderCollection = database.GetCollection<OrderDocument>(webshopDBSettings.Value.CollectionName);
    }
    
    public async Task<int> CreateOrder(Order order)
    {
        _context.Order.Add(order);
        await _context.SaveChangesAsync();
        return order.ID;
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

    public async Task CreateOrderDocument(OrderDocument orderDocument)
    {
        await _orderCollection.InsertOneAsync(orderDocument);
    }

    public async Task<OrderDocument?> GetOrderDocument(string orderUuid)
    {
        return await _orderCollection.Find(x => x.OrderId == orderUuid).FirstOrDefaultAsync();
    }
}