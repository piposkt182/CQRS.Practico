using MediatR;
using MyApp.Application.DTOs; 

namespace MyApp.Application.Commands
{
    public class CreateLenguajeCommand : IRequest<LenguajeDto> // Or IRequest<int> if returning Id
    {
        public string Name { get; set; }
    }
}
