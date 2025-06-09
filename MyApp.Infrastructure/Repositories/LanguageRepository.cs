using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly AppDbContext _context;

        public LanguageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Language> GetByIdAsync(int id)
        {
            return await _context.Languages.FindAsync(id);
        }

        public async Task<IEnumerable<Language>> GetAllAsync()
        {
            return await _context.Languages.ToListAsync();
        }

        public async Task AddAsync(Language language)
        {
            await _context.Languages.AddAsync(language);
        }

        public void Update(Language language)
        {
            _context.Languages.Update(language);
        }

        public void Remove(Language language)
        {
            _context.Languages.Remove(language);
        }
    }
}
