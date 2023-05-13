using System.Transactions;
using Domain;
using Domain.Service.IRepository;
using Infrastructure.RabbitMQ;
using Webshop.Commands;
using Webshop.Interfaces;

namespace Webshop.Handlers;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Order>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IRabbitMQProducer _rabbitMqProducer;
    
    public CreateOrderCommandHandler(IOrderRepository orderRepository, IRabbitMQProducer rabbitMqProducer)
    {
        this._orderRepository = orderRepository;
        this._rabbitMqProducer = rabbitMqProducer;
    }
    
    public async Task<Order> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        Order order = new Order()
        {
            OrderId = Guid.NewGuid(),
            CustomerId = command.CustomerId,
            Psp = command.Psp,
            // OrderProducts = command.ProductIds
        };

        CustomerDocument customerDocument = new CustomerDocument()
        {
            City = "Breda",
            Country = "Netherlands",
            Email = "borispouw@hotmail.com",
            Name = "Boris Pouw",
            Postalcode = "4814AE",
            Street = "Tramsingel 93A"
        };

        ProductDocument productDocument = new ProductDocument()
        {
            Name = "Lawn Chair",
            Description = "Chill in the summer",
            Price = 10
        };

        OrderDocument orderDocument = new OrderDocument()
        {
            Products = new List<ProductDocument>() { productDocument },
            Customer = customerDocument,
            OrderId = order.OrderId,
            Psp = command.Psp.ToString()
        };

        await _orderRepository.CreateOrderDocument(orderDocument);
        await _orderRepository.CreateOrder(order);
        _rabbitMqProducer.SendOrderMessage(order);

        return order;
    }
}