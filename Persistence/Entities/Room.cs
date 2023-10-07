using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketer.Persistence.Entities;

public class Room : Entity
{
  public new int Id { get; init; }

  public List<Show> Shows { get; } = new();

  public required int RowsAmount { get; init; }

  public required int ColumnsAmount { get; init; }

  [NotMapped]
  public int SeatsAmount => RowsAmount * ColumnsAmount;
}
