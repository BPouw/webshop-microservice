using Domain;
using Microsoft.AspNetCore.Mvc;
using Webshop.Commands;
using Webshop.Dto;
using Webshop.Interfaces;
using Webshop.Queries;

namespace Webshop.Controllers;


[ApiController]
[Route("api/")]
public class OrderController : ControllerBase
{
    private readonly ICommandHandler<CreateOrderCommand, OrderDto> _createOrderCommandHandler;
    private readonly IQueryHandler<GetOrderQuery, OrderDocument> _getOrderQueryHandler;

    public OrderController(ICommandHandler<CreateOrderCommand, OrderDto> createOrderCommandHandler, IQueryHandler<GetOrderQuery, OrderDocument> getOrderQueryHandler)
    {
        _createOrderCommandHandler = createOrderCommandHandler;
        _getOrderQueryHandler = getOrderQueryHandler;
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

        OrderDto resultOrder = await _createOrderCommandHandler.Handle(command, CancellationToken.None);

        return Created("Succes", resultOrder);
    }

    [HttpGet("orders/{id}")]
    public async Task<IActionResult> GetOrderById(string id)
    {
        var query = new GetOrderQuery()
        {
            OrderId = id
        };

        OrderDocument od = await _getOrderQueryHandler.Handle(query, CancellationToken.None);

        return Ok(od);
    }

}