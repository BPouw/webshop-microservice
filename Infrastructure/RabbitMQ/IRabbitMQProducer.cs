namespace Infrastructure.RabbitMQ;

public interface IRabbitMQProducer
{
    public void SendOrderMessage<T>(T message);
}