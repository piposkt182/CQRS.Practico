using MediatR;
using MyApp.Application.Commands;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Application.Handlers.CommandHandlers
{
    public class CreateFunctionMovieHandler : IRequestHandler<CreateFunctionMovieCommand, BuyDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateFunctionMovieHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BuyDto> Handle(CreateFunctionMovieCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var sale = await CreateSaleIfThereAreProductsAsync(request.products);

                var movie = await _unitOfWork.MovieRepository.GetByIdAsync(request.MovieId)
                    ?? throw new NotFoundException($"Movie with ID {request.MovieId} not found.");

                var ticket = CrearTicket(movie, request.Codigo, sale.Id);
                _unitOfWork.TicketRepository.Add(ticket);

                if (sale.Total > 0)
                {
                    await _unitOfWork.CommitAsync();
                }

                return new BuyDto
                {
                    Codigo = ticket.Codigo,
                    MovieId = ticket.MovieId
                };
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        private async Task<Sale> CreateSaleIfThereAreProductsAsync(List<ProductToBuyDto> products)
        {
            if (products == null || !products.Any())
                return new Sale(); 

            var productsInStock = await _unitOfWork.ProductRepository.GetListByIds(products.Select(p => p.Id).ToList());

            decimal total = 0;

            foreach (var productDto in products)
            {
                var product = productsInStock.FirstOrDefault(p => p.Id == productDto.Id)
                    ?? throw new NotFoundException($"Product with ID {productDto.Id} not found.");

                product.Stock -= productDto.Amount;
                if (product.Stock < 0)
                    throw new InvalidOperationException($"Product {product.Name} does not have enough stock.");

                await _unitOfWork.ProductRepository.UpdateAsync(product);
                total += product.Price * productDto.Amount;
            }

            var sale = new Sale
            {
                Date = DateTime.UtcNow,
                Total = total
            };

            await _unitOfWork.SaleRepository.Add(sale);
            await _unitOfWork.SaveChangesAsync();

            return sale;
        }

        private Ticket CrearTicket(Movie movie, int codigo, int saleId)
        {
            return Ticket.CreateBuilder()
                .WithCodigoTicket(codigo)
                .WithNombreTicket(movie.Name)
                .WithDesignTicket(movie.Name)
                .WithTimbrado(true)
                .WithMovieId(movie.Id)
                .WithSaleId(saleId)
                .Build();
        }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
