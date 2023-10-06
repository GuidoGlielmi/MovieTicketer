namespace MovieTicketer.Persistence.Entities;

public class Starrer : Entity
{
  public required string Name { get; init; }

  public required int Age { get; init; }

  public List<Movie> Movies { get; init; } = new();
}
