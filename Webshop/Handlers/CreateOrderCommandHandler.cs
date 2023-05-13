using Domain;
using Domain.Service.IRepository;
using Webshop.Commands;
using Webshop.Interfaces;

namespace Webshop.Handlers;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Order>
{
    private readonly IOrderRepository _orderRepository;
    
    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        this._orderRepository = orderRepository;
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

        return order;
    }
}