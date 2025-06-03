using MyApp.Application.Interfaces;
using MyApp.Domain.Interfaces;
using System.Threading.Tasks;

namespace MyApp.Application.Commands;

public class DeleteTicketCommandHandler : ICommandHandler<DeleteTicketCommand>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTicketCommandHandler(ITicketRepository ticketRepository, IUnitOfWork unitOfWork)
    {
        _ticketRepository = ticketRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(DeleteTicketCommand command)
    {
        // The actual existence check and deletion will be handled by the repository method.
        // The repository method can throw an exception if the ticket is not found,
        // which can be caught in the controller.
        await _ticketRepository.DeleteTicket(command.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
