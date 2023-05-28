using System.Text.Json;
using Domain;
using Domain.Service.IRepository;
using Infrastructure.RabbitMQ.Messages;

namespace Webshop.Consumers;

using System;
using System.Text;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class OrderConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public OrderConsumer(IServiceProvider serviceProvider)
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

        channel.QueueDeclare(queue: "order_internal",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            OrderMessage? orderMessage = JsonSerializer.Deserialize<OrderMessage>(message);

            Console.WriteLine($"Received message: {message}");
            Console.WriteLine($"Received message: {orderMessage.Psp}");

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IOrderRepository _orderRepository =
                    scope.ServiceProvider.GetRequiredService<IOrderRepository>();

                List<ProductDocument> productDocuments = new List<ProductDocument>();
                
                foreach (ProductMessage product in orderMessage.Products)
                {
                    ProductDocument pd = new ProductDocument()
                    {
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price
                    };
                    productDocuments.Add(pd);
                }

                CustomerDocument cd = new CustomerDocument()
                {
                    City = orderMessage.Customer.City,
                    Country = orderMessage.Customer.Country,
                    Email = orderMessage.Customer.Email,
                    Name = orderMessage.Customer.Name,
                    Postalcode = orderMessage.Customer.Postalcode,
                    Street = orderMessage.Customer.Street
                };

                OrderDocument od = new OrderDocument()
                {
                    Customer = cd,
                    OrderId = orderMessage.OrderUuid,
                    Psp = orderMessage.Psp,
                    Products = productDocuments
                };

                await _orderRepository.CreateOrderDocument(od);
            }

            await Task.Yield();
        };

        channel.BasicConsume(queue: "order_internal",
            autoAck: true,
            consumer: consumer);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken); // Wait for new messages
        }
        
    }
}