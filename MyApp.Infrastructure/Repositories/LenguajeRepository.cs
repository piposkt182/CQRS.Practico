using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.ApplicationDbContext;


namespace MyApp.Infrastructure.Repositories
{
    public class LenguajeRepository : ILenguajeRepository
    {
        private readonly AppDbContext _context;

        public LenguajeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Language lenguaje)
        {
            await _context.Languages.AddAsync(lenguaje);
            //SaveChangesAsync is typically called in a UnitOfWork or service layer
        }

        public async Task<IEnumerable<Language>> GetAllAsync()
        {
            return await _context.Languages.ToListAsync();
        }

        public async Task<Language> GetByIdAsync(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Languages.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}