using MediatR;
using MyApp.Application.Commands;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.CommandHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price
            };

            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return new ProductDto { Id = product.Id, Name = product.Name, Price = product.Price };
        }
    }
}
