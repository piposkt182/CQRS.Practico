using MyApp.Domain.Entities;

namespace MyApp.Domain.Interfaces
{
    public interface ISaleRepository
    {
        Task Add(Sale sale);
    }
}
