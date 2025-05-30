using MyApp.Domain.Entities;

namespace MyApp.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
    }
}
