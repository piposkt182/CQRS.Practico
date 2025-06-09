using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Application.Queries;
using MyApp.Domain.Interfaces;
using MyApp.Domain.Entities; // Required for Movie entity
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.QueryHandlers
{
    public class GetAllMoviesQueryHandler : IQueryHandler<GetAllMoviesQuery, IEnumerable<MovieDto>>
    {
        private readonly IMovieRepository _movieRepository;

        public GetAllMoviesQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }

        public async Task<IEnumerable<MovieDto>> HandleAsync(GetAllMoviesQuery query)
        {
            var movies = await _movieRepository.GetAllAsync();

            if (movies == null)
            {
                return Enumerable.Empty<MovieDto>();
            }

            return movies.Select(movie => new MovieDto
            {
                Id = movie.Id,
                Name = movie.Name,
                ReleaseDate = movie.ReleaseDate,
                Duration = movie.Duration,
                // These assume that Language and Gender are loaded by GetAllAsync()
                // If not, this will result in NullReferenceException or incorrect data.
                LanguageName = movie.Language?.Name,
                GenderName = movie.Gender?.Name,
                EndDate = movie.EndDate // Map EndDate property
            }).ToList();
        }
    }
}
