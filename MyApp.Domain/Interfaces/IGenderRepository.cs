using MyApp.Domain.Entities;
using System.Threading.Tasks;

namespace MyApp.Domain.Interfaces
{
    public interface IGenderRepository
    {
        Task AddAsync(Gender gender);
        Task<IEnumerable<Gender>> GetAllAsync(); // Add this line
    }
}
