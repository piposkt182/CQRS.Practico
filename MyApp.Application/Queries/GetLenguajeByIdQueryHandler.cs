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
                return null;
            }

            return new LenguajeDto
            {
                Id = lenguaje.Id,
                Nombre = lenguaje.Name
            };
        }
    }
}
