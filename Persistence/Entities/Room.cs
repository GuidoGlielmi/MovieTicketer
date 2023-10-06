namespace MovieTicketer.Persistence.Entities;

public class Room : Entity
{
  public new int Id { get; init; }

  public List<MovieShow> MovieShows { get; } = new();

  public int SeatsAmount { get; init; }
}
