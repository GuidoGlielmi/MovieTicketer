using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketer.Persistence.Entities;

public class Room : Entity
{
  public new int Id { get; init; }

  public List<MovieShow> MovieShows { get; } = new();

  public int RowsAmount { get; init; }

  public int ColumnsAmount { get; init; }

  [NotMapped]
  public int SeatsAmount => RowsAmount * ColumnsAmount;
}
