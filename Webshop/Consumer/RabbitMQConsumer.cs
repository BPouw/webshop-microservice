namespace Webshop.Consumer;

using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class RabbitMQConsumer : BackgroundService
{
    private readonly IConfiguration _configuration;

    public RabbitMQConsumer(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "order_notification",
            durable: false,
            exclusive: false,
            autoDelete: true,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Received message: {message}");

            // Do something with the message here, e.g. deserialize it and process it as needed

            await Task.Yield();
        };

        channel.BasicConsume(queue: "order_notification",
            autoAck: true,
            consumer: consumer);

        await Task.CompletedTask;
    }
}