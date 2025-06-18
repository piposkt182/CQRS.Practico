using MediatR;
using MyApp.Application.DTOs;

namespace MyApp.Application.Queries
{
    public record GetUserByIdQuery(int Id) : IRequest<UserDto>;
}
