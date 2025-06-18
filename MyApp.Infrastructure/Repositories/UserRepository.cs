using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.ApplicationDbContext;

namespace MyApp.Infrastructure.Repositories
{
    public class UserRepository : IUSerRepository
    {
        public readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(User user)
        {
             await _context.Users.AddAsync(user);   
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public async Task<User> GetUserById(int id) => await _context.Users.FindAsync(id);
    }
}
