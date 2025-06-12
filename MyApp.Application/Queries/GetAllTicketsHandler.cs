using MediatR;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;

namespace MyApp.Application.Queries
{
    public class GetAllTicketsHandler : IRequestHandler<GetAllTicketsQuery, IEnumerable<TicketDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTicketsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<TicketDto>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _unitOfWork.TicketRepository.GetAllAsync();
            return tickets.Select(t => new TicketDto(t.Codigo, t.NombreTicket, t.DesignTicket, t.Timbrado, t.MovieId, t.SaleId));
        }
    }
}
