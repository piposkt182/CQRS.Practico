
using MediatR;
using MyApp.Application.DTOs;

namespace MyApp.Application.Interfaces
{
    public class GetAllTicketsQuery : IRequest<IEnumerable<TicketDto>> { }
}
