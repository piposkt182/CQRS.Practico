using MediatR;
using MyApp.Application.Interfaces;
using MyApp.Application.DTOs;


namespace MyApp.Application.Queries
{
    public class GetAllLenguajesQueryHandler : IRequestHandler<GetAllLenguajesQuery, IEnumerable<LenguajeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllLenguajesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<LenguajeDto>> Handle(GetAllLenguajesQuery request, CancellationToken cancellationToken)
        {
            var lenguajes = await _unitOfWork.LenguajeRepository.GetAllAsync();

            // Map entities to DTOs
            var lenguajeDtos = lenguajes.Select(lenguaje => new LenguajeDto
            {
                Id = lenguaje.Id,
                Nombre = lenguaje.Name
            }).ToList();

            return lenguajeDtos;
        }
    }
}
