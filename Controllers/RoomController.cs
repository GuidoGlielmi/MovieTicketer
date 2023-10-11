using Microsoft.AspNetCore.Mvc;
using MovieTicketer.Persistence.Entities;
using MovieTicketer.Services;

namespace MovieTicketer.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomController : ControllerBase
{
  private readonly ILogger<RoomController> _logger;
  private readonly IUpdatableMovieTicketerService<Room> _roomService;

  public RoomController(ILogger<RoomController> logger, IUpdatableMovieTicketerService<Room> movieService)
  {
    _logger = logger;
    _roomService = movieService;
  }

  [HttpGet]
  public IEnumerable<Room> Get()
  {
    return _roomService.GetAll();
  }

  [HttpGet("{id}")]
  public ActionResult<Room?> GetOne([FromRoute] Guid id)
  {
    return _roomService.Get(id);
  }

  [HttpPost]
  public ActionResult<Guid> Post([FromBody] Room room)
  {
    _roomService.Create(room);
    return CreatedAtAction(nameof(GetOne), new { id = room.Id.ToString() }, room);
  }

  [HttpPut]
  public ActionResult<Guid> Put([FromBody] Room room)
  {
    var foundRoom = _roomService.Get(room.Id);
    if (foundRoom is null)
      return NotFound();
    _roomService.Update(room);
    return Ok();
  }

  [HttpDelete]
  public ActionResult<Guid> Delete([FromBody] Guid id)
  {
    var foundRoom = _roomService.Get(id);

    if (foundRoom is null)
      return NotFound();

    _roomService.Delete(foundRoom);
    return Ok();
  }
}
