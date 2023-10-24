using Microsoft.AspNetCore.Mvc;
using MovieTicketer.Persistence.Entities;
using MovieTicketer.Services;
using System.Linq;

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
  public ActionResult<Guid> Post([FromBody] Ticket[] tickets)
  {
    if (tickets.Any(t => t.Show.Id != tickets[0].Show.Id))
      return BadRequest();

    var show = _showService.Get(tickets[0].Show.Id);
    if (show == null)
      return NotFound();

    var occupiedSeats = tickets.Where(t => !show.AvailableSeats[(t.RowIdentifier, t.ColumnIdentifier)]);

    if (occupiedSeats.Any())
      return NotFound(occupiedSeats);

    foreach (var t in tickets)
    {
      _ticketService.Create(t);
    }

    return CreatedAtAction(nameof(GetOne), new { ids = tickets.Select(t => t.Id.ToString()) }, tickets);
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
