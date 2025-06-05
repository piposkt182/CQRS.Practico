using MediatR;
using MyApp.Application.DTOs;
using System.Collections.Generic;

namespace MyApp.Application.Queries
{
    public class GetAllLenguajesQuery : IRequest<IEnumerable<LenguajeDto>>
    {
    }
}
