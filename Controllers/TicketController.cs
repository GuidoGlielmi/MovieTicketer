using Microsoft.AspNetCore.Mvc;
using MovieTicketer.Persistence.Entities;
using MovieTicketer.Services;

namespace MovieTicketer.Controllers;
[ApiController]
[Route("[controller]")]
public class TicketController : ControllerBase
{

  private readonly ILogger<TicketController> _logger;
  private readonly IMovieTicketerService<Ticket> _ticketService;
  private readonly IMovieTicketerService<Show> _showService;

  public TicketController(ILogger<TicketController> logger, IMovieTicketerService<Ticket> ticketService, IMovieTicketerService<Show> showService)
  {
    _logger = logger;
    _ticketService = ticketService;
    _showService = showService;
  }

  [HttpGet]
  public IEnumerable<Ticket> Get()
  {
    return _ticketService.GetAll();
  }

  [HttpGet("{id}")]
  public ActionResult<Ticket?> GetOne([FromRoute] Guid id)
  {
    var ticket = _ticketService.Get(id);
    return ticket is null ? NotFound() : Ok(ticket);
  }

  [HttpPost]
  public ActionResult<Guid> Post([FromBody] Ticket ticket)
  {
    if (!_showService.Exists(ticket.Show.Id))
      return NotFound();

    _ticketService.Create(ticket);
    return CreatedAtAction(nameof(GetOne), new { id = ticket.Id.ToString() }, ticket);
  }

  [HttpDelete]
  public ActionResult<Guid> Delete([FromBody] Guid id)
  {
    var foundTicket = _ticketService.Get(id);

    if (foundTicket is null)
      return NotFound();

    _ticketService.Delete(foundTicket);
    return Ok();
  }
}
