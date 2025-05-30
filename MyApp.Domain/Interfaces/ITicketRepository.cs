using MyApp.Domain.Entities;

namespace MyApp.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllAsync();
        void Add(Ticket ticket);
        Task<Ticket> GetTicketByIdAsync(int codigo);
    }
}
