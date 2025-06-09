using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Application.Queries;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System; // For ArgumentNullException if needed, though not strictly for KeyNotFoundException
using System.Collections.Generic; // For KeyNotFoundException - actually this is in System.Collections.Generic
using System.Threading.Tasks;
// Microsoft.EntityFrameworkCore is not directly used here, but IMovieRepository implementation will be.

namespace MyApp.Application.Handlers.QueryHandlers
{
    public class GetMovieByIdQueryHandler : IQueryHandler<GetMovieByIdQuery, MovieDto>
    {
        private readonly IMovieRepository _movieRepository;

        public GetMovieByIdQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }

        public async Task<MovieDto> HandleAsync(GetMovieByIdQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var movie = await _movieRepository.GetByIdAsync(query.Id);

            if (movie == null)
            {
                // Option 1: Return null (as per current structure)
                return null;
                // Option 2: Throw an exception
                // throw new KeyNotFoundException($"Movie with Id {query.Id} not found.");
            }

            return new MovieDto
            {
                Id = movie.Id,
                Name = movie.Name,
                ReleaseDate = movie.ReleaseDate,
                Duration = movie.Duration,
                // These assume that Language and Gender are loaded by GetByIdAsync()
                LanguageName = movie.Language?.Name,
                GenderName = movie.Gender?.Name,
                EndDate = movie.EndDate // Map EndDate property
            };
        }
    }
}
