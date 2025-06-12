using MediatR;
using MyApp.Application.DTOs;

namespace MyApp.Application.Interfaces
{
    public record GetTicketByIdQuery(int id) : IRequest<TicketDto>;
    
}
