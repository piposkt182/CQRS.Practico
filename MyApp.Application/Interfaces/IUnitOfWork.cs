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
        IMovieRepository MovieRepository { get; }
        IUSerRepository USerRepository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task RollbackAsync();
        Task CommitAsync();
    }
}
