using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands;
using MyApp.Application.DTOs;
using MyApp.Application.Queries;

namespace CQRS.Practico.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            var query = new GetAllMoviesQuery();
            var movies = await _mediator.Send(query);
            return Ok(movies);
        }

        // GET: api/movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var query = new GetMovieByIdQuery(id);
            var movie = await _mediator.Send(query);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // POST: api/movies
        [HttpPost]
        public async Task<ActionResult<MovieDto>> CreateMovie(CreateMovieCommand command)
        {
            // Assuming CreateMovieCommandHandler returns the ID of the newly created movie (int)
            var movieId = await _mediator.Send(command);

            // Fetch the created movie DTO to return in the response
            var movieDto = await _mediator.Send(new GetMovieByIdQuery(movieId));

            if (movieDto == null)
            {
                // This case should ideally not be reached if command execution was successful
                // and the ID returned is valid. It might indicate an issue with GetMovieByIdQuery
                // or data consistency. For robustness, handle it.
                return Problem(detail: "Movie created but could not be retrieved.", statusCode: 500);
            }

            return CreatedAtAction(nameof(GetMovie), new { id = movieId }, movieDto);
        }
    }
}
