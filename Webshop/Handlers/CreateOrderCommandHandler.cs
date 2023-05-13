using Domain;
using Domain.Service.IRepository;
using Infrastructure.RabbitMQ;
using Webshop.Commands;
using Webshop.Interfaces;

namespace Webshop.Handlers;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Order>
{
    private readonly IOrderRepository _orderRepository;
    private IRabbitMQProducer _rabbitMqProducer;
    
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

        await _orderRepository.CreateOrder(order);
        _rabbitMqProducer.SendOrderMessage(order);

        return order;
    }
}