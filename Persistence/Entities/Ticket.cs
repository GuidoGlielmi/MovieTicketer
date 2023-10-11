
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketer.Persistence.Entities;

public class Ticket : Entity
{
  public required Show Show { get; init; }

  public required Buyer Buyer { get; init; }

  public required char RowIdentifier { get; init; }

  public required int ColumnIdentifier { get; init; }

  [NotMapped]
  public string SeatId => $"{RowIdentifier}{ColumnIdentifier}";
}