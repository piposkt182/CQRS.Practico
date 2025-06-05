using Microsoft.AspNetCore.Mvc;
using Moq;
using MyApp.Application.Commands;
using MyApp.Application.Interfaces;
using System.Threading.Tasks;
using Xunit;
using CQRS.Practico.Controllers; // Required for GendersController

namespace CQRS.Practico.Tests.Controllers
{
    public class GendersControllerTests
    {
        private readonly Mock<ICommandHandler<CreateGenderCommand>> _mockCommandHandler;
        private readonly GendersController _controller;

        public GendersControllerTests()
        {
            _mockCommandHandler = new Mock<ICommandHandler<CreateGenderCommand>>();
            _controller = new GendersController(_mockCommandHandler.Object);
        }

        [Fact]
        public async Task Create_ValidCommand_ShouldReturnNoContent()
        {
            // Arrange
            var command = new CreateGenderCommand("Valid Gender");

            // Act
            var result = await _controller.Create(command);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _mockCommandHandler.Verify(handler => handler.HandleAsync(command), Times.Once);
        }

        [Fact]
        public async Task Create_NullCommand_ShouldReturnBadRequest()
        {
            // Arrange
            CreateGenderCommand command = null;

            // Act
            var result = await _controller.Create(command);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Command cannot be null.", badRequestResult.Value);
            _mockCommandHandler.Verify(handler => handler.HandleAsync(It.IsAny<CreateGenderCommand>()), Times.Never);
        }

        [Fact]
        public async Task Create_EmptyGenderName_ShouldReturnBadRequest()
        {
            // Arrange
            var command = new CreateGenderCommand(""); // Empty name
            // The controller adds a model error for empty name
            // _controller.ModelState.AddModelError("Name", "Gender name cannot be empty.");
            // This manual addition to ModelState is not needed for this specific test's purpose
            // if we are testing the controller's explicit check for string.IsNullOrWhiteSpace(command.Name).
            // However, if `command.Name` was validated by attributes on `CreateGenderCommand` and model binding,
            // then `_controller.ModelState.AddModelError` would simulate that scenario for other tests.
            // For this test, the controller's internal logic for `string.IsNullOrWhiteSpace` is being tested.

            // Act
            var result = await _controller.Create(command);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value); // ModelState errors are returned as SerializableError
            // Check if the specific error for 'Name' is present
            var errors = badRequestResult.Value as SerializableError;
            Assert.True(errors.ContainsKey("Name"));
            _mockCommandHandler.Verify(handler => handler.HandleAsync(It.IsAny<CreateGenderCommand>()), Times.Never);
        }

        [Fact]
        public async Task Create_InvalidModelState_ShouldReturnBadRequest()
        {
            // Arrange
            var command = new CreateGenderCommand("Test");
            _controller.ModelState.AddModelError("SomeError", "Some error description");

            // Act
            var result = await _controller.Create(command);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
             _mockCommandHandler.Verify(handler => handler.HandleAsync(It.IsAny<CreateGenderCommand>()), Times.Never);
        }

        [Fact]
        public async Task Create_HandlerThrowsException_ShouldReturnStatusCode500()
        {
            // Arrange
            var command = new CreateGenderCommand("Valid Gender");
            var exceptionMessage = "Handler failed";
            _mockCommandHandler.Setup(h => h.HandleAsync(command)).ThrowsAsync(new System.Exception(exceptionMessage));

            // Act
            var result = await _controller.Create(command);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Contains(exceptionMessage, statusCodeResult.Value.ToString());
        }
    }
}
