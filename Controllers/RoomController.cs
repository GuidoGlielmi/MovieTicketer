using Microsoft.AspNetCore.Mvc;
using MovieTicketer.Persistence.Entities;
using MovieTicketer.Services;

namespace MovieTicketer.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomController : ControllerBase
{
  private readonly ILogger<RoomController> _logger;
  private readonly RoomService _service;

  public RoomController(ILogger<RoomController> logger, RoomService service)
  {
    _logger = logger;
    _service = service;
  }

  [HttpGet]
  public IEnumerable<Room> Get()
  {
    return _service.GetAll();
  }

  [HttpGet("{id}")]
  public ActionResult<Room?> GetOne([FromRoute] Guid id)
  {
    return _service.Get(id);
  }

  [HttpPost]
  public ActionResult<Guid> Post([FromBody] Room room)
  {
    _service.Create(room);
    return CreatedAtAction(nameof(GetOne), new { id = room.Id.ToString() }, room);
  }

  [HttpPut]
  public ActionResult<Guid> Put([FromBody] Room room)
  {
    _service.Update(room);
    return NoContent();
  }

  [HttpDelete("{id}")]
  public ActionResult<Guid> Delete([FromRoute] Guid id)
  {
    _service.Delete(id);
    return NoContent();
  }
}
