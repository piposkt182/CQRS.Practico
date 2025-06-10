using MyApp.Application.Commands;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.CommandHandlers
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(CreateProductCommand command)
        {
            var product = new Product
            {
                Name = command.Name,
                Price = command.Price
            };

            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
