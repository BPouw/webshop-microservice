using Domain;
using Microsoft.AspNetCore.Mvc;
using Webshop.Interfaces;
using Webshop.Queries;

namespace Webshop.Controllers;

[ApiController]
[Route("api/")]
public class ProductController : ControllerBase
{
    private readonly IQueryHandler<GetProductsQuery, List<Product>> _getProductsQueryHandler;

    public ProductController(IQueryHandler<GetProductsQuery, List<Product>> getProductsQueryHandler)
    {
        _getProductsQueryHandler = getProductsQueryHandler;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts()
    {
        var query = new GetProductsQuery();

        List<Product> pds = await _getProductsQueryHandler.Handle(query, CancellationToken.None);

        return Ok(pds);
    }
}