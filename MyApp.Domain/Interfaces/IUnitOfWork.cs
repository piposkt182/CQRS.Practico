using System.Threading.Tasks;

namespace MyApp.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IGenderRepository GenderRepository { get; }
        IProductRepository ProductRepository { get; }
        ISaleRepository SaleRepository { get; }
        ITicketRepository TicketRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
