using MovieTicketer.Persistence.Entities;

namespace MovieTicketer.Services;

public class MovieService : IMovieTicketerService<Movie>
{
  private readonly MovieTicketerDbContext _context;

  public MovieService(MovieTicketerDbContext context)
  {
    _context = context;
  }

  public List<Movie> GetAll()
  {
    return _context.Movie.ToList();
  }

  public Movie? Get(Guid id)
  {
    return _context.Movie.Find(id);
  }


  public void Create(Movie movie)
  {
    _context.Movie.Add(movie);
    _context.SaveChanges();
  }

  public void Delete(Guid id)
  {
    var movie = _context.Movie.Find(id);
    if (movie is null)
      return;

    _context.Movie.Remove(movie);
    _context.SaveChanges();
  }

  public void Update(Movie movie)
  {
    _context.Movie.Update(movie);
  }
}
