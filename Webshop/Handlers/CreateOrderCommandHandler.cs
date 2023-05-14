using System.Transactions;
using Domain;
using Domain.Service.IRepository;
using Infrastructure.RabbitMQ;
using Infrastructure.RabbitMQ.Messages;
using Webshop.Commands;
using Webshop.Dto;
using Webshop.Interfaces;

namespace Webshop.Handlers;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IRabbitMQProducer _rabbitMqProducer;
    private readonly IProductRepository _productRepository;
    private readonly IOrderProductRepository _orderProductRepository;
    
    public CreateOrderCommandHandler(IOrderRepository orderRepository, IRabbitMQProducer rabbitMqProducer, ICustomerRepository customerRepository, IProductRepository productRepository, IOrderProductRepository orderProductRepository)
    {
        this._orderRepository = orderRepository;
        this._rabbitMqProducer = rabbitMqProducer;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _orderProductRepository = orderProductRepository;
    }
    
    public async Task<OrderDto> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        List<ProductMessage> products = new List<ProductMessage>();
        
        foreach (int productId in command.ProductIds)
        {
            Product product = await _productRepository.getProductById(productId);
            ProductMessage productMessage = new ProductMessage()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
            products.Add(productMessage);
        }
        
        Customer customer = await _customerRepository.getCustomerById(command.CustomerId);
        
        CustomerMessage customerMessage = new CustomerMessage()
        {
            City = customer.Address.City,
            Country = customer.Address.Country,
            Email = customer.Email,
            Name = customer.Name,
            Postalcode = customer.Address.PostalCode,
            Street = customer.Address.Street
        };

        OrderMessage orderMessage = new OrderMessage()
        {
            Customer = customerMessage,
            OrderUuid = Guid.NewGuid().ToString(),
            Psp = command.Psp.ToString(),
            Products = products
        };

        Order order = new Order()
        {
            CustomerId = command.CustomerId,
            OrderId = Guid.Parse(orderMessage.OrderUuid),
            Psp = command.Psp,
        };

        int orderId = await _orderRepository.CreateOrder(order);

        foreach (int productId in command.ProductIds)
        {
            await _orderProductRepository.createOrderProduct(orderId, productId);
        }
        
        _rabbitMqProducer.SendOrderMessage(orderMessage);

        OrderDto orderDto = new OrderDto()
        {
            customer_id = command.CustomerId,
            product_ids = command.ProductIds,
            psp = command.Psp
        };
        
        return orderDto;
    }
}