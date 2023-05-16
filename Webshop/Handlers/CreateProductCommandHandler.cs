using Domain;
using Domain.Service.IRepository;
using Infrastructure.RabbitMQ;
using Webshop.Commands;
using Webshop.Dto;
using Webshop.Interfaces;

namespace Webshop.Handlers;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, ProductDto>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        Product product = new Product()
        {
            Name = command.Name,
            Description = command.Description,
            MerchantId = command.MerchantId,
            Price = command.Price,
            Stock = command.Stock
        };

        int productId = await _productRepository.createProduct(product);

        return new ProductDto()
        {
            name = command.Name,
            description = command.Description,
            price = command.Price,
            product_id = productId,
            merchant_id = command.MerchantId,
            stock = command.Stock
        };
    }
}