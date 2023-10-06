using MovieTicketer.Persistence.Entities;

namespace MovieTicketer.Services;

public class TicketService : IMovieTicketerService<Ticket>
{

  private readonly MovieTicketerDbContext _context;

  public TicketService(MovieTicketerDbContext repo)
  {
    _context = repo;
  }

  public List<Ticket> GetAll()
  {
    return _context.Tickets.ToList();
  }

  public Ticket? Get(Guid id)
  {
    return _context.Tickets.Find(id);
  }

  public void Create(Ticket ticket)
  {
    _context.Tickets.Add(ticket);
    _context.SaveChanges();
  }

  public void Delete(Guid id)
  {
    var ticket = _context.Tickets.Find(id);
    if (ticket is null)
      return;

    _context.Tickets.Remove(ticket);
    _context.SaveChanges();
  }

  public void Update(Ticket ticket)
  {
    _context.Tickets.Update(ticket);
  }
}
