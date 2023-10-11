namespace MovieTicketer.Persistence.Entities;

public class Actor : Entity
{
  private readonly List<Movie> _movies = new();

  public required string Name { get; init; }

  public required int Age { get; init; }

  public IReadOnlyList<Movie> Movies => _movies.AsReadOnly();

  public required ActingRole ActingRole { get; set; }
}

public enum ActingRole
{
  Background,
  Co_Star,
  Guest_Star,
  Cameo
}
