using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.ApplicationDbContext; // Corrected namespace
using Microsoft.EntityFrameworkCore;

namespace MyApp.Infrastructure.Repositories
{
    public class GenderRepository : IGenderRepository
    {
        private readonly AppDbContext _context;

        public GenderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Gender> GetByIdAsync(int id)
        {
            return await _context.Genders.FindAsync(id);
        }

        public async Task<IEnumerable<Gender>> GetAllAsync()
        {
            return await _context.Genders.ToListAsync();
        }

        public async Task AddAsync(Gender gender)
        {
            await _context.Genders.AddAsync(gender);
        }

        public void Update(Gender gender)
        {
            _context.Genders.Update(gender);
        }

        public void Remove(Gender gender)
        {
            _context.Genders.Remove(gender);
        }
    }
}
