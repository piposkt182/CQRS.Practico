using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.ApplicationDbContext;
using System.Threading.Tasks;

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
    }
}
