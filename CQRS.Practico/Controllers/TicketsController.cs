using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Interfaces;
using MyApp.Application.Queries;

namespace CQRS.Practico.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly IQueryHandler<GetAllTicketsQuery, IEnumerable<TicketDto>> _queryHandler;
        private readonly IQueryHandler<GetTicketByIdQuery, TicketDto> _getTicketByIdqueryHandler;
        private readonly ICommandHandler<CreateTicketCommand> _commandHandler;

        public TicketsController(
            IQueryHandler<GetAllTicketsQuery, IEnumerable<TicketDto>> queryHandler,
            ICommandHandler<CreateTicketCommand> commandHandler,
            IQueryHandler<GetTicketByIdQuery, TicketDto> getTicketByIdqueryHandler)
        {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
            _getTicketByIdqueryHandler = getTicketByIdqueryHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _queryHandler.HandleAsync(new GetAllTicketsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            var result = await _getTicketByIdqueryHandler.HandleAsync(new GetTicketByIdQuery(id));
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _commandHandler.HandleAsync(command);
            return NoContent();
        }
    }
}
