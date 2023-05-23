using System.Transactions;
using Domain;
using Domain.Service.IRepository;
using Domain.Service.IService;
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
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    
    public CreateOrderCommandHandler(IOrderRepository orderRepository, IRabbitMQProducer rabbitMqProducer, ICustomerRepository customerRepository, IProductRepository productRepository, IOrderProductRepository orderProductRepository, IOrderService orderService, IProductService productService)
    {
        this._orderRepository = orderRepository;
        this._rabbitMqProducer = rabbitMqProducer;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _orderProductRepository = orderProductRepository;
        _orderService = orderService;
        _productService = productService;
    }
    
    public async Task<OrderDto> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        List<ProductMessage> products = new List<ProductMessage>();
        List<Product> checkProducts = new List<Product>();
        decimal totalPrice = 0;
        
        foreach (int productId in command.ProductIds)
        {
            Product product = await _productRepository.getProductById(productId);
            totalPrice += product.Price;
            ProductMessage productMessage = new ProductMessage()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
            products.Add(productMessage);
            checkProducts.Add(product);
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

        if (!_orderService.isValidOrder(order, checkProducts))
        {
            throw new Exception("Too many products in this order");
        }
        
        foreach (Product product in checkProducts)
        {
            product.Stock = _productService.updateStock(product);
            if (!_productService.hasEnoughStock(product))
            {
                throw new Exception("Not enough stock");
            }

            await _productRepository.updateProduct(product);

            if (_productService.hasToOrderMoreStock(product))
            {
                var ps = new ProductStockMessage()
                {
                    ProductName = product.Name,
                    CurrentStock = product.Stock
                };

                _rabbitMqProducer.SendProductStockOrder(ps);
            }
        }
        
        int orderId = await _orderRepository.CreateOrder(order);

        foreach (int productId in command.ProductIds)
        {
            await _orderProductRepository.createOrderProduct(orderId, productId);
        }
        
        _rabbitMqProducer.SendOrderMessage(orderMessage);

        PaymentMessage paymentMessage = new PaymentMessage()
        {
            Price = totalPrice,
            PSP = order.Psp.ToString(),
            OrderId = order.OrderId.ToString()
        };
        
        _rabbitMqProducer.OrderPaid(paymentMessage);

        OrderDto orderDto = new OrderDto()
        {
            customer_id = command.CustomerId,
            product_ids = command.ProductIds,
            psp = command.Psp
        };
        
        return orderDto;
    }
}