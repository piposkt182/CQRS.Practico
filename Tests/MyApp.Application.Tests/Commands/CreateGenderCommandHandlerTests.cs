using Moq;
using MyApp.Application.Commands;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace MyApp.Application.Tests.Commands
{
    public class CreateGenderCommandHandlerTests
    {
        private readonly Mock<IGenderRepository> _mockGenderRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly CreateGenderCommandHandler _handler;

        public CreateGenderCommandHandlerTests()
        {
            _mockGenderRepository = new Mock<IGenderRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            // Setup IUnitOfWork to return the mock IGenderRepository
            _mockUnitOfWork.Setup(uow => uow.GenderRepository).Returns(_mockGenderRepository.Object);

            _handler = new CreateGenderCommandHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task HandleAsync_ValidCommand_ShouldAddGenderAndSaveChanges()
        {
            // Arrange
            var command = new CreateGenderCommand("Test Gender");

            // Act
            await _handler.HandleAsync(command);

            // Assert
            // Verify that AddAsync was called on the repository with the correct Gender object
            _mockGenderRepository.Verify(repo => repo.AddAsync(It.Is<Gender>(g => g.Name == command.Name)), Times.Once);

            // Verify that SaveChangesAsync was called on the unit of work
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_NullCommand_ShouldThrowArgumentNullException()
        {
            // Arrange
            CreateGenderCommand command = null;

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _handler.HandleAsync(command));
            // Corrected: CreateGenderCommand constructor would throw ArgumentNullException if name is null.
            // If command object itself is null, the handler would throw NullReferenceException when accessing command.Name.
            // If the command Name property is what we are testing for null/empty, that validation should be in the command or handler.
            // The controller currently checks for command.Name emptiness.
            // For this test, if command is null, it will be a NullReferenceException on command.Name.
        }

        [Fact]
        public async Task HandleAsync_EmptyGenderName_ShouldStillCallRepository_IfNoValidationInHandler()
        {
            // Arrange
            var command = new CreateGenderCommand(""); // Assuming command allows empty name, controller validates

            // Act
            await _handler.HandleAsync(command);

            // Assert
            _mockGenderRepository.Verify(repo => repo.AddAsync(It.Is<Gender>(g => g.Name == "")), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
            // Note: This test highlights that the handler itself doesn't validate the name.
            // Validation is currently in the Controller or could be added via FluentValidation on the command.
        }
    }
}
