using MediatR;
using MyApp.Application.DTOs;

namespace MyApp.Application.Queries
{
    public class GetMovieByIdQuery : IRequest<MovieDto>
    {
        public int Id { get; set; }

        public GetMovieByIdQuery(int id)
        {
            Id = id;
        }
    }
}
