using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands;
using MyApp.Application.Queries;


namespace CQRS.Practico.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
                _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userCreated = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUser), new { id = userCreated.Id }, userCreated);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var query = new GetUserByIdQuery(id);
            var user = await _mediator.Send(query);
            return Ok(user);
        }

        [HttpPost("AddTicketToUser")]
        public async Task<IActionResult> AddTicketToUser([FromBody] AddTicketToUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userWithTicket = await _mediator.Send(command);
            return Ok(userWithTicket);
        }
    }
}
