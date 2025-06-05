using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Application.Commands
{
    public class CreateGenderCommandHandler : ICommandHandler<CreateGenderCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateGenderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(CreateGenderCommand command)
        {
            var gender = new Gender
            {
                Name = command.Name
            };
            await _unitOfWork.GenderRepository.AddAsync(gender);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
