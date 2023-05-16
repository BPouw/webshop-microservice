namespace Webshop.Dto;

public class ProductDto
{
    public int? product_id { get; set; }
    public string name { get; set; }
    
    public string description { get; set; }
    
    public decimal price { get; set; }
    
    public int stock { get; set; }
    
    public int merchant_id { get; set; }
}