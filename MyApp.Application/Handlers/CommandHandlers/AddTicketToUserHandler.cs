using MediatR;
using MyApp.Application.Commands;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Application.Handlers.CommandHandlers
{
    public class AddTicketToUserHandler : IRequestHandler<AddTicketToUserCommand, TicketToUserDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddTicketToUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TicketToUserDto> Handle(AddTicketToUserCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var user = await GetUserByIdAsync(request.UserId);
                var ticket = await GetTicketByIdAsync(request.TicketId);

                ValidateUserHasNoTicket(user);
                ValidateTicketHasSale(ticket);

                user.TicketId = ticket.Codigo;

                await _unitOfWork.USerRepository.UpdateUser(user);
                await _unitOfWork.CommitAsync();

                return new TicketToUserDto
                {
                    UserId = user.Id,
                    TicketId = user.TicketId.Value
                };
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        private async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _unitOfWork.USerRepository.GetUserById(userId);
            if (user == null)
                throw new NotFoundException($"User with ID {userId} not found.");

            return user;
        }

        private async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            var ticket = await _unitOfWork.TicketRepository.GetTicketByIdAsync(ticketId);
            if (ticket == null)
                throw new NotFoundException($"Ticket with ID {ticketId} not found.");

            return ticket;
        }

        private static void ValidateUserHasNoTicket(User user)
        {
            if (user.TicketId != null)
                throw new InvalidOperationException("The user already has a ticket assigned.");
        }

        private static void ValidateTicketHasSale(Ticket ticket)
        {
            if (ticket.SaleId == null)
                throw new InvalidOperationException("The ticket does not have an associated sale.");
        }
    }
}
