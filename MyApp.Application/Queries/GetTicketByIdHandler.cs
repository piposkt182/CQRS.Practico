
using MyApp.Application.Interfaces;
using System.Threading.Tasks;

namespace MyApp.Application.Queries
{
    public class GetTicketByIdHandler : IQueryHandler<GetTicketByIdQuery, TicketDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetTicketByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TicketDto> HandleAsync(GetTicketByIdQuery query)
        {
            var ticket = await _unitOfWork.TicketRepository.GetTicketByIdAsync(query.id);
            return new TicketDto(ticket.Codigo, ticket.NombreTicket);
            
        }
    }
}
