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
    return _context.Starrers.ToList();
  }

  public Starrer? Get(Guid id)
  {
    return _context.Starrers.Find(id);
  }


  public void Create(Starrer starrer)
  {
    _context.Starrers.Add(starrer);
    _context.SaveChanges();
  }

  public void Delete(Guid id)
  {
    var starrer = _context.Starrers.Find(id);
    if (starrer is null)
      return;

    _context.Starrers.Remove(starrer);
    _context.SaveChanges();
  }

  public void Update(Starrer starrer)
  {
    _context.Starrers.Update(starrer);
  }
}
