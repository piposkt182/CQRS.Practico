using MediatR;
using MyApp.Application.Queries;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;

namespace MyApp.Application.Handlers.QueryHandlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllAsync();

        }
    }
}
