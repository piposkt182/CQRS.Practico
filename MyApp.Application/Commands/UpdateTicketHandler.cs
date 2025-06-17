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
            // Consider adding CancellationToken to ICommandHandler and HandleAsync signature
            // public async Task HandleAsync(UpdateTicketCommand command, CancellationToken cancellationToken)

            var ticket = await _unitOfWork.TicketRepository.GetTicketByIdAsync(command.Id);

            if (ticket == null)
            {
                throw new KeyNotFoundException($"Ticket with Id {command.Id} not found.");
            }

            // Update ticket properties.
            // This is now possible because Ticket.cs has public setters for these properties.
            ticket.NombreTicket = command.NombreTicket;
            ticket.DesignTicket = command.DesignTicket;
            ticket.Timbrado = command.Timbrado;

            // Call the repository's Update method.
            // This method still needs to be defined in ITicketRepository and implemented.
            _unitOfWork.TicketRepository.Update(ticket);

            // Persist changes to the database.
            // await _unitOfWork.SaveChangesAsync(cancellationToken); // If CancellationToken were used
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
