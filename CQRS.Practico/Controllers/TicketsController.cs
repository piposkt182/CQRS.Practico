using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Interfaces; // Contains UpdateTicketCommand, ICommandHandler
using MyApp.Application.Queries;  // Contains TicketDto, GetAllTicketsQuery, GetTicketByIdQuery
// KeyNotFoundException is in System.Collections.Generic, which might be implicitly covered
// or sometimes requires an explicit 'using System.Collections.Generic;'
// For Exception, 'using System;' is usually present or implicitly available.
using System; // For Exception
using System.Collections.Generic; // For KeyNotFoundException, IEnumerable

namespace CQRS.Practico.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly IQueryHandler<GetAllTicketsQuery, IEnumerable<TicketDto>> _queryHandler;
        private readonly IQueryHandler<GetTicketByIdQuery, TicketDto> _getTicketByIdqueryHandler;
        private readonly ICommandHandler<CreateTicketCommand> _commandHandler;
        private readonly ICommandHandler<UpdateTicketCommand> _updateCommandHandler;
        private readonly ICommandHandler<DeleteTicketCommand> _deleteCommandHandler; // Added

        public TicketsController(
            IQueryHandler<GetAllTicketsQuery, IEnumerable<TicketDto>> queryHandler,
            ICommandHandler<CreateTicketCommand> commandHandler,
            IQueryHandler<GetTicketByIdQuery, TicketDto> getTicketByIdqueryHandler,
            ICommandHandler<UpdateTicketCommand> updateCommandHandler,
            ICommandHandler<DeleteTicketCommand> deleteCommandHandler) // Added
        {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
            _getTicketByIdqueryHandler = getTicketByIdqueryHandler;
            _updateCommandHandler = updateCommandHandler;
            _deleteCommandHandler = deleteCommandHandler; // Added
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
            try
            {
                var result = await _getTicketByIdqueryHandler.HandleAsync(new GetTicketByIdQuery(id));
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _commandHandler.HandleAsync(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _updateCommandHandler.HandleAsync(command);
                return NoContent(); // Or Ok() if you want to return the updated resource
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception (implementation of logging is out of scope for this task)
                // For example: _logger.LogError(ex, "Error updating ticket {TicketId}", command.Id);
                return StatusCode(500, "An error occurred while updating the ticket.");
            }
        }

        // New DELETE endpoint
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            try
            {
                var command = new DeleteTicketCommand(id);
                await _deleteCommandHandler.HandleAsync(command);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Ticket with ID {id} not found.");
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., _logger.LogError(ex, "Error deleting ticket {TicketId}", id);)
                return StatusCode(500, $"An error occurred while deleting the ticket with ID {id}.");
            }
        }
    }
}
