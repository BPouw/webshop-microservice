using System.Text;
using System.Text.Json;
using Domain;
using Domain.Service.IRepository;
using Infrastructure.RabbitMQ.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Webshop.Consumers;

public class StockConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public StockConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = "rabbitmq"
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "stock_product",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            StockProductMessage? stockMessage = JsonSerializer.Deserialize<StockProductMessage>(message);

            Console.WriteLine($"Received message: {message}");

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IProductRepository _productRepository =
                    scope.ServiceProvider.GetRequiredService<IProductRepository>();

                Product product = await _productRepository.getProductByName(stockMessage.ProductName);

                product.Stock += stockMessage.ReceivedStock;

                await _productRepository.updateProduct(product);
            }

            await Task.Yield();
        };

        channel.BasicConsume(queue: "stock_product",
            autoAck: true,
            consumer: consumer);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken); // Wait for new messages
        }

    }
}