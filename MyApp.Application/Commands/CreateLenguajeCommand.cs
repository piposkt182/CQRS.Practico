using MediatR; // Assuming MediatR is used for IRequest
using MyApp.Application.DTOs; // For LenguajeDto if returning DTO

namespace MyApp.Application.Commands
{
    public class CreateLenguajeCommand : IRequest<LenguajeDto> // Or IRequest<int> if returning Id
    {
        public string Nombre { get; set; }
    }
}
