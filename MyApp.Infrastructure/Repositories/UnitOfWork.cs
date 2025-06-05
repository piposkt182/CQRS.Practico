using MyApp.Application.Interfaces;
using MyApp.Domain.Interfaces; // Changed from MyApp.Application.Interfaces
using MyApp.Infrastructure.Context;

namespace MyApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IGenderRepository _genderRepository;
        private ILenguajeRepository _lenguajeRepository;
        // Add other private repository fields here if converting others to lazy load

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenderRepository GenderRepository => _genderRepository ??= new GenderRepository(_context);
        public ILenguajeRepository LenguajeRepository => _lenguajeRepository ??= new LenguajeRepository(_context);
        public ITicketRepository TicketRepository => new TicketRepository(_context); // Assuming this doesn't need to be lazy-loaded for now
        public IProductRepository ProductRepository => new ProductRepository(_context); // Assuming this doesn't need to be lazy-loaded for now
        public ISaleRepository SaleRepository => new SaleRepository(_context); // Assuming this doesn't need to be lazy-loaded for now

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
