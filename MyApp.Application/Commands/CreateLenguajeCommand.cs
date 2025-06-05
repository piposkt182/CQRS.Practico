using MediatR;
using MyApp.Application.DTOs; // For LenguajeDto if returning DTO

namespace MyApp.Application.Commands
{
    public class CreateLenguajeCommand : IRequest<LenguajeDto> // Or IRequest<int> if returning Id
    {
        public string Name { get; set; }
    }
}
