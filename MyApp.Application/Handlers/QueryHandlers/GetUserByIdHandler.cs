using MediatR;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Application.Queries;

namespace MyApp.Application.Handlers.QueryHandlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetUserByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var user = await _unitOfWork.USerRepository.GetUserById(request.Id);

            if (user == null)
                throw new KeyNotFoundException($"User with Id {request.Id} not found.");

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                LastName = user.LastName,
                TicketId = user.TicketId
            };
        }
    }
}
