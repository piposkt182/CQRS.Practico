using MediatR;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;

namespace MyApp.Application.Queries
{
    public class GetTicketByIdHandler : IRequestHandler<GetTicketByIdQuery, TicketDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetTicketByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TicketDto> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _unitOfWork.TicketRepository.GetTicketByIdAsync(request.id);
            return new TicketDto(ticket.Codigo, ticket.NombreTicket, ticket.DesignTicket, ticket.Timbrado, ticket.MovieId, ticket.SaleId);
        }
    }
}
