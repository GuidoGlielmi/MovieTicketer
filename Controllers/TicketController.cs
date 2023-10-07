using Microsoft.AspNetCore.Mvc;
using MovieTicketer.Persistence.Entities;
using MovieTicketer.Services;

namespace MovieTicketer.Controllers;
[ApiController]
[Route("[controller]")]
public class TicketController : ControllerBase
{

  private readonly ILogger<TicketController> _logger;
  private readonly IMovieTicketerService<Ticket> _service;

  public TicketController(ILogger<TicketController> logger, IMovieTicketerService<Ticket> service)
  {
    _logger = logger;
    _service = service;
  }

  [HttpGet]
  public IEnumerable<Ticket> Get()
  {
    return _service.GetAll();
  }

  [HttpGet("{id}")]
  public ActionResult<Ticket?> GetOne([FromRoute] Guid id)
  {
    var ticket = _service.Get(id);
    return ticket is null ? NotFound() : ticket;
  }

  [HttpPost]
  public ActionResult<Guid> Post([FromBody] Ticket ticket)
  {
    try
    {
      _service.Create(ticket);
    } catch (ArgumentNullException)
    {
      NotFound();
    } catch (Exception)
    {
      return StatusCode(500);
    }
    return CreatedAtAction(nameof(GetOne), new { id = ticket.Id.ToString() }, ticket);
  }

  [HttpDelete("{id}")]
  public ActionResult<Guid> Delete([FromRoute] Guid id)
  {
    try
    {
      _service.Delete(id);
    } catch (ArgumentNullException)
    {
      NotFound();
    } catch (Exception)
    {
      return StatusCode(500);
    }
    return NoContent();
  }
}
