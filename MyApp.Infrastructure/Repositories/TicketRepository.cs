using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Context;

namespace MyApp.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            return await _context.Tickets.ToListAsync();
        }
        public async Task<Ticket> GetTicketByIdAsync(int codigo)
        {
            return await _context.Tickets.FirstOrDefaultAsync(t => t.Codigo.Equals(codigo));
        }

        public void Add(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
        }
    }
}
