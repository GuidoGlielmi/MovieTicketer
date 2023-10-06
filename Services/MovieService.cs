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
    return _context.Movies.ToList();
  }

  public Movie? Get(Guid id)
  {
    return _context.Movies.Find(id);
  }


  public void Create(Movie movie)
  {
    _context.Movies.Add(movie);
    _context.SaveChanges();
  }

  public void Delete(Guid id)
  {
    var movie = _context.Movies.Find(id);
    if (movie is null)
      return;

    _context.Movies.Remove(movie);
    _context.SaveChanges();
  }

  public void Update(Movie movie)
  {
    _context.Movies.Update(movie);
  }
}
