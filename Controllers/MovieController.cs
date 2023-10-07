using Microsoft.AspNetCore.Mvc;
using MovieTicketer.Persistence.Entities;
using MovieTicketer.Services;


namespace MovieTicketer.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{

  private readonly ILogger<MovieController> _logger;
  private readonly IUpdatableMovieTicketerService<Movie> _service;

  public MovieController(ILogger<MovieController> logger, IUpdatableMovieTicketerService<Movie> service)
  {
    _logger = logger;
    _service = service;
  }

  [HttpGet]
  public ActionResult<IEnumerable<Movie>> Get()
  {
    return _service.GetAll();
  }

  [HttpGet("{id}")]
  public ActionResult<Movie?> GetOne([FromRoute] Guid id)
  {
    return _service.Get(id);
  }

  [HttpPost]
  public ActionResult<Guid> Post([FromBody] Movie movie)
  {
    _service.Create(movie);
    return CreatedAtAction(nameof(GetOne), new { id = movie.Id.ToString() }, movie);
  }

  [HttpPut]
  public ActionResult<Guid> Put([FromBody] Movie movie)
  {
    _service.Update(movie);
    return NoContent();
  }

  [HttpDelete("{id}")]
  public ActionResult<Guid> Delete([FromRoute] Guid id)
  {
    _service.Delete(id);
    return NoContent();
  }
}

