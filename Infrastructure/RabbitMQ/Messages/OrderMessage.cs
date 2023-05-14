namespace Infrastructure.RabbitMQ.Messages;

public class OrderMessage
{
    public string OrderUuid { get; set; }

    public string Psp { get; set; }
    public CustomerMessage Customer { get; set; }
    public List<ProductMessage> Products { get; set; }
}

public class CustomerMessage
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Postalcode { get; set; }
    public string Country { get; set; }
}

public class ProductMessage
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}