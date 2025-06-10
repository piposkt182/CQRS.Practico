using MediatR;
using MyApp.Application.Interfaces;

namespace MyApp.Application.Queries
{
    public class GetTimbradoTicketsHandler : IRequestHandler<GetTimbradoTicketsQuery, IEnumerable<TicketDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTimbradoTicketsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TicketDto>> Handle(GetTimbradoTicketsQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _unitOfWork.TicketRepository.GetTicketsByTimbradoAsync(request.Timbrado);
            return tickets.Select(t => new TicketDto(t.Codigo, t.NombreTicket));
        }

    }
}
