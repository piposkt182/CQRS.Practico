using MyApp.Domain.Interfaces;

namespace MyApp.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ITicketRepository TicketRepository { get; }
        ISaleRepository SaleRepository { get; }
        IProductRepository ProductRepository { get; }
        IGenderRepository GenderRepository { get; }
        ILenguajeRepository LenguajeRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
