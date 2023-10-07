using System.ComponentModel;

namespace MovieTicketer.Persistence.Entities;

public class Movie : Entity
{
  public required string Title { get; init; }

  public required MovieFormat Format { get; init; }

  public required List<MovieCategory> Categories { get; init; }

  public required MovieRate Rate { get; set; }

  public required TimeSpan Duration { get; init; }

  public required List<Starrer> Starrers { get; init; }
}

public enum MovieCategory
{
  Action,
  Adventure,
  Comedy,
  Drama,
  Romance,
  Suspense,
}

public enum MovieRate
{
  G,
  PG,
  PG13,
  R,
  NC17,
  NR,
}

public enum MovieFormat
{
  Standard = 0,
  IMAX = 20,
  [Description("3D")]
  THREE_D = 30
};