using Microsoft.AspNetCore.Mvc;
using MovieTicketer.Persistence.Entities;
using MovieTicketer.Services;

namespace MovieTicketer.Controllers;
[ApiController]
[Route("[controller]")]
public class TicketController : ControllerBase
{

  private readonly ILogger<TicketController> _logger;
  private readonly TicketService _service;

  public TicketController(ILogger<TicketController> logger, TicketService service)
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
    return _service.Get(id);
  }

  [HttpPost]
  public ActionResult<Guid> Post([FromBody] Ticket ticket)
  {
    _service.Create(ticket);
    return CreatedAtAction(nameof(GetOne), new { id = ticket.Id.ToString() }, ticket);
  }

  [HttpPut]
  public ActionResult<Guid> Put([FromBody] Ticket ticket)
  {
    _service.Update(ticket);
    return NoContent();
  }

  [HttpDelete("{id}")]
  public ActionResult<Guid> Delete([FromRoute] Guid id)
  {
    _service.Delete(id);
    return NoContent();
  }
}
