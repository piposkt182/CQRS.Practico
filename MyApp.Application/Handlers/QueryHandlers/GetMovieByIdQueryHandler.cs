using MediatR;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Application.Queries;

namespace MyApp.Application.Handlers.QueryHandlers
{
    public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, MovieDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMovieByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<MovieDto> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var movie = await _unitOfWork.MovieRepository.GetByIdAsync(request.Id);

            if (movie == null)
            {
                return null;

            }

            return new MovieDto
            {
                Id = movie.Id,
                Name = movie.Name,
                ReleaseDate = movie.ReleaseDate,
                Duration = movie.Duration,
                LanguageName = movie.Language?.Name,
                GenderName = movie.Gender?.Name,
                EndDate = movie.EndDate
            };
        }
    }
}
