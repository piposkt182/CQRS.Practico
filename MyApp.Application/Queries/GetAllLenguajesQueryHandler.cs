using MediatR;
using MyApp.Application.Interfaces;
using MyApp.Application.DTOs;
using System.Collections.Generic;
using System.Linq; // Required for .Select
using System.Threading;
using System.Threading.Tasks;
using MyApp.Domain.Entities; // Not strictly necessary here if mapping directly

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
                Nombre = lenguaje.Nombre
            }).ToList();

            return lenguajeDtos;
        }
    }
}
