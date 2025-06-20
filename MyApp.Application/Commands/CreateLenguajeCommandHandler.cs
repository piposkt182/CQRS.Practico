using MediatR;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Application.DTOs; 

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
            var lenguaje = new Language
            {
                Name = request.Name
            };

            await _unitOfWork.LenguajeRepository.AddAsync(lenguaje);
            await _unitOfWork.SaveChangesAsync(); 

            return new LenguajeDto
            {
                Id = lenguaje.Id,
                Nombre = lenguaje.Name
            };
        }
    }
}
