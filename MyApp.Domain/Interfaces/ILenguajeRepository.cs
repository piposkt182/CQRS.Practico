using MyApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Domain.Interfaces
{
    public interface ILenguajeRepository
    {
        Task<Lenguaje> GetByIdAsync(int id);
        Task<IEnumerable<Lenguaje>> GetAllAsync();
        Task AddAsync(Lenguaje lenguaje);
        // Add other necessary methods like UpdateAsync, DeleteAsync if they were in scope
        // For now, sticking to the issue's request for create and get.
    }
}
