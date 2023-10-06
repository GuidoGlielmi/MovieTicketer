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
    return _context.Rooms.ToList();
  }

  public Room? Get(Guid id)
  {
    return _context.Rooms.Find(id);
  }


  public void Create(Room room)
  {
    _context.Rooms.Add(room);
    _context.SaveChanges();
  }

  public void Delete(Guid id)
  {
    var room = _context.Rooms.Find(id);
    if (room is null)
      return;

    _context.Rooms.Remove(room);
    _context.SaveChanges();
  }

  public void Update(Room room)
  {
    _context.Rooms.Update(room);
  }
}
