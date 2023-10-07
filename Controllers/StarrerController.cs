using Microsoft.AspNetCore.Mvc;
using MovieTicketer.Persistence.Entities;
using MovieTicketer.Services;

namespace MovieTicketer.Controllers;

[ApiController]
[Route("[controller]")]
public class StarrerController : ControllerBase
{

  private readonly ILogger<StarrerController> _logger;
  private readonly IUpdatableMovieTicketerService<Starrer> _service;

  public StarrerController(ILogger<StarrerController> logger, IUpdatableMovieTicketerService<Starrer> service)
  {
    _logger = logger;
    _service = service;
  }

  [HttpGet]
  public IEnumerable<Starrer> Get()
  {
    return _service.GetAll();
  }

  [HttpGet("{id}")]
  public ActionResult<Starrer?> GetOne([FromRoute] Guid id)
  {
    return _service.Get(id);
  }

  [HttpPost]
  public ActionResult<Guid> Post([FromBody] Starrer starrer)
  {
    _service.Create(starrer);
    return CreatedAtAction(nameof(GetOne), new { id = starrer.Id.ToString() }, starrer);
  }

  [HttpPut]
  public ActionResult<Guid> Put([FromBody] Starrer starrer)
  {
    _service.Update(starrer);
    return NoContent();
  }

  [HttpDelete("{id}")]
  public ActionResult<Guid> Delete([FromRoute] Guid id)
  {
    _service.Delete(id);
    return NoContent();
  }
}
