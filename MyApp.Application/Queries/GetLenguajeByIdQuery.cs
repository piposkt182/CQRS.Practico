using MediatR;
using MyApp.Application.DTOs;

namespace MyApp.Application.Queries
{
    public class GetLenguajeByIdQuery : IRequest<LenguajeDto>
    {
        public int Id { get; set; }

        public GetLenguajeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
