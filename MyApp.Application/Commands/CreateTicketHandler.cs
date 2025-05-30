
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Application.Commands
{
    public class CreateTicketHandler : ICommandHandler<CreateTicketCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTicketHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(CreateTicketCommand command)
        {
            var ticket = Ticket.CreateBuilder()
                .WithCodigoTicket(command.codigo)
                .WithNombreTicket(command.NombreTicket)
                .WithDesignTicket(command.DesignTicket)
                .WithTimbrado(command.Timbrado)
                .Build();

            _unitOfWork.TicketRepository.Add(ticket);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
