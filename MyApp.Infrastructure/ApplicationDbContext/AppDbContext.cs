using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.ApplicationDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<SaleItem> SaleItems => Set<SaleItem>();
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Gender> Genders => Set<Gender>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<User> Users => Set<User>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");
                entity.HasKey(t => t.Codigo);
                entity.Property(m => m.NombreTicket).IsRequired();
                entity.Property(m => m.DesignTicket).IsRequired();
                entity.Property(m => m.MovieId).IsRequired();
                entity.Property(m => m.SaleId).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(t => t.Id);
                entity.Property(m => m.Name).IsRequired();
                entity.Property(m => m.LastName).IsRequired();
                entity.Property(m => m.Email).IsRequired();
                entity.Property(m => m.TicketId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Name).IsRequired().HasMaxLength(100);
                entity.Property(m => m.Price).HasPrecision(18, 4);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sale");
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Date).IsRequired();
                entity.Property(m => m.Total).IsRequired().HasPrecision(18, 4);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Id).ValueGeneratedOnAdd();
                entity.Property(m => m.Name).IsRequired().HasMaxLength(100);
                entity.Property(m => m.ReleaseDate).IsRequired();
                entity.Property(m => m.Duration).IsRequired();
                entity.Property(m => m.EndDate).IsRequired(false);

                entity.HasOne(m => m.Language)
                    .WithMany(l => l.Movies)
                    .HasForeignKey(m => m.LanguageId)
                    .IsRequired();

                entity.HasOne(m => m.Gender)
                    .WithMany(g => g.Movies)
                    .HasForeignKey(m => m.GenderId)
                    .IsRequired();
            });

            // Configure Language entity
            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Language");
                entity.HasKey(l => l.Id);
                entity.Property(l => l.Id).ValueGeneratedOnAdd();
                entity.Property(l => l.Name).IsRequired().HasMaxLength(50);
            });

            // Configure Gender entity
            // Relies on the [Table("Gender", Schema = "dbo")] attribute on the Gender entity for table name and schema.
            modelBuilder.Entity<Gender>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Id).ValueGeneratedOnAdd();
                entity.Property(g => g.Name).IsRequired().HasMaxLength(50);
            });
        }
    }
}