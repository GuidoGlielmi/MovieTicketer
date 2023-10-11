using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketer.Persistence.Entities;

public class Movie : Entity
{
  private static readonly Dictionary<MovieFormat, float> _formatSurcharges = new()
  {
    { MovieFormat.Standard, 0 },
    { MovieFormat.Imax, (float)0.2 },
    { MovieFormat.Three_D, (float)0.3 },
  };

  private readonly List<MovieCategory> _categories = new();

  public required MovieFormat Format { get; init; }

  public required string Title { get; init; }

  [MinLength(1)]
  public IReadOnlyList<MovieCategory> Categories => _categories.AsReadOnly();

  public required MovieRate Rate { get; init; }

  public required TimeSpan Duration { get; init; }

  public required List<Actor> Starrers { get; init; }

  [NotMapped]
  public float FormatSurcharge => _formatSurcharges[Format];

  public void AddCategory(params MovieCategory[] category)
  {
    _categories.AddRange(category.ToList());
  }

  public void RemoveCategory(MovieCategory category)
  {
    _categories.Remove(category);
  }
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
  Standard,
  Imax,
  [Description("3D")]
  Three_D
};