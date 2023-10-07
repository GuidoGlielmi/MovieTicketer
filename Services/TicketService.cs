using MovieTicketer.Persistence.Entities;

namespace MovieTicketer.Services;

public class TicketService : IMovieTicketerService<Ticket>
{

  private readonly MovieTicketerDbContext _context;

  public TicketService(MovieTicketerDbContext context)
  {
    _context = context;
  }

  public List<Ticket> GetAll()
  {
    return _context.Ticket.ToList();
  }

  public Ticket? Get(Guid id)
  {
    return _context.Ticket.Find(id);
  }

  public void Create(Ticket ticket)
  {
    var show = _context.Show.First(s => s.Id == ticket.ShowId);
    show.AddTicket(ticket);
    _context.SaveChanges();
  }

  public void Delete(Guid id)
  {
    var ticket = _context.Ticket.First(t => t.Id == id);
    _context.Ticket.Remove(ticket);
    _context.SaveChanges();
  }
}
