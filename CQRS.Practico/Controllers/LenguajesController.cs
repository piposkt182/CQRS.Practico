using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands;
using MyApp.Application.DTOs;
using MyApp.Application.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRS.Practico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LenguajesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LenguajesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST api/lenguajes
        [HttpPost]
        public async Task<ActionResult<LenguajeDto>> CreateLenguaje([FromBody] CreateLenguajeCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lenguajeDto = await _mediator.Send(command);
            // Assuming the command handler returns the DTO and Id is populated.
            // The CreatedAtAction first argument is the action name for retrieving the resource.
            return CreatedAtAction(nameof(GetLenguajeById), new { id = lenguajeDto.Id }, lenguajeDto);
        }

        // GET: api/lenguajes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LenguajeDto>>> GetAllLenguajes()
        {
            var query = new GetAllLenguajesQuery();
            var lenguajes = await _mediator.Send(query);
            return Ok(lenguajes);
        }

        // GET: api/lenguajes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LenguajeDto>> GetLenguajeById(int id)
        {
            var query = new GetLenguajeByIdQuery(id);
            var lenguaje = await _mediator.Send(query);

            if (lenguaje == null)
            {
                return NotFound();
            }

            return Ok(lenguaje);
        }
    }
}
