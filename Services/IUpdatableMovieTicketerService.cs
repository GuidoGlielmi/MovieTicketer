using MovieTicketer.Persistence.Entities;

namespace MovieTicketer.Services;

public interface IUpdatableMovieTicketerService<T> : IMovieTicketerService<T> where T : Entity
{
  void Update(T entity);
}
