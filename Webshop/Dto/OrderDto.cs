namespace Webshop.Dto;

using Domain;

public class OrderDto
{
    public PSP psp { get; set; }
    
    public int customer_id { get; set; }
    
    public ICollection<int> product_ids { get; set; }
}