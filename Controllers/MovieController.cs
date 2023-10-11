using Microsoft.AspNetCore.Mvc;
using MovieTicketer.Persistence.Entities;
using MovieTicketer.Services;


namespace MovieTicketer.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
  private readonly ILogger<MovieController> _logger;
  private readonly IUpdatableMovieTicketerService<Movie> _movieService;

  public MovieController(ILogger<MovieController> logger, IUpdatableMovieTicketerService<Movie> movieService)
  {
    _logger = logger;
    _movieService = movieService;
  }

  [HttpGet]
  public ActionResult<IEnumerable<Movie>> Get()
  {
    return _movieService.GetAll();
  }

  [HttpGet("{id}")]
  public ActionResult<Movie?> GetOne([FromRoute] Guid id)
  {
    return _movieService.Get(id);
  }

  [HttpPost]
  public ActionResult<Guid> Post([FromBody] Movie movie)
  {
    _movieService.Create(movie);
    return CreatedAtAction(nameof(GetOne), new { id = movie.Id.ToString() }, movie);
  }

  [HttpPut]
  public ActionResult<Guid> Put([FromBody] Movie movie)
  {
    var foundMovie = _movieService.Get(movie.Id);
    if (foundMovie is null)
      return NotFound();
    _movieService.Update(movie);
    return Ok();
  }

  [HttpDelete]
  public ActionResult<Guid> Delete([FromBody] Guid id)
  {
    var foundMovie = _movieService.Get(id);

    if (foundMovie is null)
      return NotFound();

    _movieService.Delete(foundMovie);
    return Ok();
  }
}

