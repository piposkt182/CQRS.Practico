using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Context; // Corrected namespace
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repositories
{
    public class LenguajeRepository : ILenguajeRepository
    {
        private readonly AppDbContext _context;

        public LenguajeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Lenguaje lenguaje)
        {
            await _context.Lenguajes.AddAsync(lenguaje);
            //SaveChangesAsync is typically called in a UnitOfWork or service layer
        }

        public async Task<IEnumerable<Lenguaje>> GetAllAsync()
        {
            return await _context.Lenguajes.ToListAsync();
        }

        public async Task<Lenguaje> GetByIdAsync(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Lenguajes.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
