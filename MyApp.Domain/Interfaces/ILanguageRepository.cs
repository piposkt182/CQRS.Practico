using MyApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Domain.Interfaces
{
    public interface ILanguageRepository
    {
        Task<Language> GetByIdAsync(int id);
        Task<IEnumerable<Language>> GetAllAsync();
        Task AddAsync(Language language);
        void Update(Language language);
        void Remove(Language language);
    }
}
