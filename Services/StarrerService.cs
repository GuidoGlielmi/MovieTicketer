using MovieTicketer.Persistence.Entities;

namespace MovieTicketer.Services;

public class StarrerService : IMovieTicketerService<Starrer>
{
  private readonly MovieTicketerDbContext _context;

  public StarrerService(MovieTicketerDbContext repo)
  {
    _context = repo;
  }

  public List<Starrer> GetAll()
  {
    return _context.Starrer.ToList();
  }

  public Starrer? Get(Guid id)
  {
    return _context.Starrer.Find(id);
  }


  public void Create(Starrer starrer)
  {
    _context.Starrer.Add(starrer);
    _context.SaveChanges();
  }

  public void Delete(Guid id)
  {
    var starrer = _context.Starrer.Find(id);
    if (starrer is null)
      return;

    _context.Starrer.Remove(starrer);
    _context.SaveChanges();
  }

  public void Update(Starrer starrer)
  {
    _context.Starrer.Update(starrer);
  }
}
