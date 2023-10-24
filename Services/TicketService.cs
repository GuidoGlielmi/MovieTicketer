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
    _context.Ticket.Add(ticket);
    _context.SaveChanges();
  }

  public void Create(Ticket[] tickets)
  {
    _context.Ticket.AddRange(tickets);
    _context.SaveChanges();
  }

  public void Delete(Ticket ticket)
  {
    _context.Ticket.Remove(ticket);
    _context.SaveChanges();
  }
  public bool Exists(Guid id)
  {
    return _context.Ticket.Any(t => t.Id == id);
  }
}
