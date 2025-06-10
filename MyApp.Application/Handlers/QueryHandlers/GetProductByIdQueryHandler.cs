using MyApp.Application.Interfaces;
using MyApp.Application.Queries;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.QueryHandlers
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, Product>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> HandleAsync(GetProductByIdQuery query)
        {
            return await _productRepository.GetByIdAsync(query.Id);
        }
    }
}
