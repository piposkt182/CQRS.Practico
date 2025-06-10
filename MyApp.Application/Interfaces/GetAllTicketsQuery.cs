
using MediatR;
using MyApp.Application.Queries;

namespace MyApp.Application.Interfaces
{
    public class GetAllTicketsQuery : IRequest<IEnumerable<TicketDto>> { }
}
