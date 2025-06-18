using MyApp.Domain.Entities;

namespace MyApp.Domain.Interfaces
{
    public interface IUSerRepository
    {
        Task CreateUserAsync(User user);
        Task<User> GetUserById(int id);
        Task UpdateUser(User user);
    }
}
