namespace MovieTicketer.Persistence.Entities;

public class Movie : Entity
{
  public enum MovieCategories
  {
    Action,
    Adventure,
    Comedy,
    Drama,
    Romance,
    Suspense,
  }

  public enum MovieRates
  {
    G,
    PG,
    PG13,
    R,
    NC17,
    NR,
  }

  public required string Title { get; init; }

  public required List<MovieCategories> Categories { get; init; }

  public required TimeSpan Duration { get; init; }

  public required List<Starrer> Starrers { get; init; }

  public required MovieRates Rate { get; set; }
}
