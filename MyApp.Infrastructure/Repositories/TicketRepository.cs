using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Context;
using System.Collections.Generic; // For KeyNotFoundException
using System.Linq; // Added for Where()
using System.Threading.Tasks; // For Task

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

        public void Update(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
        }

        // New method implementation
        public async Task DeleteTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                throw new KeyNotFoundException($"Ticket with ID {id} not found.");
            }
            _context.Tickets.Remove(ticket);
        }

        public async Task<List<Ticket>> GetTicketsByTimbradoAsync(bool timbrado)
        {
            return await _context.Tickets.Where(t => t.Timbrado == timbrado).ToListAsync();
        }
    }
}
