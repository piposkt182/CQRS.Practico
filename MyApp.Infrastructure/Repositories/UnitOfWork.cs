using Microsoft.EntityFrameworkCore.Storage;
using MyApp.Application.Interfaces;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.ApplicationDbContext;

namespace MyApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction _currentTransaction;

        private IGenderRepository _genderRepository;
        private ILenguajeRepository _lenguajeRepository;
        private IMovieRepository _movieRepository;
        private IProductRepository _productRepository;
        private IUSerRepository _userRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenderRepository GenderRepository => _genderRepository ??= new GenderRepository(_context);
        public ILenguajeRepository LenguajeRepository => _lenguajeRepository ??= new LenguajeRepository(_context);
        public ITicketRepository TicketRepository => new TicketRepository(_context); 
        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context); 
        public ISaleRepository SaleRepository => new SaleRepository(_context); 
        public IMovieRepository MovieRepository => _movieRepository ??= new MovieRepository(_context);
        public IUSerRepository USerRepository => _userRepository ??= new UserRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task BeginTransactionAsync()
        {
            _currentTransaction = await _context.Database.BeginTransactionAsync();
        }
        public async Task RollbackAsync()
        {
            await _currentTransaction.RollbackAsync();
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            await _currentTransaction.CommitAsync();
        }
    }
}
