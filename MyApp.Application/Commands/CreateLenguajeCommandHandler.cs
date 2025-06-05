using MediatR;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Application.DTOs; // For LenguajeDto
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Commands
{
    public class CreateLenguajeCommandHandler : IRequestHandler<CreateLenguajeCommand, LenguajeDto> // Or IRequestHandler<CreateLenguajeCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateLenguajeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LenguajeDto> Handle(CreateLenguajeCommand request, CancellationToken cancellationToken)
        {
            var lenguaje = new Lenguaje
            {
                Name = request.Name
            };

            await _unitOfWork.LenguajeRepository.AddAsync(lenguaje);
            await _unitOfWork.SaveChangesAsync(); // Persist changes

            // Return DTO
            return new LenguajeDto
            {
                Id = lenguaje.Id,
                Nombre = lenguaje.Name
            };

            // Or if returning Id:
            // return lenguaje.Id;
        }
    }
}
