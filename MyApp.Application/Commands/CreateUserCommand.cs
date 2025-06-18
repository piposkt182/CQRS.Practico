using MediatR;
using MyApp.Application.DTOs;

namespace MyApp.Application.Commands
{
    public record CreateUserCommand(UserDto user) : IRequest<UserDto>;
}
