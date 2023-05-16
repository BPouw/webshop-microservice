using Domain;
using Microsoft.AspNetCore.Mvc;
using Webshop.Commands;
using Webshop.Dto;
using Webshop.Interfaces;
using Webshop.Queries;

namespace Webshop.Controllers;

[ApiController]
[Route("api/")]
public class ProductController : ControllerBase
{
    private readonly IQueryHandler<GetProductsQuery, List<Product>> _getProductsQueryHandler;

    private readonly ICommandHandler<CreateProductCommand, ProductDto> _createProductCommandHandler;

    public ProductController(IQueryHandler<GetProductsQuery, List<Product>> getProductsQueryHandler, 
        ICommandHandler<CreateProductCommand, ProductDto> createProductCommandHandler)
    {
        _getProductsQueryHandler = getProductsQueryHandler;
        _createProductCommandHandler = createProductCommandHandler;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts()
    {
        var query = new GetProductsQuery();

        List<Product> pds = await _getProductsQueryHandler.Handle(query, CancellationToken.None);

        return Ok(pds);
    }

    [HttpPost("products")]
    public async Task<IActionResult> PostProduct(ProductDto productDto)
    {
        var command = new CreateProductCommand()
        {
            Description = productDto.description,
            Price = productDto.price,
            MerchantId = productDto.merchant_id,
            Name = productDto.name,
            Stock = productDto.stock
        };

        var product = await _createProductCommandHandler.Handle(command, CancellationToken.None);
        
        return Created($"/{product.product_id}", product);
    }
}