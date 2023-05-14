using Domain;
using Webshop.Interfaces;

namespace Webshop.Queries;

public class GetOrderQuery : IQuery<OrderDocument>
{
    public string OrderId { get; set; }
}