namespace MovieTicketer.Persistence.Entities;

public interface IMovieTicketerDbContext
{
  Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
