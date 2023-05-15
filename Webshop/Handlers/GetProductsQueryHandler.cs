using Domain;
using Domain.Service.IRepository;
using Webshop.Interfaces;
using Webshop.Queries;

namespace Webshop.Handlers;

public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, List<Product>>
{

    private readonly IProductRepository _productRepository;
    
    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<List<Product>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        return _productRepository.getProducts();
    }
}