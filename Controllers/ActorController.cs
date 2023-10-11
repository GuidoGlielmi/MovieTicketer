using Microsoft.AspNetCore.Mvc;
using MovieTicketer.Persistence.Entities;
using MovieTicketer.Services;

namespace MovieTicketer.Controllers;

[ApiController]
[Route("[controller]")]
public class ActorController : ControllerBase
{

  private readonly ILogger<ActorController> _logger;
  private readonly IUpdatableMovieTicketerService<Actor> _actorService;

  public ActorController(ILogger<ActorController> logger, IUpdatableMovieTicketerService<Actor> service)
  {
    _logger = logger;
    _actorService = service;
  }

  [HttpGet]
  public IEnumerable<Actor> Get()
  {
    return _actorService.GetAll();
  }

  [HttpGet("{id}")]
  public ActionResult<Actor?> GetOne([FromRoute] Guid id)
  {
    return _actorService.Get(id);
  }

  [HttpPost]
  public ActionResult<Guid> Post([FromBody] Actor actor)
  {
    _actorService.Create(actor);
    return CreatedAtAction(nameof(GetOne), new { id = actor.Id.ToString() }, actor);
  }

  [HttpPut]
  public ActionResult<Guid> Put([FromBody] Actor actor)
  {
    var foundActor = _actorService.Get(actor.Id);
    if (foundActor is null)
      return NotFound();
    _actorService.Update(foundActor);
    return Ok();
  }

  [HttpDelete]
  public ActionResult<Guid> Delete([FromBody] Guid id)
  {
    var foundActor = _actorService.Get(id);

    if (foundActor is null)
      return NotFound();

    _actorService.Delete(foundActor);
    return Ok();
  }
}
