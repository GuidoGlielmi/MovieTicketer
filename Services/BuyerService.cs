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
    return _context.Buyer.ToList();
  }

  public Buyer? Get(Guid id)
  {
    return _context.Buyer.Find(id);
  }


  public void Create(Buyer buyer)
  {
    _context.Buyer.Add(buyer);
    _context.SaveChanges();
  }

  public void Delete(Guid id)
  {
    var buyer = _context.Buyer.Find(id);
    if (buyer is null)
      return;

    _context.Buyer.Remove(buyer);
    _context.SaveChanges();
  }

  public void Update(Buyer buyer)
  {
    _context.Buyer.Update(buyer);
  }
}
