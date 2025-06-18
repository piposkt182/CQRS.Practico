using MyApp.Application.Interfaces; // For UpdateTicketCommand, ICommandHandler, IUnitOfWork

namespace MyApp.Application.Commands
{
    public class UpdateTicketHandler : ICommandHandler<UpdateTicketCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTicketHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task HandleAsync(UpdateTicketCommand command)
        {
            var ticket = await _unitOfWork.TicketRepository.GetTicketByIdAsync(command.Id);

            if (ticket == null)
            {
                throw new KeyNotFoundException($"Ticket with Id {command.Id} not found.");
            }

            ticket.NombreTicket = command.NombreTicket;
            ticket.DesignTicket = command.DesignTicket;
            ticket.Timbrado = command.Timbrado;

            _unitOfWork.TicketRepository.Update(ticket);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
