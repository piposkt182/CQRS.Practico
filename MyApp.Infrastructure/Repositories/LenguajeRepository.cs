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
        }

        public async Task<IEnumerable<Language>> GetAllAsync()
        {
            return await _context.Languages.ToListAsync();
        }

        public async Task<Language> GetByIdAsync(int id)
        {
            return await _context.Languages.FindAsync(id);
        }
    }
}