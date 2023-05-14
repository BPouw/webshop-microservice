using Domain;
using Domain.Service.IRepository;
using Webshop.Interfaces;
using Webshop.Queries;

namespace Webshop.Handlers;

public class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, OrderDocument>
{
    private readonly IOrderRepository _orderRepository;
    
    public GetOrderQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDocument> Handle(GetOrderQuery query, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetOrderDocument(query.OrderId);
    }
}