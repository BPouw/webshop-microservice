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
            HostName = "rabbitmq"
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

        channel.QueueDeclare("order_warehouse", durable: true , autoDelete: false, exclusive: false);
        channel.QueueDeclare("order_notification", durable: true, autoDelete: false, exclusive: false);
        channel.QueueDeclare("order_internal", durable: true, autoDelete: false, exclusive: false);

        channel.QueueBind(exchange: "order_exchange", queue: "order_warehouse", routingKey: "order");
        channel.QueueBind(exchange: "order_exchange", queue: "order_notification", routingKey: "order");
        channel.QueueBind(exchange: "order_exchange", queue: "order_internal", routingKey: "order");

        channel.BasicPublish(exchange: "order_exchange", routingKey: "order", body: body);

    }

    public void SendProductStockOrder<T>(T message)
    {
        var connection = Connect();

        var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "order_exchange", ExchangeType.Topic);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        channel.QueueDeclare("product_stock", durable: true, autoDelete: false, exclusive: false);

        channel.QueueBind(exchange: "order_exchange", queue: "product_stock", routingKey: "product");

        channel.BasicPublish(exchange: "order_exchange", routingKey: "product", body: body);
    }

    public void OrderPaid<T>(T message)
    {
        var connection = Connect();

        var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "payment_exchange", ExchangeType.Direct);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        channel.QueueDeclare("payments", durable: true, autoDelete: false, exclusive: false);

        channel.QueueBind(exchange: "payment_exchange", queue: "payments", routingKey: "payments");

        channel.BasicPublish(exchange: "payment_exchange", routingKey: "payments", body: body);
    }
}