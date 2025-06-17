using MediatR;
using MyApp.Application.DTOs; 

namespace MyApp.Application.Commands
{
    public class CreateLenguajeCommand : IRequest<LenguajeDto>
    {
        public string Name { get; set; }
    }
}
