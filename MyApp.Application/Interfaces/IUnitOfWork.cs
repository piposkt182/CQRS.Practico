using MyApp.Domain.Interfaces;

namespace MyApp.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ITicketRepository TicketRepository { get; }
        ISaleRepository SaleRepository { get; }
        IProductRepository ProductRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
