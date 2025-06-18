using MyApp.Domain.Entities;

namespace MyApp.Domain.Interfaces
{
    public interface IGenderRepository
    {
        Task<Gender> GetByIdAsync(int id);
        Task<IEnumerable<Gender>> GetAllAsync();
        Task AddAsync(Gender gender);
        void Update(Gender gender);
        void Remove(Gender gender);
    }
}
