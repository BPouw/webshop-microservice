using Domain;
using Microsoft.AspNetCore.Mvc;
using Webshop.Commands;
using Webshop.Interfaces;

namespace Webshop.Controllers;


[ApiController]
[Route("api/")]
public class OrderController : ControllerBase
{
    private readonly ICommandHandler<CreateOrderCommand, Order> _createOrderCommandHandler;

    public OrderController(ICommandHandler<CreateOrderCommand, Order> createOrderCommandHandler)
    {
        _createOrderCommandHandler = createOrderCommandHandler;
    }

    [HttpPost("orders")]
    public async Task<IActionResult> CreateOrder(Order order)
    {
        var command = new CreateOrderCommand()
        {
            CustomerId = order.CustomerId,
            Psp = order.Psp,
            OrderProducts = order.OrderProducts
        };

        Order resultOrder = await _createOrderCommandHandler.Handle(command, CancellationToken.None);

        return Created("Succes", resultOrder);
    }

}