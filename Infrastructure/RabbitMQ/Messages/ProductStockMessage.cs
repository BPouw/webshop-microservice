namespace Infrastructure.RabbitMQ.Messages;

public class ProductStockMessage
{
    public string ProductName { get; set; }
    
    public int CurrentStock { get; set; }
}