using MediatR;
using MyApp.Application.DTOs;

namespace MyApp.Application.Queries
{
    public class GetAllLenguajesQuery : IRequest<IEnumerable<LenguajeDto>>
    {
    }
}
