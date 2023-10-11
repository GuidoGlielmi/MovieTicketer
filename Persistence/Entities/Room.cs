using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketer.Persistence.Entities;

public class Room : Entity
{
  public required int RoomNumber { get; init; }

  public List<Show> Shows { get; } = new();

  public required int RowsCount { get; init; }

  public required int ColumnsCount { get; init; }

  [NotMapped]
  public int SeatsCount => RowsCount * ColumnsCount;
}
