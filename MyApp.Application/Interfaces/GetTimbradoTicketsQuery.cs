using MediatR;
using MyApp.Application.Queries;

namespace MyApp.Application.Interfaces
{
    public class GetTimbradoTicketsQuery : IRequest<IEnumerable<TicketDto>>
    {
        public bool Timbrado { get; }

        public GetTimbradoTicketsQuery(bool timbrado)
        {
            Timbrado = timbrado;
        }
    }
}
