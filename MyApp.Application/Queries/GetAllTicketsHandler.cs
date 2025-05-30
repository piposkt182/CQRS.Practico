
using MyApp.Application.Interfaces;

namespace MyApp.Application.Queries
{
    public class GetAllTicketsHandler : IQueryHandler<GetAllTicketsQuery, IEnumerable<TicketDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTicketsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TicketDto>> HandleAsync(GetAllTicketsQuery query)
        {
            var tickets = await _unitOfWork.TicketRepository.GetAllAsync();
            return tickets.Select(t => new TicketDto(t.Codigo, t.NombreTicket));
        }
    }
}
