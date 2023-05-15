namespace Infrastructure.RabbitMQ.Messages;

public class StockProductMessage
{
    public string ProductName { get; set; }
    public int ReceivedStock { get; set; }
}