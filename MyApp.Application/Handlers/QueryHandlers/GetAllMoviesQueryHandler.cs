using MediatR;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Application.Queries;

namespace MyApp.Application.Handlers.QueryHandlers
{
    public class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, IEnumerable<MovieDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllMoviesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IEnumerable<MovieDto>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = await _unitOfWork.MovieRepository.GetAllAsync();

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
                LanguageName = movie.Language?.Name,
                GenderName = movie.Gender?.Name,
                EndDate = movie.EndDate
            }).ToList();
        }
    }
}
