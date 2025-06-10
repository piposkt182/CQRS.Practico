using MyApp.Application.Interfaces;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.ApplicationDbContext;

namespace MyApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IGenderRepository _genderRepository;
        private ILenguajeRepository _lenguajeRepository;
        private IMovieRepository _movieRepository;
        // Add other private repository fields here if converting others to lazy load

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenderRepository GenderRepository => _genderRepository ??= new GenderRepository(_context);
        public ILenguajeRepository LenguajeRepository => _lenguajeRepository ??= new LenguajeRepository(_context);
        public ITicketRepository TicketRepository => new TicketRepository(_context); 
        public IProductRepository ProductRepository => new ProductRepository(_context); 
        public ISaleRepository SaleRepository => new SaleRepository(_context); 
        public IMovieRepository MovieRepository => _movieRepository ??= new MovieRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
