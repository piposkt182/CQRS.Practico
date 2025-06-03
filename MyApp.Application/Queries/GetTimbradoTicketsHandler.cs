using MyApp.Application.Interfaces;
using MyApp.Domain.Entities; // Required for Ticket
using System.Collections.Generic; // Required for IEnumerable
using System.Linq; // Required for Select
using System.Threading.Tasks; // Required for Task

namespace MyApp.Application.Queries
{
    public class GetTimbradoTicketsHandler : IQueryHandler<GetTimbradoTicketsQuery, IEnumerable<TicketDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTimbradoTicketsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TicketDto>> HandleAsync(GetTimbradoTicketsQuery query)
        {
            var tickets = await _unitOfWork.TicketRepository.GetTicketsByTimbradoAsync(query.Timbrado);
            return tickets.Select(t => new TicketDto(t.Codigo, t.NombreTicket));
        }
    }
}
