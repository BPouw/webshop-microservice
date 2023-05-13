using Domain;
using Microsoft.AspNetCore.Mvc;
using Webshop.Commands;
using Webshop.Dto;
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
    public async Task<IActionResult> CreateOrder(OrderDto order)
    {
        var command = new CreateOrderCommand()
        {
            CustomerId = order.customer_id,
            Psp = order.psp,
            ProductIds = order.product_ids
        };

        Order resultOrder = await _createOrderCommandHandler.Handle(command, CancellationToken.None);

        return Created("Succes", resultOrder);
    }

}