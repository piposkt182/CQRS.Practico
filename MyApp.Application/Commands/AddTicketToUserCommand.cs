
using MediatR;
using MyApp.Application.DTOs;

namespace MyApp.Application.Commands
{
    public record AddTicketToUserCommand( int UserId, int TicketId) : IRequest<TicketToUserDto>; 
    
}
