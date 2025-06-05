using Microsoft.EntityFrameworkCore; // Add this using directive
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Context;
using System.Collections.Generic; // Add this using directive
using System.Threading.Tasks; // Add this using directive

namespace MyApp.Infrastructure.Repositories
{
    public class GenderRepository : IGenderRepository
    {
        private readonly AppDbContext _context;

        public GenderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Gender gender)
        {
            await _context.Genders.AddAsync(gender);
        }

        // Add this method
        public async Task<IEnumerable<Gender>> GetAllAsync()
        {
            return await _context.Genders.ToListAsync();
        }
    }
}
