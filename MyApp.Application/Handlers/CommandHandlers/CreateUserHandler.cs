using MediatR;
using MyApp.Application.Commands;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Application.Handlers.CommandHandlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = request.user.Name,
                Email = request.user.Email,
                LastName = request.user.LastName,
                TicketId = request.user.TicketId == 0 ? null : request.user.TicketId
            };

            await _unitOfWork.USerRepository.CreateUserAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return new UserDto { Id = user.Id, Name = user.Name, LastName = user.LastName, Email = user.Email, TicketId = user.TicketId };
        }
    }
}
