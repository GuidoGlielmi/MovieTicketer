using MovieTicketer.Persistence.Entities;
using MovieTicketer.Persistence.Wrappers;

namespace MovieTicketer.Services;

public class ShowService : IUpdatableMovieTicketerService<Show>
{
  private readonly IDateTimeOffsetWrapper _dateTimeOffsetWrapper;
  private readonly MovieTicketerDbContext _context;

  public ShowService(MovieTicketerDbContext context, IDateTimeOffsetWrapper dateTimeOffsetWrapper)
  {
    _dateTimeOffsetWrapper = dateTimeOffsetWrapper;
    _context = context;
  }

  public List<Show> GetAll()
  {
    return _context.Show.ToList();
  }

  public Show? Get(Guid id)
  {
    return _context.Show.Find(id);
  }

  public void Create(Show show)
  {
    var roomShows = GetRoomShows(show.Room.Id);
    CheckTimeRangeAvailability(show, roomShows);
    DeleteStartedShows(roomShows);

    _context.Show.Add(show);
    _context.SaveChanges();
  }

  public void Delete(Show show)
  {
    _context.Show.Remove(show);
    _context.SaveChanges();
  }

  public void Update(Show show)
  {
    _context.Show.Update(show);
  }

  public bool Exists(Guid id)
  {
    return _context.Room.Any(t => t.Id == id);
  }

  private List<Show> GetRoomShows(Guid roomId)
  {
    return _context.Room.First(r => r.Id == roomId).Shows;
  }

  private static void CheckTimeRangeAvailability(Show newShow, List<Show> roomShows)
  {
    roomShows.Sort((ms1, ms2) => ms1.StartTime > ms2.StartTime ? 1 : -1);

    var nextShowIndex = roomShows.FindIndex(ms => ms.StartTime > newShow.EndTime);
    if (nextShowIndex == -1)
      return;

    var prevShowEndTime = roomShows[nextShowIndex - 1].EndTime;
    if (newShow.StartTime < prevShowEndTime)
    {
      throw new Exception("Show time not available");
    }
  }

  private void DeleteStartedShows(List<Show> roomShows)
  {
    _context.RemoveRange(roomShows.Where(ms => ms.StartTime < _dateTimeOffsetWrapper.UtcNow));
  }
}
