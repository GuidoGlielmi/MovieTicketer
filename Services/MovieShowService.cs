using MovieTicketer.Persistence.Entities;
using MovieTicketer.Persistence.Wrappers;

namespace MovieTicketer.Services;

public class MovieShowService : IMovieTicketerService<MovieShow>
{
  private readonly IDateTimeOffsetWrapper _dateTimeOffsetWrapper;
  private readonly MovieTicketerDbContext _context;

  public MovieShowService(MovieTicketerDbContext context, IDateTimeOffsetWrapper dateTimeOffsetWrapper)
  {
    _dateTimeOffsetWrapper = dateTimeOffsetWrapper;
    _context = context;
  }

  public List<MovieShow> GetAll()
  {
    return _context.MovieShows.ToList();
  }

  public MovieShow? Get(Guid id)
  {
    return _context.MovieShows.Find(id);
  }

  public void Create(MovieShow movieShow)
  {
    var roomMovieShows = GetRoomMovieShows(movieShow.RoomId);
    CheckTimeRangeAvailability(movieShow, roomMovieShows);
    DeleteStartedMovieShows(roomMovieShows);

    _context.MovieShows.Add(movieShow);
    _context.SaveChanges();
  }

  public void Delete(Guid id)
  {
    var movieShow = _context.MovieShows.Find(id);
    if (movieShow is null)
      return;

    _context.MovieShows.Remove(movieShow);
    _context.SaveChanges();
  }

  public void Update(MovieShow movieShow)
  {
    _context.MovieShows.Update(movieShow);
  }

  private List<MovieShow> GetRoomMovieShows(int roomId)
  {
    return _context.Rooms.First(r => r.Id == roomId).MovieShows;
  }

  private static void CheckTimeRangeAvailability(MovieShow newMovieShow, List<MovieShow> roomMovieShows)
  {
    roomMovieShows.Sort((ms1, ms2) => ms1.StartTime > ms2.StartTime ? 1 : -1);

    var nextMovieShowIndex = roomMovieShows.FindIndex(ms => ms.StartTime > newMovieShow.EndTime);
    if (nextMovieShowIndex == -1)
      return;

    var prevMovieShowEndTime = roomMovieShows[nextMovieShowIndex - 1].EndTime;
    if (newMovieShow.StartTime < prevMovieShowEndTime)
    {
      throw new Exception("Show time not available");
    }
  }

  private void DeleteStartedMovieShows(List<MovieShow> roomMovieShows)
  {
    _context.RemoveRange(roomMovieShows.Where(ms => ms.StartTime < _dateTimeOffsetWrapper.UtcNow));
  }


}
