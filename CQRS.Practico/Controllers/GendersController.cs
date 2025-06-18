using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands; 
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces; 
using MyApp.Application.Queries;

namespace CQRS.Practico.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GendersController : ControllerBase
    {
        private readonly ICommandHandler<CreateGenderCommand> _createGenderCommandHandler;
        private readonly IQueryHandler<GetAllGendersQuery, IEnumerable<GenderDto>> _getAllGendersQueryHandler; // Add this line

        // Modify the constructor
        public GendersController(
            ICommandHandler<CreateGenderCommand> createGenderCommandHandler,
            IQueryHandler<GetAllGendersQuery, IEnumerable<GenderDto>> getAllGendersQueryHandler) // Add this parameter
        {
            _createGenderCommandHandler = createGenderCommandHandler;
            _getAllGendersQueryHandler = getAllGendersQueryHandler; // Add this line
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGenderCommand command)
        {
            if (command == null)
            {
                return BadRequest("Command cannot be null.");
            }

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
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = new GetAllGendersQuery(); // Create an instance of the query
                var genders = await _getAllGendersQueryHandler.HandleAsync(query);
                return Ok(genders);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
