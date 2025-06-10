using MyApp.Application.Interfaces;
using MyApp.Application.Queries;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.QueryHandlers
{
    public class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> HandleAsync(GetAllProductsQuery query)
        {
            return await _productRepository.GetAllAsync();
        }
    }
}
