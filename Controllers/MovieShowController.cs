using Microsoft.AspNetCore.Mvc;
using MovieTicketer.Persistence.Entities;
using MovieTicketer.Services;

namespace MovieTicketer.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieShowController : ControllerBase
{

  private readonly ILogger<MovieShowController> _logger;
  private readonly MovieShowService _service;

  public MovieShowController(ILogger<MovieShowController> logger, MovieShowService service)
  {
    _logger = logger;
    _service = service;
  }

  [HttpGet]
  public IEnumerable<MovieShow> Get()
  {
    return _service.GetAll();
  }

  [HttpGet("{id}")]
  public ActionResult<MovieShow?> GetOne([FromRoute] Guid id)
  {
    return _service.Get(id);
  }

  [HttpPost]
  public ActionResult<Guid> Post([FromBody] MovieShow movieShow)
  {
    _service.Create(movieShow);
    return CreatedAtAction(nameof(GetOne), new { id = movieShow.Id.ToString() }, movieShow);
  }

  [HttpPut]
  public ActionResult<Guid> Put([FromBody] MovieShow movieShow)
  {
    _service.Update(movieShow);
    return NoContent();
  }

  [HttpDelete("{id}")]
  public ActionResult<Guid> Delete([FromRoute] Guid id)
  {
    _service.Delete(id);
    return NoContent();
  }
}
