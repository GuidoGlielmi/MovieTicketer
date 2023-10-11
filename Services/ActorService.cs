using MovieTicketer.Persistence.Entities;

namespace MovieTicketer.Services;

public class ActorService : IMovieTicketerService<Actor>
{
  private readonly MovieTicketerDbContext _context;

  public ActorService(MovieTicketerDbContext repo)
  {
    _context = repo;
  }

  public List<Actor> GetAll()
  {
    return _context.Actor.ToList();
  }

  public Actor? Get(Guid id)
  {
    return _context.Actor.Find(id);
  }


  public void Create(Actor actor)
  {
    _context.Actor.Add(actor);
    _context.SaveChanges();
  }

  public void Delete(Actor actor)
  {
    _context.Actor.Remove(actor);
    _context.SaveChanges();
  }

  public void Update(Actor actor)
  {
    _context.Actor.Update(actor);
  }

  public bool Exists(Guid id)
  {
    return _context.Actor.Any(t => t.Id == id);
  }
}
