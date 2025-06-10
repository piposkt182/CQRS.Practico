using MyApp.Domain.Entities;

namespace MyApp.Domain.Interfaces
{
    public interface ILenguajeRepository
    {
        Task<Language> GetByIdAsync(int id);
        Task<IEnumerable<Language>> GetAllAsync();
        Task AddAsync(Language lenguaje);
     
    }
}
