using MediatR;
using MyApp.Application.Interfaces;
using MyApp.Application.DTOs;

namespace MyApp.Application.Queries
{
    public class GetLenguajeByIdQueryHandler : IRequestHandler<GetLenguajeByIdQuery, LenguajeDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLenguajeByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LenguajeDto> Handle(GetLenguajeByIdQuery request, CancellationToken cancellationToken)
        {
            var lenguaje = await _unitOfWork.LenguajeRepository.GetByIdAsync(request.Id);

            if (lenguaje == null)
            {
                // Handle not found case, could return null or throw an exception
                // For API controllers, returning null often results in a 404 Not Found if handled correctly
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }

            return new LenguajeDto
            {
                Id = lenguaje.Id,
                Nombre = lenguaje.Name
            };
        }
    }
}
