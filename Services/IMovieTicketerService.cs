using MovieTicketer.Persistence.Entities;

namespace MovieTicketer.Services;

public interface IMovieTicketerService<T> where T : Entity
{
  List<T> GetAll();
  T? Get(Guid id);
  void Create(T entity);
  void Delete(Guid id);
}
