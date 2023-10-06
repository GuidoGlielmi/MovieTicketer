using MovieTicketer.Persistence.Entities;

namespace MovieTicketer.Services;

public class BuyerService : IMovieTicketerService<Buyer>
{
  private readonly MovieTicketerDbContext _context;

  public BuyerService(MovieTicketerDbContext repo)
  {
    _context = repo;
  }

  public List<Buyer> GetAll()
  {
    return _context.Buyers.ToList();
  }

  public Buyer? Get(Guid id)
  {
    return _context.Buyers.Find(id);
  }


  public void Create(Buyer buyer)
  {
    _context.Buyers.Add(buyer);
    _context.SaveChanges();
  }

  public void Delete(Guid id)
  {
    var buyer = _context.Buyers.Find(id);
    if (buyer is null)
      return;

    _context.Buyers.Remove(buyer);
    _context.SaveChanges();
  }

  public void Update(Buyer buyer)
  {
    _context.Buyers.Update(buyer);
  }
}
