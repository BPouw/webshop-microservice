using System.Windows.Input;
using Webshop.Dto;
using Webshop.Interfaces;

namespace Webshop.Commands;

public class CreateProductCommand : ICommand<ProductDto>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int MerchantId { get; set; }
}