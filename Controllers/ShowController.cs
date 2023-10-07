using Microsoft.AspNetCore.Mvc;
using MovieTicketer.Persistence.Entities;
using MovieTicketer.Services;

namespace MovieTicketer.Controllers;

[ApiController]
[Route("[controller]")]
public class ShowController : ControllerBase
{
  private readonly ILogger<ShowController> _logger;
  private readonly IUpdatableMovieTicketerService<Show> _service;

  public ShowController(ILogger<ShowController> logger, IUpdatableMovieTicketerService<Show> service)
  {
    _logger = logger;
    _service = service;
  }

  [HttpGet]
  public IEnumerable<Show> Get()
  {
    return _service.GetAll();
  }

  [HttpGet("{id}")]
  public ActionResult<Show?> GetOne([FromRoute] Guid id)
  {
    return _service.Get(id);
  }

  [HttpPost]
  public ActionResult<Guid> Post([FromBody] Show show)
  {
    _service.Create(show);
    return CreatedAtAction(nameof(GetOne), new { id = show.Id.ToString() }, show);
  }

  [HttpPut]
  public ActionResult<Guid> Put([FromBody] Show show)
  {
    _service.Update(show);
    return NoContent();
  }

  [HttpDelete("{id}")]
  public ActionResult<Guid> Delete([FromRoute] Guid id)
  {
    _service.Delete(id);
    return NoContent();
  }
}
