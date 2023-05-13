using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using RabbitMQ.Client;

namespace Infrastructure.RabbitMQ;

public class RabbitMQProducer : IRabbitMQProducer
{
    private IConnection Connect()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        return factory.CreateConnection();
    }
    
    public void SendOrderMessage<T>(T message)
    {
        var connection = Connect();

        var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "order_exchange", ExchangeType.Topic);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        channel.QueueDeclare("order_warehouse", exclusive: false);
        channel.QueueDeclare("order_notification", exclusive: false);
        
        channel.QueueBind(exchange:"order_exchange", queue:"order_warehouse", routingKey:"order");
        channel.QueueBind(exchange:"order_exchange", queue:"order_notification", routingKey:"order");
        
        channel.BasicPublish(exchange: "order_exchange", routingKey: "order", body: body);

    }
}