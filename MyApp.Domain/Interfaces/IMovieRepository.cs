using MyApp.Domain.Entities;

namespace MyApp.Domain.Interfaces
{
    public interface IMovieRepository
    {
        Task<Movie> GetByIdAsync(int id);
        Task<IEnumerable<Movie>> GetAllAsync();
        Task AddAsync(Movie movie);
        void Update(Movie movie);
        void Remove(Movie movie);
    }
}
