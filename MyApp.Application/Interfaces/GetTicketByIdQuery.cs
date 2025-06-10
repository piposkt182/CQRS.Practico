using MediatR;
using MyApp.Application.Queries;

namespace MyApp.Application.Interfaces
{
    public record GetTicketByIdQuery(int id) : IRequest<TicketDto>;
    
}
