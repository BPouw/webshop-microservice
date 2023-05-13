using Domain;
using Webshop.Interfaces;

namespace Webshop.Commands;

public class CreateOrderCommand : ICommand<Order>
{
    public PSP Psp { get; set; }
    public int CustomerId { get; set; }
    public ICollection<int> ProductIds { get; set; }
}