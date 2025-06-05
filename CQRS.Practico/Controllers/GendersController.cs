using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands; 
using MyApp.Application.Interfaces; 
using System.Threading.Tasks;

namespace CQRS.Practico.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GendersController : ControllerBase
    {
        private readonly ICommandHandler<CreateGenderCommand> _createGenderCommandHandler;

        public GendersController(ICommandHandler<CreateGenderCommand> createGenderCommandHandler)
        {
            _createGenderCommandHandler = createGenderCommandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGenderCommand command)
        {
            if (command == null)
            {
                return BadRequest("Command cannot be null.");
            }

            // Basic validation example, more complex validation can be added
            // using FluentValidation or DataAnnotations on the CreateGenderCommand class.
            if (string.IsNullOrWhiteSpace(command.Name))
            {
                ModelState.AddModelError(nameof(command.Name), "Gender name cannot be empty.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _createGenderCommandHandler.HandleAsync(command);
                // Typically, for a POST that creates a resource, you might return CreatedAtAction or CreatedAtRoute
                // with a URI to the newly created resource and the resource itself or its ID.
                // Since the ID is database-generated and not immediately available from the command handler (unless modified to return it),
                // returning NoContent is a simpler approach.
                // If the ID needs to be returned, the command handler and ICommand interface would need adjustment.
                return NoContent();
            }
            catch (System.Exception ex)
            {
                // Log the exception (not implemented here)
                // Consider specific exception types if your handler can throw them
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
