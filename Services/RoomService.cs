using MovieTicketer.Persistence.Entities;

namespace MovieTicketer.Services;

public class RoomService : IMovieTicketerService<Room>
{
  private readonly MovieTicketerDbContext _context;

  public RoomService(MovieTicketerDbContext repo)
  {
    _context = repo;
  }

  public List<Room> GetAll()
  {
    return _context.Room.ToList();
  }

  public Room? Get(Guid id)
  {
    return _context.Room.Find(id);
  }


  public void Create(Room room)
  {
    _context.Room.Add(room);
    _context.SaveChanges();
  }

  public void Delete(Room room)
  {
    _context.Room.Remove(room);
    _context.SaveChanges();
  }

  public void Update(Room room)
  {
    _context.Room.Update(room);
  }

  public bool Exists(Guid id)
  {
    return _context.Room.Any(t => t.Id == id);
  }
}
