using MediatR;
using MyApp.Application.Commands;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;

namespace MyApp.Application.Handlers.CommandHandlers
{
    public class UpdateProductStockHandler : IRequestHandler<UpdateProductStockCommand, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductStockHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(_unitOfWork));
        }
        public async Task<ProductDto> Handle(UpdateProductStockCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.id);

            if (product == null)
                throw new KeyNotFoundException($"Ticket with Id {request.id} not found.");

            product.Stock = request.stock;
            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return new ProductDto
            {
                Id = request.id,
                Stock = product.Stock,
                Name = product.Name,
                Price = product.Price
            };
        }

    }
}

