using MediatR; // Added for IRequestHandler
using MyApp.Application.Commands;
// Removed using MyApp.Application.Interfaces; as ICommandHandler is replaced by IRequestHandler
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Threading; // Added for CancellationToken
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.CommandHandlers
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, int> // Implements IRequestHandler<CreateMovieCommand, int>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMovieCommandHandler(
            IMovieRepository movieRepository,
            ILanguageRepository languageRepository,
            IGenderRepository genderRepository,
            IUnitOfWork unitOfWork)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _languageRepository = languageRepository ?? throw new ArgumentNullException(nameof(languageRepository));
            _genderRepository = genderRepository ?? throw new ArgumentNullException(nameof(genderRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<int> HandleAsync(CreateMovieCommand command, CancellationToken cancellationToken) // Signature changed
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            // Validate LanguageId
            var language = await _languageRepository.GetByIdAsync(command.LanguageId);
            if (language == null)
            {
                throw new ArgumentException($"Language with Id {command.LanguageId} not found.", nameof(command.LanguageId));
            }

            // Validate GenderId
            var gender = await _genderRepository.GetByIdAsync(command.GenderId);
            if (gender == null)
            {
                throw new ArgumentException($"Gender with Id {command.GenderId} not found.", nameof(command.GenderId));
            }

            var movie = new Movie
            {
                Name = command.Name,
                ReleaseDate = command.ReleaseDate,
                Duration = command.Duration,
                LanguageId = command.LanguageId,
                GenderId = command.GenderId,
                EndDate = command.EndDate // Assign EndDate from command
            };

            await _movieRepository.AddAsync(movie);
            await _unitOfWork.SaveChangesAsync(); // Corrected to use SaveChangesAsync

            return movie.Id; // Return the new movie's ID
        }
    }
}
