
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Application.Commands
{
    public class CreateSaleHandler : ICommandHandler<CreateSaleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSaleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(CreateSaleCommand command)
        {
            var sale = new Sale { Date = DateTime.UtcNow };

            foreach (var item in command.Items)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(item.ProductId);
                if (product.Stock < item.Quantity)
                    throw new InvalidOperationException("Not enough stock for product " + product.Name);

                product.Stock -= item.Quantity;
                //sale.Items.Add(new SaleItem { ProductId = item.ProductId, Quantity = item.Quantity });
            }

            _unitOfWork.SaleRepository.Add(sale);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
